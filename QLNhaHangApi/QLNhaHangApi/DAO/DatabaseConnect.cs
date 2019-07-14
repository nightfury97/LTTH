﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace QLNhaHangApi.DAO
{
    public class DatabaseConnect
    {
        public SqlConnection dataConection()
        {
            return new SqlConnection(@"Data Source=.;Initial Catalog=QLNHAHANG2;Persist Security Info=False;User ID=sa;Password=123456;Encrypt=False;TrustServerCertificate=False");
            // các chỗ cũng sửa connect là ở BillInfoDAO và BillDAO menuDAO MenuCategoryDAO
        }
        public DataTable GETdata(string querySQL)
        {
            try
            {
                SqlConnection connection = dataConection();
                SqlDataAdapter adapter = new SqlDataAdapter(querySQL, connection);
                DataTable data = new DataTable();
                adapter.Fill(data);
                return data;
            }
            catch
            {
               
                return null;
            }
        }

        public void ExecuteNonQuery(string querySQL)
        {
            try
            {
                SqlConnection connection = dataConection();
                connection.Open();
                SqlCommand command = new SqlCommand(querySQL, connection);
                command.ExecuteNonQuery();
                command.Dispose();
                connection.Close();

            }
            catch
            {
                

            }
        }
    }
}