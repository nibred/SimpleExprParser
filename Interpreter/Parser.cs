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
        var result = _lexer.GetTokens(input);
        var head = result.FirstToken;
        return AddExpression(ref head).Evaluate();
    }

    private Expression PrimaryExpression(ref Token? token)  
    {
        Expression? result = null;
        switch (token.TokenId)
        {
            case TokenId.TOKEN_NUMBER:
                result = new NumberExpression(token.number);
                token = token.Next;
                return result;
            default:
                throw new Exception("Syntax error in expression");
        }
    }

    private Expression AddExpression(ref Token token)
    {
        Expression left = PrimaryExpression(ref token);
        while (token.TokenId == TokenId.TOKEN_PLUS || token.TokenId == TokenId.TOKEN_MINUS)
        {
            var id = token.TokenId;
            token = token.Next;
            Expression right = PrimaryExpression(ref token);
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
}

enum TokenId
{
    TOKEN_NUMBER,
    TOKEN_PLUS,
    TOKEN_MINUS,
    TOKEN_MUL,
    TOKEN_END
}

internal record Token
{
    public Token? Next;
    public TokenId TokenId;
    public int number;
}

internal record TokenList
{
    public Token? FirstToken;
    public Token? SecondToken;
}

internal class Lexer
{
    public TokenList GetTokens(string input)
    {
        TokenList list = new();
        list.FirstToken = null;
        list.SecondToken = null;
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
                    EmitToken(ref list, TokenId.TOKEN_MUL);
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
        Token token = new();
        token.Next = null;
        token.TokenId = tokenId;
        token.number = number;
        if (tokenList.FirstToken is null)
            tokenList.FirstToken = token;
        else
            tokenList.SecondToken.Next = token;
        tokenList.SecondToken = token;
    }
}