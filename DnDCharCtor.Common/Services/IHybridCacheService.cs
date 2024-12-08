﻿using DnDCharCtor.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDCharCtor.Common.Services;

public interface IHybridCacheService
{
    Task<IReadOnlyList<Character>> GetCharactersAsync();

    Task<Character?> GetCurrentCharacterAsync();
    Task<bool> SetCurrentCharacterAsync(Character character);

    Task<CultureInfo> GetSelectedLanguageAsync();
    Task<bool> SetSelectedLanguageAsync(CultureInfo cultureInfo);
}
