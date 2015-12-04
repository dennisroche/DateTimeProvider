using DateTimeProviderAnalyser.Test.TestHelpers;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.Diagnostics;

namespace DateTimeProviderAnalyser.Test
{
    public class DateTimeProviderCodeFixVerifier : CodeFixVerifier
    {
        protected override CodeFixProvider GetCSharpCodeFixProvider()
        {
            return new DateTimeAnalyserCodeFix();
        }

        protected override DiagnosticAnalyzer GetCSharpDiagnosticAnalyzer()
        {
            return new DateTimeProviderAnalyser();
        }
    }
}