"""Generate the two-page paper sheet from the campaign and system rulebooks.

All rule text comes from ordinary Markdown tables and the Evasion code block.
This file owns only layout, font selection, source checks and PDF generation.
"""

import argparse
from dataclasses import dataclass
from hashlib import sha256
from io import BytesIO
import os
from pathlib import Path
import re
import sys
import tempfile

from pypdf import PdfReader, PdfWriter
from reportlab.lib import colors
from reportlab.lib.pagesizes import A4
from reportlab.pdfbase import pdfmetrics
from reportlab.pdfbase.ttfonts import TTFont
from reportlab.pdfgen import canvas


ROOT = Path(__file__).resolve().parents[2]
DEFAULT_BOOK = ROOT / "RoleRollsPocketEdition/docs/land-of-heroes/livro-de-regras.md"
DEFAULT_BASE_BOOK = ROOT / "RoleRollsPocketEdition/docs/livro-de-regras.md"
DEFAULT_PDF = ROOT / "output/pdf/ficha-land-of-heroes-template.pdf"
WIDTH, HEIGHT = A4
MARGIN = 32
CONTENT = WIDTH - 2 * MARGIN
INK = colors.HexColor("#262626")
GRAY = colors.HexColor("#555555")
RULE = colors.HexColor("#888888")
PALE = colors.HexColor("#f4f4f4")


def clean(value):
    # Strip Markdown delimiters, preserving arithmetic operators such as '*'.
    return re.sub(r"\*\*(.*?)\*\*", r"\1", value.replace("`", "")).strip()


def display_name(value):
    return re.sub(r"\s*\([^)]*\)\s*$", "", clean(value)).strip()


def section(source, heading):
    match = re.search(r"^(#{2,3}) " + re.escape(heading) + r"\s*$", source, re.M)
    if not match:
        raise ValueError(f"Seção obrigatória ausente no livro: {heading}")
    level = len(match.group(1))
    rest = source[match.end():]
    end = re.search(r"^#{1," + str(level) + r"} ", rest, re.M)
    return rest[:end.start()] if end else rest


def table(source, headers):
    lines = source.splitlines()
    expected = list(headers)
    for index, raw in enumerate(lines):
        if not raw.strip().startswith("|"):
            continue
        cells = [clean(cell) for cell in raw.strip().strip("|").split("|")]
        if cells != expected:
            continue
        if index + 1 >= len(lines) or not re.fullmatch(r"[|\s:\-]+", lines[index + 1]):
            raise ValueError(f"Separador inválido na tabela: {headers[0]}")
        rows = []
        for row in lines[index + 2:]:
            if not row.strip().startswith("|"):
                break
            values = [clean(cell) for cell in row.strip().strip("|").split("|")]
            if len(values) != len(headers) or any(not cell for cell in values):
                raise ValueError(f"Linha incompleta na tabela: {row}")
            rows.append(dict(zip(headers, values)))
        if not rows:
            raise ValueError(f"Tabela vazia: {headers[0]}")
        return rows
    raise ValueError(f"Tabela obrigatória ausente: {' / '.join(headers)}")


@dataclass
class Rules:
    title: str
    attributes: list
    skills: list
    vitalities: list
    derived: list
    conditions: list
    defense: str
    fields: dict

    @classmethod
    def read(cls, source, base_source):
        title_match = re.search(r"^# (.+?)(?: —| -) ", source, re.M)
        if not title_match:
            raise ValueError("Título da campanha ausente no livro")
        attributes = table(section(source, "Atributos"), ("Nome usado no livro", "Nome da configuração"))
        skills = table(section(source, "2. Perícias e especialidades"), ("Atributo ligado", "Perícia", "Especialidades"))
        for skill in skills:
            # Semicolons distinguish names containing commas; short lists use commas.
            separator = ";" if ";" in skill["Especialidades"] else ","
            skill["items"] = [item.strip() for item in skill["Especialidades"].split(separator)]
            skill["name"] = display_name(skill["Perícia"])
        vitalities = table(section(source, "Vitalidades"), ("Vitalidade", "Fórmula máxima", "Ordem no dano básico"))
        derived = table(section(base_source, "Grau, crescimento e arredondamento"), ("Valor derivado", "Fórmula"))
        conditions = table(section(source, "Condições"), ("Estado", "Gatilho"))
        defense_match = re.search(r"```text\s*\n(.*?)\n```", section(source, "Evasão e bloqueio"), re.S)
        if not defense_match:
            raise ValueError("Fórmula de Evasão ausente no livro")
        fields = table(section(source, "Campos e organização"), ("Página", "Bloco", "Campos"))
        keyed = {row["Bloco"]: [value.strip() for value in row["Campos"].split(";")] for row in fields}
        return cls(title_match.group(1), attributes, skills, vitalities, derived,
                   conditions, defense_match.group(1).strip(), keyed)

    def labels(self, name):
        if name not in self.fields:
            raise ValueError(f"Bloco obrigatório ausente no livro: {name}")
        return self.fields[name]


