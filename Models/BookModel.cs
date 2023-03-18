using System.Data;

namespace Models
{
    public class BookModel : IModel
    {
        private DataSet _db;

        public string Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string AuthorId { get; set; }
        public BookModel()
        {
            Console.WriteLine("data");
        }
        public BookModel(DataSet db)
        {
            _db = db;
            CreateTable(db);
            Console.WriteLine("initial");
        }

        private static void CreateTable(DataSet db)
        {
            DataTable table = db.Tables.Add("Book");

            table.Columns.Add("Id", typeof(string));
            table.Columns.Add("Title", typeof(string));
            table.Columns.Add("Author", typeof(string));
            table.Columns.Add("AuthorId", typeof(string));

            table.PrimaryKey = new DataColumn[] { table.Columns["Id"] };
            ForeignKeyConstraint foreignKey = new ForeignKeyConstraint("AuthorBookFk",
                db.Tables["Author"].Columns["Id"], table.Columns["AuthorId"]
                );

            table.Constraints.Add(foreignKey);
        }
        //string id, string title, string author, string authorId
        public void Add<T>(List<T> data)
        {
            Console.WriteLine(data.Count);
            foreach (dynamic book in data)
            {
                _db.Tables["Book"].Rows.Add(book.Id, book.Title, book.Author, book.AuthorId);
            }
        }

        public void Print()
        {
            foreach (DataColumn c in _db.Tables["Book"].Columns)
            {
                Console.Write($"{c.ColumnName,10}");
            }
            Console.WriteLine();
            foreach (DataRow r in _db.Tables["Book"].Rows)
            {

                Console.Write($"{r[0],10} {r[1],10} {r[2],10} {r[3],10}");
                Console.WriteLine();
            }
        }
    }

}

