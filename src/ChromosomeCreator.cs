using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data.Common;
using System.Data.SQLite;

namespace Chromophobia
{

    class ChromosomeCreator
    {
        private Chromosome Source;
        private Range SourceRange;

        private String NewName;
        private String NewFileName;
        private String NewFilePath;

        private int NewChromosomeId;

        private List<Gene> Genes;
        private List<int> PositionsShuffled;

        public ChromosomeCreator(Chromosome source) : this(source, new Range(1, source.Length)) { } // Full Chromosome

        public ChromosomeCreator(Chromosome source, Range range)
        {
            Source = source;
            SourceRange = range;

            Genes = new List<Gene>();
        }


        private void Cleanup()
        {
            DbDataReader res = App.DB.Query("SELECT * FROM chromosome WHERE name='" + NewName + "' LIMIT 1");
            if (res.HasRows)
            {
                res.Read();
                String id = res["id"].ToString();

                App.DB.Exec("DELETE FROM chromosome WHERE id=" + id + "");
                App.DB.Exec("DELETE FROM exon WHERE chromosomeId=" + id);
            }

        }


        private void FetchGenes()
        {
            Genes.Clear();

            DbDataReader resChr = App.DB.Query("SELECT * FROM chromosome WHERE id="+Source.Id.ToString());
            App.Assert(resChr.HasRows, "ChromosomeCreator: Couldn't find chromosome "+Source.Name);
            resChr.Read();
            String regionId = resChr["regionId"].ToString();

            DbDataReader res = App.DB.Query("SELECT * FROM geneEns WHERE region="+regionId+" AND start >= "+SourceRange.Start.ToString()+" AND end <= "+SourceRange.End.ToString()+" AND "+App.GeneEnsWhere+" ORDER BY start ASC");
            App.Assert(res.HasRows, "ChromosomeCreator: Couldn't find genes for chromosome "+Source.Name);
            while (res.Read())
            {
                Gene g = new Gene(res, Source);
                Genes.Add(g);
            }
        }

        private void ShuffleGenes() // based on Fisher-Yates shuffle
        {
            PositionsShuffled = new List<int>();
                  
            int n = Genes.Count;
            for (int i = 0; i < n; i++) PositionsShuffled.Add(i);

            while (n > 1) 
            {  
                n--;  
                int k = App.Random.Next(n + 1);
                int pos = PositionsShuffled[k];
                PositionsShuffled[k] = PositionsShuffled[n];
                PositionsShuffled[n] = pos;  
            }
        }


