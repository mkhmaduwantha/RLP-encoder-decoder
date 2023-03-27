using System.Linq.Expressions;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RLPLibrary;

namespace RLPLibraryTest;

[TestClass]
public class RLPEncoderTest
{

    [TestMethod]
    public void Encode_WithEmptyString()
    {
        RLPEncoder rLPEncoder = new RLPEncoder();
        String input = "";
        byte[] expectedOutput = new byte[] { 0x80 };
        Assert.AreEqual(1, rLPEncoder.Encode(input).Length);
        Assert.AreEqual(expectedOutput[0], rLPEncoder.Encode(input)[0]);
    }

    [TestMethod]
    public void Encode_WithIntegers()
    {
        RLPEncoder rLPEncoder = new RLPEncoder();
        int input1 = 0;
        byte[] expectedOutput = new byte[] { 0x80 };
        Assert.AreEqual(1, rLPEncoder.Encode(input1).Length);
        Assert.AreEqual(expectedOutput[0], rLPEncoder.Encode(input1)[0]);
    }

    [TestMethod]
    public void Encode_WithStringLengthLessThan55()
    {
        RLPEncoder rLPEncoder = new RLPEncoder();
        String inputString = "dog";
        byte[] inputByteArray = System.Text.Encoding.UTF8.GetBytes(inputString);
        byte[] expectedOutput = (new byte[] { (byte)(0x80 + inputByteArray.Length) }).Concat(inputByteArray).ToArray();
        byte[] actualOutput = rLPEncoder.Encode(inputString);
        Assert.AreEqual(4, actualOutput.Length);
        for (int i=0; i < 4; i++)
        {
            Assert.AreEqual(expectedOutput[i], actualOutput[i]);
        }
    }

    [TestMethod]
    public void Encode_WithStringLengthMoreThan55()
    {
        RLPEncoder rLPEncoder = new RLPEncoder();
        String inputString = "Lorem ipsum dolor sit amet, consectetur adipisicing elit";
        byte[] inputStringByteArray = System.Text.Encoding.UTF8.GetBytes(inputString);
        byte[] actualOutput = rLPEncoder.Encode(inputString);
        Assert.AreEqual(58, actualOutput.Length);
        Assert.AreEqual(56, actualOutput[1]);
        for (int i=0; i < inputStringByteArray.Length; i++)
        {
            Assert.AreEqual(inputStringByteArray[i], actualOutput[i+2]);
        }
    }

    [TestMethod]
    public void Encode_WithListLengthLessThan55()
    {
        RLPEncoder rLPEncoder = new RLPEncoder();
        List<String> inputList = new List<string>(){"cat","dog"};

        byte[] firstItemEnodedArray = rLPEncoder.Encode("cat");
        byte[] secondItemEnodedArray = rLPEncoder.Encode("dog");

        byte[] expectedByteArray = (new byte[] { (byte)(0xc0 + firstItemEnodedArray.Length + secondItemEnodedArray.Length) })
        .Concat(firstItemEnodedArray)
        .Concat(secondItemEnodedArray)
        .ToArray();
        byte[] actualOutput = rLPEncoder.Encode(inputList);
        Assert.AreEqual(9, actualOutput.Length);
        for (int i=0; i < expectedByteArray.Length; i++)
        {
            Assert.AreEqual(expectedByteArray[i], actualOutput[i]);
        }
    }

}