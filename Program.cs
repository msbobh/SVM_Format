using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using mystrings;
using Functions;

namespace SVMFormat
{
    class Program
    {
        static void Main(string[] args)
        {
            
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
                Console.WriteLine("missing data file");
                System.Environment.Exit(1);
            }
            else if (!File.Exists(labelfile))
            {
                Console.Write(MyStrings.missinglabels);
                System.Environment.Exit(1);
            }

            vectorlength = HelperFunctions.VectorLength(inputmatrix); // Get the number of features
            string[] labels = new string[HelperFunctions.SampleSize(labelfile)]; // Calculate the number of labels and use to create storage

            /* if the input matrix is not already in the correct format Call reformat function
            * result is that a file is written that is the LIBSVM format, expects the 
            * labels to be in a separate file
            *
            * Reformatdata(string data, string[] labels, string fname)
            * 
            */
             HelperFunctions.Reformatdata(inputmatrix, labels, outputfile, vectorlength);
            Console.WriteLine(MyStrings.ConversionComplete);
        }
    }
}
