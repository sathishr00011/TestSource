using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace WixFragmentGenerator
{
    class FileHelper
    {
        /// <summary>
        /// Writes a line to the specified file
        /// Will append at all times
        /// </summary>
        /// <param name="lineToWrite">The line of text to add</param>
        /// <param name="filePath">The path to the file to write</param>
        /// <returns>true on success, false otherwise</returns>
        public static bool WriteLineToFile(string lineToWrite, string filePath)
        {
            bool written = false;
            try
            {
                FileInfo fileToWrite = new FileInfo(filePath);
 
                using (StreamWriter file = fileToWrite.AppendText())
                {
                    file.WriteLine(lineToWrite.Replace('\'', '"'));
                    file.Flush();
                    file.Close();
                }

                written = true;
            }
            catch (IOException exc)
            {
                throw new IOException(string.Format("Could not write to file {0}", filePath), exc);
            }
            return written;
        }

        /// <summary>
        /// writes the file header to the output file
        /// Deletes the output file if it exists and creates a new one
        /// </summary>
        /// <param name="filePath">The path to the output file</param>
        public static void WriteHeader(string filePath)
        {
            FileInfo fileToWrite = new FileInfo(filePath);
            if (fileToWrite.Exists)
            {
                fileToWrite.Delete();
            }
            using (StreamWriter file = fileToWrite.CreateText())
            {
                file.Write(FragGenResources.ResourceManager.GetString("Header").Replace('\'', '"'));
                file.Flush();
                file.Close();
            }
        }
    }
}
