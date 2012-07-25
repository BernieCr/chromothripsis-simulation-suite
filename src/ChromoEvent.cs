using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chromophobia
{
    class ChromoEvent
    {
        public enum Types { Deletion, Duplication, Inversion, Translocation };

        public Types Type;
        public Range Range;
        //public Sequence Dest;
        //public Sequence Source; // Für Translokationen


        public ChromoEvent()
        {
        }


        public ChromoEvent(Range Range, Types Type=Types.Deletion)
        {
            this.Range = Range;
            this.Type = Type;
        }


        public bool ConflictsWith(ChromoEvent Event)
        {
            switch (Event.Type)
            {
                case Types.Deletion:
                    return Range.Intersects(Event.Range);

            }

            return false;
        }
    }
}
