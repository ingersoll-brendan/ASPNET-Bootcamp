using Bootcamp.Data.Entities;
using Bootcamp.Data.Dtos;
using Bootcamp.Client.Blazor.Config;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using Bootcamp.Client.Blazor.Services;
using System.ComponentModel.DataAnnotations;

namespace Bootcamp.Client.Blazor.Pages
{
	public partial class Orders : ComponentBase
	{
		private OrderSearchResult OrderSearchResult { get; set; } = new OrderSearchResult()
		{
			Orders = [],
			Count = 0
		};

		private string[] SortArray { get; set; } = ["bi-arrow-up", "bi-arrow-down-up", "bi-arrow-down-up", "bi-arrow-down-up", "bi-arrow-down-up", "bi-arrow-down-up", "bi-arrow-down-up"];
		private int Skip { get; set; } = 0;
		private int Take { get; set; } = 5;
		private bool Descending { get; set; } = false;
		private int? Id { get; set; }
		private int? CustomerId { get; set; }
		private int? BillingAddressId { get; set; }
		private int? ShippingAddressId { get; set; }
		private int? OrderNumber { get; set; }
		private string? DateCreated { get; set; }
		private string? OrderDescription { get; set; }
		private string? OrderBy { get; set; } = "Id";

		private int PageNum { get; set; } = 1;
		private int NumOfPages
		{
			get
			{
				return (int)Math.Ceiling(OrderSearchResult.Count / (double)this.Take);
			}
		}
		[Inject]
		public OrderService OrderService { get; set; } = default!;

		protected override async Task OnInitializedAsync()
		{
			await this.UpdateFilters();
		}

		private async Task UpdateFilters()
		{
			this.OrderSearchResult = await OrderService.GetOrdersAsync(
				this.Skip, 
				this.Take, 
				this.Descending, 
				this.Id,
				this.CustomerId,
				this.BillingAddressId,
				this.ShippingAddressId,
				this.OrderNumber,
				Uri.EscapeDataString(this.DateCreated ?? string.Empty), 
				this.OrderDescription, 
				this.OrderBy);
		}

		private async Task ChangePage(int newPageNum)
		{
			if (newPageNum < 0 || newPageNum > this.NumOfPages)
			{
				return;
			}
			this.PageNum = newPageNum;
			this.Skip = (newPageNum - 1) * this.Take;
			await this.UpdateFilters();
		}

		private async Task ChangeSortOrder(int i)
		{
			switch (i)
			{
				case 0:
					this.OrderBy = "Id";
					break;
				case 1:
					this.OrderBy = "CustomerId";
					break;
				case 2:
					this.OrderBy = "BillingAddressId";
					break;
				case 3:
					this.OrderBy = "ShippingAddressId";
					break;
				case 4:
					this.OrderBy = "OrderNumber";
					break;
				case 5:
					this.OrderBy = "DateCreated";
					break;
				case 6:
					this.OrderBy = "OrderDescription";
					break;
			}

			if (this.SortArray[i] == "bi-arrow-down-up" || this.SortArray[i] == "bi-arrow-down")
			{
				this.SortArray[i] = "bi-arrow-up";
				this.Descending = false;
			}
			else if (this.SortArray[i] == "bi-arrow-up")
			{
				this.SortArray[i] = "bi-arrow-down";
				this.Descending = true;
			}

			for (int j = 0; j < 7; j++)
			{
				if (j != i)
				{
					this.SortArray[j] = "bi-arrow-down-up";

				}
			}

			await this.UpdateFilters();
		}
	}
}
