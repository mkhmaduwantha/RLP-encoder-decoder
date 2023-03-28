namespace RLPLibrary;

/// <summary>
/// Class to handle RLP decoding
/// </summary>
public class RLPDecoder
{
    /// <summary>
    /// Method to RLP decode, given an array of bytes for RLP encoded object
    /// </summary>
    /// <param name="input">Array of bytes for RLP encoded object</param>
    /// <returns>Decoded object</returns>
    /// <exception cref="Exception"></exception>
    public Object? Decode(byte[] input)
    {
        if (input == null || input.Length == 0)
        {
            return null;
        }

        byte firstByte = input[0];

        if (firstByte <= 0x7f)
        {
            return System.Text.Encoding.UTF8.GetString(new byte[] { firstByte });
        }

        if (firstByte <= 0xb7)
        {
            int length = firstByte - 0x80;
            byte[] byteArray = input.Skip(1).Take(length).ToArray();
            return System.Text.Encoding.UTF8.GetString(byteArray);
        }

        if (firstByte <= 0xbf)
        {
            int lengthLength = firstByte - 0xb7;
            long length = DecodeLength(input.Skip(1).Take(lengthLength).ToArray());
            byte[] byteArray = input.Skip(1 + lengthLength).Take((int)length).ToArray();
            return System.Text.Encoding.UTF8.GetString(byteArray);
        }

        if (firstByte <= 0xf7)
        {
            int length = firstByte - 0xc0;
            List<object> items = new List<object>();
            int startPointer = 1;
            return EncodeListElements(startPointer, input);
        }

        if (firstByte <= 0xff)
        {
            int lengthLength = firstByte - 0xf7;
            long length = DecodeLength(input.Skip(1).Take(lengthLength).ToArray());
            int startPointer = 1 + lengthLength;
            return EncodeListElements(startPointer, input);
        }

        throw new Exception("The given input is too long or invalid, hence can not decode.");
    }

    /// <summary>
    /// Method outputs a Decoded List given a encoded byte array of a list elements
    /// </summary>
    /// <param name="startPointer"></param>
    /// <param name="input">Byte array of a list elments</param>
    /// <returns>Decoded list of objects</returns>
    private List<object> EncodeListElements(int startPointer, byte[] input)
    {
        var items = new List<object>();
        while (startPointer < input.Length)
        {
            var (itemLength, lenOfLen) = GetItemLengthAndLengthOfLength(input.Skip(startPointer).ToArray());
            int endPointer = itemLength + startPointer + lenOfLen;
            items.Add(Decode(input.Skip(startPointer).Take(endPointer).ToArray()));
            startPointer = endPointer + 1;
        }
        return items;
    }


    /// <summary>
    /// Method outputs a integer value of the length given the byte array of binary form of the length
    /// </summary>
    /// <param name="bytes">binary representation of a the length value</param>
    /// <returns>integer value of the length</returns>
    /// <exception cref="ArgumentException">throws ArgumentException</exception>
    private int DecodeLength(byte[] bytes)
    {
        if (bytes.Length == 0)
        {
            throw new ArgumentException("Invalid binary representation of length value.");
        }

        int x = 0;
        for (int i = 0; i < bytes.Length; i++)
        {
            x = (x * 256) + bytes[i];
        }
        return x;
    }

    /// <summary>
    /// Method outputs the length and the length of the length bytes for a given encoded input byte array
    /// </summary>
    /// <param name="input">Encoded byte array</param>
    /// <returns>length and length of length</returns>
    /// <exception cref="Exception"></exception>
    public (int, int) GetItemLengthAndLengthOfLength(byte[] input)
    {
        if (input.Length == 0)
        {
            throw new Exception("The given input is null.");
        }

        byte prefix = input[0];

        if (prefix <= 0x7f)
        {
            return (1, 0);
        }
        
        if (prefix <= 0xb7)
        {
            int strLen = prefix - 0x80;
            return (strLen, 0);
        }
        
        if (prefix <= 0xbf)
        {
            int lenOfStrLen = prefix - 0xb7;
            int strLen = (int)DecodeLength(input.Skip(1).Take(lenOfStrLen).ToArray());
            return (strLen, lenOfStrLen);
        }
        
        if (prefix <= 0xf7)
        {
            int listLen = prefix - 0xc0;
            return (listLen, 0);
        }
        
        if (prefix <= 0xff)
        {
            int lenOfListLen = prefix - 0xf7;
            int listLen = (int)DecodeLength(input.Skip(1).Take(lenOfListLen).ToArray());
            return (listLen, lenOfListLen);
        }
        
        throw new Exception("The given input is either too long or invalid.");
    }
}
