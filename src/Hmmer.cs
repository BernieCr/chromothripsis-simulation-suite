using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
//using LumenWorks.Framework.IO.Csv;

namespace Chromophobia
{
    public class Hmmer
    {
        public static String ModelPath = App.Path+@"data\hmmer\PFam-A.hmm"; //@"data\hmmer\sh3_1.hmm";
        private static String InputFile = App.Path + @"data\hmmer\input.fa";
        private static String OutputFile = App.Path + @"data\hmmer\output.csv";
        public static String ModelFolder = App.Path+@"data\hmmer\";

        public static bool Debug = false;

        private StreamWriter SequenceFile = null;
        private int PeptideNameCounter = 0;

        public struct Result
        {
            public String Name;
            public String Accession;
            public double Score;
            public double Evalue;
            public String SequenceName;
        };
        public static List<Result> Results;

        public Hmmer()
        {
            try
            {
                SequenceFile = new StreamWriter(InputFile);
            }
            catch (Exception e)
            {
                // Temp files falls von DVD gestartets
                InputFile = Path.GetTempFileName();
                OutputFile = Path.GetTempFileName();

                SequenceFile = new StreamWriter(InputFile);
            }
        }

        ~Hmmer()
        {
            if (SequenceFile != null) SequenceFile.Close();
        }

        static public void SetDatabase(String modelDatabase)
        {
            ModelPath = ModelFolder + modelDatabase + ".hmm";
        }

        public void AddSequence(String peptide, String sequenceName)
        {
            WriteTempFasta(peptide, SequenceFile, sequenceName.Replace(' ', '_'));
        }

        public void AddSequence(String peptide)
        {
            AddSequence(peptide, "CHROMO" + PeptideNameCounter.ToString());
            PeptideNameCounter++;
        }

        public double DoScan()
        {
            SequenceFile.Close();
            SequenceFile = null;

            App.Status("Performing Hmmer Scan on database " + Path.GetFileName(ModelPath));
            double score = ScanFile(InputFile);
            App.Status();
            return score;
        }

        public List<Result> GetResults()
        {
            return Results;
        }

        public double ScanFile(String fileName)
        {

            String args = "--domtblout " + OutputFile.Replace('\\', '/') + " " + ModelPath.Replace('\\', '/') + " " + fileName.Replace('\\', '/');

           // ExecuteCommand(App.Path + "hmmscan.exe", args, out result, out errors);

          //  if (errors != "") throw new Exception("Hmmscan.exe failed to run. Error message: " + errors);

            var psi = new ProcessStartInfo(App.Path + @"bin\hmmscan.exe");
            psi.Arguments = args;
            psi.RedirectStandardOutput = false;
            psi.RedirectStandardError = false;
            psi.WindowStyle = ProcessWindowStyle.Hidden;
            psi.UseShellExecute = true;
            psi.CreateNoWindow = true;

            Process p = Process.Start(psi);
            p.WaitForExit();
            p.Close();

            //if (errors != "") MessageBox.Show(errors);
            //App.Log(res);
            

            double score = ReadResults();

            return score;
        }


        public double Scan(String peptide)
        {
            WriteTempFasta(peptide, InputFile);
            return ScanFile(InputFile);
        }


        private static void WriteTempFasta(String peptide, StreamWriter file, String namePeptide)
        {
            if (peptide == "") throw new InvalidDataException("Hmmer: Empty peptide sequence");

            file.WriteLine(">"+namePeptide);
            do
            {
                int posCut = Math.Min(60, peptide.Length);
                String sub = peptide.Substring(0, posCut);
                file.WriteLine(sub);

                peptide = peptide.Substring(posCut);
            }
            while (peptide != "");
            file.WriteLine();
        }

        private static void WriteTempFasta(String peptide, String fileName)
        {
            StreamWriter w = new StreamWriter(fileName);
            WriteTempFasta(peptide, w, "CHROMOTEMP");
            w.Close();
        }

        private static List<String> ReadSpaceSeparatedValues(String sLine, int maxIndex=-1)
        {
            List<String> fields = new List<string>();

            int i = 0;
            if (sLine.Length > 0)
            {
                do
                {
                    int nextSpace = sLine.IndexOf(' ');
                    if (nextSpace == -1) nextSpace = sLine.Length;
                    if (i == maxIndex) nextSpace = sLine.Length;

                    String field = sLine.Substring(0, nextSpace);
                    sLine = sLine.Substring(nextSpace);
                    sLine = sLine.TrimStart(new char[] { ' ' });
                    fields.Add(field);
                    i++;
                    
                }
                while (sLine != "");
            }

            return fields;
        }

        private double ReadResults()
        {
            Results = new List<Result>();

            double totalScore = 0;

            StreamReader reader = new StreamReader(OutputFile);
            while (reader.Peek() >= 0)
            {
                String sLine = reader.ReadLine();
                if (sLine.Length > 0) if (sLine[0] == '#') continue;
                List<String> fields = ReadSpaceSeparatedValues(sLine, 22);
                String sScore = fields[7];
                double score = Convert.ToDouble(sScore, System.Globalization.CultureInfo.InvariantCulture);
                totalScore += score;

                String sEvalue = fields[6];
                double evalue = Convert.ToDouble(sEvalue, System.Globalization.CultureInfo.InvariantCulture);

                String seqname = fields[3];
                seqname = seqname.Replace('_', ' ');
                String target = fields[22];

                Result result = new Result();
                result.Name         = fields[0];
                result.Accession    = fields[1];
                result.SequenceName = seqname;
                result.Score        = score;
                result.Evalue       = evalue;
                Results.Add(result);

                if (Debug)
                {                               
                    App.Log("<b>" + sScore + " score</b> on sequence " + seqname + " &gt; " + target + "");
                }
            }
            reader.Close();
            return totalScore;
        }



        public static String ReadNextModel(StreamReader hmm)
        {
            String model = "";

            while (hmm.Peek() >= 0)
            {
                String sLine = hmm.ReadLine();

                model += sLine + "\n";

                if (sLine.Length >= 2)
                {
                    if (sLine.Substring(0,2) == "//") break;
                }
            }
            return model;
        }

        public static String GetModelAcc(String hmmModel)
        {
            String acc = "";

            String[] lines = hmmModel.Split('\n');
            foreach (String line in lines)
            {
                if (line.Length >= 3)
                {
                    if (line.ToUpper().Substring(0, 3) == "ACC")
                    {
                        acc = line.Substring(3).Trim();
                        break;
                    }
                }
            }

            return acc;
        }
    }
}
