using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Card
    {
        private int _id;
        private string _cardImageLocation;
        private string _cardDescription;
        private string _cardFlavorText;

        public Card(int id, string cardImageLocation, string cardDescription, string cardFlavorText)
        {
            Id = id;
            CardImageLocation = cardImageLocation;
            CardDescription = cardDescription;
            CardFlavorText = cardFlavorText;
        }

        public string CardFlavorText
        {
            get { return _cardFlavorText; }
            private set
            {
                _cardFlavorText = value ?? string.Empty;
            }
        }

        public string CardDescription
        {
            get { return _cardDescription; }
            private set
            {
                if (value == null)
                    value = string.Empty;
                _cardDescription = value;
            }
        }

        public string CardImageLocation
        {
            get { return _cardImageLocation; }
            private set
            {
                if(string.IsNullOrWhiteSpace(value))
                    throw new Exception("Card Image Location is required");
                _cardImageLocation = value;
            }
        }

        public int Id
        {
            get { return _id; }
            private set
            {
                if(value <= 0)
                    throw new Exception("Id must be greater than 0");
                _id = value;
            }
        }
    }
}
