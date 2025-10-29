$fileId = 6843557
$modId = 2595901
$destination = "$PSScriptRoot/jcdcdev.Eco.Signs/lib"

$url = "https://api.mod.io/v1/games/6/mods/$modId/files/$fileId/download"

$tempPath = "$destination\temp"
$zipPath = "$tempPath\signs.zip"

Write-Host "Downloading dependencies from mod.io: $url"

if (Test-Path $destination) {
    Remove-Item $destination -Recurse -Force
}

New-Item -ItemType Directory -Path $destination | Out-Null

if (Test-Path $tempPath) {
    Remove-Item $tempPath -Recurse -Force
}

New-Item -ItemType Directory -Path $tempPath | Out-Null

Invoke-WebRequest -Uri $url -OutFile $zipPath

# Extract all dll files to folder
Expand-Archive -Path $zipPath -DestinationPath $tempPath -Force

Get-ChildItem -Path $tempPath -Filter "*.dll" -Recurse | ForEach-Object {
    Copy-Item -Path $_.FullName -Destination $destination
}

# Remove zip file
Remove-Item $tempPath -Recurse -Force