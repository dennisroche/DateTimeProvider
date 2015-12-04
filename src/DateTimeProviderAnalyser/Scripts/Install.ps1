param($installPath, $toolsPath, $package, $project)

function Add-AnalyserToProject {
    param (
        $ToolsPath,
        $Project,
        $LanguageFolder
    )

    $analyzersPaths = Join-Path (Join-Path (Split-Path -Path $ToolsPath -Parent) "analyzers" ) * -Resolve

    foreach($path in $analyzersPaths)
    {
        $path = Join-Path $path -ChildPath $LanguageFolder -Resolve

        if (Test-Path $path)
        {
            foreach ($analyzerFilePath in Get-ChildItem $path -Filter *.dll) {
                try {
                    if($Project.Object.AnalyzerReferences) {
                        $Project.Object.AnalyzerReferences.Add($analyzerFilePath.FullName)
                    }
                } catch {
                    Write-Warning "Error trying to add AnalyzerReference: '$_'"
                }
            }
        }
    }
}

Add-AnalyserToProject -ToolsPath $ToolsPath -Project $Project
if($Project.Type -eq "C#") {
    Add-AnalyserToProject -ToolsPath $ToolsPath -LanguageFolder "cs"
}