using WixFragmentGenerator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;

namespace WixFragmentGenerator.Test
{
    
    
    /// <summary>
    ///This is a test class for DirectoryWalkerTest and is intended
    ///to contain all DirectoryWalkerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class DirectoryWalkerTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for DirectoryWalker Constructor
        ///</summary>
        [TestMethod()]
        public void DirectoryWalkerConstructorTest()
        {
            DirectoryWalker target = new DirectoryWalker();
            Assert.IsInstanceOfType(target, typeof(DirectoryWalker));
        }

        /// <summary>
        ///A test for CleanName
        ///</summary>
        [TestMethod()]
        [DeploymentItem("WixFragmentGenerator.exe")]
        public void CleanNameTest()
        {
            string name = "-0123456789";
            string expected = "_abcdefghij";
            string actual;
            actual = DirectoryWalker_Accessor.CleanName(name);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GenerateNumberedItem
        ///</summary>
        [TestMethod()]
        [DeploymentItem("WixFragmentGenerator.exe")]
        public void GenerateNumberedItemTest()
        {
            string itemName = "test";
            List<string> items = new List<string>{"test", "test_1"};
            string expected = "test_2";
            string actual;
            actual = DirectoryWalker_Accessor.GenerateNumberedItem(itemName, items);
            Assert.AreEqual(expected, actual);
        }

        ///// <summary>
        /////A test for WalkDirectoryTree
        /////</summary>
        //[TestMethod()]
        //public void WalkDirectoryTreeTest()
        //{
        //    DirectoryInfo root = null; // TODO: Initialize to an appropriate value
        //    bool child = false; // TODO: Initialize to an appropriate value
        //    string source = string.Empty; // TODO: Initialize to an appropriate value
        //    string fileToWriteTo = string.Empty; // TODO: Initialize to an appropriate value
        //    DirectoryWalker.WalkDirectoryTree(root, child, source, fileToWriteTo);
        //    Assert.Inconclusive("A method that does not return a value cannot be verified.");
        //}
        //
        ///// <summary>
        /////A test for WriteComponentFragment
        /////</summary>
        //[TestMethod()]
        //[DeploymentItem("WixFragmentGenerator.exe")]
        //public void WriteComponentFragmentTest()
        //{
        //    string component = string.Empty; // TODO: Initialize to an appropriate value
        //    string outFile = string.Empty; // TODO: Initialize to an appropriate value
        //    DirectoryWalker_Accessor.WriteComponentFragment(component, outFile);
        //    Assert.Inconclusive("A method that does not return a value cannot be verified.");
        //}

        ///// <summary>
        /////A test for WriteComponentGroupRef
        /////</summary>
        //[TestMethod()]
        //[DeploymentItem("WixFragmentGenerator.exe")]
        //public void WriteComponentGroupRefTest()
        //{
        //    string fileToWriteTo = string.Empty; // TODO: Initialize to an appropriate value
        //    DirectoryWalker_Accessor.WriteComponentGroupRef(fileToWriteTo);
        //    Assert.Inconclusive("A method that does not return a value cannot be verified.");
        //}

        ///// <summary>
        /////A test for WriteFileFragment
        /////</summary>
        //[TestMethod()]
        //[DeploymentItem("WixFragmentGenerator.exe")]
        //public void WriteFileFragmentTest()
        //{
        //    FileInfo[] files = null; // TODO: Initialize to an appropriate value
        //    string source = string.Empty; // TODO: Initialize to an appropriate value
        //    string component = string.Empty; // TODO: Initialize to an appropriate value
        //    string outFile = string.Empty; // TODO: Initialize to an appropriate value
        //    DirectoryWalker_Accessor.WriteFileFragment(files, source, component, outFile);
        //    Assert.Inconclusive("A method that does not return a value cannot be verified.");
        //}

        ///// <summary>
        /////A test for writeComponentFiles
        /////</summary>
        //[TestMethod()]
        //[DeploymentItem("WixFragmentGenerator.exe")]
        //public void writeComponentFilesTest()
        //{
        //    DirectoryInfo root = null; // TODO: Initialize to an appropriate value
        //    bool child = false; // TODO: Initialize to an appropriate value
        //    string source = string.Empty; // TODO: Initialize to an appropriate value
        //    string fileToWriteTo = string.Empty; // TODO: Initialize to an appropriate value
        //    FileInfo[] files = null; // TODO: Initialize to an appropriate value
        //    DirectoryWalker_Accessor.writeComponentFiles(root, child, source, fileToWriteTo, files);
        //    Assert.Inconclusive("A method that does not return a value cannot be verified.");
        //}
    }
}
