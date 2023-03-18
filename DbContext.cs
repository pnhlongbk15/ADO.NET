using Models;
using System.Data;
using System.Data.SqlClient;


public class DbContext
{
    private DataSet dataSet;
    private SqlDataAdapter adapter;

    public DataTable AuthorTable { get { return dataSet.Tables["Author"]; } }
    public IModel Author
    { get; set; }
    public IModel Book { get; set; } // setIModel thi method Add khong chay
    public DbContext()
    {
        var connectionString = "Data Source=LAPTOP-QD1OPRSA\\SQLEXPRESS;Integrated Security=true;Initial Catalog=BookStore;TrustServerCertificate=True;Encrypt=False";
        var connection = new SqlConnection(connectionString);
        dataSet = new DataSet();
        adapter = new SqlDataAdapter();
        Author = new AuthorModel(dataSet, adapter, connection);
        Book = new BookModel(dataSet);
    }
}


