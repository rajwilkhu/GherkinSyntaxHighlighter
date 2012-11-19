namespace GherkinSyntaxHighlighter.FormatDefinitions
{
    using System.ComponentModel.Composition;
    using System.Windows.Media;

    using Microsoft.VisualStudio.Text.Classification;
    using Microsoft.VisualStudio.Utilities;

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "GherkinSyntaxHighlighter")]
    [Name("GherkinSyntaxHighlighter")]
    [DisplayName("Gherkin Syntax Highlighter")]
    [UserVisible(true)]
    [Order(Before = Priority.Default)]
    internal sealed class GherkinSyntaxHighlighterFormat : ClassificationFormatDefinition
    {
        public GherkinSyntaxHighlighterFormat()
        {
            this.DisplayName = "Gherkin Syntax Highlighter";
            this.IsItalic = true;
            this.ForegroundColor = Colors.Crimson;
        }
    }
}
