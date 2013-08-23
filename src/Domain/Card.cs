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

        public Card(int id,
            string name,
            string cardType,
            string expansion,
            CardRarity rarity,
            string artist,
            string imageLocation)
        {
            Id = id;
            Name = name;
            CardType = cardType;
            Expansion = expansion;
            Rarity = rarity;
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
            get { return _manaCost ?? string.Empty; }
            set { _manaCost = value ?? string.Empty; }
        }

        public string CardType
        {
            get { return _cardType; }
            private set
            {
                _cardType = value.Required("Card type is required");
            }
        }

        public string Expansion
        {
            get { return _expansion; }
            private set
            {
                _expansion = value.Required("Expansion is required");
            }
        }

        public CardRarity Rarity { get; private set; }

        public string Description
        {
            get { return _description ?? string.Empty; }
            set { _description = value ?? string.Empty; }
        }

        public string FlavorText
        {
            get { return _flavorText ?? string.Empty; }
            set { _flavorText = value ?? string.Empty; }
        }

        public int? Power { get; set; }

        public int? Toughness { get; set; }

        public string Artist
        {
            get { return _artist; }
            private set
            {
                _artist = value.Required("Card must have an artist");
            }
        }

        public string ImageLocation
        {
            get { return _imageLocation; }
            private set
            {
                _imageLocation = value.Required("Card Image Location is required");
            }
        }
    }
}
