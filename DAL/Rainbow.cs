using Dapper;
using FastMember;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace DAL
{
    class RainbowDatabase : Database<RainbowDatabase>
    {
        public Table<Category> Categories { get; set; }
    }

    public class Rainbow : ICategoriesCrud
    {
        public string ConnectionString { get; set; }
        public Rainbow(string connectionString)
        {
            ConnectionString = connectionString;
        }
        public int DeleteCategory(int categoryId)
        {
            using (DbConnection connection = new SqlConnection(ConnectionString))
            {
                var db = RainbowDatabase.Init(connection, commandTimeout: 2);
                db.Connection.Query($"DELETE FROM Categories WHERE CategoryID = {categoryId}");
            }

            return 1;
        }

        public DataTable GetAllCategories()
        {
            using (DbConnection connection = new SqlConnection(ConnectionString))
            {
                var db = RainbowDatabase.Init(connection, commandTimeout: 2);
                var categories = db.Categories.All().ToList();

                DataTable table = new DataTable();
                using (var reader = ObjectReader.Create(categories))
                {
                    table.Load(reader);
                }
                return table;
            }
        }

        public DataTable GetCategoriesByName(string name)
        {
            throw new NotImplementedException();
        }

        public Category GetCategoryById(int id)
        {
            using (DbConnection connection = new SqlConnection(ConnectionString))
            {
                var db = RainbowDatabase.Init(connection, commandTimeout: 2);
                var query = "SELECT * FROM Categories WHERE CategoryID = @CategoryID";
                var parameters = new { CategoryID = id };
                var result = db.Connection.QueryFirstOrDefault<Category>(query, parameters);

                return result;
            }
        }

        public int InsertCategory(string categoryName, string description, byte[]? picture)
        {
            using (DbConnection connection = new SqlConnection(ConnectionString))
            {
                var db = RainbowDatabase.Init(connection, commandTimeout: 2);
                var category = new 
                {
                    CategoryName = categoryName,
                    Description = description,
                    Picture = picture
                };
                db.Categories.Insert(category);

                return 1;
            }
        }

        public int UpdateCategory(int categoryId, string categoryName, string description, byte[]? picture)
        {
            using (DbConnection connection = new SqlConnection(ConnectionString))
            {
                var db = RainbowDatabase.Init(connection, commandTimeout: 2);

                var query = "UPDATE Categories SET CategoryName = @CategoryName, Description = @Description";
                var parameters = new { CategoryName = categoryName, Description = description, CategoryId = categoryId, Picture = picture };

                if (picture is not null)
                {
                    query += ", Picture = @Picture WHERE CategoryID = @CategoryId";
                } 
                else
                {
                    query += " WHERE CategoryID = @CategoryId";
                }

                var result = db.Connection.Execute(query, parameters);

                return result;
            }
        }
    }
}
