namespace Interpreter.Expressions;

internal class NumberExpression : Expression
{
    private int _number;
    public NumberExpression(int number) => _number = number;
    public override int Evaluate() => _number;
}
