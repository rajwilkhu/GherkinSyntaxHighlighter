using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace GherkinSyntaxHighlighter
{
    internal static class GherkinSyntaxHighlighterClassificationDefinition
    {
        [Export(typeof(ClassificationTypeDefinition))]
        [Name("GherkinSyntaxHighlighter")]
        internal static ClassificationTypeDefinition GherkinSyntaxHighlighterType { get; set; }
    }
}
