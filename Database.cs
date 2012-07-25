using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Data.SQLite;
using LumenWorks.Framework.IO.Csv;
using System.Data.Common;

namespace Chromophobia
{

    public class Database
    {
        protected static String Dbfile;

        private SQLiteConnection Conn;

        public Database()
        {
            Conn = new SQLiteConnection();
            Dbfile = App.Path + @"data\chromo.db";
        }

        private void Open()
        {
            try
            {
                Conn.Open();
            }
            catch (SQLiteException e)
            {
                App.Log("<b>" + e.Message + ", DB File: "+Dbfile+"</b>");
                throw (new Exception("Database Error"));
            }
        }

        private void Init()
        {
            if (Conn.State != System.Data.ConnectionState.Open)
            {             
                Conn.ConnectionString = "Data Source=" + Dbfile+ ";Version=3";
                Open();
            }
        }

        public void Close()
        {
            if (Conn.State == System.Data.ConnectionState.Open) Conn.Close();
        }

        public void OpenFast()
        {
            if (Conn.State == System.Data.ConnectionState.Open) Conn.Close();
            Conn = new SQLiteConnection();
            Conn.ConnectionString = "Data Source=" + Dbfile + ";Version=3;Count Changes=off;" +
                "Journal Mode=off;Pooling=true;Cache Size=10000;Page Size=4096;Synchronous=0";

            Open();      
        }

        ~Database()
        {
            Close();
        }


        public void Exec(String sql)
        {
            Init();
            SQLiteCommand MyCmd = new SQLiteCommand(sql, Conn);
            //MyCmd.ExecuteScalar();
            MyCmd.ExecuteNonQuery();
        }


        public DbDataReader Query(String sql)
        {
            Init();
            SQLiteCommand cmd = new SQLiteCommand(sql, Conn);
            SQLiteDataReader reader = cmd.ExecuteReader();
            return reader;
        }


        public DbDataReader QueryRead(String sql)
        {
            Init();
            SQLiteCommand cmd = new SQLiteCommand(sql, Conn);
            SQLiteDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
            }
            return reader;
        }

        public SQLiteCommand CreateCommand()
        {
            return Conn.CreateCommand();
        }

        public SQLiteTransaction BeginTransaction()
        {
            return Conn.BeginTransaction(); 
        }

        public CsvReader OpenCsv(String fileName)
        {
            return new CsvReader(new StreamReader(fileName), false, '\t');
        }


        public void ImportCsvOld(String fileName, String table, Dictionary<string, int> fields)
        {
            OpenFast();

            Exec("DELETE FROM " + table);
           
            int c = 0;
            using (CsvReader csv = OpenCsv(fileName))
            {
                int fieldCount = csv.FieldCount;

                while (csv.ReadNextRecord())
                {
                    String sql = "INSERT INTO " + table + "(" + String.Join(",", fields.Keys) + ") VALUES (";

                    List<String> vals = new List<String>();
                    foreach (int key in fields.Values)
                    {
                        String val = csv[key];
                        val = val.Replace("'", "''");

                        vals.Add("'" + val + "'");
                    }
                    sql += String.Join(",", vals) + ")";
                    //  App.Log(sql);

                    Exec(sql);

                    c++;
                    //if (c > 10) break;
                }
            }

            App.Log(c.ToString() + " Datensätze aus " + fileName + " in " + table + " eingefügt");

            Conn.Close();
        }




        public void ImportCsv(String fileName, String table, Dictionary<string, int> fields)
        {
            OpenFast();

            var lineCount = 0;
            using (var reader = File.OpenText(fileName))
            {
                while (reader.ReadLine() != null)
                {
                    lineCount++;
                }
            }


            Exec("DELETE FROM " + table);

          //  Exec("PRAGMA synchronous=0");

            int c = 0;
            using (CsvReader csv = new CsvReader(new StreamReader(fileName), false, '\t'))
            {
               
                int fieldCount = csv.FieldCount;

                SQLiteCommand command = Conn.CreateCommand();
                SQLiteTransaction transaction = Conn.BeginTransaction();
                command.Transaction = transaction;

                String sql = "INSERT INTO " + table + "(" + String.Join(",", fields.Keys) + ") VALUES (";
                List<String> vals = new List<String>();
                foreach (String field in fields.Keys) vals.Add("@" + field + "");
                sql += String.Join(",", vals) + ")";

                command.CommandText = sql;
                /*
                foreach (String field in fields.Keys)
                {
                    command.Parameters.AddWithValue("@" + field, "");
                }
                */

                while (csv.ReadNextRecord())
                {                  
                    foreach (String field in fields.Keys)
                    {
                        String val = csv[fields[field]];
                        val = val.Replace("'", "''");

                        command.Parameters.AddWithValue("@"+field, val);

                        //command.Parameters["@"+field].Value = val;
                    }

                    command.ExecuteNonQuery();

                    
                    //if (c > 10) break;
                   /* ctrans++;
                    if (ctrans > 10000)
                    {
                        transaction.Commit();
                        transaction.Dispose();
                        transaction = Conn.BeginTransaction();
                        ctrans = 0;
                    }*/

                    c++;        
                    if ((c % 100) == 0)
                    {
                        int percent = (int)(((float)c / (float)lineCount) * 100);
                        //.Document.InvokeScript("writeElement", new String[] { sHtml, sHtmlElementId });
                        App.Status("Table " + table + ": " + percent.ToString() + "% - " + c.ToString() + " of " + lineCount.ToString());
                    }
                }

                transaction.Commit();
                command.Dispose();

                App.Status();
                App.Log(c.ToString() + " Datensätze aus " + fileName + " in " + table + " eingefügt");
            }

            Conn.Close();
            
        }




    }


}
