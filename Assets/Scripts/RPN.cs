using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using UnityEngine;

public static class RPN
{
    public static List<string> GetElementsFromString(string input)
    {
        string trimmedInput = input.Trim();
        string operators = "+-*/%";
        List<string> elements = new List<string>();
        StringBuilder currentToken = new StringBuilder();
        string lastElement = null;
        bool pendingCloseParenthesis = false;

        for (int i = 0; i < trimmedInput.Length; i++)
        {
            char symbol = trimmedInput[i];

            if (char.IsWhiteSpace(symbol))
            {
                continue;
            }

            if (symbol == '-' && (lastElement == null || operators.Contains(lastElement)))
            {
                elements.Add("(");
                elements.Add("0");
                elements.Add("-");
                lastElement = "-";
                pendingCloseParenthesis = true;
            }
            else if (operators.Contains(symbol))
            {
                if (currentToken.Length > 0)
                {
                    elements.Add(currentToken.ToString());
                    if (pendingCloseParenthesis)
                    {
                        elements.Add(")");
                        pendingCloseParenthesis = false;
                    }
                    currentToken.Clear();
                }
                elements.Add(symbol.ToString());
                lastElement = symbol.ToString();
            }
            else
            {
                currentToken.Append(symbol);
                lastElement = currentToken.ToString();
            }
        }

        if (currentToken.Length > 0)
        {
            elements.Add(currentToken.ToString());
        }

        if (pendingCloseParenthesis)
        {
            elements.Add(")");
        }

        return elements;
    }


    public static List<string> TransferArithmeticPrefixToPostfix(List<string> elements)
    {
        var postfix = new List<string>();
        var stack = new Stack<string>();

        var priority = new Dictionary<string, int>()
    {
        { "%", 2 },
        { "/", 2 },
        { "*", 2 },
        { "+", 1 },
        { "-", 1 },
        { "(", 0 }
    };

        string operators = "+-*%/";

        foreach (var element in elements)
        {
            if (operators.Contains(element))
            {
                while (stack.Count > 0 && stack.Peek() != "(" &&
                       priority[element] <= priority[stack.Peek()])
                {
                    postfix.Add(stack.Pop());
                }
                stack.Push(element);
            }
            else if (element == "(")
            {
                stack.Push(element);
            }
            else if (element == ")")
            {
                bool foundLeftParenthesis = false;

                while (stack.Count > 0)
                {
                    string top = stack.Pop();
                    if (top == "(")
                    {
                        foundLeftParenthesis = true;
                        break;
                    }
                    postfix.Add(top);
                }

                if (!foundLeftParenthesis)
                    return new List<string>();
            }
            else
            {
                if (int.TryParse(element, out _))
                {
                    postfix.Add(element);
                }
                else
                {
                    return new List<string>();
                }
            }
        }

        while (stack.Count > 0)
        {
            string op = stack.Pop();
            if (op == "(")
                return new List<string>();
            postfix.Add(op);
        }

        return postfix;
    }
    public static int CalculationPostfix(List<string> postfix)
    {
        Stack<int> stack = new Stack<int>();
        string operators = "+-*/%";

        foreach (string element in postfix)
        {
            if (operators.Contains(element))
            {
                if (stack.Count < 2)
                {
                    return int.MaxValue;
                }

                int first = stack.Pop();
                int second = stack.Pop();
                if ((element == "/" || element == "%") && first == 0)
                {
                    return int.MaxValue;
                }

                int result;
                switch (element)
                {
                    case "-":
                        result = second - first;
                        break;
                    case "+":
                        result = second + first;
                        break;
                    case "*":
                        result = second * first;
                        break;
                    case "%":
                        result = second % first;
                        break;
                    case "/":
                        result = second / first;
                        break;
                    default:
                        return int.MaxValue;
                }
                stack.Push(result);
            }
            else
            {
                if (!int.TryParse(element, out int value))
                {
                    return int.MaxValue;
                }
                stack.Push(value);
            }
        }

        if (stack.Count == 0)
        {
            return int.MaxValue;
        }

        if (stack.Count > 1)
        {
            return int.MaxValue;
        }

        return stack.Pop();
    }


    // ������� int.MaxValue � ������ ������
    public static int CalculationArithmetic(String input)
    {
        List<String> tokens = TransferArithmeticPrefixToPostfix(GetElementsFromString(input));
        if (tokens.Count == 0) return int.MaxValue;
        return CalculationPostfix(tokens);
    }
}
    