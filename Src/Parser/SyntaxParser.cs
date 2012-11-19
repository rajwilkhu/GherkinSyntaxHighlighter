namespace GherkinSyntaxHighlighter.Parser
{
    using System.Linq;
    using System.Text.RegularExpressions;

    public interface ISyntaxParser
    {
        void Parse(string identifier);
    }

    public class SyntaxParser : ISyntaxParser
    {
        private readonly ISyntaxParserObserver syntaxParserObserver;
        private readonly Regex regex = new Regex(@"(?<!__)([A-Z][a-z]+|(?<=[a-z])[A-Z]+)", RegexOptions.Compiled);

        private readonly string[] gherkinLanguageKeywords = new[] { "Given", "When", "Then", "And", "But" };
 
        public SyntaxParser(ISyntaxParserObserver syntaxParserObserver)
        {
            this.syntaxParserObserver = syntaxParserObserver;
        }

        public void Parse(string identifier)
        {
            var matches = this.regex.Matches(identifier);
            for(var matchIndex = 0; matchIndex < matches.Count; ++matchIndex)
            {
                var captureCollection = matches[matchIndex].Captures;
                for (var captureIndex = 0; captureIndex < captureCollection.Count; ++captureIndex)
                {
                    this.EvaluateCaptureAndInformObserver(captureCollection, captureIndex);
                }
            }
        }

        private void EvaluateCaptureAndInformObserver(CaptureCollection captureCollection, int captureIndex)
        {
            var capture = captureCollection[captureIndex];
            var index = captureCollection[captureIndex].Index;
            var length = captureCollection[captureIndex].Length;
            if (index == 0)
            {
                this.syntaxParserObserver.AddGherkinSyntaxSpanAt(index, length);
            }
            else
            {
                this.EvaluateCaptureAfterFirstGroup(capture, index, length);
            }
        }

        private void EvaluateCaptureAfterFirstGroup(Capture capture, int index, int length)
        {
            var isGherkinKeyword =
                this.gherkinLanguageKeywords.Any(gherkinKeyword => string.CompareOrdinal(gherkinKeyword, capture.Value) == 0);
            if (isGherkinKeyword)
            {
                this.syntaxParserObserver.AddGherkinSyntaxSpanAt(index, length);
            }
            else
            {
                this.syntaxParserObserver.AddPascalCaseSpanAt(index, length);
            }
        }
    }
}