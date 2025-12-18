<#
.SYNOPSIS
    Publishes the XWiki.Api NuGet package to nuget.org.

.DESCRIPTION
    This script performs the following steps:
    1. Checks for uncommitted changes (git porcelain)
    2. Determines the Nerdbank GitVersioning version
    3. Validates nuget-key.txt exists, has content, and is gitignored
    4. Runs unit tests (unless -SkipTests is specified)
    5. Publishes to nuget.org

.PARAMETER SkipTests
    Skip running unit tests before publishing.

.EXAMPLE
    .\Publish.ps1
    .\Publish.ps1 -SkipTests
#>

param(
    [switch]$SkipTests
)

$ErrorActionPreference = 'Stop'

# Step 1: Check for uncommitted changes (git porcelain)
Write-Host "Checking for uncommitted changes..." -ForegroundColor Cyan
$gitStatus = git status --porcelain
if ($gitStatus) {
    Write-Error "There are uncommitted changes. Please commit or stash them before publishing."
    exit 1
}
Write-Host "No uncommitted changes detected." -ForegroundColor Green

# Step 2: Determine the Nerdbank GitVersioning version
Write-Host "Determining Nerdbank GitVersioning version..." -ForegroundColor Cyan
$nbgvOutput = nbgv get-version --format json 2>&1
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to get Nerdbank GitVersioning version. Ensure nbgv is installed (dotnet tool install -g nbgv)."
    exit 1
}
$versionInfo = $nbgvOutput | ConvertFrom-Json
$nugetVersion = $versionInfo.NuGetPackageVersion
if (-not $nugetVersion) {
    Write-Error "Failed to determine NuGet package version from Nerdbank GitVersioning."
    exit 1
}
Write-Host "Version: $nugetVersion" -ForegroundColor Green

# Step 3: Check that nuget-key.txt exists, has content, and is gitignored
Write-Host "Validating nuget-key.txt..." -ForegroundColor Cyan
$nugetKeyPath = Join-Path $PSScriptRoot "nuget-key.txt"

if (-not (Test-Path $nugetKeyPath)) {
    Write-Error "nuget-key.txt does not exist. Create it with your NuGet API key."
    exit 1
}

$nugetKey = (Get-Content $nugetKeyPath -Raw).Trim()
if ([string]::IsNullOrWhiteSpace($nugetKey)) {
    Write-Error "nuget-key.txt is empty. Add your NuGet API key."
    exit 1
}

$gitIgnoreCheck = git check-ignore "nuget-key.txt" 2>&1
if ($LASTEXITCODE -ne 0) {
    Write-Error "nuget-key.txt is not gitignored. Add 'nuget-key.txt' to .gitignore to protect your API key."
    exit 1
}
Write-Host "nuget-key.txt is valid and gitignored." -ForegroundColor Green

# Step 4: Run unit tests (unless -SkipTests is specified)
if (-not $SkipTests) {
    Write-Host "Running unit tests..." -ForegroundColor Cyan
    dotnet test --configuration Release --no-restore
    if ($LASTEXITCODE -ne 0) {
        Write-Error "Unit tests failed."
        exit 1
    }
    Write-Host "Unit tests passed." -ForegroundColor Green
} else {
    Write-Host "Skipping unit tests." -ForegroundColor Yellow
}

# Step 5: Build and pack
Write-Host "Building and packing..." -ForegroundColor Cyan
dotnet pack XWiki.Api/XWiki.Api.csproj --configuration Release --no-restore
if ($LASTEXITCODE -ne 0) {
    Write-Error "Build/pack failed."
    exit 1
}
Write-Host "Build and pack succeeded." -ForegroundColor Green

# Step 6: Publish to nuget.org
Write-Host "Publishing to nuget.org..." -ForegroundColor Cyan
$nupkgPath = "XWiki.Api/bin/Release/XWiki.Api.$nugetVersion.nupkg"
if (-not (Test-Path $nupkgPath)) {
    Write-Error "NuGet package not found at: $nupkgPath"
    exit 1
}

dotnet nuget push $nupkgPath --api-key $nugetKey --source https://api.nuget.org/v3/index.json --skip-duplicate
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to publish to nuget.org."
    exit 1
}

Write-Host "Successfully published XWiki.Api $nugetVersion to nuget.org!" -ForegroundColor Green
exit 0
