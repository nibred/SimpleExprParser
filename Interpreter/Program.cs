using Interpreter;

var parser = new Parser();
int result = parser.Evaluate("2+3*6/2");
Console.WriteLine(result);