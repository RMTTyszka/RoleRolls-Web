param(
    [switch]$Check,
    [string]$PythonPath
)

$ErrorActionPreference = 'Stop'
$generatorPath = Join-Path $PSScriptRoot 'gerar_ficha.py'
$requirementsPath = Join-Path $PSScriptRoot 'requirements.txt'

if ($PythonPath) {
    $candidates = @($PythonPath)
} else {
    $runtimePython = Join-Path $env:USERPROFILE '.cache\codex-runtimes\codex-primary-runtime\dependencies\python\python.exe'
    $candidates = @($runtimePython)
    $systemPython = Get-Command python -ErrorAction SilentlyContinue
    if ($systemPython) { $candidates += $systemPython.Source }
}

$selectedPython = $null
foreach ($candidate in $candidates) {
    if (!(Test-Path -LiteralPath $candidate -PathType Leaf)) { continue }
    try {
        & $candidate -c 'import reportlab, pypdf' 2>$null
        if ($LASTEXITCODE -eq 0) {
            $selectedPython = $candidate
            break
        }
    } catch {
        continue
    }
}
if (!$selectedPython) {
    throw "Python com reportlab e pypdf nao encontrado. Instale com python -m pip install -r '$requirementsPath', ou informe -PythonPath."
}

$generatorArgs = @($generatorPath)
if ($Check) { $generatorArgs += '--check' }
& $selectedPython @generatorArgs
if ($LASTEXITCODE -ne 0) {
    exit $LASTEXITCODE
}
