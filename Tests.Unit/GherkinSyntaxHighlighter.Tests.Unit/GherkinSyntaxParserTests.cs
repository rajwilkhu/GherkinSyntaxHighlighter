namespace GherkinSyntaxHighlighter.Tests.Unit
{
    using NSubstitute;

    using NUnit.Framework;

    using global::GherkinSyntaxHighlighter.Parser;

    // Examples from: http://dannorth.net/introducing-bdd/
    // TODO: Mock expectations could be replaced by an adapter with one expect method...
    [TestFixture]
    public class GivenAClassOrMethodIdentifier
    {
        [Test]
        public void WhenStringStartsWithGerkinSyntaxThenCallsObserverWithOneGivenAndMultiplePascalSpans()
        {
            var mockGherkinSyntaxParserObserver = Substitute.For<ISyntaxParserObserver>();
            var gherkinSyntaxParser = new SyntaxParser(mockGherkinSyntaxParserObserver);
            gherkinSyntaxParser.Parse("GivenTheAccountIsInCredit");
            mockGherkinSyntaxParserObserver.Received(1).AddGherkinSyntaxSpanAt(0, 5);   // Given
            mockGherkinSyntaxParserObserver.Received(1).AddPascalCaseSpanAt(5, 3);      // The
            mockGherkinSyntaxParserObserver.Received(1).AddPascalCaseSpanAt(8, 7);      // Account
            mockGherkinSyntaxParserObserver.Received(1).AddPascalCaseSpanAt(15, 2);     // Is
            mockGherkinSyntaxParserObserver.Received(1).AddPascalCaseSpanAt(17, 2);     // In
            mockGherkinSyntaxParserObserver.Received(1).AddPascalCaseSpanAt(19, 6);     // Credit
        }

        [Test]
        public void WhenStringStartsAndContainsGherkinSntaxThenCallsObserverWithOneGivenAndOneAndSpan()
        {
            var mockGherkinSyntaxParserObserver = Substitute.For<ISyntaxParserObserver>();
            var gherkinSyntaxParser = new SyntaxParser(mockGherkinSyntaxParserObserver);
            gherkinSyntaxParser.Parse("GivenTheAccountIsInCreditAndTheCardIsValid");
            mockGherkinSyntaxParserObserver.Received(1).AddGherkinSyntaxSpanAt(0, 5);   // Given
            mockGherkinSyntaxParserObserver.Received(1).AddPascalCaseSpanAt(5, 3);      // The
            mockGherkinSyntaxParserObserver.Received(1).AddPascalCaseSpanAt(8, 7);      // Account
            mockGherkinSyntaxParserObserver.Received(1).AddPascalCaseSpanAt(15, 2);     // Is
            mockGherkinSyntaxParserObserver.Received(1).AddPascalCaseSpanAt(17, 2);     // In
            mockGherkinSyntaxParserObserver.Received(1).AddPascalCaseSpanAt(19, 6);     // Credit
            mockGherkinSyntaxParserObserver.Received(1).AddGherkinSyntaxSpanAt(25, 3);  // And
            mockGherkinSyntaxParserObserver.Received(1).AddPascalCaseSpanAt(28, 3);     // The
            mockGherkinSyntaxParserObserver.Received(1).AddPascalCaseSpanAt(31, 4);     // Card
            mockGherkinSyntaxParserObserver.Received(1).AddPascalCaseSpanAt(35, 2);     // Is
            mockGherkinSyntaxParserObserver.Received(1).AddPascalCaseSpanAt(37, 5);     // Valid
        }

        [Test]
        public void WhenStringStartsAndContainsGherkinSntaxThenCallsObserverWithOneWhenAndOneAndSpan()
        {
            var mockGherkinSyntaxParserObserver = Substitute.For<ISyntaxParserObserver>();
            var gherkinSyntaxParser = new SyntaxParser(mockGherkinSyntaxParserObserver);
            gherkinSyntaxParser.Parse("WhenCustomerRequestsCashThenEnsureAccountIsDebitedAndEnsureCashDispensed");
            mockGherkinSyntaxParserObserver.Received(1).AddGherkinSyntaxSpanAt(0, 4);   // When
            mockGherkinSyntaxParserObserver.Received(1).AddPascalCaseSpanAt(4, 8);      // Customer
            mockGherkinSyntaxParserObserver.Received(1).AddPascalCaseSpanAt(12, 8);     // Requests
            mockGherkinSyntaxParserObserver.Received(1).AddPascalCaseSpanAt(20, 4);     // Cash
            mockGherkinSyntaxParserObserver.Received(1).AddGherkinSyntaxSpanAt(24, 4);  // Then
            mockGherkinSyntaxParserObserver.Received(1).AddPascalCaseSpanAt(28, 6);     // Ensure
            mockGherkinSyntaxParserObserver.Received(1).AddPascalCaseSpanAt(34, 7);     // Account
            mockGherkinSyntaxParserObserver.Received(1).AddPascalCaseSpanAt(41, 2);     // Is
            mockGherkinSyntaxParserObserver.Received(1).AddPascalCaseSpanAt(43, 7);     // Debited
            mockGherkinSyntaxParserObserver.Received(1).AddGherkinSyntaxSpanAt(50, 3);  // And
            mockGherkinSyntaxParserObserver.Received(1).AddPascalCaseSpanAt(53, 6);     // Ensure
            mockGherkinSyntaxParserObserver.Received(1).AddPascalCaseSpanAt(59, 4);     // Cash
            mockGherkinSyntaxParserObserver.Received(1).AddPascalCaseSpanAt(63, 9);     // Dispensed
        }
    }
}
