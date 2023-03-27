using System.Collections.Generic;
using System.Linq;
// See https://aka.ms/new-console-template for more information
using RLPLibrary;

Console.WriteLine(Class1.getClassName());



Console.WriteLine("" == null);

Char charValue = char.Parse("A");
Console.WriteLine(charValue < 0x80);

double i = Char.GetNumericValue(charValue);
Console.WriteLine(i);

double j = Math.Pow(256,7);
Console.WriteLine(byte.MaxValue);
Console.WriteLine(short.MaxValue);
Console.WriteLine(long.MaxValue);
Console.WriteLine(j);
Console.WriteLine("max==================");
Console.WriteLine(j < int.MaxValue);
Console.WriteLine("==================");


Console.WriteLine(60/256);
Console.WriteLine(60 % 256);

Console.WriteLine("==================");
double a = 60;

Console.WriteLine((long)a/256);
Console.WriteLine(a % 256);
Console.WriteLine("==================");
var data = System.Text.Encoding.UTF8.GetBytes("");
foreach(byte b in data)
{
    Console.WriteLine(b);
}


Object hi = "";
Console.WriteLine(hi == "");

Console.WriteLine("BitConverter==================");
long length=3;
var lenBytes = BitConverter.GetBytes(length).Reverse().ToArray();
foreach(byte b in lenBytes)
{
    Console.WriteLine(b);
}

Console.WriteLine("TestEncoder==================");
RLPEncoder encoder = new RLPEncoder();
List<String> inputList1 = new List<string>(){"cat", "dog"};
List<List<List<object>>> myList = new List<List<List<object>>>
{
    new List<List<object>>(),
    new List<List<object>> { new List<object>() }
};

List<String> inputList2 = new List<string>(){};
String inputString = "Lorem ipsum dolor sit amet, consectetur adipisicing elit";
List<string> stringList = new List<string>(){"cat","dog"};
byte[] output = encoder.Encode(inputString);
foreach(byte b in output)
{
    Console.WriteLine(b);
}

string hexString = BitConverter.ToString(output[..2]);
Console.WriteLine(hexString);

string result = System.Text.Encoding.UTF8.GetString(output);
Console.WriteLine(result);


Console.WriteLine("TestDecoder==================");
byte[] enodedByteList1= new byte[]{200, 131, 99, 97, 116, 131, 100, 111, 103};
byte[] enodedByteList2= new byte[]{184, 56, 76, 111, 114, 101, 109, 32, 105, 112, 115, 117, 109, 32, 100, 111, 108, 111, 114, 32, 115, 105, 116, 32, 97, 109, 101, 116, 44, 32, 99, 111, 110, 115, 101, 99, 116, 101, 116, 117, 114, 32, 97, 100, 105, 112, 105, 115, 105, 99, 105, 110, 103, 32, 101, 108, 105, 116
};

 
RLPDecoder decoder = new RLPDecoder();

String charList1 = (string)decoder.Decode(enodedByteList2);
Console.Write(charList1);

// List<object> charList = (List<object>)decoder.Decode(enodedByteList1);
// foreach(object b in charList)
// {
//     Console.Write(b);
//     Console.Write(",");
// }