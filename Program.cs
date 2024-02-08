using System.Net.Http.Headers;
using System.Text.Json;

namespace EbayTaxCalculator;

class Program
{
    private static string fulfilmentUri = "https://api.ebay.com/sell/fulfillment/v1/order";
    private static HttpClient ebayClient;

    static async Task Main(string[] args)
    {
        string token = "";
        SetupHttpClient(token);
        await GetSoldOrders();
    }

    private static void SetupHttpClient(string token)
    {
        ebayClient = new HttpClient();
        //ebayClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", token);
        ebayClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        //ebayClient.DefaultRequestHeaders.Add("Authorization: ", "Bearer " + token);
    }

    static async Task<string> GetSoldOrders()
    {
        DateTime dt = new DateTime(2024, 1, 1);
        HttpResponseMessage response = await ebayClient.GetAsync(fulfilmentUri + "?limit=200&fieldGroups=TAX_BREAKDOWN");
        //"?creationDate:[" + dt.ToString(CultureInfo.InvariantCulture) + "]");
        var json = await response.Content.ReadAsStringAsync();
        Orders orders = JsonSerializer.Deserialize<Orders>(json);
        var sellingOrders = orders?.orders.Where(x => x.buyer.username != "storng");
        var buyingOrders = orders?.orders.Where(x => x.buyer.username == "storng");

        foreach(var order in orders.orders)
        {
            Console.WriteLine("Order Number: {0} Item: {1} Buyer: {2} Payment: {3} Shipping: {4} Fees: {5} Total: {6}", order.orderId, 
                order.lineItems[0].title, order.buyer.username, order.pricingSummary.priceSubtotal.value, 
                order.pricingSummary.deliveryCost.value, order.totalMarketplaceFee.value, order.paymentSummary.totalDueSeller.value);
        }

        return "";
    }
}
