using System.Collections.Immutable;
using System.Linq;
using DateTimeProviderAnalyser.Resources;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace DateTimeProviderAnalyser
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class DateTimeProviderAnalyser : DiagnosticAnalyzer
    {
        public const string DiagnosticId = nameof(DateTimeProviderAnalyser);

        private static readonly LocalizableString Title = new LocalizableResourceString(nameof(DateTimeAnalyser.Title), DateTimeAnalyser.ResourceManager, typeof(DateTimeAnalyser));
        private static readonly LocalizableString MessageFormat = new LocalizableResourceString(nameof(DateTimeAnalyser.MessageFormat), DateTimeAnalyser.ResourceManager, typeof(DateTimeAnalyser));
        private static readonly LocalizableString Description = new LocalizableResourceString(nameof(DateTimeAnalyser.Description), DateTimeAnalyser.ResourceManager, typeof(DateTimeAnalyser));

        private const string Category = "Naming";

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Warning, isEnabledByDefault: true, description: Description));

        public override void Initialize(AnalysisContext context)
        {
            context.RegisterSymbolAction(AnalyzeSymbol, SymbolKind.NamedType);
        }

        private static void AnalyzeSymbol(SymbolAnalysisContext context)
        {
            var namedTypeSymbol = (INamedTypeSymbol)context.Symbol;
            if (!namedTypeSymbol.Name.ToCharArray().Any(char.IsLower))
                return;

            var rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Warning, isEnabledByDefault: true, description: Description);
            var diagnostic = Diagnostic.Create(rule, namedTypeSymbol.Locations[0], namedTypeSymbol.Name);

            context.ReportDiagnostic(diagnostic);
        }
    }
}
