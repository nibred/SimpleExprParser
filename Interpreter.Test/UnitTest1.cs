namespace Interpreter.Test
{
    public class UnitTest1
    {
        [Theory]
        [InlineData("2 +   2", 2+2)]
        [InlineData("2*  2", 2*2)]
        [InlineData("2 + 2   *2", 2 + 2 * 2)]
        [InlineData("2 +( 510  -  3)", 2 + (510 - 3))]
        [InlineData("  12-  32", 12 - 32)]
        [InlineData("4+43*(34   -3*4)-5+7-(4\t*3+(4-  2)  -6/3)", 4 + 43 * (34 - 3 * 4) - 5 + 7 - (4 * 3 + (4 - 2) - 6 / 3))]
        public void TestEqual(string value, int result)
        {
            var parser = new Parser();
            var eval = parser.Evaluate(value);
            Assert.Equal(result, eval);
        }

        [Theory]
        [InlineData("2 +   ")]
        [InlineData("*  2")]
        [InlineData("2 + *2")]
        [InlineData("2 +( 510  -  3")]
        [InlineData("  -12-  3")]
        [InlineData("\t-12")]
        [InlineData("45+)")]
        [InlineData("45+ (23--9)")]
        [InlineData("4//   2")]
        [InlineData("(42-7)*4-(9+2")]
        public void TestException(string value)
        {
            var parser = new Parser();
            Assert.Throws<Exception>(() => parser.Evaluate(value));
        }

        [Theory]
        [InlineData("3/0")]
        [InlineData("3/(5-(2+3))")]
        public void TestDivideByZeroException(string value)
        {
            var parser = new Parser();
            Assert.Throws<DivideByZeroException>(() => parser.Evaluate(value));
        }
    }
}