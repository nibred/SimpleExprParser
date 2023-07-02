using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.Expressions;

internal class NumberExpression : Expression
{
    private int _number;
    public NumberExpression(int number) => _number = number;
    public override int Interpret() => _number;
}
