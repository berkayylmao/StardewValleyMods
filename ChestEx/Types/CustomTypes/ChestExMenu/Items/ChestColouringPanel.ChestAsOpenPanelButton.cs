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
