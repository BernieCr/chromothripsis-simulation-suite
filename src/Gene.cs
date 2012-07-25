using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;

namespace Chromophobia
{
    public class Gene
    {
        protected Chromosome chromosome = null;

        public int Id { get; protected set;  }
        public String Name { get; protected set; }
        public int Strand { get; set; }

        public Range Range { get; set; }
        public int Start { get { return Range.Start; }  }
        public int End { get { return Range.End; } }
        public int Length { get { return Range.Length; }  }


        public Gene(Gene g)
        {
            chromosome = g.chromosome;
            Id = g.Id;
            Name = g.Name;
            Strand = g.Strand;
            Range = g.Range;
        }

        public Gene(DbDataReader r, Chromosome chr)
        {
            chromosome = chr;

            Id = Convert.ToInt32( r["id"].ToString() );
            Range = new Range(Convert.ToInt32(r["start"].ToString()), Convert.ToInt32(r["end"].ToString()));
            Strand = Convert.ToInt32(r["strand"].ToString());

            Name = r["name"].ToString();
        }

    }
}
