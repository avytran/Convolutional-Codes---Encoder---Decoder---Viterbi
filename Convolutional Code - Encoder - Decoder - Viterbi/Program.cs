using Convolutional_Code___Encoder___Decoder___Viterbi;

class Program
{
    static void Main()
    {
        ///Parameters
        List<int[]> generators = new List<int[]>();
        int n = 2;
        int k = 1;
        int m = 2;

        generators.Add(new int[] { 1, 1, 1 });
        generators.Add(new int[] { 1, 0, 1 });

        int[] myInfo = new int[] { 1, 0, 1, 1 };

        ///Encode
        Encoder myEncoder = new Encoder(generators, m);
        myEncoder.Encode(myInfo);


        Console.Write("Input: ");
        foreach (int u in myInfo)
        {
            Console.Write(u);
        }
        Console.WriteLine("\n" + myEncoder.ToString());

        Console.Write("\n============================================");

        ///Decoder
        string receivedCode = "111010010111";
        Console.Write("\n\nReceived Code: ");
        for (int i = 1; i <= receivedCode.Length; i++)
        {
            Console.Write(receivedCode[i - 1]);

            if (i % n == 0)
            {
                Console.Write(" ");
            }
        }


        //Create Truth Table
        TruthTable table = new TruthTable(m, generators);
        table.GenerateTruthTable(0);
        table.listOfStates.Sort(new SortState());

        //Create Trellis
        Trellis myTrellis = new Trellis(m, table, generators);

        //Decode
        Decoder myDecoder = new Decoder(myTrellis, receivedCode, n);

        Console.Write("\n\nError Index: ");
        for (int i = 0; i < myDecoder.errors.Count; i++)
        {
            Console.Write(myDecoder.errors[i] + n);
        }

        Console.Write("\n\nFixed Received Code: ");
        for (int i = 0; i < myDecoder.path.Count; i++)
        {
            NextState nextState = (NextState)myDecoder.path[i];
            Console.Write(nextState.output + ' ');
        }
        Console.WriteLine("\n\nDecoded: " + myDecoder.decoded);


        Console.ReadKey();
    }
}
