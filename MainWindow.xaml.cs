using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;

namespace Regression
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Func();
        }

        public static void Func()
        {
            var yValues = new double[]
                              {
                                    20647.0000000000000000,
                                    21273.0000000000000000,
                                    22040.0000000000000000,
                                    22853.0000000100000000,
                                    23802.0000000000000000,
                                    24850.0000000000000000,
                                    26008.0000000000000000,
                                    27317.0000000100000000,
                                    28676.0000000000000000,
                                    30160.0000000000000000,
                                    31710.0000000000000000,
                                    33330.0000000000000000,
                                    35080.0000000000000000,
                                    36846.0000000000000000,
                                    38732.0000000000000000,
                                    40727.0000000000000000

                              };
            var xValues = new double[]
                              {
                                    23.8227000000000000,
                                    28.7763000000000000,
                                    34.3355000000000100,
                                    40.5167000000000000,
                                    47.3323000000000000,
                                    54.7914000000000000,
                                    62.8998000000000000,
                                    71.6603000000000100,
                                    81.0731000000000000,
                                    91.1361000000000000,
                                    101.8450000000000000,
                                    113.1940000000000000,
                                    125.1760000000000000,
                                    137.7820000000000000,
                                    151.0010000000000000,
                                    164.8230000000000000
                              };

            double rSquared, intercept, slope;
            LinearRegression(xValues, yValues, out rSquared, out intercept, out slope);

            Console.WriteLine($"R-squared = {rSquared}");
            Console.WriteLine($"Intercept = {intercept}");
            Console.WriteLine($"Slope = {slope}");

            var predictedValue = (slope * 2017) + intercept;
            Console.WriteLine($"Prediction for 2017: {predictedValue}");
        }

        /// <summary>
        /// Fits a line to a collection of (x,y) points.
        /// </summary>
        /// <param name="xVals">The x-axis values.</param>
        /// <param name="yVals">The y-axis values.</param>
        /// <param name="rSquared">The r^2 value of the line.</param>
        /// <param name="yIntercept">The y-intercept value of the line (i.e. y = ax + b, yIntercept is b).</param>
        /// <param name="slope">The slop of the line (i.e. y = ax + b, slope is a).</param>
        public static void LinearRegression(
            double[] xVals,
            double[] yVals,
            out double rSquared,
            out double yIntercept,
            out double slope)
        {
            if (xVals.Length != yVals.Length)
            {
                throw new Exception("Input values should be with the same length.");
            }

            double sumOfX = 0;
            double sumOfY = 0;
            double sumOfXSq = 0;
            double sumOfYSq = 0;
            double sumCodeviates = 0;

            for (var i = 0; i < xVals.Length; i++)
            {
                var x = xVals[i];
                var y = yVals[i];
                sumCodeviates += x * y;
                sumOfX += x;
                sumOfY += y;
                sumOfXSq += x * x;
                sumOfYSq += y * y;
            }

            var count = xVals.Length;
            var ssX = sumOfXSq - ((sumOfX * sumOfX) / count);
            var ssY = sumOfYSq - ((sumOfY * sumOfY) / count);

            var rNumerator = (count * sumCodeviates) - (sumOfX * sumOfY);
            var rDenom = (count * sumOfXSq - (sumOfX * sumOfX)) * (count * sumOfYSq - (sumOfY * sumOfY));
            var sCo = sumCodeviates - ((sumOfX * sumOfY) / count);

            var meanX = sumOfX / count;
            var meanY = sumOfY / count;
            var dblR = rNumerator / Math.Sqrt(rDenom);

            rSquared = dblR * dblR;
            yIntercept = meanY - ((sCo / ssX) * meanX);
            slope = sCo / ssX;
        }
    }

}
