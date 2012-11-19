namespace GherkinSyntaxHighlighter
{
    using System;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.Text;
    using Microsoft.VisualStudio.Text.Classification;

    public class GherkinSyntaxHighlighter : IClassifier
    {
        readonly IClassificationType classificationType;

        internal GherkinSyntaxHighlighter(IClassificationTypeRegistryService registry)
        {
            this.classificationType = registry.GetClassificationType("GherkinSyntaxHighlighter");
        }

        public IList<ClassificationSpan> GetClassificationSpans(SnapshotSpan snapshotSpan)
        {
            var classifications = new List<ClassificationSpan>
                                      {
                                          new ClassificationSpan(
                                              new SnapshotSpan(snapshotSpan.Snapshot, new Span(snapshotSpan.Start, snapshotSpan.Length)),
                                              this.classificationType)
                                      };
            return classifications;
        }

        public event EventHandler<ClassificationChangedEventArgs> ClassificationChanged;
    }
}
