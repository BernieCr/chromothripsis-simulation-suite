using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chromophobia
{
    class Chromothripsis
    {
        private List<int> Cuts = new List<int>();
        private List<Range> Fragments = new List<Range>();
        private List<Range> EventFragments = new List<Range>();

        private List<ChromoEvent> Events = new List<ChromoEvent>();

        private Chromosome Source;
        private Range SourceRange;

        private Sequencer Sequencer;

        private int NumGenes, NumOrf;

        private Hmmer Hmmer;

        private bool ExtendedOutput = false;

        private String SimulationId;

        public struct Result
        {
            public String SimulationId;
            public String Hmm;
            public String ChromosomeName;
            public int Runs;
            public int Cuts;
            public double Probability;
            public int NumGenes;
            public int NumOrfs;
            public int NumHitOrfs;
            public int NumHits;
            public double RunsPerSecond;
            public double TotalScore;
        };

        public struct FusionSequence
        {
            public String SequenceId;
            public bool IsHit;
            public Exon Exon1, Exon2;
            public Range Range1, Range2;
            public String OrfName;
            public String Sequence;
        };

        public static List<FusionSequence> FusionSequences;


        /*
        public class Options
        {
            public bool Enabled { get; set; }
            public double Probability { get; set; }

            public Options()
            {
                Enabled = false;
                Probability = 1.0;
            }
        }*/

        public Chromothripsis(Chromosome chromosome) : this(chromosome, new Range(1, chromosome.Length))
        {
        }


        public Chromothripsis(Chromosome chromosome, Range chromosomeRange)
        {
            Source = chromosome;
            SourceRange = chromosomeRange;
            Sequencer = new Sequencer(Source);
        }

        protected bool Conflicts(ChromoEvent Event)
        {
            bool conflict = false;

            foreach (ChromoEvent e in Events)
            {
                if (Event.ConflictsWith(e))
                {
                    conflict = true;
                    break;
                }
            }

            return conflict;
        }

        protected void CreateCuts(int numCuts)
        {
            Cuts.Clear();

            Cuts.Add(SourceRange.Start);
            Cuts.Add(SourceRange.End);

            for (int i = 0; i < numCuts; i++)
            {
                int pos = 0;
                do
                {
                    pos = App.Random.Next(SourceRange.Start, SourceRange.End);
                }
                while (Cuts.Contains(pos));

                Cuts.Add(pos);
            }

            Cuts.Sort();
        }


        protected void RenderFragments()
        {
            Fragments.Clear();

            // Cutposition => Schnitt DAVOR

            for (int i = 0; i < Cuts.Count - 1; i++)
            {
                int start = Cuts[i];
                int end = Cuts[i + 1] - 1;
                Range fragmentRange = new Range(start, end);

                Fragments.Add(fragmentRange);
              //  App.Log("Fragment #" + i + " (" + newFragment.Start + "," + newFragment.End + ")");
            }            
        }


        private void CreateEvents(double probabilityEvent)
        {
            EventFragments.Clear();
            Events.Clear();

            for (int i = 0; i < Fragments.Count; i++)
            {
                double dice = App.Random.NextDouble();
                if (dice <= probabilityEvent)
                {
                    // Deletion       
                    Events.Add(new ChromoEvent(Fragments[i], ChromoEvent.Types.Deletion));
                 //   App.Log("Deleted Fragment #" + i+" - Dice: "+dice.ToString());
                }
                else
                {
                    EventFragments.Add(Fragments[i]);
                  //  App.Log("EventFragment #" + i + " (" + Fragments[i].Start + "," + Fragments[i].End + ") - Dice: "+dice.ToString());
                }
            }
        }

        protected void CheckCuts()
        {
            for (int i = 0; i < EventFragments.Count-1; i++)
            {
                if (!EventFragments[i].IsBefore(EventFragments[i + 1]))
                {
                    // fragment fusion happened!
                    OnFusionEvent(EventFragments[i], EventFragments[i + 1]);

                  //  App.Log("Fusion event: Fragment (" + EventFragments[i].Start + "," + EventFragments[i].End + ") + Fragment " + EventFragments[i + 1].Start + "," + EventFragments[i + 1].End + ")");
                }
                else
                {
                 //   App.Log("Cut without event: Fragment (" + EventFragments[i].Start + "," + EventFragments[i].End + ") + Fragment " + EventFragments[i + 1].Start + "," + EventFragments[i + 1].End + ")");
                }
            }
        }

        protected void OnFusionEvent(Range fragment1, Range fragment2)
        {
            Exon ex1 = Source.WithinExon(fragment1.End);
            Exon ex2 = Source.WithinExon(fragment2.Start);
          //  10371808
           // 103718617
            if ((ex1 != null) && (ex2 != null))
            {
                // Both sides of the cut are within exons
                if (ex1.Strand == ex2.Strand)
                {
                    // Both exons are on same strand

                    int bpExon1 = (int)(fragment1.End - ex1.Start);
                    int bpExon2 = (int)(ex2.End - fragment2.Start);
                    int percentExon1 = (int)(((double)bpExon1 / ex1.Length) * 100);
                    int percentExon2 = (int)(((double)bpExon2 / ex2.Length) * 100);

                    if ((bpExon1 > 100) && (bpExon2 > 100))
                    {
                        String seqFusion, seq1, seq2;

                        Range rangeLeft = new Range(ex1.Start, fragment1.End);
                        Range rangeRight = new Range(fragment2.Start, ex2.End);

                        seq1 = Source.Read(rangeLeft.Start, (int)rangeLeft.Length);
                        seq2 = Source.Read(rangeRight.Start, (int)rangeRight.Length);
                        seqFusion = seq1 + seq2;

                        Dictionary<String, String> orfs = new Dictionary<string, string>();
                        orfs.Add("Forward 0", Sequencer.GetORF(seqFusion, 0));
                        orfs.Add("Forward 1", Sequencer.GetORF(seqFusion, 1));
                        orfs.Add("Forward 2", Sequencer.GetORF(seqFusion, 2));

                        String seqInv = Sequencer.Invert(Sequencer.Complement(seqFusion));
                        orfs.Add("Reverse 0", Sequencer.GetORF(seqInv, 0));
                        orfs.Add("Reverse 1", Sequencer.GetORF(seqInv, 1));
                        orfs.Add("Reverse 2", Sequencer.GetORF(seqInv, 2));

                        Dictionary<String, String> peptides = new Dictionary<string, string>();

                        bool bSignificantPeptides = false;

                        foreach (String key in orfs.Keys)
                        {
                        	if (orfs[key].Length >= 120)
                            {
                            	String pep = Sequencer.Translate(orfs[key]);
                           
                                peptides.Add(key, pep);
                                bSignificantPeptides = true;
                            }

                        }

                        if (bSignificantPeptides)
                        {
                            NumGenes++;

                            String htmlEx1 = "<a href='http://www.ensembl.org/Homo_sapiens/Search/Details?species=Homo_sapiens;idx=Transcript;end=1;q=" + ex1.Name +
                             "' target='_blank'>" + ex1.Name + "</a> " + bpExon1.ToString() + "bp (" + percentExon1.ToString()+"%)";

                            String htmlEx2 = "<a href='http://www.ensembl.org/Homo_sapiens/Search/Details?species=Homo_sapiens;idx=Transcript;end=1;q=" + ex2.Name +
                                "' target='_blank'>" + ex2.Name + "</a> " + bpExon2.ToString() + "bp (" + percentExon2.ToString() + "%)";

                            if (ExtendedOutput) App.Log("<b>Found Fusion Gene #" + NumGenes + ": </b>" + htmlEx1 + " <b>&</b> " + htmlEx2 + "");
                            // App.Log("Fusion Sequence: " + seqFusion);

                            foreach (String key in peptides.Keys)
                            {
                                String pep = peptides[key];

                                if (ExtendedOutput) App.Log(key + ": " + pep);

                                FusionSequence fs = new FusionSequence();
                                fs.SequenceId = NumOrf.ToString();
                                fs.IsHit = false;
                                fs.Exon1 = ex1;
                                fs.Exon2 = ex2;
                                fs.OrfName = key;
                                fs.Range1 = rangeLeft;
                                fs.Range2 = rangeRight;
                                fs.Sequence = pep;
                                FusionSequences.Insert(NumOrf, fs);

                                Hmmer.AddSequence(pep, fs.SequenceId);
                                
                                //String sequenceName = "#" + NumGenes+" "+ex1.Name + " " + percentExon1.ToString() + "% & " + ex2.Name + " " + percentExon2.ToString() + "% ORF " + key;
                                NumOrf++;
                            }

                        } // if (bSignificantPeptides)
                    } //   if ((bpExon1 > 100) && (bpExon2 > 100))
                } //  if (ex1.Strand == ex2.Strand)
            } //  if ((ex1 != null) && (ex2 != null))
            else
            {
                //App.Log("No Fusion Gene :(");
            }
        }



        public void Simulate(int numCuts, double probabilityEvent)
        {
         //   App.Log("Simulating Chromothripsis: "+numCuts.ToString()+" cuts, "+probabilityEvent.ToString()+" probability");

            CreateCuts(numCuts);
            RenderFragments();
            CreateEvents(probabilityEvent);
            CheckCuts();
        }



        private void UpdateOverview(Result res)
        {
            String s = "<tr><td>" + res.SimulationId + "</td><td>" + res.Runs + "</td><td>" + res.Cuts + "</td><td>" + res.Probability
                   + "</td><td>" + res.ChromosomeName + "</td><td>" + res.Hmm + "</td><td>" + res.TotalScore.ToString("#0.00") + "</td><td>" + res.NumGenes
                   + "</td><td>" + res.NumOrfs + "</td><td>" + res.NumHits + "</td><td>" + res.NumHitOrfs + "</td>"
                   + "</td><td>" + res.RunsPerSecond.ToString("#0") + "</td>";

            if (ExtendedOutput) {
                   //+"<td><a href='#' onclick='window.scrollTo(0, $(\"#chromoDiv" + res.SimulationId + "\").position().top);return false;'>Show details</a></td>"
                s += "<td><a href='#' onclick='window.clipboardData.setData(\"Text\",$(\"#chromoResults" + res.SimulationId + "\").html());writeElement(\"Copied. Paste to Excel or any other application.\", \"#progress\");return false;'>Copy details</a></td>";
            }

            s += "</tr>";

            App.AppendHtml(s, "#overview-table");
        }

        public Result RunSimulations(int numRuns, int numCuts, double probabilityEvent, String modelDatabase="", bool extendedOutput=false)
        {
            SimulationId = DateTime.Now.ToString("yyyyMMddhhmmss") ;

            Result res = new Result();
            res.SimulationId = SimulationId;
            res.ChromosomeName = Source.Name;
            res.Runs = numRuns;
            res.Cuts = numCuts;
            res.Probability = probabilityEvent;
            res.Hmm = modelDatabase;

            ExtendedOutput = extendedOutput;

            App.Log("<div id='chromoDiv" + SimulationId + "'><b>Chromothripsis Simulation ID "+SimulationId+" on Chromosome " + Source.Name + " - " + numRuns.ToString() + " runs " +
                "- Simulating "+numCuts.ToString()+" cuts, "+probabilityEvent.ToString()+" probability</b></div>");
            App.LogScroll();

            DateTime tStart = DateTime.Now;

            NumGenes = 0;
            NumOrf = 0;
            FusionSequences = new List<FusionSequence>();

            Hmmer = new Hmmer();
            Hmmer.Debug = false;

            if (modelDatabase != "") Hmmer.SetDatabase(modelDatabase);

            if (ExtendedOutput) App.HideLog();

            for (int i = 0; i < numRuns; i++)
            {
                Simulate(numCuts, probabilityEvent);
                if ((i % 10) == 0)
                {
                    int percent = (int)( ((double)i/(double)numRuns) * 100);
                    App.Status("Simulation Progress: " + percent.ToString()+"%");
                }
            }
            App.Status();

            if (ExtendedOutput) App.ShowLog();

            if (ExtendedOutput) App.Log("");
            App.Log("Found a total of " + NumGenes.ToString() + " fusion genes<");

            if (ExtendedOutput)
            {
                App.Log("<b>Performing Hmmer Scan...</b>");
                App.LogScroll();
            }

            //App.HideLog();
            double totalScore = Hmmer.DoScan();
            App.Log("Total Hmmer Score: " + totalScore.ToString("#0.00") + "");
           // App.ShowLog();

            List<Hmmer.Result> results = Hmmer.GetResults();

            int NumHitOrfs = 0;

            String resultTable = "";
            
            if (ExtendedOutput) resultTable = "<br/><div id='chromoResults" + SimulationId + "'><table>" +
                   "<tr><th>Simulation Id</th><th>Sequence ID</th><th>Score</th><th>Target Name</th><th>Accession</th><th>Exon 1</th>" +
                   "<th>Start 1</th><th>Length 1</th><th>Exon 2</th><th>Start 2</th><th>Length 2</th><th>ORF</th><th>Sequence</th></tr>";

            foreach (Hmmer.Result result in results)
            {
                int seqId = Convert.ToInt32(result.SequenceName);
                FusionSequence fs = FusionSequences[seqId];
                if (!fs.IsHit)
                {
                    fs.IsHit = true;
                    FusionSequences[seqId] = fs;
                    NumHitOrfs++;
                }

                if (ExtendedOutput)
                {
                    resultTable += "<tr><td>" + SimulationId + "</td><td>" + fs.SequenceId + "</td><td>" + result.Score.ToString("#0.00") + "</td><td>" + result.Name + "</td><td>" + result.Accession
                        + "</td><td>" + fs.Exon1.Name + "</td><td>" + fs.Range1.Start + "</td><td>" + fs.Range1.Length
                        + "</td><td>" + fs.Exon2.Name + "</td><td>" + fs.Range2.Start + "</td><td>" + fs.Range2.Length
                        + "</td><td>" + fs.OrfName + "</td><td>" + fs.Sequence +
                        "</tr>";
                }
            }

            if (ExtendedOutput) resultTable += "</table></div>";


            App.Log("Number of fusion sequences that produced hits: " + NumHitOrfs + "");

            if (ExtendedOutput) App.Log(resultTable);

            if (ExtendedOutput) App.Log("");

            DateTime tEnd = DateTime.Now;
            TimeSpan tDiff = tEnd - tStart;

            double perSecond = ((double)numRuns / (tDiff.TotalMilliseconds / (double)1000));

            res.NumHits = results.Count;
            res.NumGenes = NumGenes;
            res.NumHitOrfs = NumHitOrfs;
            res.NumOrfs = NumOrf;
            res.TotalScore = totalScore;
            res.RunsPerSecond = perSecond;

            App.Log("Finished. "+perSecond.ToString("#0") + " runs per second, time elapsed: " + tDiff.ToString(@"mm\:ss"));
            App.Log("<br/>");
            App.LogScroll();

            UpdateOverview(res);

            return res;
        }
    }
}
