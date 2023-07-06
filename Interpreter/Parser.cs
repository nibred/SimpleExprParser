using Interpreter.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter;

internal class Parser
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
        if (head.TokenId != TokenId.TOKEN_END)
        {
            throw new Exception("syntax error");
        }
        return result;
    }

    private Expression PrimaryExpression(ref Token? token)  
    {
        Expression? result = null;
        switch (token.TokenId)
        {
            case TokenId.TOKEN_LBRACKET:
                token = token.Next;
                result = MultiExpression(ref token);
                if (token.TokenId != TokenId.TOKEN_RBRACKET)
                    throw new Exception("missing ')'");
                token = token.Next;
                return result;
            case TokenId.TOKEN_NUMBER:
                result = new NumberExpression(token.number);
                token = token.Next;
                return result;
            default:
                throw new Exception("Syntax error in expression");
        }
    }

    private Expression AddExpression(ref Token? token)
    {
        Expression left = MultiExpression(ref token);
        while (token.TokenId == TokenId.TOKEN_PLUS || token.TokenId == TokenId.TOKEN_MINUS)
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
        while (token.TokenId == TokenId.TOKEN_MULTIPLY || token.TokenId == TokenId.TOKEN_SLASH)
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
