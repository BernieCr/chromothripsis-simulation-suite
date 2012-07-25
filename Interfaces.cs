using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chromophobia
{
    interface ISequenceProvider
    {
        int Length { get; }
        String Read(int start, int length);
    }



}
