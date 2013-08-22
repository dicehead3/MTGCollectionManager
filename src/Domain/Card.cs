using System;
using Infrastructure.DomainBase;

namespace Domain
{
    public class Card : Entity
    {
        private string _imageLocation;
        private string _description;
        private string _flavorText;
        private string _name;
        private string _manaCost;
        private string _cardType;
        private string _artist;
        private string _expansion;
        private int? _power;
        private int? _toughness;

        public Card(string name,
            string manacost,
            string cardType,
            string expansion,
            CardRarity rarity,
            string description,
            string flavorText,
            int? power,
            int? toughness,
            string artist,
            string imageLocation)
        {
            Name = name;
            ManaCost = manacost;
            CardType = cardType;
            Expansion = expansion;
            Rarity = rarity;
            Description = description;
            FlavorText = flavorText;
            Power = power;
            Toughness = toughness;
            Artist = artist;
            ImageLocation = imageLocation;
        }

        public string Name
        {
            get { return _name; }
            private set
            {
                _name = value.Required("Card name is required");
            }
        }

        public string ManaCost
        {
            get { return _manaCost; }
            private set
            {
                _manaCost = value ?? string.Empty;
            }
        }
        [DomainSignature]
        public string CardType
        {
            get { return _cardType; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new Exception("Card type is required");
                _cardType = value;
            }
        }

        public string Expansion
        {
            get { return _expansion; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new Exception("Expansion is required");
                _expansion = value;
            }
        }

        public CardRarity Rarity { get; private set; }

        public string Description
        {
            get { return _description; }
            private set
            {
                _description = value ?? string.Empty;
            }
        }

        public string FlavorText
        {
            get { return _flavorText; }
            private set
            {
                _flavorText = value ?? string.Empty;
            }
        }

        public int? Power
        {
            get { return _power; }
            private set { _power = value; }
        }

        public int? Toughness
        {
            get { return _toughness; }
            private set { _toughness = value; }
        }

        public string Artist
        {
            get { return _artist; }
            set { _artist = value ?? string.Empty; }
        }

        public string ImageLocation
        {
            get { return _imageLocation; }
            private set
            {
                if(string.IsNullOrWhiteSpace(value))
                    throw new Exception("Card Image Location is required");
                _imageLocation = value;
            }
        }
    }
}
