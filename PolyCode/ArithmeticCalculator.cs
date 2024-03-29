﻿//MP2 Calculator 
//This file contains the ArithmethicCalculator class.

//You should implement the BasicArithmetic method.

using System;
using System.Collections.Generic;
using System.Text;

namespace MP2
{
    public class ArithmethicCalculator
    {
        /// <summary>
        /// Prompts the user for an arithmetic expression in the simplified
        /// Reverse Polish Notation, and returns a string that contains
        /// the arithmetic expression (in normal notation with parenthesis) 
        /// and the result.
        /// If the expression provided by the user is not correct, simply returns
        /// "Invalid expression".
        /// </summary>
        /// <returns>
        /// Returns the string that contains the arithmetic expression and the result,
        /// or the requested error message. 
        /// </returns>
        /// <example>
        /// If the user enters "2 3 +" then the method returns "2 + 3 = 5".
        /// If the user enters "4 5 + 6 * 8 / 2 ^" then the method returns:
        /// (((4 + 5) * 6) / 8 ) ^ 2 = 45.5625
        /// Extra spaces are fine, so if the user enters " 2   3    ^" then 
        /// the method returns "2 ^ 3 = 8".
        /// If the user enters "4 5" or "4 +" or any incorrect or unbalanced 
        /// expression, then the method returns "Invalid expression".
        /// </example>
        public static string BasicArithmetic()
        {

            Console.WriteLine();
            Console.WriteLine("Enter an expression (Reverse Polish Notation)");
            string expression = Console.ReadLine().Trim();
            string[] elements = expression.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            double answer;
            int lengthString = elements.Length;

            List<double> numbersList = new List<double>();
            List<char> operandsList = new List<char>();
            double number;

            StringBuilder arithmeticExpression = new StringBuilder();

            if (lengthString == 0)
            {
                return "Invalid Expression.";
            }
            else if (!double.TryParse(elements[0], out double errorTestingNumber) )
            {
                return "Invalid Expression.";
            }
            else if (double.TryParse(elements[lengthString - 1], out errorTestingNumber) && lengthString > 1)
            {
                return "Invalid Expression.";
            }
            else if (lengthString % 2 != 1)
            {
                return "Invalid Expression.";
            }
            else
            {
                for (int j = 0; j < lengthString / 2 - 1; j++)
                {
                    arithmeticExpression.Append("(");
                }
                double.TryParse(elements[0], out errorTestingNumber);
                arithmeticExpression.Append(errorTestingNumber);
                answer = errorTestingNumber;

            }

            foreach (string element in elements)
            {
                if (double.TryParse(element, out number))
                {
                    numbersList.Add(number);
                }
            }

            for (int i = 2; i < lengthString; i += 2)
            {
                if (elements[i] == "+")
                {
                    answer += numbersList[i / 2];
                    arithmeticExpression.Append(" + " + numbersList[i / 2] + ")");
                }
                else if (elements[i] == "-")
                {
                    answer -= numbersList[i / 2];
                    arithmeticExpression.Append(" - " + numbersList[i / 2] + ")");
                }
                else if (elements[i] == "*")
                {
                    answer *= numbersList[i / 2];
                    arithmeticExpression.Append(" * " + numbersList[i / 2] + ")");
                }
                else if (elements[i] == "/")
                {
                    // what to do about divide by zero??
                    answer /= numbersList[i / 2];
                    arithmeticExpression.Append(" / " + numbersList[i / 2] + ")");
                }
                else if (elements[i] == "^")
                {
                    answer = Math.Pow(answer, numbersList[i / 2]);
                    arithmeticExpression.Append(" ^ " + numbersList[i / 2] + ")");
                }
                else
                {
                    return "Invalid Expression";
                }

            }

            if (arithmeticExpression[arithmeticExpression.Length - 1] == ')' )
            {
                arithmeticExpression.Remove(arithmeticExpression.Length - 1, 1);
            }
            arithmeticExpression.Append(" = " + answer);

            return arithmeticExpression.ToString();
        }
    }
}
