using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace Chromophobia
{
    public class Randomizer : Random
    {
        private RNGCryptoServiceProvider RNG = new RNGCryptoServiceProvider();
        private byte[] Buffer = new byte[4];

        public Randomizer() { }

        public bool NextBool()
        {
            bool b = false;
            byte[] Byte = new byte[1];
            RNG.GetBytes(Byte);
            if (Byte[0] > 127) b = true;
            return b;
        }
        

        public override Int32 Next()
        {
            RNG.GetBytes(Buffer);
            return BitConverter.ToInt32(Buffer, 0) & 0x7FFFFFFF;
        }

        public override Int32 Next(Int32 maxValue)
        {
            if (maxValue < 0)
                throw new ArgumentOutOfRangeException("maxValue");
            return Next(0, maxValue);
        }

        public override Int32 Next(Int32 minValue, Int32 maxValue)
        {
            if (minValue > maxValue)
                throw new ArgumentOutOfRangeException("minValue");
            if (minValue == maxValue) return minValue;
            Int64 diff = maxValue - minValue;
            while (true)
            {
                RNG.GetBytes(Buffer);
                UInt32 rand = BitConverter.ToUInt32(Buffer, 0);

                Int64 max = (1 + (Int64)UInt32.MaxValue);
                Int64 remainder = max % diff;
                if (rand < max - remainder)
                {
                    return (Int32)(minValue + (rand % diff));
                }
            }
        }
        
        public override double NextDouble()
        {
            RNG.GetBytes(Buffer);
            UInt32 rand = BitConverter.ToUInt32(Buffer, 0);
            return rand / (1.0 + UInt32.MaxValue);
        }

        public override void NextBytes(byte[] buffer)
        {
            if (buffer == null) throw new ArgumentNullException("buffer");
            RNG.GetBytes(buffer);
        }


        public long Next(Range range)
        {
            return Next(range.Start, range.End);
        }
    }
}
