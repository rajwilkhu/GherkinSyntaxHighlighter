namespace GherkinSyntaxHighlighter
{
    using System.Collections.Generic;

    using Microsoft.VisualStudio.Text;

    using global::GherkinSyntaxHighlighter.Parser;

    internal class GherkinClassificationSpans : ISyntaxParserObserver
    {
        private readonly ITextSnapshot textSnapshot;

        private readonly SnapshotPoint snapshotPoint;

        private readonly List<SnapshotSpan> snapshotSpans = new List<SnapshotSpan>();

        public IEnumerable<SnapshotSpan> SnapshotSpans 
        { 
            get
            {
                return this.snapshotSpans;
            } 
        }
        
        public GherkinClassificationSpans(ITextSnapshot textSnapshot, SnapshotPoint snapshotPoint)
        {
            this.textSnapshot = textSnapshot;
            this.snapshotPoint = snapshotPoint;
        }

        public void Parse(string identifier)
        {
            snapshotSpans.Clear();
            var syntaxParser = new SyntaxParser(this);
            syntaxParser.Parse(identifier);
        }

        public void AddGherkinSyntaxSpanAt(int start, int length)
        {
            this.snapshotSpans.Add(new SnapshotSpan(this.textSnapshot, this.snapshotPoint + start, length));
        }

        public void AddPascalCaseSpanAt(int start, int length)
        {
        }
    }
}