param (
    [string]$ProjectFilePath,
    [string]$ProjectId
)

function To-GameVer
{
    param (
        [string]$version
    )
    $semVer = $version -replace "-.*" -replace "[^0-9.]"
    $semVer = $semVer.Split('.')
    $utput = "$( $semVer[1] ).$( $semVer[2] )"
    if ($semVer[3])
    {
        $utput += ".$( $semVer[3] )"
    }
    return $utput
}

function Sanitize-VersionRange
{
    param (
        [string]$range
    )
    return ($range -replace "[\[\]\(\)]", "").Split(',')[0]
}

function Check-Dependencies
{
    param (
        [string]$catalogEntryUrl
    )

    $catalogEntry = Invoke-RestMethod -Uri $catalogEntryUrl
    $dependencies = @()
    $catalogEntry.dependencyGroups | ForEach-Object { $_.dependencies | ForEach-Object { $dependencies += $_ } }

    $ecoReferenceAssemblies = $dependencies | Where-Object { $_.id -eq "Eco.ReferenceAssemblies" }
    if ($ecoReferenceAssemblies)
    {
        return Sanitize-VersionRange -range $ecoReferenceAssemblies.range
    }
    $elixrModsFramework = $dependencies | Where-Object { $_.id -eq "ElixrMods.Framework" }
    if ($elixrModsFramework)
    {
        $cleanRange = Sanitize-VersionRange -range $elixrModsFramework.range
        $elixrModsFrameworkUrl = "https://api.nuget.org/v3/registration5-gz-semver2/elixrmods.framework/$cleanRange.json"
        $elixrModsFrameworkResponse = Invoke-RestMethod -Uri $elixrModsFrameworkUrl
        $elixrCatalogEntryUrl = $elixrModsFrameworkResponse.catalogEntry
        $elixrModsFrameworkCatalogResponse = Invoke-RestMethod -Uri $elixrCatalogEntryUrl
        $deps = @()
        $elixrModsFrameworkCatalogResponse.dependencyGroups | ForEach-Object { $_.dependencies | ForEach-Object { $deps += $_ } }

        $ecoReferenceAssembliesInFramework = $deps | Where-Object { $_.id -eq "Eco.ReferenceAssemblies" } | Select-Object -First 1
        if ($ecoReferenceAssembliesInFramework)
        {
            return Sanitize-VersionRange -range $( $ecoReferenceAssembliesInFramework.range )
        }
        else
        {
            return "N/A"
        }
    }
    else
    {
        return "N/A"
    }
}

function Get-PackageVersion
{
    param (
        [string]$csprojPath,
        [string]$packageId
    )

    [xml]$csproj = Get-Content -Path $csprojPath
    $namespace = @{ msb = "http://schemas.microsoft.com/developer/msbuild/2003" }
    $packageReference = $csproj.Project.ItemGroup.PackageReference | Where-Object { $_.Include -eq $packageId }
    if ($packageReference)
    {
        return Sanitize-VersionRange -range $packageReference.Version
    }
    else
    {
        return "N/A"
    }
}

function Get-EcoVersionFromPackage
{
    param (
        [string]$packageId,
        [string]$version
    )
    $nugetApiUrl = "https://api.nuget.org/v3/registration5-gz-semver2/$packageId/$version.json"
    $response = Invoke-RestMethod -Uri $nugetApiUrl
    $catalogEntryUrl = $response.catalogEntry
    $ecoVersion = Check-Dependencies -catalogEntryUrl $catalogEntryUrl
    return To-GameVer -version $ecoVersion
}

function Check-Tags
{
    param (
        [string]$packageId,
        [string]$csprojPath
    )

    git fetch --all --tags

    $tags = git tag

    $results = @()
    foreach ($tag in $tags)
    {
        git checkout $tag --quiet

        $version = Get-PackageVersion -csprojPath $csprojPath -packageId $packageId
        if ($version -eq "N/A")
        {
            $ya = "elixrmods.framework"
            $yaversion = Get-PackageVersion -csprojPath $csprojPath -packageId $ya
            if ($yaversion -eq "N/A")
            {
                $ecoVersion = "N/A"
            }
            else
            {
                $ecoVersion = Get-EcoVersionFromPackage -packageId $ya -version $yaversion
            }
        }
        else
        {
            $ecoVersion = Get-EcoVersionFromPackage -packageId $packageId -version $version
        }
        $results += [PSCustomObject]@{
            Tag = $tag
            FilePath = $csprojPath
            Version = $version
            EcoVersion = $ecoVersion
        }
    }

    return $results
}

$PackageId = "jcdcdev.eco.core"
if ($ProjectFilePath -eq $null)
{
    Write-Output "No .csproj file selected. Exiting."
    exit
}

$results = Check-Tags -packageId $PackageId -csprojPath $ProjectFilePath
$markdownTable = @"
| Version | Core Version | Game Version |
|-----|---------| -----------|`n
"@

$results = $results | Sort-Object -Property Tag -Descending

$results | ForEach-Object {
    if ($_.Version -eq "N/A")
    {
        $versionTxt = "N/A"
    }
    else
    {
        $versionTxt = "[$( $_.Version )](https://github.com/jcdcdev/jcdcdev.Eco.Core/releases/tag/$( $_.Version ))"
    }
    $tagTxt = "[$( $_.Tag )](https://github.com/jcdcdev/$ProjectId/releases/tag/$( $_.Tag ))"
    $markdownTable += "| $tagTxt | $versionTxt | $( $_.EcoVersion ) |
"
}

Write-Output $markdownTable