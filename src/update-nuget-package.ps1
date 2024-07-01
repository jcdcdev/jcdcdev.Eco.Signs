param(
    [Parameter(Mandatory = $true)]
    [string]$projectFilePath,
    [Parameter(Mandatory = $true)]
    [string]$packageId,
    [Parameter(Mandatory = $false)]
    [switch]$checkOnly
)

$pattern = [regex]"((PackageReference)+.(Include=""$packageId"").+(Version=""(.+)\""))"

$content = Get-Content -Path $projectFilePath -Raw
$results = $pattern.Matches($content)

if ($results.Count -eq 0) {
    Write-Warning "⚠️ No $packageId package reference found in the project file."
    exit 0;
}

if ($results.Count -gt 1) {
    Write-Warning "⚠️ Multiple $packageId package references found in the project file."
    exit 1;
}

$match = $results[0]

$version = $match.Groups[5].Value
Write-Host "🔃 Current Version`n`n$version`n"

try {
    $latestVersionResponse = Invoke-RestMethod "https://api.nuget.org/v3-flatcontainer/$($packageId.ToLowerInvariant())/index.json"
}
catch {
    Write-Warning "⚠️  Failed to retrieve the latest version of $packageId."
    exit 1;
}

$index = $latestVersionResponse.versions.IndexOf($version)
if ($index -eq -1) {
    Write-Warning "⚠️  $version is not found in the list of versions on NuGet."
    exit 1;
}

if ($index -eq $latestVersionResponse.versions.Count - 1) {
    Write-Host "✅  $packageId is up-to-date."
    exit 0;
}

$updatedVersion = $latestVersionResponse.versions[$index + 1]

Write-Host "⬆️  Next Version`n`n$updatedVersion`n"

if (-not $checkOnly) {
    $newContent = $match.Value -replace "(Version=`"(.*?)`")", "Version=""$updatedVersion"" "
    Write-Verbose "New csproj line: $newContent"
    $updatedContent = $content.Replace($match.Value, $newContent)
    $updatedContent | Set-Content -Path $projectFilePath -NoNewline -Encoding Default
}

return @{
    SourceVersion = $version
    TargetVersion = $updatedVersion
    Updated       = $version -ne $updatedVersion
}


