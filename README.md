## Simple math parser
The Math Parser is a simple C# library that allows you to evaluate mathematical expressions containing basic operators (+, -, *, /) and parentheses.
### Features

- Supports addition (+), subtraction (-), multiplication (*), and division (/) operations.
- Handles expressions with multiple levels of parentheses.
- Follows the standard operator precedence rules (multiplication/division before addition/subtraction).
- Provides error handling for invalid expressions or divide by zero scenarios.
### Usage

Using the Math Parser in your C# code is straightforward. Here's an example:

```cs
using ExprParser;

string expression = "24+8-(2*(12-6)-3)+14/2-(13+2*5)";

try
{
    Parser parser = new Parser();
    int result = parser.Evaluate(expression);
    Console.WriteLine("Result: " + result);
}
catch (Exception ex)
{
    Console.WriteLine("Error: " + ex.Message);
}
```
### Error Handling

The Math Parser provides robust error handling for various scenarios, including:

- **Argument Exception**: If the input expression contains syntax errors or unsupported characters, a parser will be thrown with the appropriate error message.
- **DivideByZero Exception**: If the expression involves division by zero, a parser will be thrown with an error message indicating the divide by zero scenario.

Ensure that you wrap the evaluation code in a try-catch block to handle exceptions gracefully.

### Limitations

- The parser currently supports only basic mathematical operations (+, -, *, /) and parentheses. It does not support advanced functions, trigonometry, logarithms, etc.
- The parser assumes correct syntax and does not perform extensive validation or error recovery.
