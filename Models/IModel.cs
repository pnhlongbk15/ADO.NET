using System.Data;

namespace Models
{
    public interface IModel
    {
        private static void CreateTable(DataSet db) { }
        public void AddOne(dynamic data) { }

        public void GET() { }

        public void INSERT() { }
        public void DELETE() { }
        public void UPDATE() { }


        public void Print() { }
    }
}
