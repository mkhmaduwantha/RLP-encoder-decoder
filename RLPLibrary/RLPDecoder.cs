using System.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RLPLibrary;

public interface IRLPDecoder
{
    public Object Decode(byte[] input);
}

public class RLPDecoder : IRLPDecoder
{
    public Object Decode(byte[] input)
    {
        if (input == null || input.Length == 0)
        {
            return null;
        }

        byte firstByte = input[0];
        if (firstByte <= 0x7f)
        {
            string utfString = System.Text.Encoding.UTF8.GetString(new byte[] { firstByte });
            Console.WriteLine(utfString);
            return utfString;
        }

        if (firstByte <= 0xb7)
        {
            int length = firstByte - 0x80;
            byte[] byteArray =  input.Skip(1).Take(length).ToArray();

            string utfString = System.Text.Encoding.UTF8.GetString(byteArray);
            Console.WriteLine(utfString);
            // Console.WriteLine(BitConverter.ToString(byteArray));
            return utfString;
        }

        if (firstByte <= 0xbf)
        {
            int lengthLength = firstByte - 0xb7;
            long length = DecodeLength(input.Skip(1).Take(lengthLength).ToArray());
            //following explicit cast- length can be higher than int value
            byte[] byteArray = input.Skip(1 + lengthLength).Take((int)length).ToArray();
            string utfString = System.Text.Encoding.UTF8.GetString(byteArray);
            Console.WriteLine(utfString);
            return utfString;
        }

        if (firstByte <= 0xf7)
        {
            int length = firstByte - 0xc0;
            List<object> items = new List<object>();
            int offset = 1;
            while (offset < input.Length)
            {
                int itemLength =  GetItemOffset(input.Skip(offset).ToArray());
                int itemLengthWithPrefix = itemLength +1;
                items.Add(Decode(input.Skip(offset).Take(itemLengthWithPrefix).ToArray()));
                offset += itemLengthWithPrefix;
                // byte[] itemData = input.Skip(offset).Take(length).ToArray();
                // object item = Decode(itemData);
            }
            return items;
            // return System.Text.Encoding.UTF8.GetString(input.Skip(1).Take(length).ToArray()).ToList();
        }
        if (firstByte <= 0xff)
        {
            int lengthLength = firstByte - 0xf7;
            long length = DecodeLength(input.Skip(1).Take(lengthLength).ToArray());
            var items = new List<object>();
            int pos = 1 + lengthLength;
            while (pos < input.Length)
            {
                int itemLength = GetItemOffset(input.Skip(pos).ToArray());
                items.Add(Decode(input.Skip(pos).Take(itemLength).ToArray()));
                pos += itemLength;
            }
            return items.ToArray();
        }

        throw new Exception("Invalid Enocde");
    }

    private static long DecodeLength(byte[] lengthBytes)
    {
        if (lengthBytes.Length == 0)
        {
            return 0;
        }

        if (lengthBytes[0] < 0x80)
        {
            return lengthBytes[0];
        }

        int lengthLength = lengthBytes[0] - 0x80;
        return BitConverter.ToInt32(lengthBytes.Skip(1).Take(lengthLength).Reverse().Concat(new byte[] { 0 }).ToArray(), 0);
    }

    public static int GetItemOffset(byte[] input)
    {
        int length = input.Length;
        if (length == 0)
        {
            throw new Exception("Input is null");
        }

        byte prefix = input[0];
        if (prefix <= 0x7f)
        {
            return 1;
        }
        else if (prefix <= 0xb7)
        {
            int strLen = prefix - 0x80;
            return strLen;
        }
        else if (prefix <= 0xbf)
        {
            int lenOfStrLen = prefix - 0xb7;
            int strLen = (int)DecodeLength(input.Skip(1).Take(lenOfStrLen).ToArray());
            return strLen;
        }
        else if (prefix <= 0xf7)
        {
            int listLen = prefix - 0xc0;
            return listLen;
        }
        else if (prefix <= 0xff)
        {
            int lenOfListLen = prefix - 0xf7;
            int listLen = (int)DecodeLength(input.Skip(1).Take(lenOfListLen).ToArray());
            return listLen;
        }
        else
        {
            throw new Exception("Input does not conform to RLP encoding form");
        }
    }
}