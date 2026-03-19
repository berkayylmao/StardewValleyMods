#region License

// clang-format off
// 
//    ShipAnything (StardewValleyMods)
//    Copyright (c) 2026 Berkay Yigit <contact@withberkay.com>
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

using HarmonyLib;

using JetBrains.Annotations;

using StardewModdingAPI;

using StardewValley;

namespace ShipAnything {
  public static class Globals {
    public static StardewModdingAPI.IMonitor SMAPIMonitor = null;
  }

  [UsedImplicitly]
  public class Main : Mod {

    public static class Extensions {
      public static class SVObject {
        [HarmonyPrefix]
        [UsedImplicitly]
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        public static Boolean prefix_canBeShipped(StardewValley.Object __instance, ref Boolean __result) {
          __result = __instance.Type != null && !__instance.Type.Equals("Quest");
          return false;
        }
      }

      public static class SVUtility {
        [HarmonyPrefix]
        [UsedImplicitly]
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        public static Boolean prefix_highlightShippableObjects(ref Boolean __result) {
          __result = true;
          return false;
        }
      }

      public static class SVGame1 {
        [HarmonyPrefix]
        [UsedImplicitly]
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        public static Boolean prefix__newDayAfterFade() {
          if (Game1.player.useSeparateWallets || Game1.player.IsMainPlayer) {
            var shipping_bin = Game1.getFarm().getShippingBin(Game1.player);
            Int32 extra_gained = 0;

            foreach (Item item in shipping_bin) {
              if (item is null) continue;

              try {
                Int32 sell_to_store_price = item.sellToStorePrice();
                Int32 price = sell_to_store_price * item.Stack;
                extra_gained += price;
              }
              catch (Exception ex) {
                Globals.SMAPIMonitor.Log("An exception occured while processing an item at 'StardewValley.Game1._newDayAfterFade()'! Skipping this item...", LogLevel.Error);
                Globals.SMAPIMonitor.Log($"Here is the thrown exception:\n\t{ex.ToString().Replace(Environment.NewLine, $"{Environment.NewLine}\t")}", LogLevel.Trace);
              }
            }

            Game1.player.Money += extra_gained;
          }

          if (Game1.player.useSeparateWallets && Game1.player.IsMainPlayer) {
            foreach (Farmer who2 in Game1.getOfflineFarmhands()) {
              if (who2.isUnclaimedFarmhand)
                continue;

              var shipping_bin = Game1.getFarm().getShippingBin(who2);
              Int32 extra_gained = 0;
              foreach (Item item in shipping_bin) {
                if (item is null) continue;

                try {
                  Int32 sell_to_store_price = item.sellToStorePrice();
                  Int32 price = sell_to_store_price * item.Stack;
                  extra_gained += price;
                }
                catch (Exception ex) {
                  Globals.SMAPIMonitor.Log("An exception occured while processing an item at 'StardewValley.Game1._newDayAfterFade()'! Skipping this item...", LogLevel.Error);
                  Globals.SMAPIMonitor.Log($"Here is the thrown exception:\n\t{ex.ToString().Replace(Environment.NewLine, $"{Environment.NewLine}\t")}", LogLevel.Trace);
                }
              }

              Game1.player.team.AddIndividualMoney(who2, extra_gained);
            }
          }

          return true;
        }
      }
    }

    public override void Entry(IModHelper helper) {
      Globals.SMAPIMonitor = this.Monitor;

      var harmony = new Harmony("mod.berkayylmao.ShipAnything");
      harmony.Patch(AccessTools.Method(typeof(StardewValley.Object), "canBeShipped"), new HarmonyMethod(AccessTools.Method(typeof(Extensions.SVObject), "prefix_canBeShipped")));
      harmony.Patch(AccessTools.Method(typeof(Utility), "highlightShippableObjects"),
                    new HarmonyMethod(AccessTools.Method(typeof(Extensions.SVUtility), "prefix_highlightShippableObjects")));
      harmony.Patch(AccessTools.Method(typeof(StardewValley.Game1), "_newDayAfterFade"),
                    new HarmonyMethod(AccessTools.Method(typeof(Extensions.SVGame1), "prefix__newDayAfterFade")));
    }
  }
}