def register_fonts(font_dir=None):
    candidates = []
    if font_dir:
        candidates.append(Path(font_dir))
    if os.name == "nt":
        candidates.append(Path(os.environ.get("WINDIR", "C:/Windows")) / "Fonts")
    candidates.extend([Path("/usr/share/fonts/truetype/dejavu"),
                       Path("/usr/share/fonts/truetype/liberation2")])
    families = [("arial.ttf", "arialbd.ttf", "ariali.ttf"),
                ("DejaVuSans.ttf", "DejaVuSans-Bold.ttf", "DejaVuSans-Oblique.ttf"),
                ("LiberationSans-Regular.ttf", "LiberationSans-Bold.ttf", "LiberationSans-Italic.ttf")]
    for folder in candidates:
        for regular, bold, italic in families:
            paths = [folder / name for name in (regular, bold, italic)]
            if all(path.is_file() for path in paths):
                for name, path in zip(("Sheet", "SheetBold", "SheetItalic"), paths):
                    pdfmetrics.registerFont(TTFont(name, str(path)))
                return
    raise ValueError("Fonte com acentos não encontrada. Use --font-dir com Arial, DejaVu Sans ou Liberation Sans.")


class Layout:
    def __init__(self, pdf, rules):
        self.pdf = pdf
        self.rules = rules

    def line(self, x1, y1, x2, y2):
        self.pdf.setStrokeColor(RULE)
        self.pdf.setLineWidth(0.45)
        self.pdf.line(x1, y1, x2, y2)

    def box(self, x, top, width, height, fill=None):
        if x < MARGIN or x + width > WIDTH - MARGIN + 0.1 or top - height < 30:
            raise ValueError("Conteúdo excede o espaço de impressão da ficha")
        self.pdf.setStrokeColor(RULE)
        self.pdf.setLineWidth(0.55)
        self.pdf.setFillColor(fill or colors.white)
        self.pdf.rect(x, top - height, width, height, stroke=1, fill=int(fill is not None))

    def text(self, value, x, baseline, size=8, font="Sheet", width=None, color=INK):
        if width is None:
            width = WIDTH - MARGIN - x
        if pdfmetrics.stringWidth(value, font, size) > width + 0.1:
            raise ValueError(f"Texto excede o espaço disponível: {value[:90]}")
        self.pdf.setFont(font, size)
        self.pdf.setFillColor(color)
        self.pdf.drawString(x, baseline, value)

    def wrap(self, value, width, size=8, font="Sheet"):
        lines = []
        current = ""
        for word in value.split():
            trial = f"{current} {word}".strip()
            if pdfmetrics.stringWidth(trial, font, size) <= width:
                current = trial
            else:
                if current:
                    lines.append(current)
                if pdfmetrics.stringWidth(word, font, size) > width:
                    raise ValueError(f"Palavra excede o espaço disponível: {word}")
                current = word
        if current:
            lines.append(current)
        return lines

    def paragraph(self, value, x, baseline, width, size=7, line_height=10, max_lines=3, font="Sheet"):
        lines = self.wrap(value, width, size, font)
        if len(lines) > max_lines:
            raise ValueError(f"Texto excede o espaço disponível: {value[:90]}")
        for index, row in enumerate(lines):
            self.text(row, x, baseline - index * line_height, size, font, width)

    def section(self, title, x, top, width):
        self.pdf.setFillColor(INK)
        self.pdf.rect(x, top - 16, width, 16, stroke=0, fill=1)
        self.text(title.upper(), x + 6, top - 11, 8, "SheetBold", width - 12, colors.white)

    def header(self, number):
        self.pdf.setFillColor(INK)
        self.pdf.rect(MARGIN, HEIGHT - 72, CONTENT, 40, fill=1, stroke=0)
        self.text(self.rules.title.upper(), MARGIN + 12, HEIGHT - 52, 18, "SheetBold", CONTENT - 100, colors.white)
        self.text("Ficha de herói", MARGIN + 13, HEIGHT - 65, 8, "Sheet", CONTENT - 100, colors.white)
        self.text("RoleRolls", WIDTH - MARGIN - 64, HEIGHT - 55, 10, "SheetBold", 64, colors.white)
        self.line(MARGIN, 25, WIDTH - MARGIN, 25)
        self.text(f"{self.rules.title} - Ficha de herói", MARGIN, 15, 6.5, color=GRAY)
        self.text(f"Página {number} de 2", WIDTH - MARGIN - 59, 15, 6.5, width=59, color=GRAY)

    def field(self, label, x, top, width, height=28):
        self.text(label.upper(), x, top - 9, 6.8, "SheetBold", width, GRAY)
        self.line(x, top - height, x + width, top - height)

    def writing_rows(self, x, top, width, count, spacing=16):
        for index in range(count):
            self.line(x, top - index * spacing, x + width, top - index * spacing)

    def grid(self, title, labels, x, top, width, proportions, rows=5, row_height=22):
        if len(labels) != len(proportions):
            raise ValueError(f"Campos do bloco {title} mudaram: ajuste as colunas do layout")
        self.section(title, x, top, width)
        header_top = top - 23
        bottom = header_top - 17 - rows * row_height
        current_x = x
        for label, proportion in zip(labels, proportions):
            col_width = width * proportion
            self.paragraph(label.upper(), current_x + 4, header_top - 7,
                           col_width - 8, 6.1, 7, 2, "SheetBold")
            if current_x > x:
                self.line(current_x, header_top - 17, current_x, bottom)
            current_x += col_width
        self.writing_rows(x, header_top - 17, width, rows + 1, row_height)

    def page_one(self):
        self.header(1)
        self.section("Identidade", MARGIN, 755, CONTENT)
        identity = self.rules.labels("Identidade")
        if len(identity) != 7:
            raise ValueError("Campos de Identidade mudaram: ajuste o espaço do layout")
        self.field(identity[0], MARGIN, 733, 310)
        self.field(identity[1], MARGIN + 322, 733, CONTENT - 322)
        x = MARGIN
        sizes = [137, 140, 45, 93, CONTENT - 137 - 140 - 45 - 93 - 40]
        for label, width in zip(identity[2:], sizes):
            self.field(label, x, 695, width, 25)
            x += width + 10
        derived = "; ".join(f"{display_name(row['Valor derivado'])} = {row['Fórmula']}" for row in self.rules.derived)
        self.paragraph(derived, MARGIN, 654, CONTENT, 7.2, 9, 2)

        columns = [(MARGIN, 149), (MARGIN + 160, 236), (MARGIN + 407, CONTENT - 407)]
        for title, (x, width) in zip(("Atributos", "Vitalidades", "Condições"), columns):
            self.section(title, x, 634, width)
        x, width = columns[0]
        attribute_labels = self.rules.labels("Atributos")
        if len(attribute_labels) != 1:
            raise ValueError("Campos de atributo mudaram: ajuste o layout")
        for index, attribute in enumerate(self.rules.attributes):
            top = 609 - 25 * index
            if top - 22 < 461:
                raise ValueError("Atributos excedem o espaço da primeira página")
            self.box(x, top, width, 22, PALE)
            self.text(attribute["Nome usado no livro"].upper(), x + 6, top - 14, 8.7, "SheetBold", width - 46)
            self.text(attribute_labels[0].upper(), x + width - 30, top - 7, 5.2, "SheetBold", 24, GRAY)
            self.line(x + width - 30, top - 17, x + width - 6, top - 17)

        x, width = columns[1]
        vitality_labels = self.rules.labels("Vitalidades")
        if len(vitality_labels) != 2 or len(self.rules.vitalities) > 3:
            raise ValueError("Vitalidades excedem o espaço da primeira página")
        for index, row in enumerate(self.rules.vitalities):
            top = 609 - 50 * index
            self.box(x, top, width, 43, PALE)
            self.text(display_name(row["Vitalidade"]).upper(), x + 7, top - 15, 9.2, "SheetBold", width - 113)
            for offset, label in zip((width - 98, width - 49), vitality_labels):
                self.text(label.upper(), x + offset, top - 10, 5.8, "SheetBold", 44, GRAY)
                self.line(x + offset, top - 20, x + offset + 38, top - 20)
            self.paragraph(row["Fórmula máxima"], x + 7, top - 32, width - 14, 7.2, 8, 2)

        x, width = columns[2]
        if len(self.rules.conditions) > 3:
            raise ValueError("Condições excedem o espaço da primeira página")
        for index, row in enumerate(self.rules.conditions):
            top = 607 - 45 * index
            self.box(x + 2, top, 8, 8)
            self.text(display_name(row["Estado"]), x + 15, top - 7, 8.5, "SheetBold", width - 15)
            self.paragraph(row["Gatilho"], x + 15, top - 19, width - 16, 6.7, 8, 4)

        self.section("Defesa e bloqueio", MARGIN, 451, CONTENT)
        defense_labels = self.rules.labels("Defesa e bloqueio")
        if len(defense_labels) != 2:
            raise ValueError("Defesas excedem o espaço da primeira página")
        self.box(MARGIN, 427, 356, 63)
        self.text(defense_labels[0].upper(), MARGIN + 7, 411, 8.5, "SheetBold", 290)
        self.line(MARGIN + 300, 403, MARGIN + 347, 403)
        self.paragraph(self.rules.defense, MARGIN + 7, 392, 342, 7.3, 10, 2)
        self.box(MARGIN + 368, 427, CONTENT - 368, 63)
        self.text(defense_labels[1].upper(), MARGIN + 375, 411, 8.5, "SheetBold", CONTENT - 382)
        self.line(MARGIN + 375, 383, WIDTH - MARGIN - 7, 383)
        self.field(self.rules.labels("Condições")[0], MARGIN, 356, CONTENT, 22)

        self.grid("Ataques e ações de combate", self.rules.labels("Ataques e ações de combate"),
                  MARGIN, 322, CONTENT, [0.25, 0.12, 0.11, 0.12, 0.12, 0.28], rows=4, row_height=23)
        self.section("Recursos e resistências", MARGIN, 178, CONTENT)
        resources = self.rules.labels("Recursos e resistências")
        box_width = (CONTENT - 9 * (len(resources) - 1)) / len(resources)
        for index, label in enumerate(resources):
            x = MARGIN + index * (box_width + 9)
            self.box(x, 154, box_width, 35, PALE)
            self.paragraph(label.upper(), x + 5, 143, box_width - 10, 6.3, 7, 2, "SheetBold")
        self.section("Anotações rápidas", MARGIN, 106, CONTENT)
        self.writing_rows(MARGIN, 78, CONTENT, 3, 18)
        self.pdf.showPage()

    def skill_height(self, group, width):
        return 28 + sum(14 * len(self.wrap(item, width - 51, 7.5)) for item in group["items"])

    def skill_group(self, group, x, top, width):
        title = group["name"]
        attribute = group["Atributo ligado"]
        if attribute != "Variável":
            title += f" / {attribute}"
        self.text(title, x, top, 8, "SheetBold", width - 30)
        labels = self.rules.labels("Perícias e especialidades")
        if len(labels) != 3:
            raise ValueError("Campos de perícia mudaram: ajuste o layout")
        self.text(labels[0].upper(), x, top - 10, 5.7, "Sheet", 63, GRAY)
        self.line(x + 62, top - 11, x + 93, top - 11)
        self.text(labels[1].upper(), x + width - 48, top - 10, 5.2, "SheetBold", 24, GRAY)
        self.text(labels[2].upper(), x + width - 22, top - 10, 5.2, "SheetBold", 22, GRAY)
        y = top - 24
        for item in group["items"]:
            lines = self.wrap(item, width - 51, 7.5)
            for value in lines:
                self.text(value, x + 2, y, 7.5, width=width - 51)
                y -= 14
            self.line(x + width - 47, y + 12, x + width - 29, y + 12)
            self.line(x + width - 21, y + 12, x + width - 2, y + 12)
        return top - self.skill_height(group, width)

    def page_two(self):
        self.header(2)
        self.section("Perícias e especialidades", MARGIN, 755, CONTENT)
        col_width = (CONTENT - 20) / 3
        # Pack real book data by height; no second hard-coded skill tree.
        bins = [[], [], []]
        heights = [0, 0, 0]
        for index, group in sorted(enumerate(self.rules.skills),
                                   key=lambda pair: self.skill_height(pair[1], col_width), reverse=True):
            column = min(range(3), key=lambda i: heights[i])
            bins[column].append((index, group))
            heights[column] += self.skill_height(group, col_width)
        if max(heights) > 300:
            raise ValueError("Perícias excedem o espaço das duas páginas; ajuste o layout antes de regenerar")
        for column, groups in enumerate(bins):
            y = 728
            for _, group in sorted(groups):
                y = self.skill_group(group, MARGIN + column * (col_width + 10), y, col_width)

        self.section("Equipamentos", MARGIN, 412, CONTENT)
        slots = self.rules.labels("Equipamentos")
        if len(slots) > 12:
            raise ValueError("Equipamentos excedem o espaço da segunda página")
        for index, label in enumerate(slots):
            column, row = index % 3, index // 3
            x = MARGIN + column * (col_width + 10)
            top = 388 - row * 29
            self.box(x, top, col_width, 24, PALE)
            self.text(label, x + 5, top - 9, 6.7, "SheetBold", col_width - 10, GRAY)

        inventory_width = 245
        self.grid("Inventário", self.rules.labels("Inventário"), MARGIN, 262,
                  inventory_width, [0.50, 0.23, 0.27], rows=5, row_height=21)
        powers_x = MARGIN + inventory_width + 12
        powers_width = CONTENT - inventory_width - 12
        self.grid("Poderes, magias e habilidades", self.rules.labels("Poderes, magias e habilidades"),
                  powers_x, 262, powers_width, [0.32, 0.13, 0.55], rows=5, row_height=21)
        bottom_width = (CONTENT - 12) / 2
        for title, x in [("História, traços e aparência", MARGIN),
                         ("Anotações de sessão", MARGIN + bottom_width + 12)]:
            self.rules.labels(title)
            self.section(title, x, 104, bottom_width)
            self.writing_rows(x, 77, bottom_width, 3, 18)
        self.pdf.showPage()


