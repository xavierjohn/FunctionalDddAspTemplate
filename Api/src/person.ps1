
# Demo error handling in PowerShell
$base = "https://localhost:7011/" 

$body = @"
{
  "firstName": "Xavier",
  "lastName": "John",
  "email": "xavier@heaven.com",
  "password": "SimplePassword"
}
"@

$uri = $base + 'api/users?api-version=2023-06-06'

try {

    $content = Invoke-WebRequest -Method Post -Uri $uri -Body $body -ContentType 'application/json'
    Write-Host "✅ Response received:"
    Write-Output $content
}
catch {
   $webResponse = $_.Exception.Response
   if ($webResponse -ne $null) {
        $statusCode = [int]$webResponse.StatusCode
        $statusDescription = $webResponse.StatusDescription

        Write-Host "❌ HTTP Error ($statusCode): ($statusDescription)"

        $stream = $webResponse.GetResponseStream()
        $reader = New-Object System.IO.StreamReader($stream)
        $errorResponse = $reader.ReadToEnd()
        try {
            # Try to parse and pretty-print the JSON error response
            $parsedJson = $errorResponse | ConvertFrom-Json
            $formattedJson = $parsedJson | ConvertTo-Json -Depth 10
            Write-Output $formattedJson
        }
        catch {
            # If response is not valid JSON, just print raw text
            Write-Host "(Unformatted response)"
            Write-Output $errorResponse
        }
    }
    else {
        Write-Host "An unexpected error occurred:"
        Write-Host $_.Exception.Message
    }
}
