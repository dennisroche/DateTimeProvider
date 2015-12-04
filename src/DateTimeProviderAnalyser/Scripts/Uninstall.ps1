param($installPath, $toolsPath, $package, $project)

function Remove-AnalyserFromProject {
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
                        $Project.Object.AnalyzerReferences.Remove($analyzerFilePath.FullName)
                    }
                } catch {
                    Write-Warning "Error trying to remove AnalyzerReference: '$_'"
                }
            }
        }
    }
}

Remove-AnalyserFromProject -ToolsPath $ToolsPath -LanguageFolder ""
if($Project.Type -eq "C#") {
    Remove-AnalyserFromProject -ToolsPath $ToolsPath -LanguageFolder "cs"
}