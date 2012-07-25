using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Chromophobia
{
    class FastaConverter
    {
        protected StreamReader hSource;

        public FastaConverter(String sFileSource)
        {
            hSource = new StreamReader(sFileSource);
        }

        ~FastaConverter()
        {
            hSource.Close();
        }

        protected String ReadLine()
        {
            return hSource.ReadLine();
        }

       static protected bool IsCommentLine(String s) {
            if (s == "") return true;
            Char cFirst = s[0];
            return ((cFirst == ';') || (cFirst == '>'));
        }

        static protected String PrepareLine(String s)
        {
            return s.ToUpper();
        }

        static protected bool VerifyLine(String s)
        {
            char[] aCodes = { 'A', 'C', 'G', 'T', '-', 'N' };
            foreach (char c in s)
            {
                if (!aCodes.Contains(c)) {
                    return false;
                }
            }
            return true;
        }

        public bool Convert(String sFileDest)
        {
            StreamWriter hDest = File.CreateText(sFileDest);

            String s;

            while ((s = ReadLine()) != null)
            {
                if (!IsCommentLine(s))
                {
                    s = PrepareLine(s);

                    if (VerifyLine(s))
                    {
                        hDest.Write(s);
                    }
                    else
                    {
                        // Verification failed!
                        hDest.Close();
                        return false;
                    }
                }
            }

            hDest.Close();

            return true;
        }


        static public String ConvertString(String fasta)
        {
            String dest = "";

            String[] lines = fasta.Split('\n');

            foreach(String line in lines)
            {
                String s = line.Trim();
                if (!IsCommentLine(s))
                {
                    s = PrepareLine(s);

                    if (VerifyLine(s))
                    {
                        dest += s;
                    }
                    else
                    {
                        // Verification failed!
                        return "";
                    }
                }
            }

      
            return dest;
        }
   
    }
}
