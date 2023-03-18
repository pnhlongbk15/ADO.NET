using System.Data;
using System.Data.SqlClient;

namespace Models
{
    public class AuthorModel : IModel
    {
        private DataSet _db;
        private SqlDataAdapter _adapter;
        private SqlConnection _connection;
        public string Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        public AuthorModel(string Id, string Name, int Age)
        {
            this.Id = Id;
            this.Name = Name;
            this.Age = Age;
        }

        public AuthorModel(DataSet db, SqlDataAdapter adapter, SqlConnection connection)
        {
            _db = db;
            _adapter = adapter;
            _connection = connection;
            CreateTable(db);
        }
        private static void CreateTable(DataSet db)
        {
            DataTable table = db.Tables.Add("Author");

            table.Columns.Add("Id", typeof(string));
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Age", typeof(int));

            table.PrimaryKey = new DataColumn[] { table.Columns["Id"] };
        }
        public void AddOne(dynamic data)
        {
            Console.WriteLine(data);
            _db.Tables["Author"].Rows.Add(data.Id, data.Name, data.Age);

        }
        // Select command
        public void GET()
        {
            _adapter.SelectCommand = new SqlCommand("select * from Author", _connection);
            _adapter.Fill(_db, "Author");
            _adapter.TableMappings.Add("Author", "Author"); // argument second is name of table in db.
        }
        public void INSERT()
        {
            var table = _db.Tables["Author"];
            _adapter.InsertCommand = new SqlCommand("insert into Author values (@Id,@Name,@Age)", _connection);
            _adapter.InsertCommand.Parameters.Add("@Id", SqlDbType.NVarChar, 255, table.Columns["Id"].ColumnName);
            _adapter.InsertCommand.Parameters.Add("@Name", SqlDbType.NVarChar, 255, table.Columns["Name"].ColumnName);
            _adapter.InsertCommand.Parameters.Add("@Age", SqlDbType.Int, 8, table.Columns["Age"].ColumnName);
            _adapter.Update(_db.Tables["Author"]);
        }
        public void DELETE()
        {
            _db.Tables["Author"].Rows[1].Delete();
            _adapter.DeleteCommand = new SqlCommand("delete from Author where Id = @Id", _connection);
            var Id = _adapter.DeleteCommand.Parameters.Add("@Id", SqlDbType.NVarChar, 255);
            Id.SourceColumn = "Id";
            Id.SourceVersion = DataRowVersion.Original;
            _adapter.Update(_db.Tables["Author"]);
        }
        public void UPDATE()
        {
            var r = _db.Tables["Author"].Rows[0];
            r["Age"] = 20;
            _adapter.UpdateCommand = new SqlCommand("update Author set Age = @Age where Id = @Id", _connection);
            _adapter.UpdateCommand.Parameters.Add("@Age", SqlDbType.NVarChar, 255, "Age");
            var Id = _adapter.UpdateCommand.Parameters.Add("@Id", SqlDbType.NVarChar, 255, "Id");
            Id.SourceVersion = DataRowVersion.Original;
            _adapter.Update(_db.Tables["Author"]);
        }
        public void Print()
        {
            foreach (DataColumn c in _db.Tables["Author"].Columns)
            {
                Console.Write($"{c.ColumnName,10}");
            }
            Console.WriteLine();
            foreach (DataRow r in _db.Tables["Author"].Rows)
            {
                Console.Write($"{r[0],10} {r[1],10} {r[2],10}");
                Console.WriteLine();
            }
        }
    }
}
