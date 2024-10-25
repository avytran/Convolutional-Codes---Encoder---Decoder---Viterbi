using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convolutional_Code___Encoder___Decoder___Viterbi
{
    internal class NextState : State
    {
        public string output;
        public NextState() { }

        public NextState(State nextState)
        {
            this.Id = nextState.Id;
            this.state = nextState.state;
        }
    }
}
