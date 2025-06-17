if ($args.Length -gt 0) {
    Write-Error "This script does not take any parameters."
    Exit 1
}

# Get root path
$rootpath = Join-Path $PSScriptRoot '..' | Resolve-Path

# Fix the Join-Path usage for spec files - proper path construction
$specDir = Join-Path -Path $rootpath -ChildPath "spec"
# Use Get-ChildItem to find XML files in the spec/fix directory
$specs = Get-ChildItem -Path (Join-Path -Path $specDir -ChildPath "fix") -Filter "*.xml" -ErrorAction SilentlyContinue | 
    Select-Object -ExpandProperty FullName

# Check if specs were found
if ($null -eq $specs -or $specs.Count -eq 0) {
    Write-Error "No XML spec files found in $specDir\fix"
    Exit 1
}

# Execute DDTool with the found specs
$ddToolDir = Join-Path -Path $rootpath -ChildPath "DDTool"
try {
    # Store current location
    Push-Location -Path $ddToolDir
    
    # Build command arguments
    $arguments = @("run", "--project", "DDTool", "--outputdir", $rootpath)
    $arguments += $specs
    
    # Run dotnet command with all spec files
    & dotnet $arguments
}
finally {
    # Restore previous location
    Pop-Location
}
