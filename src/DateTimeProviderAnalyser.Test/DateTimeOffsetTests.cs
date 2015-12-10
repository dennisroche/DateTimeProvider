using DateTimeProviderAnalyser.DateTimeOffsetNow;
using DateTimeProviderAnalyser.Test.TestHelpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DateTimeProviderAnalyser.Test
{
    [TestClass]
    public class DateTimeOffsetTests : CodeFixVerifier
    {
        private const string SourceCodeWithIssue = @"
    using System;

    namespace ConsoleApplication1
    {
        class TypeName
        {
            public TypeName()
            {
                var now = DateTimeOffset.Now;
            }
        }
    }";

        private const string SourceWithCodeFix = @"
    using System;

    namespace ConsoleApplication1
    {
        class TypeName
        {
            public TypeName()
            {
                var now = DateTimeProvider.Now;
            }
        }
    }";

        protected override CodeFixProvider GetCSharpCodeFixProvider()
        {
            return new DateTimeOffsetNowCodeFix();
        }

        protected override DiagnosticAnalyzer GetCSharpDiagnosticAnalyzer()
        {
            return new DateTimeOffsetNowAnalyser();
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
                Id = "DateTimeOffsetNowAnalyser",
                Message = "Use DateTimeProvider.Now instead of DateTimeOffset.Now",
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
                Id = "DateTimeOffsetNowAnalyser",
                Message = "Use DateTimeProvider.Now instead of DateTimeOffset.Now",
                Severity = DiagnosticSeverity.Warning,
                Locations = new[] {new DiagnosticResultLocation("Test0.cs", 10, 27)}
            };

            VerifyCSharpDiagnostic(SourceCodeWithIssue, expected);
            VerifyCSharpFix(SourceCodeWithIssue, SourceWithCodeFix, null, true);
        }
    }
}