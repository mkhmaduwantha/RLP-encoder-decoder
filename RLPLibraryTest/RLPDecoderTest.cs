using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RLPLibrary;

namespace RLPLibraryTest;

[TestClass]
public class RLPDecoderTest
{

    [TestMethod]
    public void Decode_WithEmptyString()
    {
        RLPDecoder rLPDecoder = new RLPDecoder();
        String expectedOutput = "";
        byte[] input = new byte[] { 0x80 };
        Assert.AreEqual(expectedOutput, rLPDecoder.Decode(input));
    }

    [TestMethod]
    public void Decode_WithStringLengthLessThan55()
    {
        RLPDecoder rLPDecoder = new RLPDecoder();
        String expectedString1 = "dog";
        byte[] stringByteArray = System.Text.Encoding.UTF8.GetBytes(expectedString1);
        byte[] inputArray = (new byte[] { (byte)(0x80 + stringByteArray.Length) }).Concat(stringByteArray).ToArray();
        String actualOutput = (string)rLPDecoder.Decode(inputArray);
        Assert.AreEqual(expectedString1, actualOutput);
    }

    [TestMethod]
    public void Decode_WithStringLengthMoreThan55()
    {
        RLPDecoder rLPDecoder = new RLPDecoder();
        String expectedString1 = "Lorem ipsum dolor sit amet, consectetur adipisicing elit";
        byte[] stringByteArray = System.Text.Encoding.UTF8.GetBytes(expectedString1);
        byte[] inputArray = (new byte[] {  0xb8, 0x38 }).Concat(stringByteArray).ToArray();
        String actualOutput = (string)rLPDecoder.Decode(inputArray);
        Assert.AreEqual(expectedString1, actualOutput);
    }

    [TestMethod]
    public void Decode_WithListLengthLessThan55()
    {
        RLPEncoder rLPEncoder = new RLPEncoder();
        List<String> ecpectedList = new List<string>(){"cat","dog"};
        byte[] inputByteArray = rLPEncoder.Encode(ecpectedList);

        RLPDecoder rLPDecoder = new RLPDecoder();
        List<Object> actualOutput = (List<Object>)rLPDecoder.Decode(inputByteArray);
        Assert.AreEqual(2, actualOutput.Count);
        for (int i=0; i < ecpectedList.Count; i++)
        {
            Assert.AreEqual(ecpectedList[i], actualOutput[i]);
        }
    }

    [TestMethod]
    public void Decode_WithListLengthMoreThan55()
    {
        RLPEncoder rLPEncoder = new RLPEncoder();
        List<String> ecpectedList = new List<string>() { "cat", "cat", "cat", "cat", "cat", "cat", "cat", "cat", "cat", "cat",
        "cat", "cat", "cat", "cat", "cat", "cat", "cat", "cat", "cat", "cat",
        "cat", "cat", "cat", "cat", "cat", "cat", "cat", "cat", "cat", "cat",
        "cat", "cat", "cat", "cat", "cat", "cat", "cat", "cat", "cat", "cat",
        "cat", "cat", "cat", "cat", "cat", "cat", "cat", "cat", "cat", "cat",
        "cat", "cat", "cat", "cat", "cat", "cat", "cat", "cat", "cat", "cat" };
        byte[] inputByteArray = rLPEncoder.Encode(ecpectedList);

        RLPDecoder rLPDecoder = new RLPDecoder();
        List<Object> actualOutput = (List<Object>)rLPDecoder.Decode(inputByteArray);
        Assert.AreEqual(60, actualOutput.Count);
        for (int i=0; i < ecpectedList.Count; i++)
        {
            Assert.AreEqual(ecpectedList[i], actualOutput[i]);
        }
    }

    [TestMethod]
    public void Decode_WithList_WithStringLengthMoreThan55()
    {
        RLPEncoder rLPEncoder = new RLPEncoder();
        List<String> ecpectedList = new List<string>() {"cat", "Lorem ipsum dolor sit amet, consectetur adipisicing elit" };
        byte[] inputByteArray = rLPEncoder.Encode(ecpectedList);

        RLPDecoder rLPDecoder = new RLPDecoder();
        List<Object> actualOutput = (List<Object>)rLPDecoder.Decode(inputByteArray);
        Assert.AreEqual(2, actualOutput.Count);
        for (int i=0; i < ecpectedList.Count; i++)
        {
            Assert.AreEqual(ecpectedList[i], actualOutput[i]);
        }
    }
}