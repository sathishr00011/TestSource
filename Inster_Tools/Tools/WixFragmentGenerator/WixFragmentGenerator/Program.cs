using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace WixFragmentGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            string targetDir;
            Console.WriteLine("Starting");
            if (args.Length > 0)
            {
                targetDir = args[0];
            }
            else
            {
                targetDir = @"C:\SR_TFS\International\InStore\Main\Common\Tesco.Instore.Core\Assemblies";
            }

            DirectoryInfo currentDir = new DirectoryInfo(targetDir);
            DirectoryWalker walker = new DirectoryWalker();
            DirectoryWalker.WalkDirectoryTree(currentDir, false, "$(var.Web.ProjectDir)", "ApplicationFiles.wxs");
            Console.WriteLine("RunComplete");
            Console.ReadLine();
        }
    }
}
