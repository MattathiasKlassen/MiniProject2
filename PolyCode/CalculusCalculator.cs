//MP2 Calculator 
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

                for (int i = 0; i<elements.Length; i++)
                {
                    coefficientList.Add(Double.Parse(elements[i].ToString()));
                }
                //for (int i = 0; i < polynomial.Length; i++)
                //{
                //    if (polynomial[i] != ' ')
                //    {
                //        number.Append(polynomial[i]);
                //    }
                //    else if (polynomial[i] == ' ' && number.Length>0)
                //    {
                //        coefficientList.Add(Double.Parse(number.ToString()));
                //        number.Clear();
                //    }
                //}
                //coefficientList.Add(Double.Parse(number.ToString()));
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
                throw new InvalidCastException("No Polynomial is set");
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
                            poly.Append($" ({coefficientList[j]})");
                        }
                        else if (i == 1)
                        {
                            poly.Append($"({coefficientList[j]})*x");
                        }
                        else
                        {
                            poly.Append($"({coefficientList[j]})*x^{i} ");
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
                throw new InvalidCastException("No Polynomial is set");
            }
            else
            {
                double evaluation = 0;
                int j = 0;

                for (int i = coefficientList.Count - 1; i >= 0; i--)
                {
                    evaluation += (coefficientList[j]) * Math.Pow(x, i);
                    j++;
                }
                return evaluation;
            }
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

            return Math.Round(x, 4); //4 decimal places
            
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

            if (coefficientList.Count == 0)
            {
                throw new InvalidOperationException("No polynomial is set.");
            }

            double x;
            List<double> result = new List<double>();


            for(double guess = -50.0; guess <= 5; guess += 0.5) //What does "step is 0.5" mean
            {
                x = NewtonRaphson(guess,epsilon,10);
                
                if (x!=double.NaN)
                    result.Add(x);
            }
            return result;

            // YUDAN
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
            double xMultiply;
            double order = coefficientList.Count - 1;

            if (coefficientList.Count == 0) //polynomial field is empty??????
            {
                throw new InvalidOperationException ("No polymial is set.");
            }
            
            for (int i = 0; i < order; i++)
            {
                int count = 0;
                xMultiply = 1.0;
                while(count < order - i - 1)
                {
                    xMultiply *= x;
                    count ++;
                }
                result += coefficientList [i] * (order - i) * xMultiply;
            }
            return result;
            // YUDAN
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
            if (coefficientList.Count == 0) //polynomial field is empty??????
            {
                throw new InvalidOperationException ("No polymial is set.");
            }

            double Fa = 0;
            double Fb = 0;
            double aMultiply;
            double bMultiply;
            double order = coefficientList.Count - 1;

            for (int i = 0; i <= order; i++)
            {
                int count = 0;
                aMultiply = 1.0;
                while (count <= order - i)
                {
                    aMultiply *= a;
                    count++;
                }
                Fa += coefficientList[i] / (order - i + 1) * aMultiply;
            }

            for (int j = 0; j <= order; j++)
            {
                int count = 0;
                bMultiply = 1.0;
                while (count <= order - j)
                {
                    bMultiply *= b;
                    count++;
                }
                Fb += coefficientList[j] / (order - j + 1) * bMultiply;
            }


            return Fb - Fa;

            // YUDAN
        }
    }
}