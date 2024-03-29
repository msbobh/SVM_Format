﻿using System;
using System.IO;
using mystrings;


namespace Functions
{
    class HelperFunctions
    {
        static public int Reformatdata(string matrix, string labelfilename, string fname, int vectorlength)
        
        /* 
         * Each row begins with a label (-1 | 0)
         * The pair <index>:<value> defines a feature (attribute) value: <index> is
         * an integer starting from 1 and <value> can be a real number. The only
         * exception is the precomputed kernel, where <index> starts from 0; see
         * the section of precomputed kernels. Indices must be in ASCENDING
         *  order.
         */
        {
            // Local variables
            int labelindex = 0;
            string[] data = new string[vectorlength - 1];

            string compressed; // used to hold the vector after stripping out the spaces
            data = File.ReadAllLines(matrix); // Read in training data
            string[] Labels = File.ReadAllLines(labelfilename);
            

            // Create the output file
            StreamWriter outfile = null;
            try { outfile = new StreamWriter(fname); }
            catch (Exception e)
            {
                Console.WriteLine(MyStrings.File_error, e);
                System.Environment.Exit(0);
            }

            foreach (var row in data)   // Process one row at a time
            {
                /* 
                 * label is the first entry in each row
                 */
                 
                if (Labels[labelindex] == "0")  // LibSVM maps 0's to -1 and 1's to 1
                {
                    outfile.Write("-1 ");
                }
                else
                {
                    outfile.Write("1 ");
                }
                // Write out the label at the beginning of the row, then strip out spaces and reduce the vector by a factor of 2
                //compressed = row;
                compressed = row.Replace(" ", "");
                for (int i = 1; i <= (vectorlength - 1) / 2; i++)
                {
                    outfile.Write("{0}:{1} ", i, compressed[i]); // step through the row and add index and ":"
                }
                outfile.WriteLine();
                labelindex++;

            }
            outfile.Close();
            return labelindex;
        } // Done procecssing rows

        static public int SampleSize(string fname)
        {
            /* Returns the number of samples in a training matrix
             */
            var samples = 0;
            using (var reader = File.OpenText(fname))
            {
                while (reader.ReadLine() != null)
                {
                    samples++;
                }
            }
            return samples;
        }

        
        static public int VectorLength(string vectorfile)
        {
            /* Get the size of the feature vector from the data so we can set it automatically */
            string line;
            int Count = 0;
            StreamReader file = new StreamReader(vectorfile);
            line = file.ReadLine();
            Count = line.Length / 2; // Length returns the number of spaces as well as ints
            return (Count);
        }

        
        static public bool CheckFormat(string filename)
        {
            bool SVMFormat = false;
            string[] input;
            if (File.Exists(filename))
            {
                input = File.ReadAllLines(filename);
                foreach ( string line in input)
                {
                    if ((line.Contains("+") || line.Contains("-")) && (line.Contains("1") && line.Contains(":"))) // if each line contains expected format let's call it good
                    {
                        SVMFormat = true;
                    }
                }
            }
            else
            {
                Console.WriteLine(MyStrings.File_error, filename);
                System.Environment.Exit(1);
            }

            return SVMFormat;
        }
        
        static public bool CheckLabel (string filename)
        {
            bool success = false;
            var testlabels = File.ReadAllLines(filename);
            foreach (var row in testlabels)
            {
                if ((row != "0" && row != "1"))
                {
                    Console.WriteLine("Label file in incorrect format");
                    //System.Environment.Exit(1);
                }
            }
            success = true;
            return success;
        }
    }
}