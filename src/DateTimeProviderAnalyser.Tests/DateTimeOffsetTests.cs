using DateTimeProviderAnalyser.DateTimeOffsetNow;
using DateTimeProviderAnalyser.Tests.TestHelpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.Diagnostics;
using Xunit;

namespace DateTimeProviderAnalyser.Tests
{
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

        private const string SourceCodeWithFix = @"
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

        [Fact]
        public void ExpectNoDiagnosticResults()
        {
            const string source = @"";
            VerifyCSharpDiagnostic(source);
        }

        [Fact]
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

        [Fact]
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
            VerifyCSharpFix(SourceCodeWithIssue, SourceCodeWithFix, null, true);
        }
    }
}