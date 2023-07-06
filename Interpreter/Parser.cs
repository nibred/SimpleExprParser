using ExprParser.Expressions;

namespace ExprParser;

public class Parser
{
    private readonly Lexer _lexer;
    public Parser()
    {
        _lexer = new Lexer();
    }
    public int Evaluate(string input)
    {
        var tokenList = _lexer.GetTokens(input);
        var head = tokenList.FirstToken;
        var result = AddExpression(ref head).Evaluate();
        if (head?.TokenId != TokenId.TOKEN_END)
        {
            throw new Exception("syntax error");
        }
        return result;
    }

    private Expression PrimaryExpression(ref Token? token)
    {
        Expression? result;
        switch (token?.TokenId)
        {
            case TokenId.TOKEN_LBRACKET:
                token = token.Next;
                result = AddExpression(ref token);
                if (token?.TokenId is not TokenId.TOKEN_RBRACKET)
                    throw new ArgumentException("missing ')'");
                token = token.Next;
                return result;
            case TokenId.TOKEN_NUMBER:
                result = new NumberExpression(token.number);
                token = token.Next;
                return result;
            default:
                throw new ArgumentException();
        }
    }

    private Expression AddExpression(ref Token? token)
    {
        Expression left = MultiExpression(ref token);
        while (token?.TokenId is TokenId.TOKEN_PLUS or TokenId.TOKEN_MINUS)
        {
            var id = token.TokenId;
            token = token.Next;
            Expression right = MultiExpression(ref token);
            switch (id)
            {
                case TokenId.TOKEN_PLUS:
                    left = new AdditionExpression(left, right);
                    break;
                case TokenId.TOKEN_MINUS:
                    left = new SubtractionExpression(left, right);
                    break;
            }
        }
        return left;
    }

    private Expression MultiExpression(ref Token? token)
    {
        Expression left = PrimaryExpression(ref token);
        while (token?.TokenId is TokenId.TOKEN_MULTIPLY or TokenId.TOKEN_SLASH)
        {
            var id = token.TokenId;
            token = token.Next;
            Expression right = PrimaryExpression(ref token);
            switch (id)
            {
                case TokenId.TOKEN_MULTIPLY:
                    left = new MultiplyExpression(left, right);
                    break;
                case TokenId.TOKEN_SLASH:
                    left = new DivideExpression(left, right);
                    break;
            }
        }
        return left;
    }
}
