using System;
using System.Collections.Generic;
using System.IO;

namespace FileCount
{
    /// <summary>
    /// Gets the counts of files grouped by created month
    /// </summary>
    public class AssignmentTwo
    {
        /// <summary>
        /// Gets the user input of folder path
        /// </summary>
        /// <returns>The folder to group the files</returns>
        public string GetFolderName()
        {
            Console.WriteLine("Enter the path of the folder: ");

            // Get the folder from user input
            string folder= Console.ReadLine();

            // Verify if the directory exists
            if(Directory.Exists(folder))
            {
                throw new FileNotFoundException("Please enter a valid directory");
            }

            return folder;
        }

        /// <summary>
        /// Gets the file counts and groups them with month
        /// </summary>
        /// <param name="path">Folder path</param>
        /// <returns>A <see cref="Dictionary{TKey, TValue}"/>as the "Month" "FileCount" pairs</returns>
        public Dictionary<string,int> GetFileCount(string path)
        {
            //  Store all the files into a string array
            string[] files = Directory.GetFiles(path);

            // Declare a Dictionary for months and file counts
            Dictionary<String, int> fileGroups = new Dictionary<string, int>();
            
            // Run a loop to validate each file
            foreach(string file in files)
            {
                // Get the created month of the file
                string month = GetCreatedMonth(file);

                // Add the month to the key if not already present, and increase the count by 1 for the respective month.
                if (!fileGroups.ContainsKey(month))
                {
                    // If month is not already added, add the month and initialize the file count to 1.
                    fileGroups.Add(month, 1);
                }
                else
                {
                    // If month already exists, increase the count by 1.
                    fileGroups[month] += 1;
                }
            }

            // Return the key value pairs
            return fileGroups;
        }

        /// <summary>
        /// Gets the created month of a particular file
        /// </summary>
        /// <param name="filePath">File Path</param>
        /// <returns>Created month</returns>
        public string GetCreatedMonth(string filePath)
        {
            // Get the created month in the format "MMMM"
            return File.GetCreationTime(filePath).ToString("MMMM");
        }
    }
}
