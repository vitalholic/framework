﻿// Accord Math Library
// The Accord.NET Framework
// http://accord-framework.net
//
// Copyright © César Souza, 2009-2016
// cesarsouza at gmail.com
//
//    This library is free software; you can redistribute it and/or
//    modify it under the terms of the GNU Lesser General Public
//    License as published by the Free Software Foundation; either
//    version 2.1 of the License, or (at your option) any later version.
//
//    This library is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
//    Lesser General Public License for more details.
//
//    You should have received a copy of the GNU Lesser General Public
//    License along with this library; if not, write to the Free Software
//    Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA
//

// ======================================================================
// This code has been generated by a tool; do not edit manually. Instead,
// edit the T4 template Matrix.Product.tt so this file can be regenerated. 
// ======================================================================

namespace Accord.Math
{
    using Accord.Math;    
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    [GeneratedCode("Accord.NET T4 Templates", "3.1")]
    public static partial class Matrix
    {

        private static int GetSize<T>(T[,] matrix, int dimension)
        {
            return (dimension == 0) ? matrix.Rows() : matrix.Columns();
        }

        private static int GetSize<T>(T[][] matrix, int dimension)
        {
            return (dimension == 0) ? matrix.Rows() : matrix.Columns();
        }

        /// <summary>
        ///   Computes the mean value across all dimensions of the given matrix.
        /// </summary>
        /// 
        public static double Mean(this double[,] matrix)
        {
            return matrix.Sum() / matrix.Length();
        }

        /// <summary>
        ///   Calculates the matrix Mean vector.
        /// </summary>
        /// 
        /// <param name="matrix">A matrix whose means will be calculated.</param>
        /// <param name="dimension">
        ///   The dimension along which the means will be calculated. Pass
        ///   0 to compute a row vector containing the mean of each column,
        ///   or 1 to compute a column vector containing the mean of each row.
        ///   Default value is 0.
        /// </param>
        /// 
        /// <returns>Returns a vector containing the means of the given matrix.</returns>
        /// 
        /// <example>
        ///   <code>
        ///   double[,] matrix = 
        ///   {
        ///      { 2, -1.0, 5 },
        ///      { 7,  0.5, 9 },
        ///   };
        ///   
        ///   // column means are equal to (4.5, -0.25, 7.0)
        ///   double[] colMeans = Stats.Mean(matrix, 0);
        ///     
        ///   // row means are equal to (2.0, 5.5)
        ///   double[] rowMeans = Stats.Mean(matrix, 1);
        ///   </code>
        /// </example>
        /// 
        public static double[] Mean(this double[,] matrix, int dimension)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            double[] mean;

            if (dimension == 0)
            {
                mean = new double[cols];
                double N = rows;

                // for each column
                for (int j = 0; j < cols; j++)
                {
                    // for each row
                    for (int i = 0; i < rows; i++)
                        mean[j] += matrix[i, j];

                    mean[j] /= N;
                }
            }
            else if (dimension == 1)
            {
                mean = new double[rows];
                double N = cols;

                // for each row
                for (int j = 0; j < rows; j++)
                {
                    // for each column
                    for (int i = 0; i < cols; i++)
                        mean[j] += matrix[j, i];

                    mean[j] /= N;
                }
            }
            else
            {
                throw new ArgumentException("Invalid dimension.", "dimension");
            }

            return mean;
        }

        /// <summary>
        ///   Computes the Skewness for the given values.
        /// </summary>
        /// 
        /// <remarks>
        ///   Skewness characterizes the degree of asymmetry of a distribution
        ///   around its mean. Positive skewness indicates a distribution with
        ///   an asymmetric tail extending towards more positive values. Negative
        ///   skewness indicates a distribution with an asymmetric tail extending
        ///   towards more negative values.
        /// </remarks>
        /// 
        /// <param name="matrix">A number matrix containing the matrix values.</param>
        /// <param name="unbiased">
        ///   True to compute the unbiased estimate of the population
        ///   skewness, false otherwise. Default is true (compute the 
        ///   unbiased estimator).</param>
        /// 
        /// <returns>The skewness of the given data.</returns>
        /// 
        public static double[] Skewness(this double[,] matrix, bool unbiased = true)
        {
            return Skewness(matrix, Mean(matrix, dimension: 0), unbiased);
        }

