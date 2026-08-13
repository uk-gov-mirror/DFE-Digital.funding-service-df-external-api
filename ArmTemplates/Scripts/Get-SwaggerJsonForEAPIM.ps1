<#
.SYNOPSIS
Fetches a swagger document from a web app and updates it for consumption by EAPIM

.DESCRIPTION
Fetches a swagger document from a web app and updates it for consumption by EAPIM

.PARAMETER WebAppName
The hostname of the web app to fetch the swagger document from

.PARAMETER Scheme
The scheme to fetch the swagger document as. Defaults to https

.PARAMETER SwaggerJsonPath
The path to the swagger documenton the web app. Defaults to /swagger/v1/swagger.json

.PARAMETER OutputFile
The file to write containing the updated swagger document

.EXAMPLE
Get-SwaggerJsonForEAPIM.ps1 -WebAppName testSite.azurewebsites.net -Scheme https -SwaggerJsonPath /swagger/v1/swagger.json -OutputFile ./someFile.json
#>

[CmdletBinding()]
Param(
    [Parameter(Mandatory=$true)]
    [string] $WebAppName,
    [ValidateSet("http", "https")]
    [string] $Scheme = "https",
    [string] $SwaggerJsonPath = "/swagger/v1/swagger.json",
    [Parameter(Mandatory=$true)]
    [string] $OutputFile
)


# Step 1: Get swagger document..

$urlToSwaggerDoc = "$($Scheme)://$($WebAppName)$($SwaggerJsonPath)"
$swaggerJson = Invoke-WebRequest -UseBasicParsing -Uri $urlToSwaggerDoc

# Step 2: Convert it to a PSCustomObject and manipulate it
$swaggerObject = $swaggerJson | ConvertFrom-Json -Depth 20

Add-Member -InputObject $swaggerObject -MemberType NoteProperty -Name host -Value $WebAppName
Add-Member -InputObject $swaggerObject -MemberType NoteProperty -Name basePath -Value "/"
Add-Member -InputObject $swaggerObject -MemberType NoteProperty -Name schemes -Value @( $Scheme )

# Step 3: Write it out to a .json file for consumption
$outputJson = $swaggerObject | ConvertTo-Json -Depth 20

$outputJson | Out-File -FilePath $OutputFile