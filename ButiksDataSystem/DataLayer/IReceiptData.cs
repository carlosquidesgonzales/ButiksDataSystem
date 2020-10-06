using ButiksDataSystem.Enteties;
using System;
using System.Collections.Generic;
using System.Text;

namespace ButiksDataSystem.DataLayer
{
    public interface IReceiptData<T> where T: Receipt
    {
        void SaveReceipt(T receipt);
        void GetReceipt(string receiptItem, int choice, int receiptNumber);
        string FindSingleReceipt(string receiptIdDate, string receiptIdNumber);
    }
}
