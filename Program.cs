using System.Globalization;
using System.Net.Http.Headers;
using System.Text.Json;
using CsvHelper;
using CsvHelper.Configuration;

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
        List<CsvOrder> bluToCsv = new List<CsvOrder>(); 
        foreach(var order in apiOrders)
        {
            Console.WriteLine("Order Number: {0} Item: {1} Buyer: {2} Payment: {3} Shipping: {4} Fees: {5} Total: {6}", order.orderId, 
                order.lineItems[0].title, order.buyer.username, order.pricingSummary.priceSubtotal.value, 
                order.pricingSummary.deliveryCost.value, order.totalMarketplaceFee.value, order.paymentSummary.totalDueSeller.value);

            ordersToCsv.Add(new CsvOrder(order));
            if(order.lineItems[0].title.Contains("Blu-"))
            {
                bluToCsv.Add(new CsvOrder(order));
            }

            if(order.lineItems.Count > 1)
            {
                Console.WriteLine("This order contains multiple items");
            }
        }

        var folder = @"C:\Users\stron\Documents\";
        var file = "orders.csv";
        if (File.Exists(Path.Combine(folder, file)))
        {
            // If file found, delete it
            File.Delete(Path.Combine(folder, file));
        }

        var fileLocation = folder + file;
        var writer = new StreamWriter(fileLocation);

        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            ShouldQuote = (field) => false 
        };
        var csv = new CsvWriter(writer, config);
        Console.WriteLine("Writing orders to " + fileLocation);
        ordersToCsv.OrderBy(x => x.Date);
        csv.WriteRecords(ordersToCsv);
        writer.Flush();

        file = "blu.csv";
        fileLocation = folder + file;
        writer = new StreamWriter(fileLocation);

        csv = new CsvWriter(writer, config);
        Console.WriteLine("Writing blu-ray orders to " + fileLocation);
        bluToCsv.OrderBy(x => x.Date);
        csv.WriteRecords(bluToCsv);
        writer.Flush();
    }

    private static void SetupHttpClient(string token)
    {
        ebayClient = new HttpClient();
        ebayClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }

    static async Task<IEnumerable<Order>> GetSoldOrders()
    {
        List<Order>? sellingOrders = null;

        try
        {
            //get 2023 orders
            HttpResponseMessage response = await ebayClient.GetAsync(fulfilmentUri + 
                "?limit=100&fieldGroups=TAX_BREAKDOWN&filter=creationdate:%5B2023-01-01T00:00:00.000Z..2024-01-01T00:00:00.000Z%5D");
            var json = await response.Content.ReadAsStringAsync();
            Console.WriteLine(json);
            EbayOrders orders = JsonSerializer.Deserialize<EbayOrders>(json);
            sellingOrders = orders.orders;

            //get jan 2024 orders
            response = await ebayClient.GetAsync(fulfilmentUri + 
                "?limit=100&fieldGroups=TAX_BREAKDOWN&filter=creationdate:%5B2024-01-01T00:00:00.000Z..2024-02-01T00:00:00.000Z%5D");
            json = await response.Content.ReadAsStringAsync();
            Console.WriteLine(json);
            orders = JsonSerializer.Deserialize<EbayOrders>(json);
            sellingOrders.AddRange(orders.orders);
            
            //get feb 2024 orders
            response = await ebayClient.GetAsync(fulfilmentUri + 
                "?limit=100&fieldGroups=TAX_BREAKDOWN&filter=creationdate:%5B2024-02-01T00:00:00.000Z..2024-03-01T00:00:00.000Z%5D");
            json = await response.Content.ReadAsStringAsync();
            Console.WriteLine(json);
            orders = JsonSerializer.Deserialize<EbayOrders>(json);
            sellingOrders.AddRange(orders.orders);

            //get march 2024 orders
            response = await ebayClient.GetAsync(fulfilmentUri + 
                "?limit=100&fieldGroups=TAX_BREAKDOWN&filter=creationdate:%5B2024-03-01T00:00:00.000Z..2024-04-01T00:00:00.000Z%5D");
            json = await response.Content.ReadAsStringAsync();
            Console.WriteLine(json);
            orders = JsonSerializer.Deserialize<EbayOrders>(json);
            sellingOrders.AddRange(orders.orders);
        }
        catch
        {
            Console.WriteLine("Error occurred during parsing of data");
        }

        return sellingOrders;
    }
}
