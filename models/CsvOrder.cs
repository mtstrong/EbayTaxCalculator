using System.Text;

public class CsvOrder
{
    public string OrderId { get; set; }
    public string Description { get; set; }
    public string Buyer { get; set; }
    public string PricingSummary { get; set; }
    public string DeliveryCost { get;set; }
    public string Fees { get; set; }
    public string Total { get; set; }
    public string Date { get;set; }

    public CsvOrder(Order order)
    {
        OrderId = order.orderId;
        int count = order.lineItems.Count;
        if(count == 1)
        {
            Description = order.lineItems[0].title;
        }
        else
        {
            var result = new StringBuilder();
            int i = 0;
            foreach (var item in order.lineItems)
            {
                if(i == 0)
                {
                    result.Append(item.title);
                }
                else
                {
                    result.Append(", " + item.title);
                }
                i++;
            }
            Description = result.ToString();
        }
        Buyer = order.buyer.username;
        PricingSummary = order.pricingSummary.priceSubtotal.value;
        DeliveryCost = order.pricingSummary.deliveryCost.value;
        Fees = order.totalMarketplaceFee.value;
        Total = order.paymentSummary.totalDueSeller.value;
        Date = order.paymentSummary.payments[0].paymentDate.ToString();
    }
}