        /// <summary>
        ///   Computes the Skewness vector for the given matrix.
        /// </summary>
        /// 
        /// <remarks>
        ///   Skewness characterizes the degree of asymmetry of a distribution
        ///   around its mean. Positive skewness indicates a distribution with
        ///   an asymmetric tail extending towards more positive values. Negative
        ///   skewness indicates a distribution with an asymmetric tail extending
        ///   towards more negative values.
        /// </remarks>
        /// 
        /// <param name="matrix">A number array containing the vector values.</param>
        /// <param name="means">The mean value for the given values, if already known.</param>
        /// <param name="unbiased">
        ///   True to compute the unbiased estimate of the population
        ///   skewness, false otherwise. Default is true (compute the 
        ///   unbiased estimator).</param>
        /// 
        /// <returns>The skewness of the given data.</returns>
        /// 
        public static double[] Skewness(this double[,] matrix, double[] means, bool unbiased = true)
        {
            int n = matrix.GetLength(0);
            double[] skewness = new double[means.Length];

            for (int j = 0; j < means.Length; j++)
            {
                double s2 = 0;
                double s3 = 0;

                for (int i = 0; i < n; i++)
                {
                    var dev = matrix[i, j] - means[j];

                    s2 += dev * dev;
                    s3 += dev * dev * dev;
                }

                var m2 = s2 / n;
                var m3 = s3 / n;

                var g = m3 / (Math.Pow(m2, 3 / 2.0));

                if (unbiased)
                {
                    var a = Math.Sqrt(n * (n - 1));
                    var b = n - 2;
                    skewness[j] = (double)((a / b) * g);
                }
                else
                {
                    skewness[j] = (double)g;
                }
            }

            return skewness;
        }

        /// <summary>
        ///   Computes the Kurtosis vector for the given matrix.
        /// </summary>
        /// 
        /// <remarks>
        ///   The framework uses the same definition used by default in SAS and SPSS.
        /// </remarks>
        /// 
        /// <param name="matrix">A number multi-dimensional array containing the matrix values.</param>
        /// <param name="unbiased">
        ///   True to compute the unbiased estimate of the population
        ///   kurtosis, false otherwise. Default is true (compute the 
        ///   unbiased estimator).</param>
        /// 
        /// <returns>The kurtosis vector of the given data.</returns>
        /// 
        public static double[] Kurtosis(this double[,] matrix, bool unbiased = true)
        {
            return Kurtosis(matrix, Mean(matrix, dimension: 0), unbiased);
        }

        /// <summary>
        ///   Computes the sample Kurtosis vector for the given matrix.
        /// </summary>
        /// 
        /// <remarks>
        ///   The framework uses the same definition used by default in SAS and SPSS.
        /// </remarks>
        /// 
        /// <param name="matrix">A number multi-dimensional array containing the matrix values.</param>
        /// <param name="means">The mean value of the given values, if already known.</param>
        /// <param name="unbiased">
        ///   True to compute the unbiased estimate of the population
        ///   kurtosis, false otherwise. Default is true (compute the 
        ///   unbiased estimator).</param>
        /// 
        /// <returns>The sample kurtosis vector of the given data.</returns>
        /// 
        public static double[] Kurtosis(this double[,] matrix, double[] means, bool unbiased = true)
        {
            int n = matrix.Rows();
            int m = matrix.Columns();

            var kurtosis = new double[m];

            for (int j = 0; j < kurtosis.Length; j++)
            {
                double s2 = 0;
                double s4 = 0;

                for (int i = 0; i < n; i++)
                {
                    var dev = matrix[i, j] - means[j];
                    s2 += dev * dev;
                    s4 += dev * dev * dev * dev;
                }

                double dn = (double)n;
                double m2 = s2 / n;
                double m4 = s4 / n;

                if (unbiased)
                {
                    double v = s2 / (dn - 1);
                    double a = (dn * (dn + 1)) / ((dn - 1) * (dn - 2) * (dn - 3));
                    double b = s4 / (v * v);
                    double c = ((dn - 1) * (dn - 1)) / ((dn - 2) * (dn - 3));
                    kurtosis[j] = a * b - 3 * c;
                }
                else
                {
                    kurtosis[j] = m4 / (m2 * m2) - 3;
                }

            }

            return kurtosis;
        }

