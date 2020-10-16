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
