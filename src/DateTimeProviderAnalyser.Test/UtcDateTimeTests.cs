using DateTimeProviderAnalyser.DateTimeUtcNow;
using DateTimeProviderAnalyser.Test.TestHelpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DateTimeProviderAnalyser.Test
{
    [TestClass]
    public class UtcDateTimeTests : CodeFixVerifier
    {
        private const string SourceCodeWithIssue = @"
    using System;

    namespace ConsoleApplication1
    {
        class TypeName
        {
            public TypeName()
            {
                var now = DateTime.UtcNow;
            }
        }
    }";

        private const string SourceCodeWithFix = @"
    using System;

    namespace ConsoleApplication1
    {
        class TypeName
        {
            public TypeName()
            {
                var now = DateTimeProvider.UtcNow;
            }
        }
    }";

        protected override CodeFixProvider GetCSharpCodeFixProvider()
        {
            return new DateTimeUtcNowCodeFix();
        }

        protected override DiagnosticAnalyzer GetCSharpDiagnosticAnalyzer()
        {
            return new DateTimeUtcNowAnalyser();
        }

        [TestMethod]
        public void ExpectNoDiagnosticResults()
        {
            const string source = @"";
            VerifyCSharpDiagnostic(source);
        }

        [TestMethod]
        public void IdentifySuggestedFix()
        {
            var expected = new DiagnosticResult
            {
                Id = "DateTimeUtcNowAnalyser",
                Message = "Use DateTimeProvider.UtcNow instead of DateTime.UtcNow",
                Severity = DiagnosticSeverity.Warning,
                Locations = new[] {new DiagnosticResultLocation("Test0.cs", 10, 27)}
            };

            VerifyCSharpDiagnostic(SourceCodeWithIssue, expected);
        }

        [TestMethod]
        public void ApplySuggestedFix()
        {
            var expected = new DiagnosticResult
            {
                Id = "DateTimeUtcNowAnalyser",
                Message = "Use DateTimeProvider.UtcNow instead of DateTime.UtcNow",
                Severity = DiagnosticSeverity.Warning,
                Locations = new[] {new DiagnosticResultLocation("Test0.cs", 10, 27)}
            };

            VerifyCSharpDiagnostic(SourceCodeWithIssue, expected);
            VerifyCSharpFix(SourceCodeWithIssue, SourceCodeWithFix, null, true);
        }
    }
}