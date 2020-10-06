using ButiksDataSystem.DataLayer;
using ButiksDataSystem.Enteties;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ButiksDataSystem.Menus
{
    public class CustomerMenu
    {
        #region Properties
        private readonly IData<Product> _data = new Data<Product>();
        private IReceiptData<Receipt> _rData = new ReceiptData<Receipt>();
        private Receipt _receipt = new Receipt();
        private DateTime _receiptId => DateTime.Now;
        private List<Product> _products => _data.GetProducts().ToList();//list of all products  
        private List<ReceiptItem> _selectedProducts => _receipt.SelectedItems;//List of selected products
        private List<ReceiptItem> _receiptItemList = new List<ReceiptItem>();//A list that holds all the products picked by customer    
        #endregion
        #region Menu
        public void ShowUserMenu()
        {
            Console.WriteLine("CUSTOMER MENU");
            bool openMenu = true;
            while (openMenu)
            {
                Console.WriteLine("1: New Customer");
                Console.WriteLine("0: Exit");
                int number;
                int.TryParse(Console.ReadLine(), out number);
                switch (number)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("KASSA");
                        NewCustomer();
                        openMenu = false;
                        break;
                    case 0:
                        Environment.Exit(0);
                        openMenu = false;
                        break;
                    default:
                        Console.WriteLine("Invalid menu. Choose between 0 and 1");
                        break;
                }
            }
        }
        #endregion
        #region Methods
        public void NewCustomer()
        {
            while (true)
            {
                Console.Write("Komandon: ");
                string input = Console.ReadLine();
                if (input.ToUpper() == "PAY")//Check if the cutomer wants to pay
                {
                    Console.Clear();
                    if (_receipt.SelectedItems.Count > 0)//Check if basket is not empty
                    {
                        try
                        {
                            _receipt.PrintReceipt();
                            _rData.SaveReceipt(_receipt);
                            _receipt.SelectedItems.Clear();
                        }
                        catch (Exception ex)
                        {
                            Console.Clear();
                            Console.WriteLine(ex.Message);
                            NewCustomer();
                        }
                    }
                    else
                    {
                        Console.WriteLine("KASSA");
                        Console.WriteLine("Your basket is empty.");
                    }
                    Console.Write("<productId> <antal>\n");
                    Console.WriteLine("PAY");//Save to text file
                }
                else if (input.ToUpper() == "CANCEL")//Check if the customer want to cancel a product
                {
                    CancelProduct();
                }
                else if (input.ToUpper() == "EXIT")// Check if the customer want to cancel all products
                {
                    Console.Clear();
                    Console.WriteLine("KASSA");
                    Console.WriteLine("Operation canceled.");
                    ShowUserMenu();
                }
                else//if the customer picked any product to buy or not
                {
                    string[] product = input.Split(" ");
                    if (product.Length > 1)
                        CheckIfExist(product);
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("KASSA");
                        Console.Write("<productId> <antal>\n");
                        Console.WriteLine("Please enter a valid value.");
                    }
                }
            }
        }
        private void CancelProduct()
        {
            var receipt = new Receipt();
            if (_selectedProducts.Count() != 0)
            {
                var lastItem = _receiptItemList.LastOrDefault();
                var product = _selectedProducts.FirstOrDefault(i => i.ReceiptItemNr.Equals(lastItem.ReceiptItemNr));
                if (product != null)
                {
                    int productId = RemoveProduct(product, lastItem);
                    _receiptItemList.RemoveAt(_receiptItemList.Count - 1);
                    _receipt.UpdateReceipt(_selectedProducts);
                    if (_selectedProducts.Count() == 0)
                    {
                        Console.Clear();
                        Console.WriteLine("KASSA");
                    }
                    else
                        _receipt.PrintReceipt();
                    Console.WriteLine($"Product with product Id of {productId} has been removed!");
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("KASSA");
                Console.WriteLine("Your basket is empty");
            }
        }
        private int RemoveProduct(ReceiptItem product, ReceiptItem lastItem)
        {

            if ((product.Quantity - lastItem.Quantity) != 0)
            {
                var receiptItem = new ReceiptItem();
                int itemIndex = _selectedProducts.FindIndex(i => i.ReceiptItemNr == lastItem.ReceiptItemNr);
                product.Quantity = product.Quantity - lastItem.Quantity;
                product.TotalPrice = product.Quantity * product.Price;
                _selectedProducts.RemoveAt(itemIndex);
                _receipt.SetReceipt(product, _receiptId);
                return lastItem.ReceiptItemNr;

            }
            else
            {
                _selectedProducts.RemoveAt(_selectedProducts.Count - 1);
                return product.ReceiptItemNr;
            }

        }
        private void CheckIfExist(string[] input)
        {
            //_receiptList.Clear();
            var receiptItem = new ReceiptItem();
            int id = Convert.ToInt32(input[0].Trim());

            var product = _products.SingleOrDefault(p => p.ProductId == id);
            if (product != null) //Check if item exist
            {
                int quantity;
                int.TryParse(input[1].Trim(), out quantity);
                var singleReceipt = _receipt.SelectedItems.FirstOrDefault(r => r.ReceiptItemNr == id);
                if ((singleReceipt != null && (singleReceipt.Quantity + quantity > product.MaxQuantity && product.MaxQuantity > 1))||
                    quantity > product.MaxQuantity && product.MaxQuantity > 1)//Check if exceeds the maximum quantity or kilograms
                {
                    Console.WriteLine($"Only {product.MaxQuantity} pieces/kilograms of {product.ProductName} allowed. Try again!");
                    return;
                }
               
                if (quantity == 0) //Check if the quantity is a valid input
                {
                    Console.WriteLine("Enter a valid quantity");
                    return;
                }
                receiptItem.ReceiptItemNr = product.ProductId;
                receiptItem.ProductName = product.ProductName;
                receiptItem.Price = product.Price;
                receiptItem.CampainPrice = product.IsCampainPrice() ? product.CampainPrice : 0;
                receiptItem.TotalPrice = product.IsCampainPrice() ? quantity * product.CampainPrice : product.Price * quantity;
                receiptItem.Quantity = quantity;
                _receiptItemList.Add(receiptItem);
                _receipt.SetReceipt(receiptItem, _receiptId);
                _receipt.PrintReceipt();
            }
            else
            {
                Console.WriteLine("Item does not exist! Try again.");
            }
        }
        #endregion
    }
}
