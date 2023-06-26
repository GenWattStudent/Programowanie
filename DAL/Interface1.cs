using System.Data;

namespace DAL
{
    public interface ICategoriesCrud
    {
        DataTable GetAllCategories();
        DataTable GetCategoriesByName(string name);
        Category GetCategoryById(int id);
        int UpdateCategory(int categoryId, string categoryName, string description, byte[]? picture);
        int InsertCategory(string categoryName, string description, byte[]? picture);
        int DeleteCategory(int categoryId);
    }
}
