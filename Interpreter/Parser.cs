using Interpreter.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter;

internal class Parser
{
    public int Parse(string input)
    {
        var expr = Scan(ref input);
        return expr?.Interpret() ?? throw new Exception("Wrong expression");
    }
    private Expression? Scan(ref string input)
    {
        Stack<Expression> expressionStack = new();
        Stack<char> operatorStack = new();
        int i = 0;
        while (i < input.Length)
        {
            char c = input[i];
            if (c == ' ' || c == '\t' || c == '\n' || c == '\r')
            {
                i++;
                continue;
            }
            if (c == '(')
            {
                i++;
                operatorStack.Push(c);
            }
            if (char.IsDigit(c))
            {
                int value = c - '0';
                while (i + 1 < input.Length && char.IsDigit(input[i + 1]))
                {
                    value *= 10;
                    value += input[i + 1] - '0';
                }
                expressionStack.Push(new NumberExpression(value));
            }
        }
        return expressionStack.Count > 0 ? expressionStack.Pop() : null;
    }
}