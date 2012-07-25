using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;

namespace Chromophobia
{
    public class Exon
    {
        protected Chromosome chromosome = null;

        public int Id { get; set;  }
        public String Name { get; protected set; }
        public int Strand { get; set; }

        public Range Range { get; set; }
        public int Start { get { return Range.Start; }  }
        public int End { get { return Range.End; } }
        public int Length { get { return Range.Length; }  }

        public Exon(Exon e)
        {
            chromosome = e.chromosome;
            Id = e.Id;
            Range = e.Range;
            Strand = e.Strand;
            Name = e.Name;
        }

        public Exon(DbDataReader r, Chromosome chr)
        {
            if (!r.HasRows) throw new Exception("Exon(): Empty database row");
            App.Assert(chr.Id == Convert.ToInt32(r["chromosomeId"].ToString()));

            chromosome = chr;

            Id = Convert.ToInt32(r["exonId"].ToString());
            Range = new Range(Convert.ToInt32(r["start"].ToString()), Convert.ToInt32(r["end"].ToString()));
            Strand = Convert.ToInt32(r["strand"].ToString());

            Name = r["name"].ToString();
        }


        public bool Contains(int position)
        {
            return (Range.InRange(position));
        }

        public String Read()
        {
            String seq = chromosome.Read(Start, Length);
            if (Strand == -1)
            {
                seq = Sequencer.InvCompl(seq);
            }
            return seq;
        }


    }
}

