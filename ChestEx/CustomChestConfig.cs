﻿#region License

// clang-format off
// 
//    ChestEx (StardewValleyMods)
//    Copyright (c) 2021 Berkay Yigit <berkaytgy@gmail.com>
// 
//    This program is free software: you can redistribute it and/or modify
//    it under the terms of the GNU Affero General Public License as published
//    by the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
// 
//    This program is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//    GNU Affero General Public License for more details.
// 
//    You should have received a copy of the GNU Affero General Public License
//    along with this program. If not, see <https://www.gnu.org/licenses/>.
// 
// clang-format on

#endregion

using System;

using Microsoft.Xna.Framework;

namespace ChestEx {
  public class CustomChestConfig {
    public const String CONST_MODDATA_PREFIX = "berkayylmao.ChestEx";
    public const String CONST_NAME_KEY       = "Name";
    public const String CONST_DESC_KEY       = "Description";
    public const String CONST_HINGES_KEY     = "HingesColour";

    public const String CONST_CHESTSANYWHERE_NAME_COMPATIBILITY_KEY = "Pathoschild.ChestsAnywhere/Name";

    public const String CONST_DEFAULT_NAME = "Chest";

    public String mName         { get; set; } = CONST_DEFAULT_NAME;
    public String mDescription  { get; set; } = String.Empty;
    public Color  mHingesColour { get; set; } = Color.White;
  }
}