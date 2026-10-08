"""Integration checks: the printed sheet must follow its Markdown source."""

from pathlib import Path
import re
import subprocess
import sys
import tempfile
import unittest

from pypdf import PdfReader


SCRIPT = Path(__file__).with_name("gerar_ficha.py")
ROOT = SCRIPT.parents[2]
BOOK = ROOT / "RoleRollsPocketEdition/docs/land-of-heroes/livro-de-regras.md"
BASE_BOOK = ROOT / "RoleRollsPocketEdition/docs/livro-de-regras.md"


class SheetTests(unittest.TestCase):
    def setUp(self):
        self.temp = tempfile.TemporaryDirectory()
        self.addCleanup(self.temp.cleanup)
        self.book = Path(self.temp.name) / "livro.md"
        self.base_book = Path(self.temp.name) / "livro-base.md"
        self.pdf = Path(self.temp.name) / "ficha.pdf"
        self.source = BOOK.read_text(encoding="utf-8")
        self.book.write_text(self.source, encoding="utf-8")
        self.base_source = BASE_BOOK.read_text(encoding="utf-8")
        self.base_book.write_text(self.base_source, encoding="utf-8")

    def run_generator(self, *extra):
        return subprocess.run(
            [sys.executable, str(SCRIPT), "--book", str(self.book),
             "--base-book", str(self.base_book),
             "--output", str(self.pdf), *extra],
            capture_output=True, text=True, encoding="utf-8",
        )

    def generate(self):
        result = self.run_generator()
        self.assertEqual(result.returncode, 0, result.stdout + result.stderr)
        reader = PdfReader(self.pdf)
        self.assertEqual(len(reader.pages), 2)
        self.assertFalse(reader.get_fields(), "A ficha para papel deve ser estática")
        for page in reader.pages:
            self.assertAlmostEqual(float(page.mediabox.width), 595.2756, places=2)
            self.assertAlmostEqual(float(page.mediabox.height), 841.8898, places=2)
        return "\n".join(page.extract_text() for page in reader.pages)

    def test_accented_fields_and_current_rules(self):
        printed = self.generate()
        for label in ["INTELIGÊNCIA", "INTUIÇÃO", "FORÇA", "CONDIÇÕES",
                      "História", "Maldição", "Mão principal", "2 + nível / 6"]:
            self.assertIn(label, printed)
        self.assertNotIn("10 + 2 x Inteligencia", printed)
        self.assertNotIn("10 + 2 × Inteligência", printed)

    def test_formula_and_specialty_changes_come_from_book(self):
        for formula in ("7 + 3 × Inteligência", "7 + 3 * Inteligência"):
            with self.subTest(formula=formula):
                changed = self.source.replace("`2 + nível / 6`", f"`{formula}`")
                changed = changed.replace("Acrobacia, Esconder-se", "Acrobacia aérea, Esconder-se")
                self.assertNotEqual(changed, self.source)
                self.book.write_text(changed, encoding="utf-8")
                printed = self.generate()
                self.assertIn(formula, printed)
                self.assertNotIn("2 + nível / 6", printed)
                self.assertIn("Acrobacia aérea", printed)

    def test_dodge_defense_is_distinct_from_evasion_specialty(self):
        self.generate()
        reader = PdfReader(self.pdf)
        combat_page = reader.pages[0].extract_text()
        skills_page = reader.pages[1].extract_text()
        self.assertIn("ESQUIVA ESTÁTICA", combat_page)
        self.assertNotIn("EVASÃO ESTÁTICA", combat_page)
        self.assertIn("10 + Evasão +", combat_page)
        self.assertIn("Evasão", skills_page)
        self.assertNotIn("Esquiva", skills_page)
        self.assertIn("Esquiva = 10 + Evasão +", self.base_source)
        self.assertIn("## 7. Esquiva rolada pelo defensor", self.base_source)
        self.assertIn("### Esquiva e bloqueio", self.source)

    def test_check_detects_stale_book(self):
        self.generate()
        self.assertEqual(self.run_generator("--check").returncode, 0)
        self.book.write_text(self.source.replace("`2 + nível / 6`", "`4 + nível / 6`"), encoding="utf-8")
        result = self.run_generator("--check")
        self.assertNotEqual(result.returncode, 0)
        self.assertIn("desatualizado", result.stderr)

    def test_book_temporarily_hides_unused_grip_types(self):
        section = self.base_source.split("### Empunhaduras de arma", 1)[1].split("### Dano e bloqueio", 1)[0]
        visible = re.sub(r"<!--.*?-->", "", section, flags=re.S)
        for name in ["Arma pesada, uma mão", "Arma média, duas mãos",
                     "Escudo leve", "Escudo médio", "Escudo pesado"]:
            with self.subTest(name=name):
                self.assertIn(f"| {name} |", section, "Preserve o tipo oculto para reativação")
                self.assertNotIn(f"| {name} |", visible)
        for name in ["Arma leve, uma mão", "Arma média, uma mão",
                     "Arma pesada, duas mãos", "Duas armas leves", "Duas armas médias"]:
            self.assertIn(f"| {name} |", visible)

    def test_derived_rules_exist_only_in_base_book(self):
        self.assertIn("| Valor derivado | Fórmula |", self.base_source)
        self.assertNotIn("| Valor derivado | Fórmula |", self.source)
        self.assertIn("../livro-de-regras.md#grau-crescimento-e-arredondamento", self.source)

    def test_derived_formula_changes_come_from_base_book(self):
        changed = self.base_source.replace("`1 + piso((nível - 1) / 2)`",
                                           "`2 + piso((nível - 1) / 2)`")
        self.assertNotEqual(changed, self.base_source)
        self.base_book.write_text(changed, encoding="utf-8")
        printed = self.generate()
        self.assertIn("Grau = 2 + piso((nível - 1) / 2)", printed)
        self.assertNotIn("Grau = 1 + piso((nível - 1) / 2)", printed)

    def test_check_detects_stale_base_book(self):
        self.generate()
        self.assertEqual(self.run_generator("--check").returncode, 0)
        self.base_book.write_text(self.base_source.replace("`piso(Grau × Grau / 2)`",
                                                           "`piso(Grau × Grau / 3)`"), encoding="utf-8")
        result = self.run_generator("--check")
        self.assertNotEqual(result.returncode, 0)
        self.assertIn("desatualizado", result.stderr)

    def test_missing_base_rules_preserves_existing_pdf(self):
        self.generate()
        original = self.pdf.read_bytes()
        self.base_book.write_text(self.base_source.replace("### Grau, crescimento e arredondamento",
                                                           "### Valores antigos"), encoding="utf-8")
        result = self.run_generator()
        self.assertNotEqual(result.returncode, 0)
        self.assertIn("Grau, crescimento e arredondamento", result.stderr)
        self.assertEqual(self.pdf.read_bytes(), original)

    def test_missing_source_and_overflow_preserve_existing_pdf(self):
        self.generate()
        original = self.pdf.read_bytes()
        self.book.write_text(self.source.replace("### Vitalidades", "### Recursos antigos"), encoding="utf-8")
        result = self.run_generator()
        self.assertNotEqual(result.returncode, 0)
        self.assertIn("Vitalidades", result.stderr)
        self.assertEqual(self.pdf.read_bytes(), original)
        changed = self.source.replace("Acrobacia, Esconder-se", "Acrobacia " + "alongada " * 90 + ", Esconder-se")
        self.book.write_text(changed, encoding="utf-8")
        result = self.run_generator()
        self.assertNotEqual(result.returncode, 0)
        self.assertIn("espaço", result.stderr)
        self.assertEqual(self.pdf.read_bytes(), original)


if __name__ == "__main__":
    unittest.main()
