namespace ButiksDataSystem.Enteties
{
    public class ReceiptItem
    {
        public int ReceiptItemNr { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public decimal? CampainPrice { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public string OrderDetails { 
            get 
            {
                var total = TotalPrice.ToString("C", System.Globalization.CultureInfo.GetCultureInfo("sv-SE"));
                var price = CampainPrice != 0 ? $"({Price}) {CampainPrice.Value}" : $"{Price}";
                return $"{ProductName} {Quantity} * {price} = {total}";
            } 
        }
        public ReceiptItem(int receiptItemNr, string productName, decimal price, decimal? campainPrice, int quantity, decimal totalPrice)
        {
            ReceiptItemNr = receiptItemNr;
            ProductName = productName;
            Price = price;
            CampainPrice = campainPrice;
            Quantity = quantity;
            TotalPrice = totalPrice;
        }

    }
}
