namespace RLPLibrary;

/// <summary>
/// Utlity class to help RLP encoding - decoding
/// </summary>
public static class RLPUtil
{

    /// <summary>
    /// Method outputs an byte array that represents the binary form of a given number
    /// </summary>
    /// <param name="number">Number to be convert into binary form</param>
    /// <returns>a byte array of binary representation</returns>
    public static byte[] GetBinaryByteArray(long number)
    {
        if (number == 0)
        {
            return new byte[0];
        }
        else
        {
            byte[] bytes = GetBinaryByteArray(number / 256);
            byte[] result = new byte[bytes.Length + 1];
            bytes.CopyTo(result, 0);
            result[bytes.Length] = (byte)(number % 256);
            return result;
        }
    }
}