using Microsoft.Azure.Management.HDInsight;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using System;

namespace HDInsightSDKConsoleAppServicePrincipal
{
    class Program
    {
        static void Main(string[] args)
        {
            // Get yer own service principal! 
            // Make sure it is authorized to do the operations you want (e.g. list/create clusters)
            var clientId = ""; // e.g. mine was "fe0ce48f-e89e-4129-b2bd-dbc28b6cf675"
            var clientSecret = "";

            // Look it up on AAD
            var tenantId = ""; // e.g. the tenant I used was "ed87f914-7867-4e20-82ae-2833eb814124"

            var subscriptionId = "a0e790da-735e-4b22-be58-57337ae411c1";

            var env = new AzureEnvironment
            {
                Name = "Dogfood",
                AuthenticationEndpoint = "https://login.windows-ppe.net/",
                GraphEndpoint = "https://graph.ppe.windows.net/",
                ManagementEndpoint = "https://management.core.windows.net/",
                ResourceManagerEndpoint = "https://api-dogfood.resources.windows-int.net/"
            };

            var credentials = SdkContext.AzureCredentialsFactory
                .FromServicePrincipal(clientId, clientSecret, tenantId, env);
            
            var hdiClient = new HDInsightManagementClient(credentials)
            {
                SubscriptionId = subscriptionId,
                BaseUri = new Uri(env.ResourceManagerEndpoint)                
            };

            var clusters = hdiClient.Clusters.ListByResourceGroup("wawon-rg");

            Console.WriteLine(clusters);
        }
    }
}
