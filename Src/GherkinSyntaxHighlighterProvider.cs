namespace GherkinSyntaxHighlighter
{
    using System.ComponentModel.Composition;

    using Microsoft.VisualStudio.Text;
    using Microsoft.VisualStudio.Text.Classification;
    using Microsoft.VisualStudio.Utilities;

    using global::GherkinSyntaxHighlighter.Parser;

    [Export(typeof(IClassifierProvider))]
    [ContentType("CSharp")]
    internal class GherkinSyntaxHighlighterProvider : IClassifierProvider
    {
        private bool ignoreRequest;

        [Import]
        internal IClassificationTypeRegistryService ClassificationRegistry { get; set; }

        [Import]
        public IClassifierAggregatorService ClassifierAggregatorService { get; set; }

        public IClassifier GetClassifier(ITextBuffer buffer)
        {
            if (this.ignoreRequest)
            {
                return null;
            }

            try
            {
                this.ignoreRequest = true;
                return
                    buffer.Properties.GetOrCreateSingletonProperty(() =>
                        new GherkinSyntaxHighlighter(
                            this.ClassifierAggregatorService.GetClassifier(buffer), this.ClassificationRegistry, new CSharpPatternMatcher()));
            }
            finally
            {
                this.ignoreRequest = false;
            }
        }
    }
}