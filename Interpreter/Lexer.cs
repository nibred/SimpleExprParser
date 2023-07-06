namespace ExprParser;

internal class Lexer
{
    public TokenList GetTokens(string input)
    {
        TokenList list = new()
        {
            FirstToken = null,
            SecondToken = null
        };
        int i = 0;
        while (i < input.Length)
        {
            switch (input[i])
            {
                case ' ':
                case '\t':
                case '\n':
                case '\r':
                    i++;
                    continue;
                case '+':
                    EmitToken(ref list, TokenId.TOKEN_PLUS);
                    i++;
                    continue;
                case '-':
                    EmitToken(ref list, TokenId.TOKEN_MINUS);
                    i++;
                    continue;
                case '*':
                    EmitToken(ref list, TokenId.TOKEN_MULTIPLY);
                    i++;
                    continue;
                case '/':
                    EmitToken(ref list, TokenId.TOKEN_SLASH);
                    i++;
                    continue;
                case '(':
                    EmitToken(ref list, TokenId.TOKEN_LBRACKET);
                    i++;
                    continue;
                case ')':
                    EmitToken(ref list, TokenId.TOKEN_RBRACKET);
                    i++;
                    continue;
                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                    int value = 0;
                    do
                    {
                        value *= 10;
                        value += input[i++] - '0';
                    } while (i < input.Length && char.IsDigit(input[i]));
                    EmitToken(ref list, TokenId.TOKEN_NUMBER, value);
                    continue;
                default:
                    throw new ArgumentException();
            }
        }
        EmitToken(ref list, TokenId.TOKEN_END);
        return list;
    }
    private void EmitToken(ref TokenList tokenList, TokenId tokenId, int number = 0)
    {
        Token token = new()
        {
            Next = null,
            TokenId = tokenId,
            number = number
        };
        if (tokenList.FirstToken is null)
            tokenList.FirstToken = token;
        else
            tokenList.SecondToken.Next = token;
        tokenList.SecondToken = token;
    }
}