using ButiksDataSystem.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ButiksDataSystem.Enteties
{
    public class SelectedItems: Product
    {
        public int NumberOfPiecesOrKilo { get; set; }      
        public SelectedItems(string productId, string productName, decimal price, PriceType priceType, int numberOfPieceOrKg)
            :base(productId, productName, price, priceType)
        {
            NumberOfPiecesOrKilo = numberOfPieceOrKg;
            
        }
        
    }
}
