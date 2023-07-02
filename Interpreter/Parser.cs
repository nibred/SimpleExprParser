using Interpreter.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter;

internal class Parser
{
    public string Evaluate(string input)
    {
        var expr = Scan(ref input);
        return expr?.Interpret().ToString() ?? "Wrong expression";
    }
    private Expression? Scan(ref string input)
    {
        Stack<Expression> expressionStack = new();
        Stack<char> operatorStack = new();
        int i = 0;
        while (i < input.Length)
        {
            char c = input[i];
            if (c is ' ' or '\t' or '\n' or '\r')
            {
                i++;
                continue;
            }
            if (c == '(')
            {
                operatorStack.Push(c);
            }
            if (char.IsDigit(c))
            {
                int value = c - '0';
                while (i + 1 < input.Length && char.IsDigit(input[i + 1])) //FIXME overflow
                {
                    value *= 10;
                    value += input[i + 1] - '0';
                    i++;
                }
                expressionStack.Push(new NumberExpression(value));
            }
            if (c is '+' or '-')
            {
                operatorStack.Push(c);
            }
            if (c == ')')
            {
                if (operatorStack.Count == 0)
                    return null;
                while (operatorStack.Count > 0 && operatorStack.Peek() != '(')
                {
                    char op = operatorStack.Pop();
                    if (expressionStack.TryPop(out Expression? right) && expressionStack.TryPop(out Expression? left))
                    {
                        Expression subExpression = CreateSubExpression(left, op, right);
                        expressionStack.Push(subExpression);
                    }
                    else
                        return null;
                }
                if (operatorStack.Count > 0 && operatorStack.Peek() == '(')
                    operatorStack.Pop();
            }
            i++;
        }

        while (operatorStack.Count > 0)
        {
            char op = operatorStack.Pop();
            if (expressionStack.TryPop(out Expression? right) && expressionStack.TryPop(out Expression? left))
            {
                Expression subExpression = CreateSubExpression(left, op, right);
                expressionStack.Push(subExpression);
            }
            else
                return null;
        }
        if (expressionStack.Count > 0 && operatorStack.Count == 0)
            return expressionStack.Pop();
        return null;
    }

    private Expression CreateSubExpression(Expression left, char op, Expression right) => op switch
    {
        '+' => new AdditionExpression(left, right),
        '-' => new SubtractionExpression(left, right),
        _ => throw new NotImplementedException()
    };
}