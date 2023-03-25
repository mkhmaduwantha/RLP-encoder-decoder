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
double length=65;
var lenBytes = BitConverter.GetBytes(length).Reverse().ToArray();
foreach(byte b in lenBytes)
{
    Console.WriteLine(b);
}

