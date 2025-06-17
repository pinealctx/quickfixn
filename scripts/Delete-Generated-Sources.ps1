if ($args.Length -gt 0) {
    Write-Error "This script does not take any parameters."
    Exit 1
}

# Get root path
$rootpath = Join-Path $PSScriptRoot '..' | Resolve-Path

$isErr = $false

# Correctly build field file paths
$fieldsDir = Join-Path -Path $rootpath -ChildPath "QuickFIXn\Fields"
$fieldFiles = @(
    (Join-Path -Path $fieldsDir -ChildPath "Fields.cs")
    (Join-Path -Path $fieldsDir -ChildPath "FieldTags.cs")
)

# Correctly build message file paths
$messagesDir = Join-Path -Path $rootpath -ChildPath "QuickFIXn\Messages"
# Use Get-ChildItem instead of wildcard paths
$messageFiles = Get-ChildItem -Path $messagesDir -Filter "FIX*.cs" -ErrorAction SilentlyContinue

Write-Host '--Deleting generated code--' -ForegroundColor Cyan
Write-Host 'Field definition files:' -ForegroundColor Cyan

foreach ($file in $fieldFiles) {
    Write-Host "* Attempting to delete file: $file" -ForegroundColor Cyan
    if (Test-Path $file) {
        Remove-Item $file
    } else {
        Write-Host '  WARNING: File does not exist' -ForegroundColor Red
        $isErr = $true
    }
}

Write-Host 'Message definition files:' -ForegroundColor Cyan

if ($null -eq $messageFiles -or $messageFiles.Count -eq 0) {
    Write-Host "  WARNING: No FIX*.cs files found in $messagesDir." -ForegroundColor Red
    $isErr = $true
} else {
    # Delete each found file
    foreach ($file in $messageFiles) {
        Write-Host "* Deleting: $($file.FullName)" -ForegroundColor Cyan
        Remove-Item -Path $file.FullName -Force
    }
    Write-Host "* All FIX*.cs files are deleted." -ForegroundColor Cyan
}

if ($isErr) {
  Write-Host -ForegroundColor Red "---"
  Write-Host -ForegroundColor Red "GENERAL ERROR: Not all expected files were found, but all that *were* found were deleted."
  Write-Host -ForegroundColor Red "  This may be ok, for instance, if you ran this script multiple times in a row."
  Write-Host -ForegroundColor Red "  This should *not* happen on a fresh checkout, or right after running the generator."
  Write-Host -ForegroundColor Red "  Users of this script must evaluate this result accordingly."
}

Write-Host -ForegroundColor Green "Deletion complete."
