namespace ExprParser;

internal record Token
{
    public Token? Next;
    public TokenId TokenId;
    public int number;
}
enum TokenId
{
    TOKEN_NUMBER,
    TOKEN_PLUS,
    TOKEN_MINUS,
    TOKEN_MULTIPLY,
    TOKEN_SLASH,
    TOKEN_PERCENT,
    TOKEN_LBRACKET,
    TOKEN_RBRACKET,
    TOKEN_EQUAL,
    TOKEN_NOT_EQUAL,
    TOKEN_END
}

internal record TokenList
{
    public Token? FirstToken;
    public Token? LastToken;
}