using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace Chromophobia
{
    [System.Runtime.InteropServices.ComVisible(true)]
    public partial class Form1 : Form
    {
        protected String appPath;

        protected String chromosome, modelDatabase;
        protected int runs, cuts;
        protected float probability;
        protected bool extendedOutput;

        protected String nullModelName;
     
        public Form1()
        {
            InitializeComponent();

            webBrowser1.Url = new Uri(String.Format("file:///{0}html/index.html", App.Path));
            webBrowser1.ObjectForScripting = this;

            selectModelDatabase.SelectedIndex = 0;

            RefreshForm();    
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            WriteHtml("<div id='overview-container'><table id='overview-table'><tr><th>Simulation ID</th><th>Runs</th><th>Cuts</th><th>Probability</th><th>Chromosome</th><th>"
                   + "Model</th><th>Total Score</th><th>#Genes</th><th>#ORFs</th><th>#Hits</th><th>#Hit ORFs</th><th>Runs/s</th></tr>"
                   + "</table></div><div id='overviewCopyLink'><a href='#' onclick='window.clipboardData.setData(\"Text\",$(\"#overview-container\").html());writeElement(\"Copied. Paste to Excel or any other application.\", \"#progress\");return false;'>Copy overview table to clipboard</a>"
                   + "</div>", "#overview");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }


        public void RefreshForm()
        {
            selectChromosome.Items.Clear();
            selectChromosome.Items.Add("4q");

            // Virtuelle Chromosomen laden
            System.Data.Common.DbDataReader res = App.DB.Query("SELECT * FROM chromosome WHERE facFile != '' AND originalChromosomeId > 0 ORDER BY name ASC");
            while (res.Read())
            {
                String chromosome = res["name"].ToString();
                selectChromosome.Items.Add(chromosome);
            }

            selectChromosome.SelectedIndex = 0;
        }


        protected void WebBrowserInvoke(String func, object[] args)
        {
            if (webBrowser1.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    webBrowser1.Document.InvokeScript(func, args);
                });
            }
            else webBrowser1.Document.InvokeScript(func, args);
        }

        public void LogScroll()
        {
            WebBrowserInvoke("logScroll", null);
        }


        public void Log(String s)
        {
            WebBrowserInvoke("log", new String[] { s });
        }


        public void WriteHtml(String sHtml, String sHtmlElementId)
        {
            WebBrowserInvoke("writeElement", new String[] { sHtml, sHtmlElementId });
        }

        public void AppendHtml(String sHtml, String sHtmlElementId)
        {
            WebBrowserInvoke("appendElement", new String[] { sHtml, sHtmlElementId });
        }

        public void SwitchToOutput()
        {
            tabControl.SelectTab("tabOutput");
        }

        private void buttonCreateNullModel_Click(object sender, EventArgs e)
        {
            nullModelName = textNullmodelName.Text;

            SwitchToOutput();

            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += worker_CreateNullModel;
            bw.RunWorkerAsync();
            bw.Dispose();
        }


        private void worker_CreateNullModel(object sender, DoWorkEventArgs e)
        {
            Chromosome chr4 = new Chromosome("4");
            ChromosomeCreator creator = new ChromosomeCreator(chr4, chr4.GetQ());

            creator.CreateNullModel(nullModelName);
        }

        private void buttonTestNull_Click(object sender, EventArgs e)
        {
            SwitchToOutput();
            nullModelName = textNullmodelName.Text;       

            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += worker_TestNullModel;
            bw.RunWorkerAsync();
            bw.Dispose();
        }

        private void worker_TestNullModel(object sender, DoWorkEventArgs e)
        {
            Test.TestNullModell4(nullModelName);
        }
 

        private void buttonEnsemblSqlite_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("tabOutput");

            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += worker_ConvertEnsembl;
            bw.RunWorkerAsync();
            bw.Dispose();
        }

        private void buttonImportEnsemblSynteny_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("tabOutput");

            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += worker_SqliteSynteny;
            bw.RunWorkerAsync();
            bw.Dispose();
        }

        private void buttonConvertChromosome_Click(object sender, EventArgs e)
        {
            Import.ConvertFasta();
        }

        private void worker_SqliteSynteny(object sender, DoWorkEventArgs e)
        {
            Import.EnsemblSynteny();
        }

        private void worker_ConvertEnsembl(object sender, DoWorkEventArgs e)
        {
            App.DB.OpenFast();

            Import.EnsemblToSqlite();

            Import.ImportEnsemblChromosomes();
            Import.ImportEnsemblExons();
            
        }

        private void buttonChromoBatch_Click(object sender, EventArgs e)
        {
            SwitchToOutput();

            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += worker_ChromothripsisBatch;
            bw.RunWorkerAsync();
            bw.Dispose();
        }

        

        public void worker_ChromothripsisBatch(object sender, EventArgs e)
        {
            App.Log();
            App.Log();

            Chromothripsis chromo4q, chromo4qnull, chromo4qnull2, chromo4qnull3;

            Chromosome chr4 = new Chromosome("4");
            Range chr4q = chr4.GetQ();
            chromo4q = new Chromothripsis(chr4, chr4q);

            Chromosome chr4qnull = new Chromosome("4qnull");
            chromo4qnull = new Chromothripsis(chr4qnull);

            Chromosome chr4qnull2 = new Chromosome("4qnull_2");
            chromo4qnull2 = new Chromothripsis(chr4qnull2);

            Chromosome chr4qnull3 = new Chromosome("4qnull_3");
            chromo4qnull3 = new Chromothripsis(chr4qnull3);

            int i;

            for (i = 0; i < 2; i++) chromo4qnull.RunSimulations(100000, 400, 0.5, "chromo");
            for (i = 0; i < 2; i++) chromo4qnull2.RunSimulations(100000, 400, 0.5, "chromo");
            for (i = 0; i < 2; i++) chromo4qnull3.RunSimulations(100000, 400, 0.5, "chromo");

            for (i = 0; i < 20; i++) chromo4q.RunSimulations(100000, 200, 0.5, "chromo");
            for (i = 0; i < 10; i++) chromo4qnull.RunSimulations(100000, 200, 0.5, "chromo");

            chromo4q.RunSimulations(100000, 200, 0.5, "pfam-a");
            chromo4q.RunSimulations(100000, 200, 0.5, "pfam-a");
            chromo4q.RunSimulations(100000, 200, 0.5, "pfam-a");
            chromo4q.RunSimulations(100000, 200, 0.5, "pfam-a");
            chromo4q.RunSimulations(100000, 200, 0.5, "pfam-a");
            chromo4q.RunSimulations(100000, 200, 0.5, "pfam-a");
            chromo4qnull.RunSimulations(100000, 200, 0.5, "pfam-a");
            chromo4qnull2.RunSimulations(100000, 200, 0.5, "pfam-a");
            chromo4qnull3.RunSimulations(100000, 200, 0.5, "pfam-a");

            for (i = 0; i < 20; i++) chromo4q.RunSimulations(100000, 200, 0.5, "chromo");
            for (i = 0; i < 10; i++) chromo4qnull2.RunSimulations(100000, 200, 0.5, "chromo");
            for (i = 0; i < 10; i++) chromo4qnull3.RunSimulations(100000, 200, 0.5, "chromo");
            for (i = 0; i < 10; i++) chromo4qnull.RunSimulations(100000, 200, 0.5, "chromo");
            for (i = 0; i < 100; i++) chromo4q.RunSimulations(100000, 200, 0.5, "chromo");

            /*

            for (i = 0; i < 5; i++) chromo4q.RunSimulations(250000, 200, 0.5, "chromo");
            for (i = 0; i < 3; i++) chromo4qnull.RunSimulations(250000, 200, 0.5, "chromo");
            for (i = 0; i < 3; i++) chromo4qnull2.RunSimulations(250000, 200, 0.5, "chromo");
            for (i = 0; i < 3; i++) chromo4qnull3.RunSimulations(250000, 200, 0.5, "chromo");

            for (i = 0; i < 15; i++) chromo4q.RunSimulations(100000, 100, 0.5, "chromo");
            for (i = 0; i < 7; i++) chromo4qnull.RunSimulations(100000, 100, 0.5, "chromo");
            for (i = 0; i < 7; i++) chromo4qnull2.RunSimulations(100000, 100, 0.5, "chromo");
            for (i = 0; i < 7; i++) chromo4qnull3.RunSimulations(100000, 100, 0.5, "chromo");

            for (i = 0; i < 12; i++) chromo4q.RunSimulations(100000, 400, 0.5, "chromo");
            for (i = 0; i < 5; i++) chromo4qnull.RunSimulations(100000, 400, 0.5, "chromo");
            for (i = 0; i < 5; i++) chromo4qnull2.RunSimulations(100000, 400, 0.5, "chromo");
            for (i = 0; i < 5; i++) chromo4qnull3.RunSimulations(100000, 400, 0.5, "chromo");
            */
          //  App.Log("<h3>Original chromosome vs. null models</h3>", true);
              
           /* for (i = 0; i < 9; i++) chromo4q.RunSimulations(100000, 200, 0.5, "chromo");
            chromo4q.RunSimulations(100000, 200, 0.5, "chromo", true);
            for (i = 0; i < 9; i++) chromo4qnull.RunSimulations(100000, 200, 0.5, "chromo");
            chromo4qnull.RunSimulations(100000, 200, 0.5, "chromo", true);
            for (i = 0; i < 10; i++) chromo4qnull2.RunSimulations(100000, 200, 0.5, "chromo");
            for (i = 0; i < 10; i++) chromo4qnull3.RunSimulations(100000, 200, 0.5, "chromo");*/
            /*
            App.Log("<h3>PFam-A Test</h3>", true);
            chromo4q.RunSimulations(100000, 200, 0.5, "pfam-a");
            chromo4q.RunSimulations(100000, 200, 0.5, "pfam-a");
            chromo4q.RunSimulations(100000, 200, 0.5, "pfam-a");
            chromo4qnull.RunSimulations(100000, 200, 0.5, "pfam-a");
            chromo4qnull.RunSimulations(100000, 200, 0.5, "pfam-a");
            chromo4qnull2.RunSimulations(100000, 200, 0.5, "pfam-a");
            chromo4qnull2.RunSimulations(100000, 200, 0.5, "pfam-a");
            chromo4qnull2.RunSimulations(100000, 200, 0.5, "pfam-a");
            chromo4qnull2.RunSimulations(100000, 200, 0.5, "pfam-a");
            */
/*
              App.Log("<h3>Run numbers</h3>", true);
              for (i = 0; i < 5; i++) chromo4q.RunSimulations(1000, 100, 0.5, "chromo");
              for (i = 0; i < 5; i++) chromo4q.RunSimulations(5000, 100, 0.5, "chromo");
              for (i = 0; i < 5; i++) chromo4q.RunSimulations(10000, 100, 0.5, "chromo");
              for (i = 0; i < 5; i++) chromo4q.RunSimulations(50000, 100, 0.5, "chromo");
              for (i = 0; i < 3; i++) chromo4q.RunSimulations(100000, 100, 0.5, "chromo");
              for (i = 0; i < 2; i++) chromo4q.RunSimulations(500000, 100, 0.5, "chromo");
              chromo4q.RunSimulations(1000000, 100, 0.5, "chromo");
              chromo4q.RunSimulations(2000000, 100, 0.5, "chromo");
        */


           // for (i = 0; i < 10; i++) chromo4q.RunSimulations(100000, 200, 0.5, "chromo");
            
      
        }


        private void buttonRun_Click(object sender, EventArgs e)
        {
            runs = (int)numericRuns.Value;
            cuts = (int)numericCuts.Value;
            probability = (float)numericProbability.Value;
            chromosome = selectChromosome.Text;
            modelDatabase = selectModelDatabase.Text;
            extendedOutput = checkExtendedOutput.Checked;

            tabControl.SelectTab("tabOutput");
          //  tabControl.Enabled = false;

            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += worker_Chromothripsis;
            bw.RunWorkerAsync();
            bw.Dispose();
        }

        private void worker_Chromothripsis(object sender, DoWorkEventArgs e)
        {
            DoChromothripsis();
        }


        public void DoChromothripsis()
        {
            Chromothripsis chromo;

            if (chromosome == "4q")
            {
                Chromosome chr = new Chromosome("4");
                Range chr4q = chr.GetQ();
                chromo = new Chromothripsis(chr, chr4q);
            }
            else
            {
                Chromosome chr = new Chromosome(chromosome);
                chromo = new Chromothripsis(chr); 
            }

            chromo.RunSimulations(runs, cuts, probability, modelDatabase, extendedOutput);
        }

    

        private void buttonHmmscan_Click(object sender, EventArgs e)
        {
            String title = textHmmTitle.Text;
            String seq1 = textHmmSequence.Text;
            String seq2 = textHmmSequence2.Text;

            String res = Import.FusionHmmScan(title, seq1, seq2);

            textHmmResults.Text += res;
            textHmmResults.ScrollToCaret();
        }

        private void buttonHmmClear_Click(object sender, EventArgs e)
        {
            textHmmResults.Clear();
        }

        private void buttonHmmCreateFile_Click(object sender, EventArgs e)
        {
            int iWritten = Import.CreateHmmModelFile(textHmmDomains.Lines, textHmmFilename.Text);

            labelHmmFileResult.Text = iWritten.ToString() + " models written";
        }

        private void selectChromosome_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void buttonChromosomeRead_Click(object sender, EventArgs e)
        {
            String name = textCRChromosome.Text;
            int start = Convert.ToInt32(textCRStart.Text);
            int end = Convert.ToInt32(textCREnd.Text);

            Chromosome chr = new Chromosome(name);
            String res = chr.Read(start, end - start);
            textCRRes.Text += res + Environment.NewLine + Environment.NewLine;
        }

       

       
        

      
     



    }
}
