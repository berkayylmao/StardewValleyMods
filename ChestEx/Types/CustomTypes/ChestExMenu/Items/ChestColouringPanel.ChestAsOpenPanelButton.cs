using System;

using ChestEx.Types.BaseTypes;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using StardewModdingAPI.Events;

using StardewValley.Menus;
using StardewValley.Objects;

namespace ChestEx.Types.CustomTypes.ChestExMenu.Items {
   public partial class ChestColouringPanel {
      private class ChestAsOpenPanelButton : BaseTypes.ICustomItemGrabMenuItem.BasicComponent {
         public override void OnCursorMoved(CursorMovedEventArgs e) {
            base.OnCursorMoved(e);

            if (this.cursorStatus == CursorStatus.Hovering) {
               if (this.MenuChest.sv_currentLidFrame != this.MenuChest.getLastLidFrame())
                  this.MenuChest.sv_currentLidFrame++;
            } else {
               if (this.MenuChest.sv_currentLidFrame != this.MenuChest.startingLidFrame)
                  this.MenuChest.sv_currentLidFrame--;
            }
         }

         // Public:
         #region Public

         public InteractableMenuChest MenuChest { get; set; }

         // Overrides:
         #region Overrides

         public override void Draw(SpriteBatch b) {
            if (!this.IsVisible)
               return;

            this.MenuChest.Draw(b, this.Bounds, this.textureTintColourCurrent.A / 255.0f);

            if (this.cursorStatus != CursorStatus.None && !String.IsNullOrWhiteSpace(this.hoverText))
               ICustomMenu.DrawHoverText(b, StardewValley.Game1.smallFont, this.hoverText, (8, 8, 8, 8), this.HostMenuItem.ActionColours.BackgroundColour, this.HostMenuItem.ActionColours.ForegroundColour);
         }

         #endregion

         #endregion

         // Constructors:
         #region Constructors

         public ChestAsOpenPanelButton(BaseTypes.ICustomItemGrabMenuItem hostMenuItem, Rectangle bounds, String componentName = "", EventHandler<ICustomMenu.MouseStateEx> onMouseClick = null, String hoverText = "", BaseTypes.IActionColours textureTintColours = null) : base(hostMenuItem, bounds, true, componentName, onMouseClick, hoverText, textureTintColours) {
            // create and sync dummy chest
            this.MenuChest = new InteractableMenuChest(this.Bounds);
            this.MenuChest.playerChoiceColor.Value = hostMenuItem.HostMenu.GetSourceAs<Chest>().playerChoiceColor.Value;
         }

         #endregion
      }
   }
}
