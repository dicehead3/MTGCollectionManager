﻿using System.Globalization;

namespace Infrastructure.Translations
{
    public interface ITranslationService
    {
        dynamic Translate { get; }
    }
}
