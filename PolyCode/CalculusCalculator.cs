﻿//MP2 Calculator 
//This file contains the CalculusCalculator class.

//You should implement the requesed methods.

using System;
using System.Collections.Generic;
using System.Text;


namespace MP2
{
    public class CalculusCalculator
    {
        string polynomial = string.Empty;
        List<double> coefficientList = new List<double>();

        /// <summary>
        /// Prompts the user for the coefficients of a polynomial, and sets the 
        /// polynomial field and the coefficientList field of the object.
        /// It must use the isValidPolynomial method to check for the validity
        /// of the polynomial entered by the user, otherwise the fields must 
        /// not change.
        /// The acceptable format of the coefficients received from the user is 
        /// a series of numbers (one for each coefficient) separated by spaces. 
        /// All coefficients values must be entered even those that are zero.
        /// </summary>
        /// <returns>True if the polynomial is succeffully set, false otherwise.</returns>
        public bool SetPolynomial()
        {
            StringBuilder number = new StringBuilder();
            coefficientList.Clear();

            Console.WriteLine( Environment.NewLine + "Enter all coefficients for a polynomial in descending order seperated by spaces.");
            polynomial = Console.ReadLine().Trim();

            if (IsValidPolynomial(polynomial))
            {
                string[] elements = polynomial.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                foreach( string element in elements)
                {
                    coefficientList.Add(Double.Parse(element));
                }

            }
            else
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Checks if the passed polynomial string is valid.
        /// The acceptable format of the coefficient string is a series of 
        /// numbers (one for each coefficient) separated by spaces. 
        /// </summary>
        /// <example>
        /// Examples of valid strings: "2   3.5 0  ", or "-2 -3.5 0 0"
        /// Examples of invalid strings: "3..5", or "2x^2+1", or "a b c", or "3 - 5"
        /// </example>
        /// <param name="polynomial">
        /// A string containing the coefficient of a polynomial. Index 0 is the
        /// highest order, and all coefficients exist (even 0's).
        /// </param>
        /// <returns>True if a valid polynomial, false otherwise.</returns>
        public bool IsValidPolynomial(string polynomial)
        {
            string[] elements = polynomial.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            for(int i = 0; i<elements.Length; i++)
            {
                if(!Double.TryParse(elements[i], out double number))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Returns a string representing this polynomial.
        /// </summary>
        /// <returns>
        /// A string containing the polynomial in the format:
        /// (a_n)*x^n + (a_n_1)*x^n_1 + ... + (a1)*x + (a0) 
        /// It does not display the term of any coefficient that is 0.
        /// If all coefficients are 0, then it returns "0".
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the polynomial field is empty. 
        /// Exception message used: "No polynomial is set."
        /// </exception>
        public string GetPolynomialString()
        {
            if (polynomial == "")
            {
                throw new InvalidOperationException("No Polynomial is set");
            }
            else
            {
                StringBuilder poly = new StringBuilder();
                int length = coefficientList.Count;
                int j = 0;
                
                for (int i = length - 1; i >= 0; i--)
                {
                    if (coefficientList[j] != 0)
                    {
                        if (i == 0)
                        {
                            if (i != length -1)
                            {
                                poly.Append(" + ");
                            }

                            poly.Append($"({coefficientList[j]})");

                        }
                        else if (i == 1 && i != length - 1)
                        {
                            poly.Append($" + ({coefficientList[j]})*x");
                        }
                        
                        else if (i != length - 1)
                        {
                            poly.Append($" + ({coefficientList[j]})*x^{i}");
                        }
                        else
                        {
                            poly.Append($"({coefficientList[j]})*x^{i}");

                        }

                    }
                    j++;
                }
                return poly.ToString();
            }
        }

        /// <summary>
        /// Evaluates this polynomial at the x passed to the method.
        /// </summary>
        /// <param name="x">The x at which we are evaluating the polynomial.</param>
        /// <returns>The result of the polynomial evaluation.</returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the polynomial field is empty. 
        /// Exception message used: "No polynomial is set."
        /// </exception>
        public double EvaluatePolynomial(double x)
        {
            if (polynomial == "")
            {
                throw new InvalidOperationException("No Polynomial is set");
            }
            
            double evaluation = 0;
            int j = 0;

            for (int i = coefficientList.Count - 1; i >= 0; i--)
            {
                evaluation += (coefficientList[j]) * Math.Pow(x, i);
                j++;
             }

            return evaluation;

            }
        

        /// <summary>
        /// Finds a root of this polynomial using the provided guess.
        /// </summary>
        /// <param name="guess">The initial value for the Newton method.</param>
        /// <param name="epsilon">The desired accuracy: stops when |f(result)| is
        /// less than or equal epsilon.</param>
        /// <param name="iterationMax">A max cap on the number of iterations in the
        /// Newton-Raphson method. This is to also guarantee no infinite loops.
        /// If this iterationMax is reached, a double.NaN is returned.</param>
        /// <returns>
        /// The root found using the Netwon-Raphson method. 
        /// A double.NaN is returned if a root cannot be found.
        /// The return value is rounded to have 4 digits after the decimal point.
        /// </returns>
        public double NewtonRaphson(double guess, double epsilon, int iterationMax)
        {
            int count = 0;
            double x = guess;

            while (Math.Abs(EvaluatePolynomial(x)) > epsilon && count < iterationMax)
            {
                x = x - EvaluatePolynomial(x) / EvaluatePolynomialDerivative(x);
                count++;
            }

            if (count == iterationMax || double.IsInfinity(x))
            {
                return double.NaN;
            }

            return Math.Round(x, 2);
            
        }

        /// <summary>
        /// Calculates and returns all unique real roots of this polynomial 
        /// that can be found using the NewtonRaphson method. 
        /// The method uses all initial guesses between -50 and 50 with 
        /// steps of 0.5 to find all unique roots it can find. 
        /// A root is considered unique, if there is no root already found 
        /// that is within our desired accuracy level.
        /// Uses 10 as the max number of iterations used by Newton-Raphson method.
        /// </summary>
        /// <param name="epsilon">The desired accuracy.</param>
        /// <returns>A list containing all the unique roots that the method finds.</returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the polynomial field is empty. 
        /// Exception message used: "No polynomial is set."
        /// </exception>
        public List<double> GetAllRoots(double epsilon) 
        {

            if (polynomial == "")
            {
                throw new InvalidOperationException("No polynomial is set.");
            }

            double x;
            int maxIterations = 10;
            List<double> result = new List<double>();

            for (double guess = -50.0; guess <= 50; guess += 0.5) 
            {
                x = NewtonRaphson(guess, epsilon, maxIterations);

                if (!double.IsNaN(x))
                {
                    if (!result.Contains(x))
                    {
                        result.Add(x);
                    }

                }

            }
            

            return result;
        }

        /// <summary>
        /// Evaluates the 1st derivative of this polynomial at x, passed to the method.
        /// The method uses the exact numerical technique, since it is easy to derive the 
        /// derivative of a polynomial.
        /// </summary>
        /// <param name="x">The x at which we are evaluating the polynomial derivative.</param>
        /// <returns>The result of the polynomial derivative evaluation.</returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the polynomial field is empty.
        /// Exception message used: "No polynomial is set."
        /// </exception>
        public double EvaluatePolynomialDerivative(double x)
        {
            double result = 0;
            double order = coefficientList.Count - 1;

            if (polynomial == "") 
            {
                throw new InvalidOperationException ("No polymial is set.");
            }
            
            for (int i = 0; i < order; i++)
            {
                result += coefficientList[i] * (order - i) * Math.Pow(x, order - i - 1);
            }
            return result;
        }

        /// <summary>
        /// Evaluates the definite integral of this polynomial from a to b.
        /// The method uses the exact numerical technique, since it is easy to derive the 
        /// indefinite integral of a polynomial.
        /// </summary>
        /// <param name="a">The lower limit of the integral.</param>
        /// <param name="b">The upper limit of the integral.</param>
        /// <returns>The result of the integral evaluation.</returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the polynomial field is empty.
        /// Exception message used: "No polynomial is set."
        /// </exception>
        public double EvaluatePolynomialIntegral(double a, double b)
        {

            if (polynomial == "") 
            {
                throw new InvalidOperationException ("No polymial is set.");
            }

            double Fa = 0;
            double Fb = 0;
            double order = coefficientList.Count - 1;

            for (int i = 0; i <= order; i++)
            {
                
                Fa += coefficientList[i] / (order - i + 1) * Math.Pow(a, order - i + 1);
                Fb += coefficientList[i] / (order - i + 1) * Math.Pow(b, order - i + 1);
            }

            return Fb - Fa;

        }


        /// <summary>
        /// Assume parameters of the polynomial string and coefficient list are not 
        /// empty.
        /// </summary>
        /// <param name="coefficientList"></param>
        /// <param name="polynomial"></param>
        /// <returns>True if the polynomial is succeffully set, false otherwise.</returns>
        public void SetPolynomialHelper(string polynomial, List<double> coefficientList)
        {
            this.polynomial = polynomial;
            this.coefficientList = coefficientList;

        }
    }
}

