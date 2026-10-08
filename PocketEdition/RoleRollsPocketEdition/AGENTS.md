# Orientações para Codex

Estas instruções valem para todo o repositório.

## Habilidades e comunicação

- No início de cada tarefa, use a skill `caveman` em modo `full`.
- Antes de implementar mudanças, use a skill `superpowers:brainstorming`.
- O brainstorming deve acontecer somente no chat: apresente raciocínio, opções
  ou dúvidas de modo curto e execute quando o pedido estiver claro.
- Não crie, atualize ou exija arquivos de especificação, documentos de design,
  planos de implementação ou arquivos de tarefas, salvo pedido explícito do
  usuário.

## Área de trabalho

- Nunca crie, use ou gerencie Git worktrees.
- Trabalhe no checkout fornecido pelo usuário.
- Preserve alterações não relacionadas que já existirem no diretório de
  trabalho.

## Ficha impressa do Land of Heroes

- `docs/land-of-heroes/livro-de-regras.md` é a fonte dos campos, nomes,
  especialidades, fórmulas de campanha e gatilhos da ficha de impressão do LoH.
- Definições do sistema, como Grau (`Tier`), Crescimento (`Growth`) e
  arredondamento, pertencem a `docs/livro-de-regras.md`. O livro do LoH
  referencia essas regras e explica sua aplicação, sem duplicar as definições.
- Qualquer informação necessária à ficha deve estar descrita nesse livro
  ou na regra do livro-base que ele referencia. Acrescente o que faltar ao
  livro antes de atualizar o PDF.
- Ao alterar uma regra ou um campo impresso, regenere a ficha. A partir de
  `PocketEdition`, execute `powershell -NoProfile -File scripts/loh-ficha/gerar-ficha.ps1`.
- Preserve o gerador em `PocketEdition/scripts/loh-ficha/gerar_ficha.py`;
  regras devem ser lidas dos respectivos livros, sem fórmulas ou listas
  duplicadas no código de layout. Mantenha texto UTF-8 e fontes incorporadas
  com acentos.
- O resultado é `PocketEdition/output/pdf/ficha-land-of-heroes-template.pdf`,
  estático, em branco, com duas páginas A4. Renderize e revise as duas páginas
  após regenerar; confira a origem com o mesmo comando seguido de `-Check`.
