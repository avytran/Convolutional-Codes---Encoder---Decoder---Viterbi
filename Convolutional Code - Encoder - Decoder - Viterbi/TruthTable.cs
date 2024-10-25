using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convolutional_Code___Encoder___Decoder___Viterbi
{
    internal class TruthTable
    {
        public int[] states;
        public int stateCount;
        public int elementCount;
        public int[] currentCombination;
        public List<State> listOfStates = new List<State>();
        public List<int[]> generators = new List<int[]>();
        public TruthTable(int elementCount, List<int[]> generators)
        {
            states = new int[2] { 0, 1 };
            stateCount = states.Length;
            this.elementCount = elementCount;
            currentCombination = new int[elementCount];
            this.generators = generators;
        }
        public void GenerateTruthTable(int currentIndex)
        {
            if (currentIndex == elementCount)
            {
                string curr_state = "";
                for (int i = 0; i < currentCombination.Length; i++)
                {
                    curr_state += currentCombination[i];
                }

                listOfStates.Add(new State(curr_state, generators));
            }
            else
            {
                for (int j = 0; j < stateCount; j++)
                {
                    currentCombination[currentIndex] = j;
                    GenerateTruthTable(currentIndex + 1);
                }
            }
        }
        public State findStateById(int id)
        {
            foreach (State state in listOfStates)
            {
                if (state.Id == id)
                    return state;
            }

            return null;
        }
        public State findState(string state)
        {
            foreach (State s in listOfStates)
            {
                if (s.state == state)
                    return s;
            }

            return null;
        }
    }
}
