using System;
using SQLite;
namespace NewsApp
{
    [Table("Sources")]
    public class NewsSource
    {
        [PrimaryKey, AutoIncrement]
        public int IDKey { get; set; }

        public string ID { get; set; }
        public string Name { get; set; }

		public override int GetHashCode()
		{
            return ID.GetHashCode();
		}
    }
}
