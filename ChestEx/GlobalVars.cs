//
//    Copyright (C) 2020 Berkay Yigit <berkaytgy@gmail.com>
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

using StardewModdingAPI;

namespace ChestEx {
  public static class GlobalVars {
    public static IModHelper SMAPIHelper;
    public static IMonitor SMAPIMonitor;

    public static Microsoft.Xna.Framework.Rectangle UIViewport {
      get {
        var _ = StardewValley.Game1.uiViewport;
        return new Microsoft.Xna.Framework.Rectangle(
          0, 0, _.Width, _.Height);
      }
    }

    public static Microsoft.Xna.Framework.Rectangle GameViewport {
      get {
        return StardewValley.Utility.getSafeArea();
      }
    }
  }
}