def fingerprints(book, base_book):
    return {"/SourceBookSHA256": sha256(book.read_bytes()).hexdigest(),
            "/SourceBaseBookSHA256": sha256(base_book.read_bytes()).hexdigest(),
            "/GeneratorSHA256": sha256(Path(__file__).read_bytes()).hexdigest()}


def generate(book, base_book, output, font_dir):
    rules = Rules.read(book.read_text(encoding="utf-8-sig"),
                       base_book.read_text(encoding="utf-8-sig"))
    register_fonts(font_dir)
    stream = BytesIO()
    pdf = canvas.Canvas(stream, pagesize=A4, invariant=1, pageCompression=1)
    pdf.setTitle(f"{rules.title} - Ficha de herói")
    pdf.setAuthor("RoleRolls")
    pdf.setSubject("Template em branco para impressão, gerado dos livros do sistema e da campanha")
    layout = Layout(pdf, rules)
    layout.page_one()
    layout.page_two()
    pdf.save()
    reader = PdfReader(BytesIO(stream.getvalue()))
    if len(reader.pages) != 2:
        raise ValueError("A ficha precisa conter exatamente duas páginas")
    writer = PdfWriter()
    writer.clone_document_from_reader(reader)
    writer.add_metadata(fingerprints(book, base_book))
    result = BytesIO()
    writer.write(result)
    output.parent.mkdir(parents=True, exist_ok=True)
    # Publish only a fully generated file; failed layouts preserve the old PDF.
    with tempfile.NamedTemporaryFile(dir=output.parent, suffix=".pdf", delete=False) as temp:
        temp.write(result.getvalue())
        staging = Path(temp.name)
    try:
        staging.replace(output)
    finally:
        staging.unlink(missing_ok=True)


