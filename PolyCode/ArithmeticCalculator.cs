//MP2 Calculator 
//This file contains the ArithmethicCalculator class.

//You should implement the BasicArithmetic method.

using System;

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

            // To implement
        }
    }
}
