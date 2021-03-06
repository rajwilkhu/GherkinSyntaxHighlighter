﻿namespace GherkinSyntaxHighlighter
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.VisualStudio.Text;
    using Microsoft.VisualStudio.Text.Classification;

    using global::GherkinSyntaxHighlighter.Parser;

    public class GherkinSyntaxHighlighter : IClassifier
    {
        private readonly IClassifier classifier;

        private readonly IClassificationTypeRegistryService classificationTypeRegistryService;

        private readonly ILanguagePatternMatcher languagePatternMatcher;

        private static readonly IList<ClassificationSpan> EmptyListOfClassifications = new List<ClassificationSpan>();

        internal GherkinSyntaxHighlighter(
            IClassifier classifier,
            IClassificationTypeRegistryService classificationTypeRegistryService,
            ILanguagePatternMatcher languagePatternMatcher)
        {
            this.classifier = classifier;
            this.classificationTypeRegistryService = classificationTypeRegistryService;
            this.languagePatternMatcher = languagePatternMatcher;
        }

        public IList<ClassificationSpan> GetClassificationSpans(SnapshotSpan trackingSpan)
        {
            var classificationSpans = this.classifier.GetClassificationSpans(trackingSpan);
            return !this.ShouldProcessBasedOnLanguagePattern(classificationSpans)
                    ? EmptyListOfClassifications
                    : (!this.ShouldProcessBasedOnCheckingSpanPattern(classificationSpans)
                      ? EmptyListOfClassifications
                       : this.CreateClassificationSpans(classificationSpans[2].Span));

        }

        public event EventHandler<ClassificationChangedEventArgs> ClassificationChanged;

        private IList<ClassificationSpan> CreateClassificationSpans(SnapshotSpan trackingSpan)
        {
            var gherkinClassificationSpans = new GherkinClassificationSpans(
                this.classificationTypeRegistryService, trackingSpan.Snapshot, trackingSpan.Start);
            gherkinClassificationSpans.Parse(trackingSpan.GetText());
            return gherkinClassificationSpans.ClassificationSpans.ToList();
        }

        private bool ShouldProcessBasedOnCheckingSpanPattern(IList<ClassificationSpan> classificationSpans)
        {
            var spanText = classificationSpans[2].Span.GetText();
            return this.languagePatternMatcher.ShouldProcess(spanText);
        }

        private bool ShouldProcessBasedOnLanguagePattern(IList<ClassificationSpan> classificationSpans)
        {
            return this.languagePatternMatcher.ShouldProcess(classificationSpans);
        }
    }
}
