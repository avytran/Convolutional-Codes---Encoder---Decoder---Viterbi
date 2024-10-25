using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convolutional_Code___Encoder___Decoder___Viterbi
{
    internal class Decoder
    {
        public List<TrellisNode> trellisNodes;
        public string[] received;
        public List<State> path;
        public string decoded;
        public List<int> errors = new List<int>();

        public Decoder(Trellis trellis, string receivedCode, int n)
        {
            this.trellisNodes = trellis.trellisNodes;
            this.received = new string[receivedCode.Length / n];

            //Convert string -> string[]
            int j = 0;
            for (int i = 0; i < received.Length; i++)
            {
                while (received[i] == null || received[i].Length < n)
                {
                    this.received[i] += receivedCode[j];
                    j++;
                }
            }
            path = Decode();
            errors = findErrorIndex();
        }
        public List<State> Decode()
        {
            int totalDistance = 0;
            List<State> path = new List<State>();

            int[] tempDistance = new int[2];

            for (int i = 0; i < received.Length; i++)
            {
                List<State> currStates = new List<State>();
                if (path.Count == 0)
                {
                    currStates = TraverseTrellis(received[i], 0, ref totalDistance, tempDistance);
                }
                else
                {
                    currStates = TraverseTrellis(received[i], path[path.Count - 1].Id, ref totalDistance, tempDistance);
                }

                if (currStates.Count == 1)
                {
                    path.Add(currStates[0]);
                }
                else
                {
                    int selected = comparePath(currStates[0].Id, currStates[1].Id, received[i + 1]);

                    if (selected == 0)
                    {
                        path.Add(currStates[currStates.Count - 2]);
                        decoded += '0';
                        totalDistance += tempDistance[0];
                    }
                    else
                    {
                        path.Add(currStates[currStates.Count - 1]);
                        decoded += '1';
                        totalDistance += tempDistance[1];
                    }

                }

            }

            return path;

        }

        //Continue Improving
        public List<State> TraverseTrellis(string currCode, int rootIndex, ref int totalDistance, int[] tempDistance)
        {
            List<State> possibleStates = new List<State>();
            string path0 = trellisNodes[rootIndex].next0.output;
            string path1 = trellisNodes[rootIndex].next1.output;

            int hamming0 = calculateHamming(currCode, path0);
            int hamming1 = calculateHamming(currCode, path1);

            if (hamming0 < hamming1)
            {
                State state0 = trellisNodes[rootIndex].next0;
                possibleStates.Add(state0);
                totalDistance += hamming0;
                decoded += '0';
            }
            else if (hamming1 < hamming0)
            {
                State state1 = trellisNodes[rootIndex].next1;
                possibleStates.Add(state1);
                totalDistance += hamming1;
                decoded += '1';
            }
            else
            {
                State state0 = trellisNodes[rootIndex].next0;
                State state1 = trellisNodes[rootIndex].next1;

                possibleStates.Add(state0);
                possibleStates.Add(state1);

                tempDistance[0] = hamming0;
                tempDistance[1] = hamming1;
            }


            return possibleStates;
        }

        public int comparePath(int option0, int option1, string code)
        {
            string path00 = trellisNodes[option0].next0.output;
            string path01 = trellisNodes[option0].next1.output;
            int selected0 = 0;

            string path10 = trellisNodes[option1].next0.output;
            string path11 = trellisNodes[option1].next1.output;
            int selected1 = 0;

            if (calculateHamming(path00, code) < calculateHamming(path01, code))
                selected0 = calculateHamming(path00, code);
            else selected0 = calculateHamming(path01, code);

            if (calculateHamming(path10, code) < calculateHamming(path11, code))
                selected1 = calculateHamming(path10, code);
            else selected1 = calculateHamming(path11, code);

            if (selected0 < selected1)
                return 0;
            else return 1;


        }

        public int calculateHamming(string x, string y)
        {
            int distance = 0;

            for (int i = 0; i < x.Length; i++)
            {
                if (x[i] != y[i])
                    distance++;
            }

            return distance;
        }

        public List<int> findErrorIndex()
        {
            string[] fixedreceived = new string[received.Length];
            for (int i = 0; i < path.Count; i++)
            {
                NextState nextState = (NextState)path[i];
                fixedreceived[i] = nextState.output;
            }

            for (int i = 0; i < received.Length; i++)
            {
                if (received[i] != fixedreceived[i])
                {
                    errors.Add(i);
                }
            }

            return errors;
        }
    }
}
