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

using Microsoft.Xna.Framework;

namespace ChestEx.Types.BaseTypes {
   public class IActionColours {
      // Public static instances:
      #region Public static instances

      public static readonly IActionColours Default = new IActionColours(
         Color.Transparent,
         Color.White,
         Color.White,
         Color.White);
      public static readonly IActionColours DarkenOnAction = new IActionColours(
         Color.Transparent,
         Color.FromNonPremultiplied(255,255,255,255),
         Color.FromNonPremultiplied(204,204,204,255),
         Color.FromNonPremultiplied(153,153,153,255));
      public static readonly IActionColours LightenOnAction = new IActionColours(
         Color.Transparent,
         Color.FromNonPremultiplied(153,153,153,255),
         Color.FromNonPremultiplied(204,204,204,255),
         Color.FromNonPremultiplied(255,255,255,255));

      public static readonly IActionColours TurnTranslucentOnAction = new IActionColours(
         Color.Transparent,
         Color.Multiply(Color.White, 1.00f),
         Color.Multiply(Color.White, 0.75f),
         Color.Multiply(Color.White, 0.50f));
      public static readonly IActionColours TurnSlightlyTranslucentOnAction = new IActionColours(
         Color.Transparent,
         Color.Multiply(Color.White, 1.00f),
         Color.Multiply(Color.White, 0.85f),
         Color.Multiply(Color.White, 0.70f));

      public static readonly IActionColours TurnOpaqueOnAction = new IActionColours(
         Color.Transparent,
         Color.Multiply(Color.White, 0.50f),
         Color.Multiply(Color.White, 0.75f),
         Color.Multiply(Color.White, 1.00f));
      public static readonly IActionColours TurnSlightlyOpaqueOnAction = new IActionColours(
         Color.Transparent,
         Color.Multiply(Color.White, 0.50f),
         Color.Multiply(Color.White, 0.65f),
         Color.Multiply(Color.White, 0.80f));

      #endregion

      // Public:
      #region Public

      public Color BackgroundColour { get; set; }
      public Color ForegroundColour { get; set; }
      public Color HoverColour { get; set; }
      public Color PressedColour { get; set; }

      #endregion

      // Constructors:
      #region Constructors

      public IActionColours(Color backgroundColour, Color foregroundColour, Color hoverColour, Color pressedColour) {
         this.BackgroundColour = backgroundColour;
         this.ForegroundColour = foregroundColour;
         this.HoverColour = hoverColour;
         this.PressedColour = pressedColour;
      }

      public IActionColours() {
         this.BackgroundColour = Default.BackgroundColour;
         this.ForegroundColour = Default.ForegroundColour;
         this.HoverColour = Default.HoverColour;
         this.PressedColour = Default.PressedColour;
      }

      #endregion
   }
}
