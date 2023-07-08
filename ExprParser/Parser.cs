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
        var result = EqualExpr(ref head).Evaluate();
        if (head?.TokenId != TokenId.TOKEN_END)
        {
            throw new ArgumentException();
        }
        return result;
    }

    private Expression PrimaryExpr(ref Token? token)
    {
        Expression? result;
        switch (token?.TokenId)
        {
            case TokenId.TOKEN_LBRACKET:
                token = token.Next;
                result = AddExpr(ref token);
                if (token?.TokenId is not TokenId.TOKEN_RBRACKET)
                    throw new ArgumentException("missing ')'");
                token = token.Next;
                return result;
            case TokenId.TOKEN_NUMBER:
                result = new NumExpression(token.number);
                token = token.Next;
                return result;
            default:
                throw new ArgumentException();
        }
    }

    private Expression AddExpr(ref Token? token)
    {
        Expression left = MultiExpr(ref token);
        while (token?.TokenId is TokenId.TOKEN_PLUS or TokenId.TOKEN_MINUS)
        {
            var id = token.TokenId;
            token = token.Next;
            Expression right = MultiExpr(ref token);
            switch (id)
            {
                case TokenId.TOKEN_PLUS:
                    left = new AddExpression(left, right);
                    break;
                case TokenId.TOKEN_MINUS:
                    left = new SubExpression(left, right);
                    break;
            }
        }
        return left;
    }

    private Expression MultiExpr(ref Token? token)
    {
        Expression left = PrimaryExpr(ref token);
        while (token?.TokenId is TokenId.TOKEN_MULTIPLY 
            or TokenId.TOKEN_SLASH 
            or TokenId.TOKEN_PERCENT)
        {
            var id = token.TokenId;
            token = token.Next;
            Expression right = PrimaryExpr(ref token);
            switch (id)
            {
                case TokenId.TOKEN_MULTIPLY:
                    left = new MultExpression(left, right);
                    break;
                case TokenId.TOKEN_SLASH:
                    left = new DivExpression(left, right);
                    break;
                case TokenId.TOKEN_PERCENT:
                    left = new RemExpression(left, right);
                    break;
            }
        }
        return left;
    }

    private Expression EqualExpr(ref Token? token)
    {
        Expression left = AddExpr(ref token);
        while (token?.TokenId is TokenId.TOKEN_EQUAL or TokenId.TOKEN_NOT_EQUAL)
        {
            var id = token.TokenId;
            token = token.Next;
            Expression right = AddExpr(ref token);
            switch (id)
            {
                case TokenId.TOKEN_EQUAL:
                    left = new EqualExpression(left, right);
                    break;
                case TokenId.TOKEN_NOT_EQUAL:
                    left = new NotEqualExpression(left, right);
                    break;
            }
        }
        return left;
    }
}
