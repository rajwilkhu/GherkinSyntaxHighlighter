namespace GherkinSyntaxHighlighter
{
    using System.Collections.Generic;

    using Microsoft.VisualStudio.Text;
    using Microsoft.VisualStudio.Text.Classification;

    using global::GherkinSyntaxHighlighter.Parser;

    internal class GherkinClassificationSpans : ISyntaxParserObserver
    {
        private const int FirstCharacterWidth = 1;
 
        private readonly IClassificationTypeRegistryService classificationTypeRegistryService;

        private readonly ITextSnapshot textSnapshot;

        private readonly SnapshotPoint snapshotPoint;

        private readonly List<ClassificationSpan> classificationSpans = new List<ClassificationSpan>();

        public IEnumerable<ClassificationSpan> ClassificationSpans 
        { 
            get
            {
                return this.classificationSpans;
            } 
        }
        
        public GherkinClassificationSpans(IClassificationTypeRegistryService classificationTypeRegistryService, ITextSnapshot textSnapshot, SnapshotPoint snapshotPoint)
        {
            this.classificationTypeRegistryService = classificationTypeRegistryService;
            this.textSnapshot = textSnapshot;
            this.snapshotPoint = snapshotPoint;
        }

        public void Parse(string identifier)
        {
            this.classificationSpans.Clear();
            var syntaxParser = new SyntaxParser(this);
            syntaxParser.Parse(identifier);
        }

        public void AddGherkinSyntaxSpanAt(int start, int length)
        {
            this.classificationSpans.Add(
                new ClassificationSpan(new SnapshotSpan(this.textSnapshot, this.snapshotPoint + start, length),
                                       this.classificationTypeRegistryService.GetClassificationType("GherkinSyntaxHighlighter")));
        }

        public void AddPascalCaseSpanAt(int start, int length)
        {
            this.classificationSpans.Add(
                new ClassificationSpan(new SnapshotSpan(this.textSnapshot, this.snapshotPoint + start, FirstCharacterWidth),
                                       this.classificationTypeRegistryService.GetClassificationType("PascalSyntaxHighlighter")));
        }
    }
}