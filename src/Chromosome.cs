using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;


namespace Chromophobia
{
    public class Chromosome : ISequenceProvider
    {
        public String Name { get; private set; }
        public int Id { get; private set; }
        public int Length { get; private set; }
        public Range Centromere { get; private set; }

        public int GenomeDbId { get; private set; }

        private bool InMemory = false;
        private String FacFile;
        private byte[] DataBuffer;

        protected Exon[] Exons = new Exon[0];

        private bool LoadedExons = false;
        protected int[] ExonStarts;
        protected int ExonRangeSize = 10000;

        public Chromosome(Chromosome chr)
        {
            Name = chr.Name;
            FacFile = chr.FacFile;

            Id = chr.Id;
            Length = chr.Length;
            Exons = chr.Exons;
            GenomeDbId = chr.GenomeDbId;
        }


        public Chromosome(String name, int genomeDbId=90)
        {
            this.Name = name;
            this.GenomeDbId = genomeDbId;

            System.Data.Common.DbDataReader res = App.DB.QueryRead("SELECT * FROM chromosome WHERE name='" + Name + "'");
            App.Assert(res.HasRows, "Couldn't find chromosome " + Name);

            FacFile = App.Path + @"data\" + res["facFile"].ToString();

            Id = Convert.ToInt32(res["id"]);
            Centromere = new Range(Convert.ToInt32(res["centromereStart"].ToString()),
                Convert.ToInt32(res["centromereEnd"].ToString()));

            Length = Convert.ToInt32(res["length"].ToString());


            /*
            FileStream f = new FileStream(FacFile, FileMode.Open, FileAccess.Read);
            long length = f.Length;
            DataBuffer = new byte[length];
            int pos = 0;
            int read;
            while ( (read = f.Read(DataBuffer, pos, (int)length-pos)) > 0)
            {
                pos += read;
                if (pos >= length) break;
            }
            f.Close();
            InMemory = true;
             **/

            //LoadExons();
        }

        public void SetFacFile(String facFile)
        {
            this.FacFile = facFile;
        }

        private void LoadExons()
        {
            Exons = new Exon[0];

            List<Exon> list = new List<Exon>();

            App.Status("Loading exons. This may take a while...");

            System.Data.Common.DbDataReader res = 
                App.DB.Query("SELECT * FROM exon WHERE chromosomeId=" + Id + " ORDER BY start ASC");

            App.Status("Saving exons.");
            while (res.Read())
            {
                Exon exon = new Exon(res, this);
                list.Add(exon);
            }

            Exons = list.ToArray();


            App.Status("Indexing exons.");
            int numIndizes = (int)(Length / ExonRangeSize);
            ExonStarts = new int[numIndizes];

            int exonStart = 0;

            for (int i = 0; i < numIndizes; i++)
            {
                int start = i * ExonRangeSize;
                for (int j = exonStart; j < Exons.Length; j++)
                {
                    if (Exons[j].Start >= start)
                    {
                        ExonStarts[i] = j;
                        exonStart = j;
                        break;
                    }
                }
            }

            App.Status();

            LoadedExons = true;
        }

        public bool Equals(Chromosome chromosome2)
        {
            return (this.Name == chromosome2.Name);
        }


        public int ReadBuffer(int start, int length, out byte[] buffer)
        {
            buffer = new byte[length];
            int read = 0;
         /*   if (InMemory)
            {
                Buffer.BlockCopy(DataBuffer, start, buffer, 0, length);
                read = length;
            }
            else*/
            {
                Stream f = File.OpenRead(FacFile);
                f.Seek(start - 1, SeekOrigin.Begin);
                read = f.Read(buffer, 0, length);
                f.Close();
            }
            return read;
        }


        public String Read(int start, int length)
        {
            String sRead = "";

            byte[] buffer;
            ReadBuffer(start, length, out buffer);

            sRead = System.Text.Encoding.UTF8.GetString(buffer);
            
            return sRead;
        }

        public Range GetP()
        {
            return new Range(0, Centromere.Start - 1);
        }

        public Range GetQ()
        {
            return new Range(Centromere.End+1, Length);
        }

        public Exon WithinExon(int position)
        {
            if (!LoadedExons) LoadExons();

            int exonStartIndex = Math.Min((int)Math.Floor((double)position / ExonRangeSize), ExonStarts.Length-1);
            int exonStart = Math.Max(ExonStarts[exonStartIndex]-1, 0);
            int exonEnd = Math.Min(ExonStarts[Math.Min(exonStartIndex + 1, ExonStarts.Length - 1)] + 1, Exons.Length-1);

            for (int i = exonStart; i <= exonEnd; i++)
            {
                if (Exons[i].Contains(position))
                {
                    return Exons[i];
                }
            }
            return null;
        }

     
    }
}
