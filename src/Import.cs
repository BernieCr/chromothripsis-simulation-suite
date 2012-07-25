using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Chromophobia
{
    static class Import
    {

        static public void ConvertFasta()
        {
            int counter = 0;
            string line;

            string sFile = @"data\Homo_sapiens.GRCh37.hg19.dna.chromosome.4.fa";

            String sFileNew = @"data\chr4.hg19.fac";

            FastaConverter fastaConverter = new FastaConverter(App.Path + sFile);
            bool b = fastaConverter.Convert(App.Path + sFileNew);
            if (b)
            {
                App.Log("Conversion to " + sFileNew + " was successful");
            }
            else
            {
                App.Log("Conversion failed");
            }

            // Read the file and display it line by line.
            StreamReader file = new StreamReader(App.Path + sFile);
            App.Log("Opening " + sFile);
            while ((line = file.ReadLine()) != null)
            {
                App.Log(line);
                counter++;
                if (counter > 30) break;
            }

            file.Close();
        }



        static public void EnsemblSynteny()
        {
            Dictionary<int, String> species = App.LoadSpecies();
            String sSpecies = String.Join(", ", species.Values);

            App.Log("Converting Synteny Data for species <i>" + sSpecies + "</i>...");

            App.DB.OpenFast();
            App.DB.Exec("DELETE FROM speciesChromosomeEns");

            LumenWorks.Framework.IO.Csv.CsvReader csv = App.DB.OpenCsv(@"data\synteny\dnafrag.txt");

            while (csv.ReadNextRecord())
            {
                int genomeDbId = Convert.ToInt32(csv[3]);
                if (species.ContainsKey(genomeDbId))
                {
                    String type = csv[4];
                    String isRef = csv[5];
                    if ((type == "chromosome") && (isRef == "1"))
                    {
                        String name = csv[2];
                        long dnafragId = Convert.ToInt64(csv[0]);

                        String sql = "INSERT INTO speciesChromosomeEns (genomeDbId, dnafragId, name) VALUES (" +
                            genomeDbId.ToString() + "," + dnafragId.ToString() + ",'" + name + "')";
                        App.DB.Exec(sql);
                        App.Log("Inserted " + species[genomeDbId] + " chromosome " + name + "");
                    }
                }
            }

            App.Log("Done reading chromosomes");
            App.Log("");


            App.Log("Converting Synteny Regions...");

            App.DB.Exec("DELETE FROM syntenyEns");

            Dictionary<string, int> fields = new Dictionary<string, int>();

            fields.Add("syntenyRegionId", 0);
            fields.Add("dnafragId", 1);
            fields.Add("start", 2);
            fields.Add("end", 3);
            fields.Add("strand", 4);

            App.DB.ImportCsv(@"data\synteny\dnafrag_region.txt", "syntenyEns", fields);

            App.Log("");


            App.Log("Connecting Tables...");

            System.Data.Common.DbDataReader res =
                App.DB.Query("SELECT * FROM speciesChromosomeEns");
            while (res.Read())
            {
                long dnafragId = Convert.ToInt64(res["dnafragId"].ToString());
                int genomeDbId = Convert.ToInt32(res["genomeDbId"].ToString());
                String chromosome = res["name"].ToString();

                String sql = "UPDATE syntenyEns SET genomeDbId=" + genomeDbId.ToString() + ", chromosome='" +
                    chromosome + "' WHERE dnafragId=" + dnafragId.ToString();
                App.DB.Exec(sql);
                // App.Log(sql);
            }

            App.Log("Cleaning up table...");
            App.DB.Exec("DELETE FROM syntenyEns WHERE genomeDbId IS NULL");

            App.Log("Done.");

            App.DB.Close();
        }



        static public void EnsemblToSqlite()
        {
            Dictionary<string, int> fields = new Dictionary<string, int>();


            fields.Clear();
            fields.Add("exonId", 0);
            fields.Add("transcriptId", 1);
            fields.Add("indexRank", 2);

            App.DB.ImportCsv(@"data\exon_transcript.txt", "exonTranscriptEns", fields);


            fields.Clear();
            fields.Add("id", 0);
            fields.Add("geneId", 1);
            fields.Add("name", 13);

            App.DB.ImportCsv(@"data\transcript.txt", "transcriptEns", fields);


            fields.Clear();
            fields.Add("id", 0);
            fields.Add("name", 1);
            fields.Add("coordSystemId", 2);
            fields.Add("length", 3);

            App.DB.ImportCsv(@"data\seq_region.txt", "seqRegionEns", fields);


            fields.Clear();
            fields.Add("id", 0);
            fields.Add("name", 9);
            fields.Add("region", 1);
            fields.Add("start", 2);
            fields.Add("end", 3);
            fields.Add("strand", 4);
            fields.Add("isCurrent", 7);
            fields.Add("isConstitutive", 8);

            App.DB.ImportCsv(@"data\exon.txt", "exonEns", fields);


            fields.Clear();

            fields.Add("id", 0);
            fields.Add("seqRegionId", 1);
            fields.Add("start", 2);
            fields.Add("end", 3);
            fields.Add("band", 4);
            fields.Add("stain", 5);

            App.DB.ImportCsv(@"data\karyotype.txt", "chromosomeEns", fields);


            fields.Clear();

            fields.Add("id", 0);
            fields.Add("region", 3);
            fields.Add("start", 4);
            fields.Add("end", 5);
            fields.Add("strand", 6);
            fields.Add("status", 9);
            fields.Add("biotype", 1);
            fields.Add("source", 8);
            fields.Add("name", 14);
            fields.Add("description", 10);
            fields.Add("isCurrent", 11);

            App.DB.ImportCsv(@"data\geneEns.txt", "geneEns", fields);
        }



        static public void ImportEnsemblChromosomes()
        {
            App.Log("Converting Chromosome Data");
            App.DB.Exec("DELETE FROM chromosome");

            System.Data.Common.DbDataReader res =
                App.DB.Query("SELECT * FROM seqRegionEns WHERE coordSystemId=2 ORDER BY id ASC");
            while (res.Read())
            {
                int id = 0;
                Int32.TryParse(res["name"].ToString(), out id);
                if (res["name"].ToString() == "X") id = 23;
                if (res["name"].ToString() == "Y") id = 24;
                if (id > 0)
                {
                    int cenStart = 0;
                    int cenEnd = 0;
                    System.Data.Common.DbDataReader res2 =
                        App.DB.Query("SELECT * FROM chromosomeEns WHERE seqRegionId=" + res["id"].ToString() +
                            " AND stain='acen' ORDER BY start ASC");
                    res2.Read(); // p-Arm
                    cenStart = Convert.ToInt32(res2["end"].ToString());
                    res2.Read(); // q-Arm
                    cenEnd = Convert.ToInt32(res2["start"].ToString());

                    String ins = "INSERT INTO chromosome (id, name, regionId, centromereStart, centromereEnd, length) VALUES (" +
                        id + ", '" + res["name"].ToString() + "', " + res["id"].ToString() + ", " + cenStart.ToString() + ", " +
                        cenEnd.ToString() + ", " + res["length"].ToString() + ")";

                    App.DB.Exec(ins);
                    // App.Log(ins);
                }
            }
        }


        static public void ImportEnsemblExons()
        {
            App.DB.OpenFast();

            App.Log("Converting Exon Data");
            App.DB.Exec("DELETE FROM exon");

            int c = 0;

            String sql = "SELECT e.id, e.start, e.end, e.strand, e.name, et.indexRank,  chromosome.id as chrId, g.id as geneId " +
               "FROM exonEns as e " +
               "JOIN geneEns as g ON g.isCurrent=1 AND g.status='KNOWN' AND g.biotype='protein_coding' " +
               "JOIN chromosome ON chromosome.regionId=g.region " +
               "JOIN transcriptEns as t on t.geneId=g.id " +
               "JOIN exonTranscriptEns as et ON et.transcriptId=t.id " +
               "WHERE e.isCurrent=1 and e.id=et.exonId  " +
               "GROUP BY e.id";

            App.Log("Querying: " + sql);

            System.Data.SQLite.SQLiteCommand command = App.DB.CreateCommand();
            System.Data.SQLite.SQLiteTransaction transaction = App.DB.BeginTransaction();
            command.Transaction = transaction;

            String ins = "INSERT OR IGNORE INTO exon (exonId, geneId, chromosomeId, name, start, end, strand, isVirtual) VALUES (" +
                             "@exonId, @geneId, @chromosomeId, @name, @start, @end, @strand, 0)";

            command.CommandText = ins;

            System.Data.Common.DbDataReader res = App.DB.Query(sql);

            App.Log("Inserting Exon Rows...");

            while (res.Read())
            {
                int chromosomeId = Convert.ToInt16(res["chrId"].ToString());
                int geneId = Convert.ToInt32(res["geneId"].ToString());
                int indexRank = Convert.ToInt32(res["indexRank"].ToString());
                int exonId = Convert.ToInt32(res["id"].ToString());
                int start = Convert.ToInt32(res["start"].ToString());
                int end = Convert.ToInt32(res["end"].ToString());
                int strand = Convert.ToInt32(res["strand"].ToString());
                String name = res["name"].ToString();

                command.Parameters.AddWithValue("@exonId", exonId);
                command.Parameters.AddWithValue("@geneId", geneId);
                command.Parameters.AddWithValue("@chromosomeId", chromosomeId);
                command.Parameters.AddWithValue("@start", start);
                command.Parameters.AddWithValue("@end", end);
                command.Parameters.AddWithValue("@strand", strand);
                command.Parameters.AddWithValue("@name", name);

                command.ExecuteNonQuery();

                /* String ins = "INSERT OR IGNORE INTO exon (exonId, geneId, chromosomeId, start, end, strand, indexRank) VALUES (" +
                     exonId.ToString()+","+geneId.ToString()+","+chromosomeId.ToString()+","+
                     start.ToString()+","+end.ToString()+","+strand.ToString()+","+
                     indexRank.ToString()+")";*/

                // App.DB.Exec(ins);
                // App.Log(ins);
                c++;
                if ((c % 10) == 0)
                {
                    App.Status("Exon #" + c.ToString());
                }

            }

            App.Status();
            App.Log(c.ToString() + " exons inserted");

            transaction.Commit();
            command.Dispose();

            App.DB.Close();
        }



        static public String FusionHmmScan(String title, String seq1, String seq2)
        {
            String ret = "";
            
            seq1 = FastaConverter.ConvertString(seq1);
            seq2 = FastaConverter.ConvertString(seq2);

            List<String> seqs = new List<string>();
            for (int i = 0; i < 5; i++)
            {
                seqs.Add(seq1 + seq2);                       // + +
                seqs.Add(seq1 + Sequencer.InvCompl(seq2));   // + -
                seqs.Add(Sequencer.InvCompl(seq1) + seq2);   // - +

                // 20% deletieren
                int cut1 = (int)Math.Round(seq1.Length * 0.2);
                int cut2 = (int)Math.Round(seq2.Length * 0.2);
                seq1 = seq1.Substring(0, seq1.Length - cut1);
                seq2 = seq2.Substring(cut2);
            }


            List<String> orfs = new List<string>();
            foreach (String seq in seqs) // 6-frame ORFs
            {
                orfs.Add(seq.Substring(0));
                orfs.Add(seq.Substring(1));
                orfs.Add(seq.Substring(2));

                String seqInv = Sequencer.InvCompl(seq);
                orfs.Add(seqInv.Substring(0));
                orfs.Add(seqInv.Substring(1));
                orfs.Add(seqInv.Substring(2));
            }


            Hmmer hmmer = new Hmmer();
            Hmmer.SetDatabase("PFam-A");
            Hmmer.Debug = true;

            int c = 0;
            foreach (String orf in orfs)
            {
                String peptide = Sequencer.Translate(orf, true); // Translatieren ohne Stop-Codon
                hmmer.AddSequence(peptide, title + " #" + c.ToString());
                c++;
            }

            hmmer.DoScan();

            List<Hmmer.Result> results = hmmer.GetResults();
            List<string> resultsUnique = new List<string>();

            ret += "> " + title + Environment.NewLine;

            foreach (Hmmer.Result result in results)
            {
                if (result.Evalue >= 1) continue;

                string res = result.Accession + ": " + result.Name;
                if (!resultsUnique.Contains(res))
                {
                    resultsUnique.Add(res);
                    ret += res + Environment.NewLine;
                }
            }
            ret += Environment.NewLine;


            return ret;
        }


        static public int CreateHmmModelFile(String[] models, String filename)
        {
            // Generate list of unique accession IDs
            List<string> IDs = new List<string>();
            foreach (String line in models)
            {
                String s = line.Trim();
                if (s == "") continue;
                if (s[0] == '>') continue;

                String id = s.Substring(0, s.IndexOf(':'));
                if (!IDs.Contains(id))
                {
                    IDs.Add(id);
                }
            }


            // Parse PFAM-A.HMM and write new HMM file
            String fnSource = Hmmer.ModelPath;
            StreamReader source = new StreamReader(fnSource);

            String fnDest = Hmmer.ModelFolder + filename + ".hmm";
            StreamWriter dest = new StreamWriter(fnDest);

            int i = 0;
            int iWritten = 0;
            while (source.Peek() >= 0)
            {
                String model = Hmmer.ReadNextModel(source);
                String acc = Hmmer.GetModelAcc(model);
                if (IDs.Contains(acc))
                {
                    dest.Write(model);
                    iWritten++;
                }
                i++;
            }

            if (iWritten != IDs.Count) throw new Exception("Model Count Mismatch");

            source.Close();
            dest.Close();

            return iWritten;
        }


    }
}
