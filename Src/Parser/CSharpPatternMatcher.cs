namespace GherkinSyntaxHighlighter.Parser
{
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;

    using Microsoft.VisualStudio.Text.Classification;
    using Microsoft.VisualStudio.Utilities;

    public interface ILanguagePatternMatcher
    {
        bool ShouldProcess(IList<ClassificationSpan> classificationSpans);

        bool ShouldProcess(string spanText);
    }

    // TODO: Find a way to unit test this class by creating mock/fake classification spans
    // Null exception thrown if I try to fake the classification spans here..
    public class CSharpPatternMatcher : ILanguagePatternMatcher
    {
        private const int MinimalClassOrMethodLength = 4;

        private static readonly string[] ClassPattern = new[] { "keyword", "keyword", "User Types" };
        private static readonly string[] MethodPattern = new[] { "keyword", "keyword", "identifier" };

        [Export(typeof(ClassificationTypeDefinition))]
        [Name("GivenWhenThenHighlighter")]
        internal static ClassificationTypeDefinition ClassificationType { get; set; }

        public bool ShouldProcess(IList<ClassificationSpan> classificationSpans)
        {
            return classificationSpans != null
                   && (classificationSpans.Count == ClassPattern.Length
                       && (IsClassOrMethod(classificationSpans)
                           && IsMinimalClassOrMethodLength(classificationSpans)));
        }

        public bool ShouldProcess(string spanText)
        {
            return spanText.Length != 0 && (spanText[0] == 'G' || spanText[0] == 'W');
        }

        private static bool IsClassOrMethod(IList<ClassificationSpan> classificationSpans)
        {
            return SpansDoNotMatchClassDeclaration(classificationSpans) || SpansDoNotMatchMethodDeclaration(classificationSpans);
        }

        private static bool IsMinimalClassOrMethodLength(IList<ClassificationSpan> classificationSpans)
        {
            return classificationSpans[2].Span.Length >= MinimalClassOrMethodLength;
        }

        private static bool SpansDoNotMatchClassDeclaration(IList<ClassificationSpan> classificationSpans)
        {
            return !ClassPattern.Where(
                (t, idx) => string.CompareOrdinal(classificationSpans[idx].ClassificationType.Classification, t) > 0).Any();
        }

        private static bool SpansDoNotMatchMethodDeclaration(IList<ClassificationSpan> classificationSpans)
        {
            return !MethodPattern.Where(
                (t, idx) => string.CompareOrdinal(classificationSpans[idx].ClassificationType.Classification, t) > 0).Any();
        }
    }
}
