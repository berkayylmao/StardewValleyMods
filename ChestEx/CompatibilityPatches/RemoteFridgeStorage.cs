#region License

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
using System.Diagnostics.CodeAnalysis;

using ChestEx.LanguageExtensions;
using ChestEx.Types.BaseTypes;

using Harmony;

using JetBrains.Annotations;

using StardewModdingAPI;

using StardewValley;

using Object = System.Object;

namespace ChestEx.CompatibilityPatches {
  // TODO: fix the button being behind the dark background
  internal class RemoteFridgeStorage : CompatibilityPatch {
  #region Patches

    [HarmonyPatch]
    private static class ChestController {
      private static Type sType => Type.GetType("RemoteFridgeStorage.controller.ChestController, RemoteFridgeStorage");

      [HarmonyPostfix]
      [UsedImplicitly]
      [SuppressMessage("ReSharper", "InconsistentNaming")]
      private static void postfixUpdatePos(Object ____config) {
        if (Game1.activeClickableMenu is not CustomItemGrabMenu menu) return;
        Traverse.Create(____config).Property<Boolean>("OverrideOffset").Value = true;
        Traverse.Create(____config).Property<Int32>("XOffset").Value          = menu.mSourceInventoryOptions.mBounds.X - 48;
        Traverse.Create(____config).Property<Int32>("YOffset").Value          = menu.mSourceInventoryOptions.mBounds.Y + 96;
      }

      public static void Install() {
        GlobalVars.gHarmony.PatchEx(AccessTools.Method(sType, "UpdatePos"),
                                    postfix: new HarmonyMethod(AccessTools.Method(typeof(ChestController), "postfixUpdatePos")),
                                    reason: "move RemoteFridgeStorage's button");
      }
    }

  #endregion

    // Protected:
  #region Protected

    protected override void InstallPatches() { ChestController.Install(); }

    protected override void OnLoaded() {
      GlobalVars.gIsRemoteFridgeStorageLoaded = true;

      base.OnLoaded();
    }

  #endregion

    // Constructors:
  #region Constructors

    internal RemoteFridgeStorage() : base("EternalSoap.RemoteFridgeStorage", new SemanticVersion("1.8.1")) { }

  #endregion
  }
}
