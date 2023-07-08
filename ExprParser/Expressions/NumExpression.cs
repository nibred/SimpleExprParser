namespace ExprParser.Expressions;

internal class NumExpression : Expression
{
    private int _number;
    public NumExpression(int number) => _number = number;
    public override int Evaluate() => _number;
}
