//
//    Copyright (C) 2021 Berkay Yigit <berkaytgy@gmail.com>
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

using ChestEx.LanguageExtensions;
using ChestEx.Types.BaseTypes;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using StardewValley.Objects;

namespace ChestEx.Types.CustomTypes.ChestExMenu.Items {
  public partial class ChestConfigPanel : ICustomItemGrabMenuItem {
    // Private:

    private ColourPalette _colourPalette;

    private Boolean _isShowingColourEditors;

    // Component event handlers:
    #region Component event handlers

    private void _componentOnClick(Object sender, ICustomMenu.MouseStateEx mouseState) {
      switch ((sender as BaseTypes.ICustomItemGrabMenuItem.BasicComponent).Name) {
        case "openPanelBTN":
          _isShowingColourEditors.Flip();
          _colourPalette.SetVisible(_isShowingColourEditors);
          this.HostMenu.SourceInventoryOptions.SetVisible(!_isShowingColourEditors);
          break;
        case "palette":
          var pos_as_point = mouseState.Pos.AsXNAPoint();
          var colour = _colourPalette.GetColourAt(pos_as_point.X, pos_as_point.Y);

          if (mouseState.Button == StardewModdingAPI.SButton.MouseLeft) {
            this.ChestAsPanelButton.MenuChest.ChestColour = colour;
            this.HostMenu.GetSourceAs<Chest>().playerChoiceColor.Value = colour;
            this.HostMenu.SourceInventoryOptions.BackgroundColour = colour;
          } else {
            this.ChestAsPanelButton.MenuChest.HingesColour = colour;
          }
          break;
      }
    }

    #endregion

    // Public:

    public ExtendedSVObjects.ExtendedChestInCustomItemGrabMenu ChestAsPanelButton;

    // Overrides:

    public override void Draw(SpriteBatch b) {
      if (!this.IsVisible)
        return;

      if (_isShowingColourEditors) {
        var wrap_rectangle_ItemsToGrabMenu = this.HostMenu.ItemsToGrabMenu.GetDialogueBoxRectangle();
        StardewValley.Game1.drawDialogueBox(
           wrap_rectangle_ItemsToGrabMenu.X, wrap_rectangle_ItemsToGrabMenu.Y,
           wrap_rectangle_ItemsToGrabMenu.Width, wrap_rectangle_ItemsToGrabMenu.Height,
           false, true,
           r: this.Colours.BackgroundColour.R,
           g: this.Colours.BackgroundColour.G,
           b: this.Colours.BackgroundColour.B);
      }

      base.Draw(b);
    }

    // Constructors:

    public ChestConfigPanel(ICustomItemGrabMenu hostMenu) : base(hostMenu, GlobalVars.UIViewport, true, Colours.Default) {
      _isShowingColourEditors = false;

      var source_menu_bounds = this.HostMenu.SourceInventoryOptions.Bounds;
      var menu_chest_size = new Point(
            source_menu_bounds.Height / 4,
            source_menu_bounds.Height / 2
            );

      this.ChestAsPanelButton = new ExtendedSVObjects.ExtendedChestInCustomItemGrabMenu(this,
         new Rectangle(
            source_menu_bounds.X - menu_chest_size.X - 24,
            source_menu_bounds.Y + ((source_menu_bounds.Height / 2) - (menu_chest_size.Y / 2)),
            menu_chest_size.X,
            menu_chest_size.Y),
         "openPanelBTN", _componentOnClick, "Toggle configuration panel", BaseTypes.Colours.TurnTranslucentOnAction);

      _colourPalette = new ColourPalette(this, this.HostMenu.ItemsToGrabMenu.GetContentRectangle(), this.ChestAsPanelButton.MenuChest, "palette", _componentOnClick);
      _colourPalette.SetVisible(false);

      this.Colours = new Colours(Color.FromNonPremultiplied(50, 60, 70, 255), Color.White, Color.White, Color.White);

      this.Components.Add(_colourPalette);
      this.Components.Add(this.ChestAsPanelButton);

      this.HostMenu.SourceInventoryOptions.BackgroundColour = this.ChestAsPanelButton.MenuChest.playerChoiceColor.Value;
    }
  }
}
