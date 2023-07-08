namespace ExprParser;
internal class Lexer
{
    public TokenList GetTokens(string input)
    {
        TokenList list = new()
        {
            FirstToken = null,
            LastToken = null
        };
        int i = 0;
        while (i < input.Length)
        {
            if (char.IsWhiteSpace(input[i]))
            {
                i++;
                continue;
            }
            switch (input[i])
            {
                case '+':
                    EmitToken(ref list, TokenId.TOKEN_PLUS);
                    break;
                case '-':
                    EmitToken(ref list, TokenId.TOKEN_MINUS);
                    break;
                case '*':
                    EmitToken(ref list, TokenId.TOKEN_MULTIPLY);
                    break;
                case '/':
                    EmitToken(ref list, TokenId.TOKEN_SLASH);
                    break;
                case '%':
                    EmitToken(ref list, TokenId.TOKEN_PERCENT);
                    break;
                case '(':
                    EmitToken(ref list, TokenId.TOKEN_LBRACKET);
                    break;
                case ')':
                    EmitToken(ref list, TokenId.TOKEN_RBRACKET);
                    break;
                case '=':
                    EmitToken(ref list, TokenId.TOKEN_EQUAL);
                    break;
                case '!':
                    i++;
                    switch (input[i])
                    {
                        case '=':
                            EmitToken(ref list, TokenId.TOKEN_NOT_EQUAL);
                            break;
                        default:
                            throw new ArgumentException();
                    }
                    break;
                default:
                    if (char.IsDigit(input[i]))
                    {
                        int value = 0;
                        do
                        {
                            value *= 10;
                            value += input[i++] - '0';
                        } while (i < input.Length && char.IsDigit(input[i]));
                        EmitToken(ref list, TokenId.TOKEN_NUMBER, value);
                        continue;
                    }
                    throw new ArgumentException();
            }
            i++;
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
            tokenList.LastToken.Next = token;
        tokenList.LastToken = token;
    }
}