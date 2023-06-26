using System.Data;
using System.Data.SqlClient;
using Dapper.Contrib.Extensions;
using System.Drawing;
using FastMember;

namespace DAL
{
    [Table("Categories")]
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }
        public string CategoryName { get; set; } = "";
        public string Description { get; set; } = "";
        public byte[]? Picture { get; set; }
    }

    public class DapperCategory : ICategoriesCrud
    {
        private readonly string _connectionString;

        public DapperCategory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public int DeleteCategory(int categoryId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                db.Delete(new Category { CategoryID = categoryId });

                return 1;
            }
        }

        public DataTable GetAllCategories()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                List<Category> categories = db.GetAll<Category>().ToList();

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
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                Category category = db.Get<Category>(id);


                return category;
            }
        }

        public int InsertCategory(string categoryName, string description, byte[]? picture)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                Category category = new Category()
                {
                    CategoryName = categoryName,
                    Description = description
                };

                db.Insert(category);

                return 1;
            }
        }

        public int UpdateCategory(int categoryId, string categoryName, string description, byte[]? picture)
        {
            var categoryToUpdate = GetCategoryById(categoryId);

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                categoryToUpdate.CategoryName = categoryName;
                categoryToUpdate.Description = description;
                categoryToUpdate.Picture = picture;
                db.Update(categoryToUpdate);
            }

            return 1;
        }
    }
}