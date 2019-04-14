using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace a1.Controllers
{
    public class qaRatingController
    {
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;

        void connectionString()
        {
            con.ConnectionString = "data source=.\\SQLEXPRESS; database=paathshala; integrated security=SSPI;";
        }

        public bool isLike(int qa_id)
        {
            int qaLike=0;
            connectionString();
            con.Open();
            com.Connection = con;
            com.CommandText = "SELECT * FROM qa_rating WHERE Qa_id='" + qa_id + "' AND U_id='" + globalVariable.uid + "'";
            dr = com.ExecuteReader();
            while (dr.Read())
            {
                qaLike = Convert.ToInt32(dr["Qa_rating"]);
            }
            con.Close();

            if(qaLike == 1)
                return true;
            else
                return false;
        }

        public bool isDislike(int qa_id)
        {
            int qaDislike = 1;
            connectionString();
            con.Open();
            com.Connection = con;
            com.CommandText = "SELECT * FROM qa_rating WHERE Qa_id='" + qa_id + "' AND U_id='" + globalVariable.uid + "'";
            dr = com.ExecuteReader();
            while (dr.Read())
            {
                qaDislike = Convert.ToInt32(dr["Qa_rating"]);
            }
            con.Close();

            if (qaDislike == 0)
                return true;
            else
                return false;
        }

        public int likeCount(int qa_id)
        {
            int lc = 0;
            connectionString();
            con.Open();
            com.Connection = con;
            com.CommandText = "SELECT COUNT (Qa_rating) FROM qa_rating WHERE Qa_id='" + qa_id + "' AND Qa_rating='1'";
            dr = com.ExecuteReader();
            while (dr.Read())
            {
                lc = dr.GetInt32(0);
            }
            con.Close();

            return lc;
        }

        public int dislikeCount(int qa_id)
        {
            int dc = 0;
            connectionString();
            con.Open();
            com.Connection = con;
            com.CommandText = "SELECT COUNT (Qa_rating) FROM qa_rating WHERE Qa_id='" + qa_id + "' AND Qa_rating='0'";
            dr = com.ExecuteReader();
            while (dr.Read())
            {
                dc = dr.GetInt32(0);
            }
            con.Close();

            return dc;
        }

        public void insertLike(int qa_id)
        {
            connectionString();
            con.Open();
            com.Connection = con;
            com.CommandText = "MERGE qa_rating AS T USING (VALUES('" + qa_id + "', '" + globalVariable.uid + "', '1')) AS S(Qa_id,U_id,Qa_rating) ON (T.Qa_id = S.Qa_id AND T.U_id = S.U_id)" +
                "WHEN NOT MATCHED BY TARGET THEN INSERT(Qa_id,U_id,Qa_rating ) VALUES(S.Qa_id,S.U_id, S.Qa_rating) " +
                "WHEN MATCHED THEN UPDATE SET T.Qa_rating = S.Qa_rating;";
            dr = com.ExecuteReader();
            con.Close();

            int uid=0;
            con.Open();
            com.Connection = con;
            com.CommandText = "SELECT U_id FROM qa WHERE Qa_id ='" + qa_id + "'";
            dr = com.ExecuteReader();
            while(dr.Read())
            {
                uid = Convert.ToInt32(dr["U_id"]);
            }
            con.Close();

            con.Open();
            com.Connection = con;
            com.CommandText = "UPDATE user_t SET Rating = Rating+1 WHERE U_id ='" + uid + "'";
            dr = com.ExecuteReader();
            con.Close();
        }

        public void insertDislike(int qa_id)
        {
            connectionString();
            con.Open();
            com.Connection = con;
            com.CommandText = "MERGE qa_rating AS T USING (VALUES('" + qa_id + "', '" + globalVariable.uid + "', '0')) AS S(Qa_id,U_id,Qa_rating) ON (T.Qa_id = S.Qa_id AND T.U_id = S.U_id)" +
                "WHEN NOT MATCHED BY TARGET THEN INSERT(Qa_id,U_id,Qa_rating ) VALUES(S.Qa_id,S.U_id, S.Qa_rating) " +
                "WHEN MATCHED THEN UPDATE SET T.Qa_rating = S.Qa_rating;";
            dr = com.ExecuteReader();
            con.Close();

            int uid = 0;
            con.Open();
            com.Connection = con;
            com.CommandText = "SELECT U_id FROM qa WHERE Qa_id ='" + qa_id + "'";
            dr = com.ExecuteReader();
            while (dr.Read())
            {
                uid = Convert.ToInt32(dr["U_id"]);
            }
            con.Close();

            con.Open();
            com.Connection = con;
            com.CommandText = "UPDATE user_t SET Rating = Rating-1 WHERE U_id ='" + uid + "'";
            dr = com.ExecuteReader();
            con.Close();
        }

        public void unRate(int qa_id)
        {
            connectionString();
            con.Open();
            com.Connection = con;
            com.CommandText = "DELETE FROM qa_rating WHERE Qa_id='" + qa_id + "' AND U_id='" + globalVariable.uid + "'";
            dr = com.ExecuteReader();
            con.Close();
        }
    }
}