using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using TransmaxGrading.DataModels;
using TransmaxGrading.Logger;

namespace TransmaxGrading.Tests
{
    [TestClass]
    public class TestValidator
    {
        Validator validator;
        Processor processor;

        string filePath = @"TestCases\";

        public TestValidator()
        {
            ILogger logger = new FileLogger();

            validator = new Validator(logger);

            processor = new Processor(logger);
        }


        [TestMethod]
        public void TestFileDoesntExist()
        {
            List<Scores> scores;
            bool result = validator.ValidateAndParse(filePath + "xxyyzz.txt", out scores);

            Assert.AreEqual(result, false, "File validation passed");
        }

        [TestMethod]
        public void TestEmptyFile()
        {
            List<Scores> scores;
            bool result = validator.ValidateAndParse(filePath + "emptyfile.txt", out scores);

            Assert.AreEqual(result, false, "File validation passed");
        }

        [TestMethod]
        public void TestEmptyStudentName1()
        {
            List<Scores> scores;
            bool result = validator.ValidateAndParse(filePath + "emptystudentname1.txt", out scores);

            Assert.AreEqual(result, false, "File validation passed");
        }

        [TestMethod]
        public void TestEmptyStudentName2()
        {
            List<Scores> scores;
            bool result = validator.ValidateAndParse(filePath + "emptystudentname2.txt", out scores);

            Assert.AreEqual(result, false, "File validation passed");
        }

        [TestMethod]
        public void TestEmptyScoreWithComma()
        {
            List<Scores> scores;
            bool result = validator.ValidateAndParse(filePath + "emptyscorewithcomma.txt", out scores);

            Assert.AreEqual(result, false, "File validation passed");
        }

        [TestMethod]
        public void TestEmptyScoreWithoutComma()
        {
            List<Scores> scores;
            bool result = validator.ValidateAndParse(filePath + "emptyscorewithoutcomma.txt", out scores);

            Assert.AreEqual(result, false, "File validation passed");
        }

        [TestMethod]
        public void TestNameAfterScore()
        {
            List<Scores> scores;
            bool result = validator.ValidateAndParse(filePath + "nameafterscore.txt", out scores);

            Assert.AreEqual(result, false, "File validation passed");
        }

        [TestMethod]
        public void TestEmptyLineInMiddle()
        {
            List<Scores> scores;
            bool result = validator.ValidateAndParse(filePath + "emptylineinmiddle.txt", out scores);

            Assert.AreEqual(result, false, "File validation passed");
        }

        [TestMethod]
        public void TestTwoNumbersInLine()
        {
            List<Scores> scores;
            bool result = validator.ValidateAndParse(filePath + "twonumbers.txt", out scores);

            Assert.AreEqual(result, true, "File validation failed");

            scores = processor.OrderList(scores);

            processor.SaveNewFile("twonumbers-graded.txt", scores);
        }        

        [TestMethod]
        public void TestProperFormatFile()
        {
            List<Scores> scores;
            bool result = validator.ValidateAndParse(filePath + "properformat.txt", out scores);

            Assert.AreEqual(result, true, "File validation failed");

            Assert.IsTrue(scores.Count > 0, "File content not parsed correctly");

            scores = processor.OrderList(scores);

            processor.SaveNewFile("properformat-graded.txt", scores);
        }
    }
}
