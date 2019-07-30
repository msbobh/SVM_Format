using System;
using System.IO;
using mystrings;
using Functions;

namespace SVMFormat
{
    class Program
    {
        static void Main(string[] args)
        {

            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            int vectorlength; // number of features
           
            int numberofArgs = args.Length;
            string inputmatrix, outputfile, labelfile;
            string path = Directory.GetCurrentDirectory();

            if (numberofArgs <= 1)
            {
                Console.WriteLine(MyStrings.usage);
                System.Environment.Exit(1);
            } // Exit if no params passed on the command line
            else if (numberofArgs > 2)
            {
                Console.WriteLine(MyStrings.usage);
                System.Environment.Exit(1);
            }
            

            inputmatrix = args[0];
            labelfile = args[1];
            outputfile = inputmatrix.Replace(".mat", ".svm");
            if (!File.Exists(inputmatrix))
            {
                Console.WriteLine(MyStrings.MissingDataFile);
                System.Environment.Exit(1);
            }
            else if (!File.Exists(labelfile))
            {
                Console.Write(MyStrings.missinglabels);
                System.Environment.Exit(1);
            }
            
            vectorlength = HelperFunctions.VectorLength(inputmatrix); // Get the number of features
            
            /* if the input matrix is not already in the correct format Call reformat function
            * result is that a file is written that is the LIBSVM format, expects the 
            * labels to be in a separate file
            *
            * Reformatdata(string data, string labels, string fname)
            * 
            */
            int linesprocessed = HelperFunctions.Reformatdata(inputmatrix, labelfile, outputfile, vectorlength);
            Console.WriteLine(MyStrings.ConversionComplete, linesprocessed,watch.Elapsed); 
        }
    }
}
