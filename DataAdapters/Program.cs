using System.Data;
using System.Data.SqlClient;

class Program
{
    static void DisplayCustomers(DataRowCollection rows, DataRelation custOrderRel)
    {
        foreach (DataRow pRow in rows)
        {
            Console.WriteLine($"id: {pRow["CustomerID"]},companyName: { pRow["CompanyName"]}");
            foreach (DataRow cRow in pRow.GetChildRows(custOrderRel))
                Console.WriteLine("\t" + cRow["OrderID"] + " " + cRow["ShipName"]);
        }
    }
    static void Main()
    {
        var connectionString = "Data Source=LAPTOP-HA5MSFQO\\SQLEXPRESS;Database=Northwind;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;";
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            var query = "SELECT CustomerID, CompanyName FROM Customers";
            var orderQuery = "SELECT * FROM Orders";
            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                SqlDataAdapter customerDA = new SqlDataAdapter(query,  connection);
                SqlDataAdapter orderDA = new SqlDataAdapter(orderQuery, connection);

                connection.Open();
                DataSet customerDS = new DataSet();

                customerDA.Fill(customerDS, "Customers");
                orderDA.Fill(customerDS, "Orders");

                // instert
                
                var newRow = customerDS.Tables["Customers"].NewRow();
                newRow["CompanyName"] = "adrianek";
                customerDS.Tables["Customer"].Rows.Add(newRow);
                customerDA.Update(customerDS);
                connection.Close();

                DataRelation custOrderRel = customerDS.Relations.Add("CustOrders", customerDS.Tables["Customers"].Columns["CustomerID"], customerDS.Tables["Orders"].Columns["CustomerID"]);
                DisplayCustomers(customerDS.Tables["Customers"].Rows, custOrderRel);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}