        /// <summary>
        ///   Calculates the covariance matrix of a sample matrix.
        /// </summary> 
        /// 
        /// <remarks>
        ///   In statistics and probability theory, the covariance matrix is a matrix of
        ///   covariances between elements of a vector. It is the natural generalization
        ///   to higher dimensions of the concept of the variance of a scalar-valued
        ///   random variable.
        /// </remarks>
        /// 
        /// <param name="matrix">A number multi-dimensional array containing the matrix values.</param>
        /// <returns>The covariance matrix.</returns>
        /// 
        public static double Covariance(this double[,] matrix)
        {
            var s = Scatter(matrix, Mean(matrix));
            return s / matrix.Length;
        }

        /// <summary>
        ///   Calculates the covariance matrix of a sample matrix.
        /// </summary>
        /// 
        /// <remarks>
        ///   In statistics and probability theory, the covariance matrix is a matrix of
        ///   covariances between elements of a vector. It is the natural generalization
        ///   to higher dimensions of the concept of the variance of a scalar-valued
        ///   random variable.
        /// </remarks>
        /// 
        /// <param name="matrix">A number multi-dimensional array containing the matrix values.</param>
        /// <param name="dimension">
        ///   The dimension of the matrix to consider as observations. Pass 0 if the matrix has
        ///   observations as rows and variables as columns, pass 1 otherwise. Default is 0.
        /// </param>
        /// 
        /// <returns>The covariance matrix.</returns>
        /// 
        public static double[,] Covariance(this double[,] matrix, int dimension)
        {
            var s = Scatter(matrix, dimension, Mean(matrix, dimension));
            return s.Divide(GetSize(matrix, dimension), result: s);
        }

        /// <summary>
        ///   Calculates the covariance matrix of a sample matrix.
        /// </summary> 
        /// 
        /// <remarks>
        ///   In statistics and probability theory, the covariance matrix is a matrix of
        ///   covariances between elements of a vector. It is the natural generalization
        ///   to higher dimensions of the concept of the variance of a scalar-valued
        ///   random variable.
        /// </remarks>
        /// 
        /// <param name="matrix">A number multi-dimensional array containing the matrix values.</param>
        /// <param name="means">The mean value of the given values, if already known.</param>
        /// 
        /// <returns>The covariance matrix.</returns>
        /// 
        public static double[,] Covariance(this double[,] matrix, int dimension, double[] means)
        {
            var s = Scatter(matrix, dimension, means);
            return s.Divide(GetSize(matrix, dimension), result: s);
        }



        /// <summary>
        ///   Calculates the scatter matrix of a sample matrix.
        /// </summary>
        /// 
        /// <remarks>
        ///   By dividing the Scatter matrix by the sample size, we get the population
        ///   Covariance matrix. By dividing by the sample size minus one, we get the
        ///   sample Covariance matrix.
        /// </remarks>
        /// 
        /// <param name="matrix">A number multi-dimensional array containing the matrix values.</param>
        /// <param name="means">The mean value of the given values, if already known.</param>
        /// <param name="divisor">A real number to divide each member of the matrix.</param>
        /// 
        /// <returns>The covariance matrix.</returns>
        /// 
        public static double Scatter(this double[,] matrix)
        {
            return Scatter(matrix, Mean(matrix));
        }

