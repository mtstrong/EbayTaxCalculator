// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Amount
    {
        public string value { get; set; }
        public string currency { get; set; }
    }

    public class Buyer
    {
        public string username { get; set; }
        public TaxAddress taxAddress { get; set; }
        public BuyerRegistrationAddress buyerRegistrationAddress { get; set; }
    }

    public class BuyerRegistrationAddress
    {
        public string fullName { get; set; }
        public ContactAddress contactAddress { get; set; }
        public PrimaryPhone primaryPhone { get; set; }
        public string email { get; set; }
        public SecondaryPhone secondaryPhone { get; set; }
    }

    public class CancelStatus
    {
        public string cancelState { get; set; }
        public List<object> cancelRequests { get; set; }
    }

    public class ContactAddress
    {
        public string addressLine1 { get; set; }
        public string city { get; set; }
        public string stateOrProvince { get; set; }
        public string postalCode { get; set; }
        public string countryCode { get; set; }
        public string addressLine2 { get; set; }
    }

    public class DeliveryCost
    {
        public string value { get; set; }
        public string currency { get; set; }
        public ShippingCost shippingCost { get; set; }
    }

    public class EbayCollectAndRemitTaxis
    {
        public string taxType { get; set; }
        public Amount amount { get; set; }
        public string collectionMethod { get; set; }
    }

    public class FulfillmentStartInstruction
    {
        public string fulfillmentInstructionsType { get; set; }
        public DateTime minEstimatedDeliveryDate { get; set; }
        public DateTime maxEstimatedDeliveryDate { get; set; }
        public bool ebaySupportedFulfillment { get; set; }
        public ShippingStep shippingStep { get; set; }
    }

    public class ItemLocation
    {
        public string location { get; set; }
        public string countryCode { get; set; }
        public string postalCode { get; set; }
    }

    public class LineItem
    {
        public string lineItemId { get; set; }
        public string legacyItemId { get; set; }
        public string title { get; set; }
        public LineItemCost lineItemCost { get; set; }
        public int quantity { get; set; }
        public string soldFormat { get; set; }
        public string listingMarketplaceId { get; set; }
        public string purchaseMarketplaceId { get; set; }
        public string lineItemFulfillmentStatus { get; set; }
        public Total total { get; set; }
        public DeliveryCost deliveryCost { get; set; }
        public List<object> appliedPromotions { get; set; }
        public List<Taxis> taxes { get; set; }
        public List<EbayCollectAndRemitTaxis> ebayCollectAndRemitTaxes { get; set; }
        public Properties properties { get; set; }
        public LineItemFulfillmentInstructions lineItemFulfillmentInstructions { get; set; }
        public ItemLocation itemLocation { get; set; }
    }

    public class LineItemCost
    {
        public string value { get; set; }
        public string currency { get; set; }
    }

    public class LineItemFulfillmentInstructions
    {
        public DateTime minEstimatedDeliveryDate { get; set; }
        public DateTime maxEstimatedDeliveryDate { get; set; }
        public DateTime shipByDate { get; set; }
        public bool guaranteedDelivery { get; set; }
    }

    public class Order
    {
        public string orderId { get; set; }
        public string legacyOrderId { get; set; }
        public DateTime creationDate { get; set; }
        public DateTime lastModifiedDate { get; set; }
        public string orderFulfillmentStatus { get; set; }
        public string orderPaymentStatus { get; set; }
        public string sellerId { get; set; }
        public Buyer buyer { get; set; }
        public PricingSummary pricingSummary { get; set; }
        public CancelStatus cancelStatus { get; set; }
        public PaymentSummary paymentSummary { get; set; }
        public List<FulfillmentStartInstruction> fulfillmentStartInstructions { get; set; }
        public List<string> fulfillmentHrefs { get; set; }
        public List<LineItem> lineItems { get; set; }
        public bool ebayCollectAndRemitTax { get; set; }
        public string salesRecordReference { get; set; }
        public TotalFeeBasisAmount totalFeeBasisAmount { get; set; }
        public TotalMarketplaceFee totalMarketplaceFee { get; set; }
    }

    public class Payment
    {
        public string paymentMethod { get; set; }
        public string paymentReferenceId { get; set; }
        public DateTime paymentDate { get; set; }
        public Amount amount { get; set; }
        public string paymentStatus { get; set; }
    }

    public class PaymentSummary
    {
        public TotalDueSeller totalDueSeller { get; set; }
        public List<object> refunds { get; set; }
        public List<Payment> payments { get; set; }
    }

    public class PriceSubtotal
    {
        public string value { get; set; }
        public string currency { get; set; }
    }

    public class PricingSummary
    {
        public PriceSubtotal priceSubtotal { get; set; }
        public DeliveryCost deliveryCost { get; set; }
        public Tax tax { get; set; }
        public Total total { get; set; }
    }

    public class PrimaryPhone
    {
        public string phoneNumber { get; set; }
    }

    public class Properties
    {
        public bool buyerProtection { get; set; }
    }

    public class EbayOrders
    {
        public string href { get; set; }
        public int total { get; set; }
        public string next { get; set; }
        public int limit { get; set; }
        public int offset { get; set; }
        public List<Order> orders { get; set; }
    }

    public class SecondaryPhone
    {
        public string phoneNumber { get; set; }
    }

    public class ShippingCost
    {
        public string value { get; set; }
        public string currency { get; set; }
    }

    public class ShippingStep
    {
        public ShipTo shipTo { get; set; }
        public string shippingCarrierCode { get; set; }
        public string shippingServiceCode { get; set; }
    }

    public class ShipTo
    {
        public string fullName { get; set; }
        public ContactAddress contactAddress { get; set; }
        public PrimaryPhone primaryPhone { get; set; }
        public string email { get; set; }
    }

    public class Tax
    {
        public string value { get; set; }
        public string currency { get; set; }
    }

    public class TaxAddress
    {
        public string city { get; set; }
        public string stateOrProvince { get; set; }
        public string postalCode { get; set; }
        public string countryCode { get; set; }
    }

    public class Taxis
    {
        public Amount amount { get; set; }
        public string taxType { get; set; }
    }

    public class Total
    {
        public string value { get; set; }
        public string currency { get; set; }
    }

    public class TotalDueSeller
    {
        public string value { get; set; }
        public string currency { get; set; }
    }

    public class TotalFeeBasisAmount
    {
        public string value { get; set; }
        public string currency { get; set; }
    }

    public class TotalMarketplaceFee
    {
        public string value { get; set; }
        public string currency { get; set; }
    }

