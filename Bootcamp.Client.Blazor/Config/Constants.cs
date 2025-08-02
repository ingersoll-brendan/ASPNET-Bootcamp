namespace Bootcamp.Client.Blazor.Config;

public static class Constants
{
	#region Bootcamp API Config

	public const string Bootcamp_ApiHttpClientName = "BootcampAPI";

	// TODO: We need to determine how to configure this for all environments?

	// TODO: Use a "Compiler" Statement (i.e. #if DEV, #if INT, #if PROD, etc.
	// Or does Blazor have AppSettings.json that can be used simaliar to Web Projects
	// and an run time, we can load the correct ones?
	
	//This is not ideal because this requires the CI/CD process to compile and publish under all possible environment
	//configurations ahead of time, which slows down the process. Also, it makes the CD YAML a little more complicated.
	//Ideally I would use an appsettings.json file for each configuration (ie Dev, Int, QA, Release)
	//There are two ways to use those appsettings files:

	//OPTION 1: Add an Azure function app to the solution that is related to the client project (ie Bootcamp.Client.FunctionApp)
	//And deploy this to the static web app resource in Azure along with the client files. This function app would expose a single
	//API called Api/settings which returns the main web APIs base URL

	//OPTION 2: During the CI process, copy all of the app settings for all environments into an "AppSettings" folder at the root
	//at the root of the artifact, and then during the CD process copy the appropriate environment app setting into the wwwroot folder
	//and deploy along with all of the other client files. NOTE: you need to copy using the name appsettings.json (remove the
	//environment specific suffix). This option requires the blazor code to parse the appsettings.json to get the main API base URL
	//in the program.cs
#if DEBUG || DEV
		public const string Bootcamp_ApiBaseUrl = "https://localhost:7119/api/";
#elif INT
		public const string Bootcamp_ApiBaseUrl = "https://symmsoft-bootcamp-api-int.azurewebsites.net/api/";

		// FUTURE: Add other Environment URLs (i.e. QA, UAT)
#else
	public const string Bootcamp_ApiBaseUrl = "https://symmsoft-bootcamp-api-prod.azurewebsites.net/api/";
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
