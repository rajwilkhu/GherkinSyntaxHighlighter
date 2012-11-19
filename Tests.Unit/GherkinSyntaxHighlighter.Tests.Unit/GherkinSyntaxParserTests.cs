namespace GherkinSyntaxHighlighter.Tests.Unit
{
    using System.Text.RegularExpressions;

    using NSubstitute;

    using NUnit.Framework;

    [TestFixture]
    public class GivenAClassOrMethodIdentifier
    {
        [Test]
        public void WhenStringStartsWithGerkinSyntaxThenCallsObserverWithSpans()
        {
            var mockGherkinSyntaxParserObserver = Substitute.For<ISyntaxParserObserver>();
            var gherkinSyntaxParser = new SyntaxParser(mockGherkinSyntaxParserObserver);
            gherkinSyntaxParser.Parse("GivenSomeBankAccount");
            mockGherkinSyntaxParserObserver.Received(1).AddGherkinSyntaxSpanAt(0, 5);   // Given
            mockGherkinSyntaxParserObserver.Received(1).AddPascalCaseSpanAt(5, 4);      // Some
            mockGherkinSyntaxParserObserver.Received(1).AddPascalCaseSpanAt(9, 4);      // Bank
            mockGherkinSyntaxParserObserver.Received(1).AddPascalCaseSpanAt(13, 7);     // Account
        }
    }

    public interface ISyntaxParserObserver
    {
        void AddGherkinSyntaxSpanAt(int start, int length);

        void AddPascalCaseSpanAt(int start, int length);
    }

    public class SyntaxParser
    {
        private readonly ISyntaxParserObserver syntaxParserObserver;
        private readonly Regex regex = new Regex(@"(?<!__)([A-Z][a-z]+|(?<=[a-z])[A-Z]+)", RegexOptions.Compiled);

        public SyntaxParser(ISyntaxParserObserver syntaxParserObserver)
        {
            this.syntaxParserObserver = syntaxParserObserver;
        }

        public void Parse(string identifier)
        {
            var matches = regex.Matches(identifier);
            for(var matchIndex = 0; matchIndex < matches.Count; ++matchIndex)
            {
                var captureCollection = matches[matchIndex].Captures;
                for (var captureIndex = 0; captureIndex < captureCollection.Count; ++captureIndex)
                {
                    var capture = captureCollection[captureIndex];
                    var index = captureCollection[captureIndex].Index;
                    var length = captureCollection[captureIndex].Length;
                    if (index == 0)
                    {
                        syntaxParserObserver.AddGherkinSyntaxSpanAt(index, length);
                    }
                    else
                    {
                        syntaxParserObserver.AddPascalCaseSpanAt(index, length);   
                    }
                }
            }
        }
    }
}
