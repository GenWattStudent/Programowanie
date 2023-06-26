using DAL;
using System.Data.SqlClient;

namespace Forms
{
    public partial class Form1 : Form
    {
        private ICategoriesCrud _category;
        private Category? _categoryToUpdate;
        private byte[]? Picture;
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

            var connectionString = "Data Source=LAPTOP-HA5MSFQO\\SQLEXPRESS;Database=Northwind;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;";
            SqlConnection connection = new SqlConnection(connectionString);
            _category = new DapperCategory(connectionString);

            var cat = _category.GetAllCategories();

            categoryGrid.DataSource = cat;
        }

        private void SetUpdateUI(DataGridViewRow row)
        {
            _categoryToUpdate = _category.GetCategoryById((int)row.Cells["CategoryIDColumn"].Value);
            categoryNameInput.Text = _categoryToUpdate.CategoryName;
            descriptionInput.Text = _categoryToUpdate.Description;

            if (_categoryToUpdate.Picture is not null)
            {
                byte[] imageData = _categoryToUpdate.Picture;
                bool hasOffset = imageData.Length > 78 && imageData[78] != 0;

                if (hasOffset)
                {
                    byte[] imageDataWithoutOffset = new byte[imageData.Length - 78];
                    Array.Copy(imageData, 78, imageDataWithoutOffset, 0, imageDataWithoutOffset.Length);

                    using (MemoryStream ms = new MemoryStream(imageDataWithoutOffset))
                    {
                        Bitmap bmp = new Bitmap(ms);
                        imagePreview.Image = bmp;
                    }
                }
                else
                {
                    using (MemoryStream ms = new MemoryStream(imageData))
                    {
                        Bitmap bmp = new Bitmap(ms);
                        imagePreview.Image = bmp;
                    }
                }
            }

            button2.Text = "Update Category";
            button2.Click -= AddCategory;
            button2.Click += UpdateCategory;
        }

        private void categoryGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (e.ColumnIndex == senderGrid.Columns["deleteButton"].Index && e.RowIndex >= 0)
            {
                DeleteCategory((int)senderGrid.Rows[e.RowIndex].Cells["categoryIDColumn"].Value);
            }

            else if (e.ColumnIndex == senderGrid.Columns["updateButton"].Index && e.RowIndex >= 0)
            {
                SetUpdateUI(senderGrid.Rows[e.RowIndex]);
            }

            else
            {
                ClearInputs();
                _categoryToUpdate = null;
                button2.Text = "Add Category";
                button2.Click -= UpdateCategory;
                button2.Click += AddCategory;
            }
        }
        private void ClearInputs()
        {
            categoryNameInput.Text = "";
            descriptionInput.Text = "";
            imagePreview.ImageLocation = "";
            imagePreview.Image = null;
        }

        private void AddCategory(object sender, EventArgs e)
        {
            if (categoryNameInput.Text == "")
            {
                MessageBox.Show("Category name is required");
                return;
            }

            var result = _category.InsertCategory(categoryNameInput.Text, descriptionInput.Text, Picture);

            if (result == 1)
            {
                categoryGrid.DataSource = _category.GetAllCategories();
                ClearInputs();
            }
            else
            {
                MessageBox.Show("Category not added");
            }
        }

        private void UpdateCategory(object sender, EventArgs e)
        {
            if (_categoryToUpdate is not null)
            {
                var result = _category.UpdateCategory(_categoryToUpdate.CategoryID, categoryNameInput.Text, descriptionInput.Text, Picture);
                if (result == 1)
                {
                    categoryGrid.DataSource = _category.GetAllCategories();
                    ClearInputs();
                    _categoryToUpdate = null;
                    button2.Text = "Add Category";
                    button2.Click -= UpdateCategory;
                    button2.Click += AddCategory;
                    MessageBox.Show($"Category {categoryNameInput.Text} updated successfully");
                }
                else
                {
                    MessageBox.Show("Category not updated");
                }
            }
        }

        private void DeleteCategory(int id)
        {
            var result = _category.DeleteCategory(id);
            if (result == 1)
            {
                categoryGrid.DataSource = _category.GetAllCategories();
                MessageBox.Show($"Category with id: {id} deleted successfully");
            }
            else
            {
                MessageBox.Show("Category not deleted");
            }
        }

        private void imageButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Wybierz obraz";
                openFileDialog.Filter = "Pliki obraz�w (*.png;)|*.png; |Wszystkie pliki (*.*)|*.*";
                openFileDialog.Multiselect = false;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    Image image = Image.FromFile(filePath);
                    imagePreview.ImageLocation = openFileDialog.FileName;

                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        image.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                        Picture = memoryStream.ToArray();
                    }
                }
                else
                {
                    imagePreview.ImageLocation = "";
                    imagePreview.Image = null;
                }
            }
        }

        private void categoryGrid_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void imagePreview_Click(object sender, EventArgs e)
        {

        }
    }
}