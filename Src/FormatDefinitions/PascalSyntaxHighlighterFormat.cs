namespace GherkinSyntaxHighlighter.FormatDefinitions
{
    using System.ComponentModel.Composition;
    using System.Windows;
    using System.Windows.Media;

    using Microsoft.VisualStudio.Text.Classification;
    using Microsoft.VisualStudio.Utilities;

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "PascalSyntaxHighlighter")]
    [Name("PascalSyntaxHighlighter")]
    [DisplayName("Pascal Syntax Highlighter")]
    [UserVisible(true)]
    [Order(Before = Priority.Default)]
    internal sealed class PascalSyntaxHighlighterFormat : ClassificationFormatDefinition
    {
        public PascalSyntaxHighlighterFormat()
        {
            var firstCharacterUnderline = new TextDecoration();

            var underlinePen = new Pen
                            {
                                Brush =
                                    new LinearGradientBrush(
                                    Colors.White, Colors.Magenta, new Point(0, 0.5), new Point(1, 0.5)) { Opacity = 0.5 },
                                Thickness = 1.5,
                                DashStyle = DashStyles.Solid
                            };

            firstCharacterUnderline.Pen = underlinePen;
            firstCharacterUnderline.PenThicknessUnit = TextDecorationUnit.FontRecommended;

            var textDecorations = new TextDecorationCollection { firstCharacterUnderline };
            this.TextDecorations = textDecorations;
            
            this.DisplayName = "Pascal Syntax Highlighter";
            this.IsBold = true;
        }
    }
}
