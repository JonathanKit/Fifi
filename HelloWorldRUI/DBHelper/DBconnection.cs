//using Microsoft.Analytics.Interfaces;
//using Microsoft.Analytics.Types.Sql;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using MySql.Data.MySqlClient;


namespace HelloWorldRUI.DBHelper
{
    public static class DBHelper
    {

        public static MySqlConnection connection;
        private static MySqlCommand cmd = null;
        private static DataTable dt;
        private static MySqlDataAdapter sda;

        /*
         * Job is to establish connection to the database
         */
        public static void EstablishConnection()
        {
            try
            {
                //private string connection_info = "server=localhost;user id=root;persistsecurityinfo=True;database=movies;password=root";
                MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
                builder.Server = "localhost";
                builder.UserID = "root";
                builder.Password = "root";
                builder.Database = "movies";
                builder.SslMode = MySqlSslMode.Required;
                connection = new MySqlConnection(builder.ToString());
                Console.WriteLine("Connection estabilished");
                //MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Database connection successfull", "Connection", MessageBoxButtons.OK, MessageBoxIcon.Information);
            


            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("connection Failed");
            }
        }

        public static MySqlCommand RunQuery(string query, string username)
        {
            try
            {
                if (connection != null)
                {
                    connection.Open();
                    cmd = new MySqlCommand(query, connection);
                    
                    /*
                    cmd = connection.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = query;
                    //cmd.Parameters.AddWithValue("@username", username);
                    cmd.ExecuteNonQuery();
                    */
                    
                    
                }
            }
            catch (Exception ex)
            {
                connection.Close();
            }
            return cmd;
        }
    }
}


        //string connection_info = "SERVER=localhost,DATABASE=movies;UID=root;PASSWORD=root";
        //MySqlConnection connection = new MySqlConnection(connection_info);

