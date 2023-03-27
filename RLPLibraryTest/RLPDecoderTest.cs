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
}