using DateTimeProviderAnalyser.Test.TestHelpers;
using Microsoft.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DateTimeProviderAnalyser.Test
{
    [TestClass]
    public class UnitTests
    {
        public UnitTests()
        {
            _diagnosticVerifier = new DateTimeProviderDiagnosticVerifier();
            _fixVerifier = new DateTimeProviderCodeFixVerifier();
        }

        [TestMethod]
        public void ExpectNoDiagnosticResults()
        {
            const string source = @"";
            _diagnosticVerifier.VerifyCSharpDiagnostic(source);
        }

        [TestMethod]
        public void ApplySuggestedFix()
        {
            const string sourceCodeWithIssue = @"
    using System;

    namespace ConsoleApplication1
    {
        class TypeName
        {
        }
    }";

            var expected = new DiagnosticResult
            {
                Id = "DateTimeProviderAnalyser",
                Message = "Type name \'TypeName\' contains lowercase letters",
                Severity = DiagnosticSeverity.Warning,
                Locations = new[] { new DiagnosticResultLocation("Test0.cs", 6, 15) }
            };

            _diagnosticVerifier.VerifyCSharpDiagnostic(sourceCodeWithIssue, expected);

            const string sourceWithCodeFix = @"
    using System;

    namespace ConsoleApplication1
    {
        class TYPENAME
        {
        }
    }";
            _fixVerifier.VerifyCSharpFix(sourceCodeWithIssue, sourceWithCodeFix);
        }

        private readonly DateTimeProviderCodeFixVerifier _fixVerifier;
        private readonly DateTimeProviderDiagnosticVerifier _diagnosticVerifier;
    }
}