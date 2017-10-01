using WixFragmentGenerator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace WixFragmentGenerator.Test
{
    
    
    /// <summary>
    ///This is a test class for FileHelperTest and is intended
    ///to contain all FileHelperTest Unit Tests
    ///</summary>
    [TestClass()]
    public class FileHelperTest
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
        ///A test for WriteLineToFile
        ///</summary>
        [TestMethod()]
        public void WriteLineToFileTest()
        {
            string lineToWrite = "Line to Write Test";
            string filePath = "WriteLineToFileTest"+DateTime.Now.ToString("ddMMMyyyyHHmmss")+".txt";
            bool expected = true;
            bool actual;
            actual = FileHelper.WriteLineToFile(lineToWrite, filePath);
            Assert.AreEqual(expected, actual);

            TextReader tr = new StreamReader(filePath);
            string inFile = tr.ReadToEnd();
            Assert.AreEqual(lineToWrite+"\r\n", inFile);
        }

        /// <summary>
        /// A test for WriteHeader
        /// Is an integration test
        ///</summary>
        [TestMethod()]
        public void WriteHeaderTest()
        {
            string filePath = "WriteHeaderTest.xml";
            FileHelper.WriteHeader(filePath);
            FileInfo file = new FileInfo(filePath);
            Assert.IsTrue(file.Exists);
            TextReader tr = new StreamReader(filePath);
            string inFile = tr.ReadToEnd();
           
            Assert.AreEqual(inFile.ToString(), TestData.ResourceManager.GetString("HeaderString"));

        }

        /// <summary>
        ///A test for FileHelper Constructor
        ///</summary>
        [TestMethod()]
        public void FileHelperConstructorTest()
        {
            FileHelper target = new FileHelper();
            Assert.IsInstanceOfType(target, typeof(FileHelper));
        }
    }
}
