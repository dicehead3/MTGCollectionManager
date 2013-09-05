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

        public Card(
            string name,
            string cardType,
            string expansion,
            CardRarity rarity,
            string artist,
            string imageLocation)
        {
            Name = name;
            CardType = cardType;
            Expansion = expansion;
            Rarity = rarity;
            Artist = artist;
            ImageLocation = imageLocation;
        }

        protected Card()
        {
        }

        public virtual string Name
        {
            get { return _name; }
            protected set
            {
                _name = value.Required("Card name is required");
            }
        }

        public virtual string ManaCost
        {
            get { return _manaCost ?? string.Empty; }
            set { _manaCost = value ?? string.Empty; }
        }

        public virtual string ConvertedManaCost { get; set; }

        public virtual string CardType
        {
            get { return _cardType; }
            protected set
            {
                _cardType = value.Required("Card type is required");
            }
        }

        public virtual string Expansion
        {
            get { return _expansion; }
            protected set
            {
                _expansion = value.Required("Expansion is required");
            }
        }

        public virtual CardRarity Rarity { get; protected set; }

        public virtual string Description
        {
            get { return _description ?? string.Empty; }
            set { _description = value ?? string.Empty; }
        }

        public virtual string FlavorText
        {
            get { return _flavorText ?? string.Empty; }
            set { _flavorText = value ?? string.Empty; }
        }

        public virtual int? Power { get; set; }

        public virtual int? Toughness { get; set; }

        public virtual string Artist
        {
            get { return _artist; }
            protected set
            {
                _artist = value.Required("Card must have an artist");
            }
        }

        public virtual string ImageLocation
        {
            get { return _imageLocation; }
            protected set
            {
                _imageLocation = value.Required("Card Image Location is required");
            }
        }
    }
}
