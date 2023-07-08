namespace ExprParser.Expressions;

internal class AddExpression : Expression
{
    private Expression _leftExpression;
    private Expression _rightExpression;

    public AddExpression(Expression leftExpression, Expression rightExpression)
    {
        _leftExpression = leftExpression;
        _rightExpression = rightExpression;
    }
    public override int Evaluate() => _leftExpression.Evaluate() + _rightExpression.Evaluate();
}
