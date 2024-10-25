using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convolutional_Code___Encoder___Decoder___Viterbi
{
    internal class Encoder
    {
        public List<int[]> generators = new List<int[]>();
        public string[] result;
        public int m;

        public Encoder(List<int[]> generators, int m)
        {
            this.generators = generators;
            this.m = m;
        }

        //Encode
        public void Encode(int[] infos)
        {
            result = getTemplateResult<string>(infos, m);

            for (int i = 0; i < generators.Count; i++)
            {
                int[] curr_result = getTemplateResult<int>(infos, m);

                for (int k = 0; k < infos.Length; k++)
                {
                    for (int j = 0; j < generators[i].Length; j++)
                    {
                        curr_result[k + j] = (curr_result[k + j] + ((infos[k] * generators[i][j]) % 2)) % 2;
                    }
                }

                for (int j = 0; j < result.Length; j++)
                {
                    result[j] += curr_result[j];
                }
            }


        }

        //Create a template for result arrays
        public T[] getTemplateResult<T>(int[] infos, int m)
        {

            int resultLength = infos.Length + m;

            T[] result = new T[resultLength];
            if (result is string[])
            {
                for (int i = 0; i < result.Length; i++)
                {
                    result[i] = (dynamic)"";
                }
            }
            else if (result is int[])
            {
                for (int i = 0; i < result.Length; i++)
                {
                    result[i] = (dynamic)0;
                }
            }

            return result;
        }

        public int findMaxLength()
        {
            int indexOfMaxLength = 0;
            for (int i = 0; i < generators.Count; i++)
            {
                if (generators[i].Length >= generators[indexOfMaxLength].Length)
                {
                    indexOfMaxLength = i;
                }
            }
            return indexOfMaxLength;
        }

        //Print Encoder's Infomation
        public override string ToString()
        {
            string infos = "\n";
            infos += "Encoded: ";
            foreach (string c in result)
            {
                infos += (c + " ");
            }
            return infos;
        }
    }
}
