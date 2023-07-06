using Interpreter;

var parser = new Parser();
int result = parser.Evaluate("2+(510-3)+2");
Console.WriteLine(result);