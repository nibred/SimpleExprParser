using Interpreter;

var parser = new Parser();
string result = parser.Evaluate("2+(7-(2+5)");
Console.WriteLine(result);