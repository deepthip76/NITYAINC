using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace FileCount
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create object for the class
            AssignmentTwo assignmentObject = new AssignmentTwo();

            // Get the folder path from user input
            string folder = assignmentObject.GetFolderName();

            // Get the files of the folder
            string[] files = assignmentObject.GetFiles(folder);

            // Create a dictionary object to store the result
            Dictionary<string, int> fileGroups = new Dictionary<string, int>();

            // Get files from folder
            fileGroups = assignmentObject.GetFileCount(files, fileGroups, files.Length-1);

            // Print the Months and File Counts
            foreach (KeyValuePair<string, int> fileGroup in fileGroups)
            {
                Console.WriteLine($"{fileGroup.Key}: {fileGroup.Value}" );
            }

        }
    }
}
