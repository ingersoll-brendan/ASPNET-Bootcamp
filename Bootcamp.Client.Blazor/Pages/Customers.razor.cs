using Bootcamp.Data.Entities;
using Bootcamp.Data.Dtos;
using Bootcamp.Client.Blazor.Config;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using Bootcamp.Client.Blazor.Services;
using System.ComponentModel.DataAnnotations;

namespace Bootcamp.Client.Blazor.Pages
{
    public partial class Customers : ComponentBase
    {
		private CustomerSearchResult CustomerSearchResult { get; set; } = new CustomerSearchResult()
        {
            Customers = [],
            Count = 0
		};

        private string[] SortArray { get; set; } = ["bi-arrow-up", "bi-arrow-down-up", "bi-arrow-down-up", "bi-arrow-down-up", "bi-arrow-down-up"];
		private int Skip { get; set; } = 0;
        private int Take { get; set; } = 5;
        private bool Descending { get; set; } = false;
		private int? Id { get; set; }
		private string? FirstName { get; set; }
		private string? LastName { get; set; }
		private string? Email { get; set; }
		private string? PhoneNumber { get; set; }
        private string? OrderBy { get; set; } = "Id";

		private int PageNum { get; set; } = 1;
        private int NumOfPages
        {
            get
            {
                return (int)Math.Ceiling(CustomerSearchResult.Count / (double) this.Take);
            }
        }

        [Inject]
        public CustomerService CustomerService { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            await this.UpdateFilters();
		}

        private async Task UpdateFilters()
        {
            this.CustomerSearchResult = await CustomerService.GetCustomersAsync(
                this.Skip, 
                this.Take, 
                this.Descending, 
                this.Id, 
                this.FirstName, 
                this.LastName, 
                this.Email, 
                this.PhoneNumber, 
                this.OrderBy);
		}

        private async Task ChangePage(int newPageNum)
        {
            if(newPageNum < 0 || newPageNum > this.NumOfPages)
            {
                return;
            }
            this.PageNum = newPageNum;
            this.Skip = (newPageNum - 1) * this.Take;
            await this.UpdateFilters();
        }

        private async Task ChangeSortOrder(int i)
        {
            switch(i)
            {
                case 0:
					this.OrderBy = "Id";
                    break;
                case 1:
					this.OrderBy = "FirstName";
                    break;
                case 2:
					this.OrderBy = "LastName";
                    break;
                case 3:
					this.OrderBy = "Email";
                    break;
                case 4:
					this.OrderBy = "PhoneNumber";
                    break;
			}

            if (this.SortArray[i] == "bi-arrow-down-up" || this.SortArray[i] == "bi-arrow-down")
            {
                this.SortArray[i] = "bi-arrow-up";
                this.Descending = false;
			}
            else if(this.SortArray[i] == "bi-arrow-up")
            {
				this.SortArray[i] = "bi-arrow-down";
                this.Descending = true;
			}

            for(int j = 0; j < 5; j++)
            {
                if(j != i)
                {
                    this.SortArray[j] = "bi-arrow-down-up";

				}
            }

            await this.UpdateFilters();
        }
    }
}
