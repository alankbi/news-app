using System;
namespace NewsApp
{
    public class NewsSource
    {
        public string ID { get; set; }
        public string Name { get; set; }

		public override int GetHashCode()
		{
            return ID.GetHashCode();
		}
    }
}
