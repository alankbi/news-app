using System;
namespace NewsApp
{
    public class NewsSource
    {
        public string id { get; set; }
        public string name { get; set; }

		public override int GetHashCode()
		{
            return id.GetHashCode();
		}
    }
}
