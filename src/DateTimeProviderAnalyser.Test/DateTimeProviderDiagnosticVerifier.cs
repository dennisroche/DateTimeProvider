using DateTimeProviderAnalyser.Test.TestHelpers;
using Microsoft.CodeAnalysis.Diagnostics;

namespace DateTimeProviderAnalyser.Test
{
    public class DateTimeProviderDiagnosticVerifier : DiagnosticVerifier
    {
        protected override DiagnosticAnalyzer GetCSharpDiagnosticAnalyzer()
        {
            return new DateTimeProviderAnalyser();
        }
    }
}