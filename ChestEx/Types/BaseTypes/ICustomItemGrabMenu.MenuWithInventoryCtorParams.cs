using System;

using StardewValley.Menus;

namespace ChestEx.Types.BaseTypes {
   public partial class ICustomItemGrabMenu {
      public class MenuWithInventoryCtorParams {
         // Public:
         #region Public

         public InventoryMenu.highlightThisItem HighlighterMethod;
         public Boolean OKButton;
         public Boolean TrashCan;
         public Int32 InventoryXOffset;
         public Int32 InventoryYOffset;
         public Int32 MenuOffsetHack;

         #endregion

         // Constructors:
         #region Constructors

         public MenuWithInventoryCtorParams(InventoryMenu.highlightThisItem highlighterMethod = null,
                                            Boolean okButton = false,
                                            Boolean trashCan = false,
                                            Int32 inventoryXOffset = 0,
                                            Int32 inventoryYOffset = 0,
                                            Int32 menuOffsetHack = 0) {
            this.HighlighterMethod = highlighterMethod;
            this.OKButton = okButton;
            this.TrashCan = trashCan;
            this.InventoryXOffset = inventoryXOffset;
            this.InventoryYOffset = inventoryYOffset;
            this.MenuOffsetHack = menuOffsetHack;
         }

         #endregion
      }
   }
}
