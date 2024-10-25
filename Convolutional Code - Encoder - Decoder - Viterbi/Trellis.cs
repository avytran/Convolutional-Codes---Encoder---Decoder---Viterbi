using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convolutional_Code___Encoder___Decoder___Viterbi
{
    internal class TrellisNode
    {
        public State curr_state;
        public NextState next0;
        public NextState next1;

        public TrellisNode(State curr_state)
        {
            this.curr_state = curr_state;
            next0 = null;
            next1 = null;
        }

        public string getOutput(List<int[]> generators, NextState nextState)
        {
            string result = "";
            string temp = nextState.state;

            for (int i = curr_state.state.Length - 1; temp.Length < generators[0].Length; i--)
            {
                temp += curr_state.state[i];
            }

            for (int i = 0; i < generators.Count; i++)
            {
                int calculate = 0;
                for (int j = 0; j < generators[0].Length; j++)
                {
                    int num = temp[j] - '0';
                    calculate += generators[i][j] * num;
                }

                calculate = calculate % 2;
                result += calculate;
            }
            return result;
        }

        public override string ToString()
        {
            if (next0 != null && next0 != null)
            {
                return $"{curr_state.Id}: \t{next0.Id}\t{next1.Id} \t\t{next0.output}\t{next1.output}";
            }

            return "";
        }
    }
    internal class Trellis
    {
        public List<TrellisNode> trellisNodes = new List<TrellisNode>();
        public TruthTable truthTable;
        List<int[]> generators;
        public Queue<int> register0 = new Queue<int>();
        public Queue<int> register1 = new Queue<int>();


        public Trellis(int m, TruthTable truthTable, List<int[]> generators)
        {
            this.truthTable = truthTable;
            this.generators = generators;

            for (int i = 0; i < truthTable.listOfStates.Count; i++)
            {
                State currentState = truthTable.findStateById(i);

                TrellisNode currentNode = new TrellisNode(currentState);

                setRegister(currentState, register0);
                setRegister(currentState, register1);

                currentNode.next0 = new NextState(getNextState(0, register0));
                currentNode.next1 = new NextState(getNextState(1, register1));

                currentNode.next0.output = currentNode.getOutput(generators, currentNode.next0);
                currentNode.next1.output = currentNode.getOutput(generators, currentNode.next1);


                trellisNodes.Add(currentNode);
            }

        }

        public void setRegister(State currentState, Queue<int> register)
        {
            for (int i = currentState.state.Length - 1; i >= 0; i--)
            {
                int number = currentState.state[i] - '0';
                register.Enqueue(number);
            }
        }
        public State getNextState(int input, Queue<int> register)
        {
            register.Dequeue();
            register.Enqueue(input);

            string tempResult = "";

            foreach (int i in register)
            {
                tempResult += i;
            }

            string result = "";

            for (int i = tempResult.Length - 1; i >= 0; i--)
            {
                result += tempResult[i];
            }

            register.Clear();

            return truthTable.findState(result);
        }

        public override string ToString()
        {
            string result = "";
            foreach (TrellisNode node in trellisNodes)
            {
                result += node.ToString() + "\n";
            }

            return result;
        }
    }
}
