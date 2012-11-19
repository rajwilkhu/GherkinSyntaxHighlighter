namespace GherkinSyntaxHighlighter.Parser
{
    public interface ISyntaxParserObserver
    {
        void AddGherkinSyntaxSpanAt(int start, int length);

        void AddPascalCaseSpanAt(int start, int length);
    }
}