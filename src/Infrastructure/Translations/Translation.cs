using System;
using System.Globalization;
using Infrastructure.DomainBase;

namespace Infrastructure.Translations
{
    public class Translation
    {
        private string _code;
        private string _text;

        protected Translation(){}
        public Translation(
            string code,
            string  text,
            CultureInfo culture
        )
        {
            _code = code;
            _text = text;
            Culture = culture.Required();
        }

        public virtual CultureInfo Culture { get; protected set; }
        public virtual Guid Id { get; protected set; }

        public virtual string Code
        {
            get { return _code; }
            set
            {
                _code = value.Required();
            }
        }

        public virtual string Text
        {
            get { return _text; }
            set
            {
                _text = value.Required();
            }
        }
    }
}
