using System;
namespace Atlas.Domain
{
	public class PhotoToGood
	{
		public Guid Id { get; set; }

		public Guid GoodId { get; set; }

		public string PhotoPath { get; set; }

		public Good Good { get; set; }
	}
}