        /// <summary>
        ///   Calculates the scatter matrix of a sample matrix.
        /// </summary>
        /// 
        /// <remarks>
        ///   By dividing the Scatter matrix by the sample size, we get the population
        ///   Covariance matrix. By dividing by the sample size minus one, we get the
        ///   sample Covariance matrix.
        /// </remarks>
        /// 
        /// <param name="matrix">A number multi-dimensional array containing the matrix values.</param>
        /// <param name="means">The mean value of the given values, if already known.</param>
        /// <param name="divisor">A real number to divide each member of the matrix.</param>
        /// <param name="dimension">
        ///   Pass 0 if the mean vector is a row vector, 1 otherwise. Default value is 0.
        /// </param>
        /// 
        /// <returns>The covariance matrix.</returns>
        /// 
        public static double[,] Scatter(this double[,] matrix, int dimension, double[] means)
        {
            int rows = matrix.Rows();
            int cols = matrix.Columns();

            double[,] cov;

            if (dimension == 0)
            {
                if (means.Length != cols)
                    throw new ArgumentException("Length of the mean vector should equal the number of columns", "means");

                cov = new double[cols, cols];
                for (int i = 0; i < cols; i++)
                {
                    for (int j = i; j < cols; j++)
                    {
                        double s = 0;
                        for (int k = 0; k < rows; k++)
                        {
                            var v = matrix[k, j] - means[j];
                            s += v * v;
                        }
                        cov[i, j] = s;
                        cov[j, i] = s;
                    }
                }
            }
            else if (dimension == 1)
            {
                if (means.Length != rows) 
                    throw new ArgumentException("Length of the mean vector should equal the number of rows", "means");

                cov = new double[rows, rows];
                for (int i = 0; i < rows; i++)
                {
                    for (int j = i; j < rows; j++)
                    {
                        double s = 0;
                        for (int k = 0; k < cols; k++)
                        {
                            var v = matrix[j, k] - means[j];
                            s += v * v;
                        }   
                        cov[i, j] = s;
                        cov[j, i] = s;
                    }
                }
            }
            else
            {
                throw new ArgumentException("Invalid dimension.", "dimension");
            }

            return cov;
        }

