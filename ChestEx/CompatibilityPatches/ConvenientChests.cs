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

using Microsoft.Xna.Framework;

using StardewModdingAPI;

using StardewValley;

using Object = System.Object;

namespace ChestEx.CompatibilityPatches {
  internal class ConvenientChests : CompatibilityPatch {
  #region Patches

    [HarmonyPatch]
    private static class CategoryMenu {
      private static Type sType => Type.GetType("ConvenientChests.CategorizeChests.Interface.Widgets.CategoryMenu, ConvenientChests");

      [HarmonyPrefix]
      [UsedImplicitly]
      [SuppressMessage("ReSharper", "InconsistentNaming")]
      private static void prefixRecreateItemToggles(Object __instance) {
        if (Game1.activeClickableMenu is not CustomItemGrabMenu menu) return;

        Int32 width = menu.mSourceInventoryOptions.mBounds.Width;
        Traverse.Create(__instance).Property<Int32>("Width").Value = width;
        Traverse.Create(__instance).Property("ToggleBag").Property<Int32>("Width").Value =
          width - Traverse.Create(__instance).Property("ScrollBar").Property<Int32>("Width").Value - 16;
      }

      [HarmonyPostfix]
      [UsedImplicitly]
      [SuppressMessage("ReSharper", "InconsistentNaming")]
      private static void postfixPositionElements(Object __instance) {
        if (Game1.activeClickableMenu is not CustomItemGrabMenu menu) return;

        Int32 height = menu.mPlayerInventoryOptions.mBounds.Bottom - menu.mSourceInventoryOptions.mBounds.Y;
        Traverse.Create(__instance).Property<Int32>("Height").Value                        = height;
        Traverse.Create(__instance).Property("Background").Property<Int32>("Height").Value = height;
        Traverse.Create(__instance).Property("ScrollBar").Property<Int32>("Height").Value =
          height - Traverse.Create(__instance).Property("CloseButton").Property<Int32>("Height").Value - 16;
      }

      public static void Install() {
        GlobalVars.gHarmony.PatchEx(AccessTools.Method(sType, "RecreateItemToggles"),
                                    new HarmonyMethod(AccessTools.Method(typeof(CategoryMenu), "prefixRecreateItemToggles")),
                                    reason: "move ConvenientChests' UI");
        GlobalVars.gHarmony.PatchEx(AccessTools.Method(sType, "PositionElements"),
                                    postfix: new HarmonyMethod(AccessTools.Method(typeof(CategoryMenu), "postfixPositionElements")),
                                    reason: "move ConvenientChests' UI");
      }
    }

    [HarmonyPatch]
    private static class ChestOverlay {
      private static Type sType => Type.GetType("ConvenientChests.CategorizeChests.Interface.Widgets.ChestOverlay, ConvenientChests");

      [HarmonyPostfix]
      [UsedImplicitly]
      [SuppressMessage("ReSharper", "InconsistentNaming")]
      private static void postfixPositionButtons(Object __instance) {
        if (Game1.activeClickableMenu is not CustomItemGrabMenu menu) return;

        var openbtn_pos       = Traverse.Create(__instance).Property("OpenButton").Property<Point>("Position");
        var openbtn_width     = Traverse.Create(__instance).Property("OpenButton").Property<Int32>("Width");
        var openbtn_height    = Traverse.Create(__instance).Property("OpenButton").Property<Int32>("Height");
        var openbtn_lpadding  = Traverse.Create(__instance).Property("OpenButton").Property<Int32>("LeftPadding");
        var stashbtn_pos      = Traverse.Create(__instance).Property("StashButton").Property<Point>("Position");
        var stashbtn_width    = Traverse.Create(__instance).Property("StashButton").Property<Int32>("Width");
        var stashbtn_height   = Traverse.Create(__instance).Property("StashButton").Property<Int32>("Height");
        var stashbtn_lpadding = Traverse.Create(__instance).Property("StashButton").Property<Int32>("LeftPadding");

        Rectangle menu_rect = menu.mSourceInventoryOptions.mBounds;
        stashbtn_pos.Value = new Point(menu_rect.X - stashbtn_width.Value + stashbtn_lpadding.Value - 4, menu_rect.Bottom - stashbtn_height.Value - 32);
        openbtn_pos.Value  = new Point(menu_rect.X - openbtn_width.Value + openbtn_lpadding.Value - 4, stashbtn_pos.Value.Y - openbtn_height.Value);
      }

      [HarmonyPostfix]
      [UsedImplicitly]
      [SuppressMessage("ReSharper", "InconsistentNaming")]
      private static void postfixOpenCategoryMenu(Object __instance) {
        if (Game1.activeClickableMenu is not CustomItemGrabMenu menu) return;

        Traverse.Create(__instance).Property("CategoryMenu").Property<Point>("Position").Value =
          new Point(menu.mSourceInventoryOptions.mBounds.X + 12, menu.mSourceInventoryOptions.mBounds.Y);
      }

      public static void Install() {
        GlobalVars.gHarmony.PatchEx(AccessTools.Method(sType, "PositionButtons"),
                                    postfix: new HarmonyMethod(AccessTools.Method(typeof(ChestOverlay), "postfixPositionButtons")),
                                    reason: "move ConvenientChests' buttons");
        GlobalVars.gHarmony.PatchEx(AccessTools.Method(sType, "OpenCategoryMenu"),
                                    postfix: new HarmonyMethod(AccessTools.Method(typeof(ChestOverlay), "postfixOpenCategoryMenu")),
                                    reason: "move ConvenientChests' UI");
      }
    }

  #endregion

    // Protected:
  #region Protected

    protected override void InstallPatches() {
      CategoryMenu.Install();
      ChestOverlay.Install();
    }

    protected override void OnLoaded() {
      GlobalVars.gIsConvenientChestsLoaded = true;

      base.OnLoaded();
    }

  #endregion

    // Constructors:
  #region Constructors

    internal ConvenientChests() : base("aEnigma.ConvenientChests", new SemanticVersion("1.5.2-unofficial.2-borthain")) { }

  #endregion
  }
}
