using System;
using System.Collections.Generic;
using System.Text;

namespace ButiksDataSystem.Enteties
{
    public class Receipt
    {
        public DateTime ReceiptId { get; set; }
        public List<SelectedItems> SelectedItems { get; set; }
        public decimal Dicount { get; set; }
        public decimal ItemsTotal { get; set; }
        public decimal Total { get; set; }
        public decimal GetTotal()
        {
            return 0;
        }
    }
   
}
