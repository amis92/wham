using System;
using System.Threading;
using System.Threading.Tasks;
using WarHub.ArmouryModel.Source;

namespace WarHub.ArmouryModel.Analysis
{
    public class Class1
    {

    }

    public interface ICatalogueAnalyzer
    {
        void Analyze(ICatalogueAnalysisContext context, CancellationToken cancellationToken = default);
    }

    public interface ICatalogueAnalysisContext
    {
        CatalogueNode Catalogue { get; }

        void AddDiagnostic(Diagnostic diagnostic);
    }

    public sealed class Diagnostic
    {
        public DiagnosticDescriptor Descriptor { get; }

        public SourceLocation Location { get; }

        // also parameters for descriptor's message format
    }

    // single descriptor per diagnostic "type"
    public sealed class DiagnosticDescriptor
    {

    }

    // should be probably a part of Source. name: Location or Path?
    public class SourceLocation
    {

    }
}
