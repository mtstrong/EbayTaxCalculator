using System.Globalization;
using System.Net.Http.Headers;
using System.Text.Json;
using CsvHelper;

namespace EbayTaxCalculator;

class Program
{
    private static string fulfilmentUri = "https://api.ebay.com/sell/fulfillment/v1/order";
    private static HttpClient? ebayClient;

    static async Task Main(string[] args)
    {
        string token = "";
        SetupHttpClient(token);
        var apiOrders = await GetSoldOrders();

        List<CsvOrder> ordersToCsv = new List<CsvOrder>(); 
        foreach(var order in apiOrders)
        {
            Console.WriteLine("Order Number: {0} Item: {1} Buyer: {2} Payment: {3} Shipping: {4} Fees: {5} Total: {6}", order.orderId, 
                order.lineItems[0].title, order.buyer.username, order.pricingSummary.priceSubtotal.value, 
                order.pricingSummary.deliveryCost.value, order.totalMarketplaceFee.value, order.paymentSummary.totalDueSeller.value);

            ordersToCsv.Add(new CsvOrder(order));
        }

        var writer = new StreamWriter("orders.csv");

        var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
        csv.WriteRecords(ordersToCsv);
    }

    private static void SetupHttpClient(string token)
    {
        ebayClient = new HttpClient();
        ebayClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }

    static async Task<IEnumerable<Order>> GetSoldOrders()
    {
        IEnumerable<Order>? sellingOrders = null;

        try
        {
            //DateTime dt = new DateTime(2024, 1, 1);
            HttpResponseMessage response = await ebayClient.GetAsync(fulfilmentUri + "?limit=200&fieldGroups=TAX_BREAKDOWN");
            //"?creationDate:[" + dt.ToString(CultureInfo.InvariantCulture) + "]");
            var json = await response.Content.ReadAsStringAsync();
            EbayOrders orders = JsonSerializer.Deserialize<EbayOrders>(json);
            sellingOrders = orders?.orders.Where(x => x.buyer.username != "storng");
        }
        catch
        {
            Console.WriteLine("Error occurred during parsing of data");
        }

        return sellingOrders;
    }
}
