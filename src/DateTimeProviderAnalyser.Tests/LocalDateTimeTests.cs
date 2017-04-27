using DateTimeProviderAnalyser.DateTimeNow;
using DateTimeProviderAnalyser.Test.TestHelpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DateTimeProviderAnalyser.Test
{
    [TestClass]
    public class LocalDateTimeTests : CodeFixVerifier
    {
        private const string SourceCodeWithIssue = @"
    using System;

    namespace ConsoleApplication1
    {
        class TypeName
        {
            public TypeName()
            {
                var now = DateTime.Now;
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
                var now = DateTimeProvider.LocalNow;
            }
        }
    }";

        protected override CodeFixProvider GetCSharpCodeFixProvider()
        {
            return new DateTimeNowCodeFix();
        }

        protected override DiagnosticAnalyzer GetCSharpDiagnosticAnalyzer()
        {
            return new DateTimeNowAnalyser();
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
                Id = "DateTimeNowAnalyser",
                Message = "Use DateTimeProvider.LocalNow instead of DateTime.Now",
                Severity = DiagnosticSeverity.Warning,
                Locations = new[] { new DiagnosticResultLocation("Test0.cs", 10, 27) }
            };

            VerifyCSharpDiagnostic(SourceCodeWithIssue, expected);
        }

        [TestMethod]
        public void ApplySuggestedFix()
        {
            var expected = new DiagnosticResult
            {
                Id = "DateTimeNowAnalyser",
                Message = "Use DateTimeProvider.LocalNow instead of DateTime.Now",
                Severity = DiagnosticSeverity.Warning,
                Locations = new[] { new DiagnosticResultLocation("Test0.cs", 10, 27) }
            };

            VerifyCSharpDiagnostic(SourceCodeWithIssue, expected);
            VerifyCSharpFix(SourceCodeWithIssue, SourceCodeWithFix, null, true);
        }

    }
}