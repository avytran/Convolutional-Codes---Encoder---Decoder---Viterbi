using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convolutional_Code___Encoder___Decoder___Viterbi
{
    internal class State
    {
        public int Id;
        public string state;

        public State() { }

        public State(string state)
        {

        }

        public State(string state, List<int[]> generators)
        {
            this.state = state;
            int id = 0;
            for (int i = 0; i < state.Length; i++)
            {
                id += (int)Math.Pow(2, i) * getBit(state[i]);
            }
            Id = id;
        }

        public int getBit(char bit)
        {
            switch (bit)
            {
                case '0':
                    return 0;
                case '1':
                    return 1;
                default:
                    return -1;
            }
        }

        public override string ToString()
        {
            return $"{Id}:\t {state}";
        }
    }

    class SortState : IComparer<State>
    {
        public int Compare(State a, State b)
        {
            State stateX = a;
            State stateY = b;

            if (stateX.Id > stateY.Id)
                return 1;
            else
                return -1;

        }
    }
}
