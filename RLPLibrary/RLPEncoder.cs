using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RLPLibrary;

public interface IRLPEncoder
{
    public byte[] Encode(Object input);
}

public class RLPEncoder : IRLPEncoder
{

    public byte[] Encode(Object input)
    {
        if (input == null)
        {
            return new byte[] { 0x80 };
        }
        else if (input is String)
        {
            if ((string)input == "")
            {
                return new byte[] { 0x80 };
            }

            var data = System.Text.Encoding.UTF8.GetBytes((string)input);
            if (data.Length == 1 && data[0] < 0x80)
            {
                return data;
            } 
            return EncodeLength(data.Length, 0x80) 
                   .Concat(data).ToArray();

        } 
        else if (input is byte[])
        {
            var data = (byte[])input;
            if (data.Length == 1 && data[0] < 0x80)
            {
                return data;
            }
            return EncodeLength(data.Length, 0x80) 
                   .Concat(data).ToArray();
        }
        else if (input is int)
        {
            var data = BitConverter.GetBytes((int)input).Reverse().ToArray();
            if (data.Length == 1 && data[0] < 0x80)
            {
                return data;
            }
            return EncodeLength(data.Length, 0x80) 
                   .Concat(data).ToArray();
        }
        else
        {
            throw new NotImplementedException();
        }
    }

    public byte[] EncodeLength(double length, byte offset)
    {
        if (length < 56)
        {
            return new byte[]{ ((byte)(length + offset)) };
        }
        else if (length < Math.Pow(256,8))
        {
            var lenBytes = BitConverter.GetBytes(length).Reverse().ToArray();
            var lenBytesLength = lenBytes.Length;
            return new byte[] { (byte)(offset + 55 + lenBytesLength) }
                   .Concat(lenBytes).ToArray();
        }
        else
        {
            throw new Exception("Input is too long");
        }
    }
}