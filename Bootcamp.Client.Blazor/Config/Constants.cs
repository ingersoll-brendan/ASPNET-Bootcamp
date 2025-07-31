namespace Bootcamp.Client.Blazor.Config;

public static class Constants
{
	#region Bootcamp API Config

	public const string Bootcamp_ApiHttpClientName = "BootcampAPI";

	// TODO: We need to determine how to configure this for all environments?

	// TODO: Use a "Compiler" Statement (i.e. #if DEV, #if INT, #if PROD, etc.
	// Or does Blazor have AppSettings.json that can be used simaliar to Web Projects
	// and an run time, we can load the correct ones?
	//test
#if DEBUG || DEV
		public const string Bootcamp_ApiBaseUrl = "https://localhost:7119/api/";
#elif INT
		public const string Bootcamp_ApiBaseUrl = "https://symmsoft-bootcamp-api-int.azurewebsites.net/api/";

		// FUTURE: Add other Environment URLs (i.e. QA, UAT)
#else
	public const string Bootcamp_ApiBaseUrl = "https://symmsoft-bootcamp-api-prod.azurewebsites.net/api";
	#endif

	#endregion

	#region Bootcamp API Endpoints

	public const string Bootcamp_Customers = "customers";
	public const string Bootcamp_Customer = "customers/{id}";

	public const string Bootcamp_Addresses = "addresses";
	public const string Bootcamp_Address = "addresses/{id}";
	public const string Bootcamp_AddressesByCustomerId = "addresses/customer/{id}";

	public const string Bootcamp_Orders = "orders";
	public const string Bootcamp_Order = "orders/{id}";
	public const string Bootcamp_OrdersByCustomerId = "orders/customer/{id}";

	#endregion
}
