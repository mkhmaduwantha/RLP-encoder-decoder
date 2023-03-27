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
        Assert.AreEqual(expectedOutput[0], rLPEncoder.Encode(input)[0]);
    }
}