        private void CreateChromosome()
        {
            App.DB.OpenFast();

            SQLiteCommand command = App.DB.CreateCommand();
            SQLiteTransaction transaction = App.DB.BeginTransaction();
            command.Transaction = transaction;

            String sql = "INSERT INTO exon (exonId, name, geneId, start, end, strand, isVirtual, chromosomeId) " +
                   "VALUES(@exonId, @name, @geneId, @start, @end, @strand, 1, " + NewChromosomeId + ")";
            command.CommandText = sql;



            int posOld = SourceRange.Start;
            int posNew = 1;
            int curGene = 0;
            byte[] buffer;

            FileStream fNew = File.Create(NewFilePath);

            do
            {
                App.Status("Writing Gene " + (curGene + 1) + " of " + Genes.Count);

                Gene geneOld = null;
                Gene geneNew = null;

        // WriteNonCodingSequence
                int nonCodingLength = 0;

                if (curGene < Genes.Count)
                {
                    geneOld = Genes[curGene];
                    geneNew = Genes[PositionsShuffled[curGene]];

                    nonCodingLength = geneOld.Start - posOld;
                    if (nonCodingLength < 0)
                    {
                       /* App.Log("Overlapping Gene: " + geneOld.Name + " - skipping");
                        curGene++;
                        continue;*/

                        int diff = posOld - geneOld.Start;
                        App.Log("Overlapping Gene: " + geneOld.Name + " - "+diff+"bp");
                        posOld = geneOld.Start;
                        nonCodingLength = 0;
                    }
                }
                else
                {
                    nonCodingLength = SourceRange.End - posOld;
                }

                if (nonCodingLength > 0)
                {
                    Source.ReadBuffer(posOld, nonCodingLength, out buffer);
                   // App.Log("Reading Non Coding Sequence from pos " + posOld + ", length " + nonCodingLength);
                    fNew.Write(buffer, 0, buffer.Length);
                }
                posOld += nonCodingLength;
                posNew += nonCodingLength;

               // App.Log("# " + curGene + " NC Length " + nonCodingLength);
                

                if (curGene >= Genes.Count) break;

                bool switchStrand = App.Random.NextBool();
               // switchStrand = false;

            // WriteNewGene
                Source.ReadBuffer(geneNew.Start, geneNew.Length, out buffer);
                //App.Log("Reading Gene New from pos " + geneNew.Start + ", length " + geneNew.Length);

                Gene geneNewNull = new Gene(geneNew);
                geneNewNull.Range = new Range(posNew, posNew + geneNew.Length);
                if (switchStrand) 
                {
                    Sequencer.InvCompl(ref buffer);
                    if (geneNewNull.Strand == 1) 
                        geneNewNull.Strand = -1;
                    else 
                        geneNewNull.Strand = 1;
                   // App.Log("Switched Strands");
                }


                fNew.Write(buffer, 0, buffer.Length);


            // Exons
                System.Data.Common.DbDataReader res =
                    App.DB.Query("SELECT * FROM exon WHERE geneId="+geneNew.Id+" and chromosomeId = "+Source.Id+" ORDER BY start ASC");

                while (res.Read())
                {
                    Exon exonOld = new Exon(res, Source);
                    Exon exonNew = new Exon(exonOld);

                    int bpFromGeneStart = exonNew.Start - geneNew.Start;

                    int newStart = geneNewNull.Start + bpFromGeneStart;
                    int newEnd = newStart + exonOld.Length;
                    
                    if (switchStrand)
                    {
                        newStart = geneNewNull.End - bpFromGeneStart - exonOld.Length;
                        newEnd = geneNewNull.End - bpFromGeneStart;
                    }

                    exonNew.Range = new Range(newStart, newEnd);
                    exonNew.Strand = geneNewNull.Strand;

                    command.Parameters.AddWithValue("@exonId", exonNew.Id);
                    command.Parameters.AddWithValue("@geneId", geneNewNull.Id);
                    command.Parameters.AddWithValue("@start", newStart);
                    command.Parameters.AddWithValue("@end", newEnd);
                    command.Parameters.AddWithValue("@strand", exonNew.Strand);
                    command.Parameters.AddWithValue("@name", exonNew.Name);
                    command.ExecuteNonQuery();
                }

                App.Log("# " + curGene + " " + geneNewNull.Name + "(ID "+geneNewNull.Id+") -&gt; " +geneOld.Name + " - Pos: " + geneNewNull.Start + " Strand: " + geneOld.Strand + (switchStrand ? " (reversed)" : ""));

              //  App.Log("# " + curGene + " New Gene Length " + geneNew.Length);

              //  App.Log("# " + curGene + " posNew " + posNew + " - posOld "+posOld);

              //  App.Log("# " + curGene + " Old Gene Distance " + (Genes[curGene+1].Start-geneOld.Start));
            // Next Gene

                long filePos = fNew.Position;

                posNew += geneNewNull.Length;
                posOld += geneOld.Length;

                App.Assert(filePos == (posNew - 1));

                curGene++;

                App.LogScroll();
           //     if (curGene > 40) break;
            }
            while (posOld <= SourceRange.End);

            fNew.Close();

            transaction.Commit();
            command.Dispose();

            App.DB.Exec("UPDATE chromosome SET length="+posNew+" WHERE id="+NewChromosomeId);

            App.DB.Close();

            App.Status("");
        }


        public void CreateChromosomeInDb()
        {
            App.DB.Exec("INSERT INTO chromosome (name, regionId, centromereStart, centromereEnd, length, facFile, originalChromosomeId) " +
                 "VALUES('" + NewName + "', -1, 1, 1, 0, '" + NewFileName + "', " + Source.Id + ")");

            System.Data.Common.DbDataReader res =
                    App.DB.Query("SELECT id FROM chromosome WHERE name='" + NewName + "'");
            res.Read();

            NewChromosomeId = Convert.ToInt32(res["id"].ToString());
        }


        public void CreateNullModel(String newName)
        {
            string newFileName = newName + ".fac";

            App.Log("Creating Null Modell " + newName);
            NewName = newName;
            NewFileName = newFileName;
            NewFilePath = App.Path + @"data\" + NewFileName;

            Cleanup();

            CreateChromosomeInDb();
            
            FetchGenes();

            ShuffleGenes();

            CreateChromosome();

            App.Log("Finished.");
        }
        
        
    }
}
