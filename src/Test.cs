using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chromophobia
{
    public class Test
    {

        public static void TestNullModell4(String nameNull)
        {
            Chromosome chrOrig = new Chromosome("4");
            Chromosome chrNull = new Chromosome(nameNull);
            int idOrig = chrOrig.Id;
            int idNull = chrNull.Id;

            Range rangeOrig = chrOrig.GetQ();



            
            System.Data.Common.DbDataReader res = 
                   App.DB.Query("SELECT * FROM exon WHERE chromosomeId=" + idOrig + " AND start >= " + 
                    rangeOrig.Start + " AND end <= " + rangeOrig.End + " ORDER BY start asc");
            /*
             *     App.DB.Query("SELECT * FROM exon WHERE chromosomeId=" + idOrig + " AND start >= " + 
                    rangeOrig.Start + " AND end <= " + rangeOrig.End + " ORDER BY RANDOM() LIMIT 50");
             * */

            int i = 0;
            while (res.Read())
            {
                String name = res["name"].ToString();
                Exon exonOrig = new Exon(res, chrOrig);

                System.Data.Common.DbDataReader resNull = App.DB.QueryRead("SELECT * FROM exon WHERE name='" + 
                        name + "' AND chromosomeId=" + idNull);

                if (!resNull.HasRows)
                {
                    App.Log(name + " not found in " + nameNull);
                    continue;
                }
                Exon exonNull = new Exon(resNull, chrNull);

                String seqOrig = exonOrig.Read();
                String seqNull = exonNull.Read();

                if (seqOrig == seqNull)
                {
                  //  if (exonNull.Strand != exonOrig.Strand) App.Log("Strand switcher! :");
                    App.Log("#"+i+" "+name+" OK");
                }
                else
                {
                    App.Log("#"+i+" "+name+" FFFFFUUUUUUU....");
                }
                i++;
       
            }
             
        }

        /*
         // TestChr4Fac 
            const int iRefPosition = 104345123;
            const int iRefLength = 50;
            //>4 dna:chromosome chromosome:GRCh37:4:104345123:104345172:1
            // TTATAAAAATGCAATGACATCACCTCATAAAAGTATATCATGACCATGCT

            const String sReference = "TTATAAAAATGCAATGACATCACCTCATAAAAGTATATCATGACCATGCT";


            Chromosome chr4 = new Chromosome("4");
            String sRead = chr4.Read(iRefPosition, iRefLength);

            if (sRead == sReference)
            {
                Log("Chromosome 4 FAC verified.");
            }
            else
            {
                Log("Chromosome 4 FAC Verification failed.");
                Log(sReference + " (Reference)");
                Log(sRead + " (Read)");
            }
         *     public void TestHmmscan()
        {
            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += worker_TestHmmscan;
            bw.RunWorkerAsync();
            bw.Dispose();
        }

        private void worker_TestHmmscan(object sender, DoWorkEventArgs e)
        {
            DoTestHmmscan();
        }

        private void DoTestHmmscan()
        {
       
            double totalScore = 0;
            DateTime tStart = DateTime.Now;

            int runs = 100;

            App.Log("Running " + runs.ToString() + " hmmscans with model database "+Hmmer.ModelPath+", test sequence: sh3 domain containing protein (ENSP00000369020)");

            // >ENST00000379698 peptide: ENSP00000369020 pep:KNOWN_protein_coding
            String testPeptide = 
                "MEVSAAKAPSAADLSEIEIKKEMKKDPLTNKAPEKPLHEVPSGNSLLSSETILRTNKRGE"+
                "RRRRRCQVAFSYLPQNDDELELKVGDIIEVVGEVEEGWWEGVLNGKTGMFPSNFIKELSG"+
                "ESDELGISQDEQLSKSSLRETTGSESDGGDSSSTKSEGANGTVATAAIQPKKVKGVGFGD"+
                "IFKDKPIKLRPRSIEVENDFLPVEKTIGKKLPATTATPDSSKTEMDSRTKSKDYCKVIFP"+
                "YEAQNDDELTIKEGDIVTLINKDCIDVGWWEGELNGRRGVFPDNFVKLLPPDFEKEGNRP"+
                "KKPPPPSAPVIKQGAGTTERKHEIKKIPPERPEMLPNRTEEKERPEREPKLDLQKPSVPA"+
                "IPPKKPRPPKTNSLSRPGALPPRRPERPVGPLTHTRGDSPKIDLAGSSLSGILDKDLSDR"+
                "SNDIDLEGFDSVVSSTEKLSHPTTSRPKATGRRPPSQSLTSSSLSSPDIFDSPSPEEDKE"+
                "EHISLAHRGVDASKKTSKTVTISQVSDNKASLPPKPGTMAAGGGGPAPLSSAAPSPLSSS"+
                "LGTAGHRANSPSLFGTEGKPKMEPAASSQAAVEELRTQVRELRSIIETMKDQQKREIKQL"+
                "LSELDEEKKIRLRLQMEVNDIKKALQSK";

            Hmmer hmmer = new Hmmer();

            App.Log("Adding sequences...");
            for (int i = 0; i < runs; i++)
            {          
                //App.Status("Run "+(i+1).ToString());
                //totalScore += Hmmer.Scan(testPeptide);
                hmmer.AddSequence(testPeptide);
            }

            App.Log("Scanning...");
            totalScore = hmmer.DoScan();

            App.Status();
            App.Log("Total Hmmer Score: " + totalScore.ToString("#0.00") + "");

            DateTime tEnd = DateTime.Now;
            TimeSpan tDiff = tEnd - tStart;

            double perSecond = ((double)runs / (tDiff.TotalMilliseconds/(double)1000));

            App.Log("<h4>Finished! Time elapsed: " + tDiff.ToString(@"mm\:ss") + " (" + perSecond.ToString("#0.00") + "/second)</h4>", true);
        }
         * 
         * 
         * void Testrandom(
         *  int n0 = 0;
            int n1 = 0;
            int n2 = 0;
            int n3 = 0;
            int n4 = 0;
            int n5 = 0;
            for (int i = 0; i < 100000; i++)
            {
              // bool rand = App.Random.NextBool();
              //  if (rand ) n0++;
              //  else n1++;
               
                int rand = App.Random.Next(5);
                if (rand == 0) n0++;
                if (rand == 1) n1++;
                if (rand == 2) n2++;
                if (rand == 3) n3++;
                if (rand == 4) n4++;
                if (rand == 5) n5++;

            }

            App.Log("0: " + n0 + "x, 1: " + n1 + "x, 2: " + n2 + "x, 3: " + n3 + "x, 4: " + n4 + "x, 5: " + n5 + "x");

         * */
    }
}
