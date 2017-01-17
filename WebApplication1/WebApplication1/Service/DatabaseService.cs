using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Service
{
    public class DatabaseService
    {

        public List<Models.Member> LoadAllMember()
        {
            List<Models.Member> result = new List<Models.Member>();

            var connection = new System.Data.SqlClient.SqlConnection(@"
Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Terry\Source\Repos\homework2\WebApplication1\WebApplication1\App_Data\Database1.mdf;Integrated Security=True");
            connection.Open();
            var command = new System.Data.SqlClient.SqlCommand("", connection);
            command.CommandText = @"
Select * from Member";
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                Models.Member item = new Models.Member();

                item.ID = reader["ID"].ToString();
                item.job = reader["job"].ToString();
                item.name = reader["name"].ToString();
                item.motto = reader["motto"].ToString();
                item.ImageUrl = reader["ImageUrl"].ToString();
                result.Add(item);
            }
            connection.Close();
            return result;
        }
        public Models.Member GetMemberByID(string id)
        {
            Models.Member result = new Models.Member();

            var connection = new System.Data.SqlClient.SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Terry\Source\Repos\homework2\WebApplication1\WebApplication1\App_Data\Database1.mdf;Integrated Security=True");
            connection.Open();


            var command = new System.Data.SqlClient.SqlCommand("", connection);
            command.CommandText = string.Format(@"
Select * from Member
Where ID='{0}'", id);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                Models.Member item = new Models.Member();

                item.ID = reader["ID"].ToString();
                item.job = reader["job"].ToString();
                item.name = reader["name"].ToString();
                item.motto = reader["motto"].ToString();
                item.ImageUrl = reader["ImageUrl"].ToString();
                result = item;
            }
            connection.Close();
            return result;
        }
        public void CreateMember(Models.Member newMember)
        {
            var connection = new System.Data.SqlClient.SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Terry\Source\Repos\homework2\WebApplication1\WebApplication1\App_Data\Database1.mdf;Integrated Security=True");
            connection.Open();


            var command = new System.Data.SqlClient.SqlCommand("", connection);
            command.CommandText = string.Format(@"
INSERT        INTO    Member(ID, job, name, motto, ImageUrl)
VALUES          ('{0}','{1}','{2}','{3}','{4}')
", newMember.ID, newMember.job, newMember.name, newMember.motto, newMember.ImageUrl);

            command.ExecuteNonQuery();


            connection.Close();
        }


        public void DeleteMember(string id)
        {
            var connection = new System.Data.SqlClient.SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Terry\Source\Repos\homework2\WebApplication1\WebApplication1\App_Data\Database1.mdf;Integrated Security=True");
            connection.Open();
            var command = new System.Data.SqlClient.SqlCommand("", connection);
            command.CommandText = string.Format(@"
DELETE FROM Member
Where ID='{0}'
", id);

            command.ExecuteNonQuery();
            connection.Close();

        }
        public void UpdateMember(Models.Member updateMember)
        {
            var connection = new System.Data.SqlClient.SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Terry\Source\Repos\homework2\WebApplication1\WebApplication1\App_Data\Database1.mdf;Integrated Security=True");
            connection.Open();
            var command = new System.Data.SqlClient.SqlCommand("", connection);
            command.CommandText = string.Format(@"
UPDATE          Member
SET             job='{1}',name='{2}',motto='{3}',ImageUrl='{4}'
Where           ID='{0}'
", updateMember.ID, updateMember.job, updateMember.name, updateMember.motto, updateMember.ImageUrl);
            command.ExecuteNonQuery();
            connection.Close();
        }

    }
}