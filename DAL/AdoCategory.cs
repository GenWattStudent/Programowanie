using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class AdoCategory : ICategoriesCrud
    {
        public SqlConnection connection { get; set; }
        public string query = "SELECT * FROM Categories";
        public string catByNameQuery = "SELECT * FROM Categories WHERE CategoryName = ";
        public string insertQuery = "INSERT INTO Categories(CategoryName, Description) VALUES(@name, @desc)";
        public string updateQuery = "UPDATE Categories SET CategoryName = @name, Description = @desc WHERE CategoryID = @id";
        public string deleteQuery = "DELETE FROM Categories WHERE CategoryID = @id";
        public AdoCategory(SqlConnection con)
        {
            connection = con;
        }
        public int DeleteCategory(int categoryId)
        {
            connection.Open();
            SqlCommand command4 = new SqlCommand(deleteQuery, connection);
            command4.Parameters.AddWithValue("@id", categoryId);

            var result = command4.ExecuteNonQuery();
            connection.Close();
            return result;
        }

        public DataTable GetAllCategories()
        {
            var table = new DataTable();
            using (var da = new SqlDataAdapter(query, connection))
            {
                da.Fill(table);
            }

            return table;
        }

        public DataTable GetCategoriesByName(string name)
        {
            var table = new DataTable();
            catByNameQuery += name;
            using (var da = new SqlDataAdapter(catByNameQuery, connection))
            {
                da.Fill(table);
            }

            return table;
        }

        public int InsertCategory(string categoryName, string description, byte[] picture)
        {
            connection.Open();

            var insertCommand = new SqlCommand(insertQuery, connection);
            insertCommand.Parameters.AddWithValue("@name", categoryName);
            insertCommand.Parameters.AddWithValue("@desc", description);

            var result = insertCommand.ExecuteNonQuery();
            connection.Close();
            return result;
        }

        public int UpdateCategory(int categoryId, string categoryName, string description, byte[] picture)
        {
            connection.Open();
            SqlCommand updateCommand = new SqlCommand(updateQuery, connection);
            updateCommand.Parameters.AddWithValue("@name", categoryName);
            updateCommand.Parameters.AddWithValue("@id", categoryId);
            updateCommand.Parameters.AddWithValue("@desc", description);

            var result = updateCommand.ExecuteNonQuery();
            connection.Close();
            return result;
        }

        public Category GetCategoryById(int id)
        {
            connection.Open();

            var table = new DataTable();
            var query = "SELECT * FROM Categories WHERE CategoryID = @id";
            using (var da = new SqlDataAdapter(query, connection))
            {
                da.SelectCommand.Parameters.AddWithValue("@id", id);
                da.Fill(table);
            }

            connection.Close();
            return new Category
            {
                CategoryID = (int)table.Rows[0]["CategoryID"],
                CategoryName = table.Rows[0]["CategoryName"].ToString(),
                Description = table.Rows[0]["Description"].ToString(),
            };
        }
    }
}
