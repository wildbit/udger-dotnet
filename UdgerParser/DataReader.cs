﻿/*
  UdgerParser - Local parser lib
  
  UdgerParser class parses useragent strings based on a database downloaded from udger.com
 
 
  author     The Udger.com Team (info@udger.com)
  copyright  Copyright (c) Udger s.r.o.
  license    GNU Lesser General Public License
  link       https://udger.com/products/local_parser
 */

using System;
using System.Data;
using System.Data.SQLite;

namespace Udger.Parser
{
    class DataReader
    {

        public string DataSourcePath { get; set; }
        private bool connected;
        public bool Connected { get { return this.connected; } }
        private SQLiteConnection sqlite;
        public string data_filenam { get; set; }
        public string data_dir { get; set; }
        UdgerParser udger;


        public DataReader()
        {

        }

        public void connect(UdgerParser _udger)
        {
            udger = _udger;
            try
            { 
                if (!this.Connected)
                {                   
                    sqlite = new SQLiteConnection(@"Data Source=" + DataSourcePath + ";Mode=ReadOnly;");
                    this.connected = true;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public DataTable selectQuery(string query)
        {
            if (connected)
            {
                SQLiteDataAdapter ad;
                DataTable dt = new DataTable();

                try
                {
                    SQLiteCommand cmd;
                    sqlite.Open();  //Initiate connection to the db
                    cmd = sqlite.CreateCommand();
                    cmd.CommandText = query;  //set the passed query
                    ad = new SQLiteDataAdapter(cmd);
                    ad.Fill(dt); //fill the datasource
                }
                catch (SQLiteException ex)
                {
                    throw ex;
                }
                sqlite.Close();
                return dt;
            }
            return new DataTable();
        }


    }
}