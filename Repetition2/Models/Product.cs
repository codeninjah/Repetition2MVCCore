using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Repetition2.Models
{
    public class Product
    {
		public int ProductID { get; set; }
		[Required]
		[MinLength(3)]
		public string Name { get; set; }

		[Range(1000000, double.MaxValue)]
		public decimal UnitPrice { get; set; }
		public DateTime ReleaseDate { get; set; }
		public bool IsDeleted { get; set; }

		List<Product> Products = new List<Product>();
	}
}
