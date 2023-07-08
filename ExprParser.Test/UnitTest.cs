namespace ExprParser.Test
{
    public class UnitTest
    {
        [Theory]
        [InlineData("2 +   2", 2 + 2)]
        [InlineData("2*  2", 2 * 2)]
        [InlineData("2/  3", 2 / 3)]
        [InlineData("2 + 2   *2", 2 + 2 * 2)]
        [InlineData("2 +( 510  -  3)", 2 + (510 - 3))]
        [InlineData("  12- \t32", 12 - 32)]
        [InlineData("(5 )", 5)]
        [InlineData("(14+\r(7*6%2)   )\t%3-7", (14 + (7 * 6 % 2)) % 3 - 7)]
        [InlineData("(5 %2)", 5 % 2)]
        [InlineData("231 % 23", 231 % 23)]
        [InlineData("((3)+(2)-(5))", 3 + 2 - 5)]
        [InlineData("4+43*(34   -3*4)-5+7-(4\t*3+(4-  2)  -6/3)", 4 + (43 * (34 - (3 * 4))) - 5 + 7 - ((4 * 3) + (4 - 2) - (6 / 3)))]
        [InlineData("24+8-(2*(12-6)-3)+14/2-(13+2*5)", 24 + 8 - (2 * (12 - 6) - 3) + 14 / 2 - (13 + 2 * 5))]
        public void TestEqual(string value, int result)
        {
            var parser = new Parser();
            var eval = parser.Evaluate(value);
            Assert.Equal(result, eval);
        }

        [Theory]
        [InlineData("2 +   2 = 5 -1")]
        [InlineData("2*  2 = 2+2")]
        [InlineData("2/  3 = 2 / 3")]
        [InlineData("1 =        1")]
        [InlineData("(1) = (1)")]
        [InlineData("5= (2 +2*1 +1   )")]
        [InlineData("(2+(4*2+1))*2 = 2*  (  2 +(4 * 2+ 1 ))")]
        [InlineData("2 !=3")]
        [InlineData("2 !=(3-(7* 2))")]
        [InlineData("4!= ( 5-7)")]
        public void TestEqual_ReturnTrue(string value)
        {
            var parser = new Parser();
            var eval = parser.Evaluate(value);
            Assert.True(Convert.ToBoolean(eval));
        }

        [Theory]
        [InlineData("2 +   2 = 5 ")]
        [InlineData("2*  2 = 2+3")]
        [InlineData("2/  3 = 2 - 3")]
        [InlineData("1 =        0")]
        [InlineData("1 != ( 3-2)")]
        [InlineData("(2+(4*2+1))*2 = 2*  ( (4 * 2+ 1 ))")]
        public void TestEqual_ReturnFalse(string value)
        {
            var parser = new Parser();
            var eval = parser.Evaluate(value);
            Assert.False(Convert.ToBoolean(eval));
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
        [InlineData("()")]
        [InlineData("5==1")]
        [InlineData("5=3=-1")]
        [InlineData("=1")]
        [InlineData("5===1")]
        [InlineData("5=(1")]
        [InlineData("5=()")]
        [InlineData("5=(1)=")]
        [InlineData("2+(3+4))")]
        [InlineData("(42-7)*4-(9+2")]
        [InlineData("5! =")]
        [InlineData("5!=")]
        [InlineData("5=!=")]
        [InlineData("5!==")]
        [InlineData("5!(=")]
        [InlineData("5%")]
        [InlineData("%")]
        [InlineData("(%)")]
        public void TestException(string value)
        {
            var parser = new Parser();
            Assert.Throws<ArgumentException>(() => parser.Evaluate(value));
        }

        [Theory]
        [InlineData("3/0")]
        [InlineData("3%0")]
        [InlineData("3/(5-(2+3))")]
        [InlineData("3 %(5-(2+3))")]
        [InlineData("0/0")]
        [InlineData("0 %0")]
        public void TestDivideByZeroException(string value)
        {
            var parser = new Parser();
            Assert.Throws<DivideByZeroException>(() => parser.Evaluate(value));
        }
    }
}