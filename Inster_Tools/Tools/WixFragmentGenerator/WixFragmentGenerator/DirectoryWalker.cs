using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;


namespace WixFragmentGenerator
{
    //   <summary>
    //   Class to walk the directory
    //   </summary>
    public class DirectoryWalker
    {
        static System.Collections.Specialized.StringCollection log = new System.Collections.Specialized.StringCollection();
        static List<string> components = new List<string>();
        static List<string> directories = new List<string>();

        //   <summary>
        //   Walk the directory tree to generate fragments
        //   </summary>
        //   <param name="root"></param>
        //   <param name="child">false if parent, true if a child</param>
        public static void WalkDirectoryTree(System.IO.DirectoryInfo root, bool child, string source, string fileToWriteTo)
        {
            System.IO.FileInfo[] files = null;
            System.IO.DirectoryInfo[] subDirs = null;

            // Write the directory fragment
            if (!child)
            {
                FileHelper.WriteHeader(fileToWriteTo);
            }
            else
            {
                string dirFragment = string.Format("<Directory Id='{0}' Name='{1}'>", GenerateNumberedItem(root.Name,directories), root.Name);
                FileHelper.WriteLineToFile(dirFragment, fileToWriteTo);
            }

            //  First, process all the files directly under this folder
            try
            {
                files = root.GetFiles("*.*");
            }
            catch (System.IO.DirectoryNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }

            if (files.Length > 0)
            {
                writeComponentFiles(root, child, source, fileToWriteTo, files);

            }

            //  Now find all the subdirectories under this directory.
            subDirs = root.GetDirectories();

            foreach (System.IO.DirectoryInfo dirInfo in subDirs)
            {
                string newSource = string.Concat(source, "\\", dirInfo.Name);
                //  Resursive call for each subdirectory.
                WalkDirectoryTree(dirInfo, true, newSource, fileToWriteTo);
            }
            if (!child)
            {
                FileHelper.WriteLineToFile("</DirectoryRef>", fileToWriteTo);
            }
            else
            {
                FileHelper.WriteLineToFile("</Directory>", fileToWriteTo);
            }

            // write the close out
            if (!child)
            {
                //Write the components
                WriteComponentGroupRef(fileToWriteTo);

                //Write the file ending
                FileHelper.WriteLineToFile("</Fragment>", fileToWriteTo);
                FileHelper.WriteLineToFile("</Wix>", fileToWriteTo);
            }
        }

        //   <summary>
        //   Write the componentGroupRefout
        //   </summary>
        //   <param name="fileToWriteTo"></param>
        private static void WriteComponentGroupRef(string fileToWriteTo)
        {
            // write the component group
            FileHelper.WriteLineToFile("<ComponentGroup Id='ApplicationFiles'>", fileToWriteTo);
            foreach (string component in components)
            {
                FileHelper.WriteLineToFile(string.Format("<ComponentRef Id='{0}' />", component), fileToWriteTo);
            }
            FileHelper.WriteLineToFile("</ComponentGroup>", fileToWriteTo);
        }

        //   <summary>
        //   Write the component and file fragments out
        //   </summary>
        //   <param name="root">The directory that is being explored</param>
        //   <param name="child">whether this is the root or a child dir</param>
        //   <param name="source">the source text to put in the fragment</param>
        //   <param name="fileToWriteTo">The name of the outputfile</param>
        //   <param name="files">the files in the directory</param>
        private static void writeComponentFiles(System.IO.DirectoryInfo root, bool child, string source, string fileToWriteTo, System.IO.FileInfo[] files)
        {
            string component = string.Empty;
            //  If there are files, we need to make a component, then list the files in the component
            if (child)
            {
                component = GenerateNumberedItem(root.Name, components);
            }
            else
            {
                component = "BaseFiles";
                // need to add so that the component gets included at the end
               // components.Add(component);
            }


            // write the file fragments out
            WriteFileFragment(files, source, component, fileToWriteTo);

        }

        /// <summary>
        /// creates a numbered version of the name
        /// </summary>
        /// <param name="itemName"></param>
        /// <param name="items">the list of items</param>
        /// <returns></returns>
        private static string GenerateNumberedItem(string itemName, List<string> items)
        {
            string baseName = CleanName(itemName);
            string newName = baseName;
            int nameCount = 1;
            while (items.Contains(newName))
            {
                newName = String.Concat(baseName, "_", nameCount);
                nameCount++;
            }
            items.Add(newName);

            return newName;
        }


        /// <summary>
        /// Remove illegal characters from names
        /// </summary>
        /// <param name="name">The incoming name</param>
        /// <returns>a wix friendly name</returns>
        private static string CleanName(string name)
        {
            string returnName = name;
            returnName = returnName.Replace('-', '_');
            returnName = returnName.Replace('0', 'a');
            returnName = returnName.Replace('1', 'b');
            returnName = returnName.Replace('2', 'c');
            returnName = returnName.Replace('3', 'd');
            returnName = returnName.Replace('4', 'e'); 
            returnName = returnName.Replace('5', 'f');
            returnName = returnName.Replace('6', 'g');
            returnName = returnName.Replace('7', 'h');
            returnName = returnName.Replace('8', 'i');
            returnName = returnName.Replace('9', 'j');
            return returnName;


        }

        //   <summary>
        //   writes a file fragment out
        //   </summary>
        //   <param name="files">The array of files to write a fragment for</param>
        //   <param name="source">the source directory</param>
        //   <param name="component">the component name</param>
        //   <param name="outFile">the file to write the fragment to</param>
        private static void WriteFileFragment(FileInfo[] files, string source, string baseComponent, string outFile)
        {
            int fileCount = 0;
            foreach (FileInfo file in files)
            {
                string component = GenerateNumberedItem(baseComponent, components);

                // write the component header
                WriteComponentFragment(component, outFile);

                // write the fragment string for the file

                // build the string
                string fileFragment = string.Format("<File Id='{0}File{1}' Name='{3}' Source='{2}\\{3}' Vital='yes' />", component, fileCount.ToString(), source, file.Name);
                // write to the file
                FileHelper.WriteLineToFile(fileFragment, outFile);

                // close the component
                FileHelper.WriteLineToFile("</Component>", outFile);

                fileCount++;
            }
        }

        //   <summary>
        //   Write a component header out
        //   </summary>
        //   <param name="component">The name of the component</param>
        //   <param name="outFile">The file to write to</param>
        private static void WriteComponentFragment(string component, string outFile)
        {
            // <Component Id="BaseFiles" Guid="{22BB848D-BB53-43BF-874C-88839FD3800E}" DiskId="1" KeyPath="yes">
            Guid guid = Guid.NewGuid();
            string componentFragment = string.Format("<Component Id='{0}' Guid='{1}' DiskId='1' KeyPath='yes'>", component, guid.ToString());
            FileHelper.WriteLineToFile(componentFragment, outFile);
        }


    }
}

