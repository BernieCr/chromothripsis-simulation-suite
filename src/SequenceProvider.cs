using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chromophobia
{

    class SequenceProvider : ISequenceProvider
    {
        public int Length { get { return Sequence.Length;  } }

        private String Sequence;

        public SequenceProvider()
        {
            Sequence = "";
        }

        public SequenceProvider(String sequence)
        {
            Sequence = sequence;
        }

        public String Read(int start, int length)
        {
            App.Assert((start + length) <= Length);

            return Sequence.Substring((int)start, length);
        }

    }
}
