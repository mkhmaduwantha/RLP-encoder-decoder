using RLPLibrary;

internal class RLPApplication
{
    private static void Main(string[] args)
    {
        // example-use-case
        List<object> inputList = new List<object>() {"cat", "Lorem ipsum dolor sit amet, consectetur adipisicing elit", new List<object>{"dog"}};
        //using strings.join

        RLPEncoder rLPEncoder = new RLPEncoder();
        byte[] encodedObject = rLPEncoder.Encode(inputList);
        Console.WriteLine("--------------------------------------------------------------");
        Console.WriteLine("Byte array of encoded object;");
        Console.WriteLine(string.Join(", ", encodedObject));

        RLPDecoder rLPDecoder = new RLPDecoder();
        List<object> decodedObject = (List<object>)rLPDecoder.Decode(encodedObject);
        Console.WriteLine("--------------------------------------------------------------");
        Console.WriteLine("Length of the decoded list of objects :" + decodedObject.Count);
        Console.WriteLine("First element of the decoded list of objects :" + decodedObject[0]);
        Console.WriteLine("Second element of the decoded list of objects :" + decodedObject[1]);
        Console.WriteLine("Type of third element of the decoded list of objects :" + decodedObject[2].GetType());
        List<object> thirdElement = (List<object>)decodedObject[2];
        Console.WriteLine("Length of third element of the decoded list of objects :" + thirdElement.Count);
        Console.WriteLine("first element of third element of the decoded list of objects :" + thirdElement[0]);
        
    }
}