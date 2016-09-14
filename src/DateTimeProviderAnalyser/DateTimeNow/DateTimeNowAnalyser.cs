using System;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace DateTimeProviderAnalyser.DateTimeNow
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class DateTimeNowAnalyser : DiagnosticAnalyzer
    {
        public const string DiagnosticId = nameof(DateTimeNowAnalyser);

        public const string Title = "Use DateTimeProvider.LocalNow instead of DateTime";
        public const string MessageFormat = "Use DateTimeProvider.LocalNow instead of DateTime.Now";
        public const string Description = "Use DateTimeProvider so that date and time is abstracted and easier to test";
        public const string HelpLinkUri = "https://github.com/dennisroche/DateTimeProvider";

        private const string Category = "Syntax";
        private const bool AlwaysEnabledByDefault = true;

        public DateTimeNowAnalyser()
        {
            Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Warning, AlwaysEnabledByDefault, Description, HelpLinkUri);
            SupportedDiagnostics = ImmutableArray.Create(Rule);
        }

        public DiagnosticDescriptor Rule { get; }
        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; }

        public override void Initialize(AnalysisContext context)
        {
            context.RegisterSyntaxNodeAction(AnalyzeNode, SyntaxKind.SimpleMemberAccessExpression);
        }

        private void AnalyzeNode(SyntaxNodeAnalysisContext context)
        {
            // The analyzer will run on every keystroke in the editor, so we are performing the quickest tests first
            var member = context.Node as MemberAccessExpressionSyntax;
            var identifier = member?.Expression as IdentifierNameSyntax;

            if (identifier == null)
                return;

            if (identifier.Identifier.Text != nameof(DateTime))
                return;

            var identifierSymbol = context.SemanticModel.GetSymbolInfo(identifier).Symbol as INamedTypeSymbol;
            if (identifierSymbol?.ContainingNamespace.ToString() != nameof(System))
                return;

            var accessor = member.Name.ToString();
            if (accessor != nameof(DateTime.Now))
                return;

            var rule = Rule;
            var diagnostic = Diagnostic.Create(rule, member.GetLocation());
            context.ReportDiagnostic(diagnostic);
        }
    }
}