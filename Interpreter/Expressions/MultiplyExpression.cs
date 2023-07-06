namespace Interpreter.Expressions;

internal class MultiplyExpression : Expression
{
    private Expression _leftExpression;
    private Expression _rightExpression;

    public MultiplyExpression(Expression leftExpression, Expression rightExpression)
    {
        _leftExpression = leftExpression;
        _rightExpression = rightExpression;
    }
    public override int Evaluate() => _leftExpression.Evaluate() * _rightExpression.Evaluate();
}
