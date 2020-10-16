using System;
using System.ComponentModel;

using ChestEx.LanguageExtensions;

using Microsoft.Xna.Framework;

using StardewModdingAPI;

namespace ChestEx.Types.BaseTypes {
   public partial class ICustomMenu {
      public class MouseStateEx {
         // Public static instances:
         #region Public static instances

         public static readonly MouseStateEx Default = new MouseStateEx(SButton.None);

         #endregion

         // Public:
         #region Public
         public SButton Button { get; }

         public Vector2 Pos { get; set; }

         public SButtonState ButtonState { get; set; }

         // Overrides:
         #region Overrides

         public override Boolean Equals(Object obj) {
            if (obj is MouseStateEx other)
               return other.Button == this.Button && other.ButtonState == this.ButtonState && other.Pos.NearlyEquals(this.Pos);
            return false;
         }

         public override Int32 GetHashCode() {
            return base.GetHashCode();
         }

         #endregion

         #endregion

         // Constructors:
         #region Constructors

         public MouseStateEx(SButton button) {
            this.Button = button;
            this.Pos = Vector2.Zero;
            this.ButtonState = SButtonState.Released;
         }

         #endregion
      }
   }
}
