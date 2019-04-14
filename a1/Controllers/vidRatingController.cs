using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace a1.Controllers
{
    public class vidRatingController
    {
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;

        void connectionString()
        {
            con.ConnectionString = "data source=.\\SQLEXPRESS; database=paathshala; integrated security=SSPI;";
        }

        public bool isLike(int vid_id)
        {
            int vidLike = 0;
            connectionString();
            con.Open();
            com.Connection = con;
            com.CommandText = "SELECT * FROM vid_rating WHERE Vid_id='" + vid_id + "' AND U_id='" + globalVariable.uid + "'";
            dr = com.ExecuteReader();
            while (dr.Read())
            {
                vidLike = Convert.ToInt32(dr["Vid_rating"]);
            }
            con.Close();

            if (vidLike == 1)
                return true;
            else
                return false;
        }

        public bool isDislike(int vid_id)
        {
            int Dislike = 1;
            connectionString();
            con.Open();
            com.Connection = con;
            com.CommandText = "SELECT * FROM vid_rating WHERE Vid_id='" + vid_id + "' AND U_id='" + globalVariable.uid + "'";
            dr = com.ExecuteReader();
            while (dr.Read())
            {
                Dislike = Convert.ToInt32(dr["Vid_rating"]);
            }
            con.Close();

            if (Dislike == 0)
                return true;
            else
                return false;
        }

        public int likeCount(int vid_id)
        {
            int lc = 0;
            connectionString();
            con.Open();
            com.Connection = con;
            com.CommandText = "SELECT COUNT (Vid_rating) FROM vid_rating WHERE Vid_id='" + vid_id + "' AND Vid_rating='1'";
            dr = com.ExecuteReader();
            while (dr.Read())
            {
                lc = dr.GetInt32(0);
            }
            con.Close();

            return lc;
        }

        public int dislikeCount(int vid_id)
        {
            int dc = 0;
            connectionString();
            con.Open();
            com.Connection = con;
            com.CommandText = "SELECT COUNT (Vid_rating) FROM vid_rating WHERE Vid_id='" + vid_id + "' AND Vid_rating='0'";
            dr = com.ExecuteReader();
            while (dr.Read())
            {
                dc = dr.GetInt32(0);
            }
            con.Close();

            return dc;
        }

        public void insertLike(int vid_id)
        {
            connectionString();
            con.Open();
            com.Connection = con;
            com.CommandText = "MERGE vid_rating AS T USING (VALUES('" + vid_id + "', '" + globalVariable.uid + "', '1')) AS S(Vid_id,U_id,Vid_rating) ON (T.Vid_id = S.Vid_id AND T.U_id = S.U_id)" +
                "WHEN NOT MATCHED BY TARGET THEN INSERT(Vid_id,U_id,Vid_rating ) VALUES(S.Vid_id,S.U_id, S.Vid_rating) " +
                "WHEN MATCHED THEN UPDATE SET T.Vid_rating = S.Vid_rating;";
            dr = com.ExecuteReader();
            con.Close();

            int uid = 0;
            con.Open();
            com.Connection = con;
            com.CommandText = "SELECT U_id FROM video WHERE Vid_id ='" + vid_id + "'";
            dr = com.ExecuteReader();
            while (dr.Read())
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

        public void insertDislike(int vid_id)
        {
            connectionString();
            con.Open();
            com.Connection = con;
            com.CommandText = "MERGE vid_rating AS T USING (VALUES('" + vid_id + "', '" + globalVariable.uid + "', '0')) AS S(Vid_id,U_id,Vid_rating) ON (T.Vid_id = S.Vid_id AND T.U_id = S.U_id)" +
                "WHEN NOT MATCHED BY TARGET THEN INSERT(Vid_id,U_id,Vid_rating ) VALUES(S.Vid_id,S.U_id, S.Vid_rating) " +
                "WHEN MATCHED THEN UPDATE SET T.Vid_rating = S.Vid_rating;";
            dr = com.ExecuteReader();
            con.Close();

            int uid = 0;
            con.Open();
            com.Connection = con;
            com.CommandText = "SELECT U_id FROM video WHERE Vid_id ='" + vid_id + "'";
            dr = com.ExecuteReader();
            while (dr.Read())
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

        public void unRate(int vid_id)
        {
            connectionString();
            con.Open();
            com.Connection = con;
            com.CommandText = "DELETE FROM vid_rating WHERE Vid_id='" + vid_id + "' AND U_id='" + globalVariable.uid + "'";
            dr = com.ExecuteReader();
            con.Close();
        }
    }
}