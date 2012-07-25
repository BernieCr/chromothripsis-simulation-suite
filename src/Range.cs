using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chromophobia
{
    public class Range
    {
        public int Start { get; set; }
        public int End { get; set; }
        public int Length { get { return Size(); } }

        public Range()
        {
            Start = 0;
            End = 0;
        }

        public Range(Range r) : this(r.Start, r.End)
        {
        }

        public Range(int start, int end)
        {
            Start = start;

            if (end < start)
            {
                throw new ArgumentOutOfRangeException("end", "The start value of the range must not be greater than its end value.");
            }
            End = end;
        }

        public int Size()
        {
            return (End - Start);
        }

        public bool InRange(int i)
        {
            return ((i >= Start) && (i <= End));
        }

        public bool Intersects(Range r)
        {
            return ( InRange(r.Start) || (InRange(r.End)) );
        }

        public bool Contains(Range r)
        {
            return (InRange(r.Start) && (InRange(r.End)));
        }


        public Range CreateFragment(Range subrange)
        {
            App.Assert(Contains(subrange));

            return new Range(subrange);
        }

        public bool IsBefore(Range r2)
        {
            if ((End + 1) == r2.Start)
            {
                return true;
            }
            return false;
        }

        public bool IsAfter(Range r2)
        {
            if ((Start - 1) == r2.End)
            {
                return true;
            }
            return false;
        }

        public bool IsConsecutiveWith(Range r2)
        {
            return (IsBefore(r2) || IsAfter(r2));
        }
        
    }


}
