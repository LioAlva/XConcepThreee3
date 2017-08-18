using SQLite.Net.Attributes;

namespace XConcepThreee3.Classes
{
    public class Product
    {
        [PrimaryKey,AutoIncrement]
        public int ProductId { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }
    }
}