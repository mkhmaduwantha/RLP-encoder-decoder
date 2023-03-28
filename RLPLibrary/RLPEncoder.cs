namespace RLPLibrary;

/// <summary>
/// Class to handle RLP encoder
/// </summary>
public class RLPEncoder
{
    /// <summary>
    /// method to RLP encode, given a byte array, string, int or a list
    /// </summary>
    /// <param name="input">input object to be encoded</param>
    /// <returns>byte array of encoded object</returns>
    /// <exception cref="NotImplementedException"></exception>
    public byte[] Encode(Object input)
    {
        if (input == null)
        {
            return new byte[] { 0x80 };
        }
        
        if (input is String)
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
            return EncodeLength(data.Length, 0x80).Concat(data).ToArray();
        }

        if (input is byte[])
        {
            var data = (byte[])input;
            if (data.Length == 1 && data[0] < 0x80)
            {
                return data;
            }
            return EncodeLength(data.Length, 0x80).Concat(data).ToArray();
        }

        if (input is int)
        {
            if ((int)input == 0)
            {
                return new byte[] { 0x80 };
            }
            var data = RLPUtil.GetBinaryByteArray((long)input);
            if (data.Length == 1 && data[0] < 0x80)
            {
                return data;
            }
            return EncodeLength(data.Length, 0x80).Concat(data).ToArray();
        }
        
        if (input is IEnumerable<object>)
        {
            var inputList = (IEnumerable<object>)input;
            if (!inputList.Any())
            {
                return new byte[] { 0xc0 };
            }
            var encodedItems = (inputList).Select(item => Encode(item)).ToArray();
            var data = encodedItems.SelectMany(item => item).ToArray();
            return EncodeLength(data.Length, 0xc0).Concat(data).ToArray();
        }
        
        throw new NotImplementedException("The given data type can not be encoded, not implemented.");
        
    }

    /// <summary>
    /// Method that encodes the length value and outputs a byte array
    /// </summary>
    /// <param name="length">length of the enocded items</param>
    /// <param name="offset">offset value according to RLP algorithm</param>
    /// <returns>byte array of the encoded length</returns>
    /// <exception cref="Exception"></exception>
    private byte[] EncodeLength(long length, byte offset)
    {
        if (length < 56)
        {
            return new byte[] { ((byte)(length + offset)) };
        }
        else if (length < Math.Pow(256, 8))
        {
            var lenBytes = RLPUtil.GetBinaryByteArray(length);
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