        /// <summary>
        ///   Calculates the scatter matrix of a sample matrix.
        /// </summary>
        /// 
        /// <remarks>
        ///   By dividing the Scatter matrix by the sample size, we get the population
        ///   Covariance matrix. By dividing by the sample size minus one, we get the
        ///   sample Covariance matrix.
        /// </remarks>
        /// 
        /// <param name="matrix">A number multi-dimensional array containing the matrix values.</param>
        /// <param name="means">The mean value of the given values, if already known.</param>
        /// <param name="divisor">A real number to divide each member of the matrix.</param>
        /// <param name="dimension">
        ///   Pass 0 if the mean vector is a row vector, 1 otherwise. Default value is 0.
        /// </param>
        /// 
        /// <returns>The covariance matrix.</returns>
        /// 
        public static double Scatter(this double[,] matrix, double mean)
        {
            int rows = matrix.Rows();
            int cols = matrix.Columns();

            double var = 0;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    var v = matrix[i, j] - mean;
                    var += v * v;
                }
            }

            return var;
        }
        /// <summary>
        ///   Computes the mean value across all dimensions of the given matrix.
        /// </summary>
        /// 
        public static float Mean(this float[,] matrix)
        {
            return matrix.Sum() / matrix.Length();
        }

        /// <summary>
        ///   Calculates the matrix Mean vector.
        /// </summary>
        /// 
        /// <param name="matrix">A matrix whose means will be calculated.</param>
        /// <param name="dimension">
        ///   The dimension along which the means will be calculated. Pass
        ///   0 to compute a row vector containing the mean of each column,
        ///   or 1 to compute a column vector containing the mean of each row.
        ///   Default value is 0.
        /// </param>
        /// 
        /// <returns>Returns a vector containing the means of the given matrix.</returns>
        /// 
        /// <example>
        ///   <code>
        ///   double[,] matrix = 
        ///   {
        ///      { 2, -1.0, 5 },
        ///      { 7,  0.5, 9 },
        ///   };
        ///   
        ///   // column means are equal to (4.5, -0.25, 7.0)
        ///   double[] colMeans = Stats.Mean(matrix, 0);
        ///     
        ///   // row means are equal to (2.0, 5.5)
        ///   double[] rowMeans = Stats.Mean(matrix, 1);
        ///   </code>
        /// </example>
        /// 
        public static float[] Mean(this float[,] matrix, int dimension)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            float[] mean;

            if (dimension == 0)
            {
                mean = new float[cols];
                float N = rows;

                // for each column
                for (int j = 0; j < cols; j++)
                {
                    // for each row
                    for (int i = 0; i < rows; i++)
                        mean[j] += matrix[i, j];

                    mean[j] /= N;
                }
            }
            else if (dimension == 1)
            {
                mean = new float[rows];
                float N = cols;

                // for each row
                for (int j = 0; j < rows; j++)
                {
                    // for each column
                    for (int i = 0; i < cols; i++)
                        mean[j] += matrix[j, i];

                    mean[j] /= N;
                }
            }
            else
            {
                throw new ArgumentException("Invalid dimension.", "dimension");
            }

            return mean;
        }

        /// <summary>
        ///   Computes the Skewness for the given values.
        /// </summary>
        /// 
        /// <remarks>
        ///   Skewness characterizes the degree of asymmetry of a distribution
        ///   around its mean. Positive skewness indicates a distribution with
        ///   an asymmetric tail extending towards more positive values. Negative
        ///   skewness indicates a distribution with an asymmetric tail extending
        ///   towards more negative values.
        /// </remarks>
        /// 
        /// <param name="matrix">A number matrix containing the matrix values.</param>
        /// <param name="unbiased">
        ///   True to compute the unbiased estimate of the population
        ///   skewness, false otherwise. Default is true (compute the 
        ///   unbiased estimator).</param>
        /// 
        /// <returns>The skewness of the given data.</returns>
        /// 
        public static float[] Skewness(this float[,] matrix, bool unbiased = true)
        {
            return Skewness(matrix, Mean(matrix, dimension: 0), unbiased);
        }

        /// <summary>
        ///   Computes the Skewness vector for the given matrix.
        /// </summary>
        /// 
        /// <remarks>
        ///   Skewness characterizes the degree of asymmetry of a distribution
        ///   around its mean. Positive skewness indicates a distribution with
        ///   an asymmetric tail extending towards more positive values. Negative
        ///   skewness indicates a distribution with an asymmetric tail extending
        ///   towards more negative values.
        /// </remarks>
        /// 
        /// <param name="matrix">A number array containing the vector values.</param>
        /// <param name="means">The mean value for the given values, if already known.</param>
        /// <param name="unbiased">
        ///   True to compute the unbiased estimate of the population
        ///   skewness, false otherwise. Default is true (compute the 
        ///   unbiased estimator).</param>
        /// 
        /// <returns>The skewness of the given data.</returns>
        /// 
        public static float[] Skewness(this float[,] matrix, float[] means, bool unbiased = true)
        {
            int n = matrix.GetLength(0);
            float[] skewness = new float[means.Length];

            for (int j = 0; j < means.Length; j++)
            {
                float s2 = 0;
                float s3 = 0;

                for (int i = 0; i < n; i++)
                {
                    var dev = matrix[i, j] - means[j];

                    s2 += dev * dev;
                    s3 += dev * dev * dev;
                }

                var m2 = s2 / n;
                var m3 = s3 / n;

                var g = m3 / (Math.Pow(m2, 3 / 2.0));

                if (unbiased)
                {
                    var a = Math.Sqrt(n * (n - 1));
                    var b = n - 2;
                    skewness[j] = (float)((a / b) * g);
                }
                else
                {
                    skewness[j] = (float)g;
                }
            }

            return skewness;
        }

        /// <summary>
        ///   Computes the Kurtosis vector for the given matrix.
        /// </summary>
        /// 
        /// <remarks>
        ///   The framework uses the same definition used by default in SAS and SPSS.
        /// </remarks>
        /// 
        /// <param name="matrix">A number multi-dimensional array containing the matrix values.</param>
        /// <param name="unbiased">
        ///   True to compute the unbiased estimate of the population
        ///   kurtosis, false otherwise. Default is true (compute the 
        ///   unbiased estimator).</param>
        /// 
        /// <returns>The kurtosis vector of the given data.</returns>
        /// 
        public static float[] Kurtosis(this float[,] matrix, bool unbiased = true)
        {
            return Kurtosis(matrix, Mean(matrix, dimension: 0), unbiased);
        }

        /// <summary>
        ///   Computes the sample Kurtosis vector for the given matrix.
        /// </summary>
        /// 
        /// <remarks>
        ///   The framework uses the same definition used by default in SAS and SPSS.
        /// </remarks>
        /// 
        /// <param name="matrix">A number multi-dimensional array containing the matrix values.</param>
        /// <param name="means">The mean value of the given values, if already known.</param>
        /// <param name="unbiased">
        ///   True to compute the unbiased estimate of the population
        ///   kurtosis, false otherwise. Default is true (compute the 
        ///   unbiased estimator).</param>
        /// 
        /// <returns>The sample kurtosis vector of the given data.</returns>
        /// 
        public static float[] Kurtosis(this float[,] matrix, float[] means, bool unbiased = true)
        {
            int n = matrix.Rows();
            int m = matrix.Columns();

            var kurtosis = new float[m];

            for (int j = 0; j < kurtosis.Length; j++)
            {
                float s2 = 0;
                float s4 = 0;

                for (int i = 0; i < n; i++)
                {
                    var dev = matrix[i, j] - means[j];
                    s2 += dev * dev;
                    s4 += dev * dev * dev * dev;
                }

                float dn = (float)n;
                float m2 = s2 / n;
                float m4 = s4 / n;

                if (unbiased)
                {
                    float v = s2 / (dn - 1);
                    float a = (dn * (dn + 1)) / ((dn - 1) * (dn - 2) * (dn - 3));
                    float b = s4 / (v * v);
                    float c = ((dn - 1) * (dn - 1)) / ((dn - 2) * (dn - 3));
                    kurtosis[j] = a * b - 3 * c;
                }
                else
                {
                    kurtosis[j] = m4 / (m2 * m2) - 3;
                }

            }

            return kurtosis;
        }

        /// <summary>
        ///   Calculates the covariance matrix of a sample matrix.
        /// </summary> 
        /// 
        /// <remarks>
        ///   In statistics and probability theory, the covariance matrix is a matrix of
        ///   covariances between elements of a vector. It is the natural generalization
        ///   to higher dimensions of the concept of the variance of a scalar-valued
        ///   random variable.
        /// </remarks>
        /// 
        /// <param name="matrix">A number multi-dimensional array containing the matrix values.</param>
        /// <returns>The covariance matrix.</returns>
        /// 
        public static float Covariance(this float[,] matrix)
        {
            var s = Scatter(matrix, Mean(matrix));
            return s / matrix.Length;
        }

        /// <summary>
        ///   Calculates the covariance matrix of a sample matrix.
        /// </summary>
        /// 
        /// <remarks>
        ///   In statistics and probability theory, the covariance matrix is a matrix of
        ///   covariances between elements of a vector. It is the natural generalization
        ///   to higher dimensions of the concept of the variance of a scalar-valued
        ///   random variable.
        /// </remarks>
        /// 
        /// <param name="matrix">A number multi-dimensional array containing the matrix values.</param>
        /// <param name="dimension">
        ///   The dimension of the matrix to consider as observations. Pass 0 if the matrix has
        ///   observations as rows and variables as columns, pass 1 otherwise. Default is 0.
        /// </param>
        /// 
        /// <returns>The covariance matrix.</returns>
        /// 
        public static float[,] Covariance(this float[,] matrix, int dimension)
        {
            var s = Scatter(matrix, dimension, Mean(matrix, dimension));
            return s.Divide(GetSize(matrix, dimension), result: s);
        }

        /// <summary>
        ///   Calculates the covariance matrix of a sample matrix.
        /// </summary> 
        /// 
        /// <remarks>
        ///   In statistics and probability theory, the covariance matrix is a matrix of
        ///   covariances between elements of a vector. It is the natural generalization
        ///   to higher dimensions of the concept of the variance of a scalar-valued
        ///   random variable.
        /// </remarks>
        /// 
        /// <param name="matrix">A number multi-dimensional array containing the matrix values.</param>
        /// <param name="means">The mean value of the given values, if already known.</param>
        /// 
        /// <returns>The covariance matrix.</returns>
        /// 
        public static float[,] Covariance(this float[,] matrix, int dimension, float[] means)
        {
            var s = Scatter(matrix, dimension, means);
            return s.Divide(GetSize(matrix, dimension), result: s);
        }



        /// <summary>
        ///   Calculates the scatter matrix of a sample matrix.
        /// </summary>
        /// 
        /// <remarks>
        ///   By dividing the Scatter matrix by the sample size, we get the population
        ///   Covariance matrix. By dividing by the sample size minus one, we get the
        ///   sample Covariance matrix.
        /// </remarks>
        /// 
        /// <param name="matrix">A number multi-dimensional array containing the matrix values.</param>
        /// <param name="means">The mean value of the given values, if already known.</param>
        /// <param name="divisor">A real number to divide each member of the matrix.</param>
        /// 
        /// <returns>The covariance matrix.</returns>
        /// 
        public static float Scatter(this float[,] matrix)
        {
            return Scatter(matrix, Mean(matrix));
        }

        /// <summary>
        ///   Calculates the scatter matrix of a sample matrix.
        /// </summary>
        /// 
        /// <remarks>
        ///   By dividing the Scatter matrix by the sample size, we get the population
        ///   Covariance matrix. By dividing by the sample size minus one, we get the
        ///   sample Covariance matrix.
        /// </remarks>
        /// 
        /// <param name="matrix">A number multi-dimensional array containing the matrix values.</param>
        /// <param name="means">The mean value of the given values, if already known.</param>
        /// <param name="divisor">A real number to divide each member of the matrix.</param>
        /// <param name="dimension">
        ///   Pass 0 if the mean vector is a row vector, 1 otherwise. Default value is 0.
        /// </param>
        /// 
        /// <returns>The covariance matrix.</returns>
        /// 
        public static float[,] Scatter(this float[,] matrix, int dimension, float[] means)
        {
            int rows = matrix.Rows();
            int cols = matrix.Columns();

            float[,] cov;

            if (dimension == 0)
            {
                if (means.Length != cols)
                    throw new ArgumentException("Length of the mean vector should equal the number of columns", "means");

                cov = new float[cols, cols];
                for (int i = 0; i < cols; i++)
                {
                    for (int j = i; j < cols; j++)
                    {
                        float s = 0;
                        for (int k = 0; k < rows; k++)
                        {
                            var v = matrix[k, j] - means[j];
                            s += v * v;
                        }
                        cov[i, j] = s;
                        cov[j, i] = s;
                    }
                }
            }
            else if (dimension == 1)
            {
                if (means.Length != rows) 
                    throw new ArgumentException("Length of the mean vector should equal the number of rows", "means");

                cov = new float[rows, rows];
                for (int i = 0; i < rows; i++)
                {
                    for (int j = i; j < rows; j++)
                    {
                        float s = 0;
                        for (int k = 0; k < cols; k++)
                        {
                            var v = matrix[j, k] - means[j];
                            s += v * v;
                        }   
                        cov[i, j] = s;
                        cov[j, i] = s;
                    }
                }
            }
            else
            {
                throw new ArgumentException("Invalid dimension.", "dimension");
            }

            return cov;
        }

        /// <summary>
        ///   Calculates the scatter matrix of a sample matrix.
        /// </summary>
        /// 
        /// <remarks>
        ///   By dividing the Scatter matrix by the sample size, we get the population
        ///   Covariance matrix. By dividing by the sample size minus one, we get the
        ///   sample Covariance matrix.
        /// </remarks>
        /// 
        /// <param name="matrix">A number multi-dimensional array containing the matrix values.</param>
        /// <param name="means">The mean value of the given values, if already known.</param>
        /// <param name="divisor">A real number to divide each member of the matrix.</param>
        /// <param name="dimension">
        ///   Pass 0 if the mean vector is a row vector, 1 otherwise. Default value is 0.
        /// </param>
        /// 
        /// <returns>The covariance matrix.</returns>
        /// 
        public static float Scatter(this float[,] matrix, float mean)
        {
            int rows = matrix.Rows();
            int cols = matrix.Columns();

            float var = 0;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    var v = matrix[i, j] - mean;
                    var += v * v;
                }
            }

            return var;
        }
    }
}
