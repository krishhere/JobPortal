using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;

namespace ClinetraSolutions.Services
{
    public class DbData
    {
        public static string jsonPath = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
        public static string jsonString = File.ReadAllText(jsonPath);
        public static JObject ReadJSONData()
        {
            JObject jObject;
            using (StreamReader file = System.IO.File.OpenText(jsonPath))
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                jObject = (JObject)JToken.ReadFrom(reader);
            }
            return jObject;
        }

        public static DataTable GetAllEmployee()
        {
            string query = "SELECT id, full_name, email, password, phone, user_type, profile_picture, created_at FROM users where user_type = 'employee'";
            return GetDBTable(query, "Users");
        }
        public static DataTable GetDBTable(string query, string tableName)
        {
            MySqlConnection connection = null;
            DataTable dt = new DataTable(tableName);
            JObject db = ReadJSONData();
            string conn = db["ConnectionStrings"]["MySqlConnection"].ToString();
            try
            {
                connection = new MySqlConnection(conn);
                connection.Open();
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.Text;
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Technical issues. please try later');", true);
            }
            finally
            {
                connection.Close();
            }
            return dt;
        }
        public static DataTable GetUserCountByType()
        {
            string query = @"
            SELECT user_type, COUNT(*) AS Total
            FROM users
            GROUP BY user_type";
            return GetDBTable(query, "UserTypeSummary");
        }

        public static DataTable GetJobPostCountByCategory()
        {
            string query = @"
            SELECT jc.name AS Category, COUNT(*) AS Total
            FROM job_posts jp
            JOIN job_categories jc ON jp.category_id = jc.id
            GROUP BY jc.name";
            return GetDBTable(query, "JobCategorySummary");
        }
    }
}