def main():
    if hasattr(sys.stdout, "reconfigure"):
        sys.stdout.reconfigure(encoding="utf-8")
        sys.stderr.reconfigure(encoding="utf-8")
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("--book", type=Path, default=DEFAULT_BOOK)
    parser.add_argument("--base-book", type=Path, default=DEFAULT_BASE_BOOK,
                        help="Livro do sistema: fonte de Grau e Crescimento")
    parser.add_argument("--output", type=Path, default=DEFAULT_PDF)
    parser.add_argument("--font-dir", type=Path)
    parser.add_argument("--check", action="store_true", help="Verifica a origem do PDF sem regravar")
    args = parser.parse_args()
    try:
        if args.check:
            metadata = PdfReader(args.output).metadata or {}
            if any(metadata.get(key) != value for key, value in fingerprints(args.book, args.base_book).items()):
                raise ValueError("PDF desatualizado: execute gerar-ficha.ps1 novamente")
            print("PDF corresponde aos dois livros e ao gerador atuais.")
        else:
            generate(args.book, args.base_book, args.output, args.font_dir)
            print(f"Ficha gerada: {args.output.resolve()}")
    except (ValueError, OSError) as error:
        print(f"Erro: {error}", file=sys.stderr)
        return 1
    return 0


if __name__ == "__main__":
    sys.exit(main())
