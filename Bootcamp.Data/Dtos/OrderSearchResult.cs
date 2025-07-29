using Bootcamp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bootcamp.Data.Dtos
{
	public class OrderSearchResult
	{
		public IEnumerable<Order> Orders { get; set; } = [];
		public int Count { get; set; }
	}
}
