namespace ExprParser.Expressions;

internal class EqualExpression : Expression
{
    private Expression _leftExpression;
    private Expression _rightExpression;

    public EqualExpression(Expression leftExpression, Expression rightExpression)
    {
        _leftExpression = leftExpression;
        _rightExpression = rightExpression;
    }
    public override int Evaluate() => Convert.ToInt32(_leftExpression.Evaluate() == _rightExpression.Evaluate());
}
