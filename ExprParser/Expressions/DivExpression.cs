namespace ExprParser.Expressions;

internal class DivExpression : Expression
{
    private Expression _leftExpression;
    private Expression _rightExpression;

    public DivExpression(Expression leftExpression, Expression rightExpression)
    {
        _leftExpression = leftExpression;
        _rightExpression = rightExpression;
    }
    public override int Evaluate()
    {
        if (_rightExpression.Evaluate() == 0)
        {
            throw new DivideByZeroException();
        }
        return _leftExpression.Evaluate() / _rightExpression.Evaluate();
    }
}
