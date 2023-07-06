using ExprParser;

var parser = new Parser();
Console.Write("Enter expression: ");
string? input = Console.ReadLine();
Console.WriteLine($"Result: {parser.Evaluate(input)}");