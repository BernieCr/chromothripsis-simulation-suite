using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace Chromophobia
{
    public class App
    {
        public static Database DB;
        public static Randomizer Random;
        public static String Path;

        public static Form1 MainForm;

        public static String FileChromosome4;
        public static String FileChromosome4qnull;

        public static String GeneEnsWhere = "(geneEns.isCurrent=1 AND geneEns.status='KNOWN' AND geneEns.biotype='protein_coding')";

        public static void Assert(bool condition, String message = "")
        {
            System.Diagnostics.Debug.Assert(condition, message);
        }

        private static String LogTemp = "";
        private static bool LogHidden = false;

        public static void HideLog()
        {
            LogHidden = true;
            LogTemp = "";
        }

        public static void ShowLog(bool discard=false)
        {
            LogHidden = false;
            if (!discard) Log(LogTemp);
            LogTemp = "";
        }

        public static void Log(String s="", bool showTime=false)
        {
            String ts = "";
            if (showTime) ts = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss") + " ";

            String sLog = ts + s;

            if (!LogHidden)
            {
                MainForm.Log(sLog);
            }
            else
            {
                LogTemp += sLog + "<br/>";
            }
        }

        public static void LogScroll()
        {
            MainForm.LogScroll();
        }

        public static void WriteHtml(String sHtml, String sHtmlElementId)
        {
            MainForm.WriteHtml(sHtml, sHtmlElementId);
        }

        public static void AppendHtml(String sHtml, String sHtmlElementId)
        {
            MainForm.AppendHtml(sHtml, sHtmlElementId);
        }

        public static void Status(String status="")
        {
            WriteHtml(status, "#progress");
        }
       

        public static Dictionary<int, String> LoadSpecies(bool bScientificNames = false)
        {
            Dictionary<int, String> species = new Dictionary<int, string>();

            System.Data.Common.DbDataReader res =
                App.DB.Query("SELECT * FROM speciesEns");
            while (res.Read())
            {
                int genomeDbId = Convert.ToInt32(res["genomeDbId"].ToString());
                String name = res["name"].ToString();
                if (bScientificNames) name = res["nameScientific"].ToString();
                species.Add(genomeDbId, name);
            }

            return species;
        }




        public App()
        {
            Path = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
            System.IO.DirectoryInfo directoryInfo = System.IO.Directory.GetParent(Path);
            Path = directoryInfo.FullName + "\\";

            DB = new Database();
            Random = new Randomizer();
            
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            MainForm = new Form1();

            Application.Run(MainForm);
        }

    }
}
