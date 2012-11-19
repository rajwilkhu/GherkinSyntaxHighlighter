using System.ComponentModel.Composition;
using System.Windows.Media;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace GherkinSyntaxHighlighter
{
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "GherkinSyntaxHighlighter")]
    [Name("GherkinSyntaxHighlighter")]
    [UserVisible(true)]
    [Order(Before = Priority.Default)]
    internal sealed class GherkinSyntaxHighlighterFormat : ClassificationFormatDefinition
    {
        public GherkinSyntaxHighlighterFormat()
        {
            this.DisplayName = "Gherkin Syntax Highlighter";
            this.BackgroundColor = Colors.BlueViolet;
            this.TextDecorations = System.Windows.TextDecorations.Underline;
        }
    }
}
