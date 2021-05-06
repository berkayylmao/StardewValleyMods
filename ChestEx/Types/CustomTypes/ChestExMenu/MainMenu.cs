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
using System.Collections.Generic;

using ChestEx.LanguageExtensions;
using ChestEx.Types.CustomTypes.ChestExMenu.Items;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using StardewValley.Menus;

namespace ChestEx.Types.CustomTypes.ChestExMenu {
  public class MainMenu : BaseTypes.ICustomItemGrabMenu {
    // Private:
    #region Private

    // Protected:
    #region Protected

    // Overrides:
    #region Overrides

    protected override BaseTypes.ICustomItemGrabMenu clone() {
      return new MainMenu(
         this.ItemsToGrabMenu.actualInventory, false, true,
         new InventoryMenu.highlightThisItem(InventoryMenu.highlightAllItems), this.sv_behaviorFunction, null,
         this.behaviorOnItemGrab, false, true, true, true, true, this.source, this.sv_sourceItem,
         this.whichSpecialButton, this.context);
    }

    #endregion

    #endregion

    #endregion

    // Public:
    #region Public

    // Overrides:
    #region Overrides

    public override void draw(SpriteBatch b) {
      /*
       *    
       *                       __________ /---------------------------------\
       *                       | Config | |                                 |
       *                       |  BTN   | |                                 |
       *       /-------\       ‾‾‾‾‾‾‾‾‾‾ |                                 |
       *       |       |       __________ |                                 |
       *       | Dummy |       | Colour | |      this.ItemsToGrabMenu       |
       *       | Chest |       |  PLT   | |  (config + colour palette too)  |
       *       |       |       ‾‾‾‾‾‾‾‾‾‾ |                                 |
       *       \-------/       __________ |                                 |
       *                       | Colour | |                                 |
       *                       |  RAW   | |                                 |
       *                       ‾‾‾‾‾‾‾‾‾‾ |                                 |
       *   /----------------\             \---------------------------------/
       *   | ChestsAnywhere |   
       *   \----------------/                  /----------------------\
       *                                       |    this.inventory    |
       *                                       \----------------------/
       *    
       *    
      */

      Action<SpriteBatch> _ = base.draw;
      _(b);
    }

    #endregion

    #endregion

    // Constructors:
    #region Constructors

    public MainMenu(IList<StardewValley.Item> inventory,
                Boolean reverseGrab,
                Boolean showReceivingMenu,
                InventoryMenu.highlightThisItem highlightFunction,
                ItemGrabMenu.behaviorOnItemSelect behaviorOnItemSelectFunction,
                String message,
                ItemGrabMenu.behaviorOnItemSelect behaviorOnItemGrab = null,
                Boolean snapToBottom = false,
                Boolean canBeExitedWithKey = false,
                Boolean playRightClickSound = true,
                Boolean allowRightClick = true,
                Boolean showOrganizeButton = false,
                Int32 source = 0,
                StardewValley.Item sourceItem = null,
                Int32 whichSpecialButton = -1,
                Object context = null)
       : base(inventory, reverseGrab, showReceivingMenu, highlightFunction, behaviorOnItemSelectFunction, message, behaviorOnItemGrab, snapToBottom, canBeExitedWithKey, playRightClickSound, allowRightClick, showOrganizeButton, source, sourceItem, whichSpecialButton, context) {
      // recreate menu
      {
        var ui_viewport = GlobalVars.UIViewport;

        this.ItemsToGrabMenu = new InventoryMenu(
           (ui_viewport.Width / 2) - (this.ItemsToGrabMenu.width / 2) + /* chest icon padding */ (this.ItemsToGrabMenu.height / 8),
           Math.Max(64, (ui_viewport.Height / 2) - Convert.ToInt32(this.ItemsToGrabMenu.height * StardewValley.Game1.options.uiScale)),
           false, this.ItemsToGrabMenu.actualInventory,
           this.ItemsToGrabMenu.highlightMethod, this.ItemsToGrabMenu.capacity, this.ItemsToGrabMenu.rows,
           this.ItemsToGrabMenu.horizontalGap, this.ItemsToGrabMenu.verticalGap, this.ItemsToGrabMenu.drawSlots);
        this.SourceInventoryOptions.Bounds = this.ItemsToGrabMenu.GetDialogueBoxRectangle();

        this.inventory = new InventoryMenu(
           this.SourceInventoryOptions.Bounds.X + (this.SourceInventoryOptions.Bounds.Width / 2) - (this.inventory.width / 2),
           Math.Min(ui_viewport.Height - this.inventory.height - 32, (this.SourceInventoryOptions.Bounds.Y + this.SourceInventoryOptions.Bounds.Height) + 32),
           false, null,
           this.inventory.highlightMethod, this.inventory.capacity, this.inventory.rows,
           this.inventory.horizontalGap, this.inventory.verticalGap, this.inventory.drawSlots);
        this.PlayerInventoryOptions.Bounds = this.inventory.GetDialogueBoxRectangle();

        this.ItemsToGrabMenu.populateClickableComponentList();
        for (Int32 i = 0; i < this.ItemsToGrabMenu.inventory.Count; i++) {
          if (this.ItemsToGrabMenu.inventory[i] != null) {
            this.ItemsToGrabMenu.inventory[i].myID += ItemGrabMenu.region_itemsToGrabMenuModifier;
            this.ItemsToGrabMenu.inventory[i].upNeighborID += ItemGrabMenu.region_itemsToGrabMenuModifier;
            this.ItemsToGrabMenu.inventory[i].rightNeighborID += ItemGrabMenu.region_itemsToGrabMenuModifier;
            this.ItemsToGrabMenu.inventory[i].downNeighborID = ClickableComponent.CUSTOM_SNAP_BEHAVIOR;
            this.ItemsToGrabMenu.inventory[i].leftNeighborID += ItemGrabMenu.region_itemsToGrabMenuModifier;
            this.ItemsToGrabMenu.inventory[i].fullyImmutable = true;
          }
        }

        this.SetBounds(
           this.SourceInventoryOptions.Bounds.X,
           this.SourceInventoryOptions.Bounds.Y,
           this.SourceInventoryOptions.Bounds.Width,
           this.SourceInventoryOptions.Bounds.Height
              + this.SourceInventoryOptions.Bounds.Y + 32
              + this.PlayerInventoryOptions.Bounds.Height);

        // handle organization buttons
        {
          this.createOrganizationButtons(false, false, false);
          this.okButton = this.trashCan = null;
          if (!(this.dropItemInvisibleButton is null)) {
            this.dropItemInvisibleButton.bounds = new Rectangle(
               this.PlayerInventoryOptions.Bounds.X + (this.PlayerInventoryOptions.Bounds.Width / 2),
               this.PlayerInventoryOptions.Bounds.Y - ((this.PlayerInventoryOptions.Bounds.Y - (this.SourceInventoryOptions.Bounds.Y + this.SourceInventoryOptions.Bounds.Height)) / 2),
               64, 64);
          }
        }

        this.populateClickableComponentList();
        if (StardewValley.Game1.options.SnappyMenus)
          this.snapToDefaultClickableComponent();
        this.SetupBorderNeighbors();
      }

      this.MenuItems.Add(new ChestConfigPanel(this));
    }

    #endregion
  }
}
