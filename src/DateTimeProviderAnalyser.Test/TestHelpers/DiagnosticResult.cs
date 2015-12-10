using Microsoft.CodeAnalysis;

namespace DateTimeProviderAnalyser.Test.TestHelpers
{
    public struct DiagnosticResult
    {
        public string Path => Locations.Length > 0 ? Locations[0].Path : "";
        public int Line => Locations.Length > 0 ? Locations[0].Line : -1;
        public int Column => Locations.Length > 0 ? Locations[0].Column : -1;

        public DiagnosticResultLocation[] Locations
        {
            get { return _locations ?? (_locations = new DiagnosticResultLocation[] {}); }
            set { _locations = value; }
        }

        public DiagnosticSeverity Severity { get; set; }
        public string Id { get; set; }
        public string Message { get; set; }

        private DiagnosticResultLocation[] _locations;
    }
}