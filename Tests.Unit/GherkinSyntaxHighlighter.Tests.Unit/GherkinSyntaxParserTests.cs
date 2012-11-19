namespace GherkinSyntaxHighlighter.Tests.Unit
{
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
            gherkinSyntaxParser.Parse("GivenABankAccount");
            mockGherkinSyntaxParserObserver.Received(1).AddGherkinSyntaxSpanAt(0, 5);   // Given
            mockGherkinSyntaxParserObserver.Received(1).AddPascalCaseSpanAt(5, 1);      // A
            mockGherkinSyntaxParserObserver.Received(1).AddPascalCaseSpanAt(6, 4);      // Bank
            mockGherkinSyntaxParserObserver.Received(1).AddPascalCaseSpanAt(10, 7);     // Account
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

        public SyntaxParser(ISyntaxParserObserver syntaxParserObserver)
        {
            this.syntaxParserObserver = syntaxParserObserver;
        }

        public void Parse(string identifier)
        {
           
        }
    }
}
