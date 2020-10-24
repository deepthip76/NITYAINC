using System;
using System.Collections.Generic;

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

            // Get files from folder
            Dictionary<string, int> fileGroups = assignmentObject.GetFileCount(folder);

            // Print the Months and File Counts
            foreach (KeyValuePair<string, int> fileGroup in fileGroups)
            {
                Console.WriteLine($"{fileGroup.Key}: {fileGroup.Value}" );
            }
        }
    }
}
