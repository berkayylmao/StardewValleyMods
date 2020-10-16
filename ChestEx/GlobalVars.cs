using StardewModdingAPI;

namespace ChestEx {
   public static class GlobalVars {
      public static IModHelper SMAPIHelper;
      public static IMonitor SMAPIMonitor;

      public static Microsoft.Xna.Framework.Rectangle GameViewport {
         get {
            return StardewValley.Utility.getSafeArea();
         }
      }
   }
}
