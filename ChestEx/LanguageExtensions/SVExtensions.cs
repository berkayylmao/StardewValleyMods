using System;
using System.Linq;

using ChestEx.Types.CustomTypes.ExtendedSVObjects;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using StardewValley.Menus;
using StardewValley.Objects;

namespace ChestEx.LanguageExtensions {
  public static class SVExtensions {
    /// <summary>
    /// Gets a <see cref="Rectangle"/> to wrap the given rectangle to be used with'<see cref="StardewValley.Game1.drawDialogueBox(Int32, Int32, Int32, Int32, Boolean, Boolean, String, Boolean, Boolean, Int32, Int32, Int32)"/>'.
    /// </summary>
    /// <returns>A <see cref="Rectangle"/> that can be used to draw a dialogue box around the given rectangle.</returns>
    public static Rectangle GetDialogueBoxRectangle(this Rectangle rectangle) {
      return new(rectangle.X - 36,
                 rectangle.Y - IClickableMenu.spaceToClearTopBorder - 8,
                 rectangle.Width + IClickableMenu.borderWidth + 32,
                 rectangle.Height + IClickableMenu.spaceToClearTopBorder + 48);
    }

    /// <summary>
    /// Gets a <see cref="Rectangle"/> to wrap the given <see cref="InventoryMenu"/> using '<see cref="StardewValley.Game1.drawDialogueBox(Int32, Int32, Int32, Int32, Boolean, Boolean, String, Boolean, Boolean, Int32, Int32, Int32)"/>'.
    /// </summary>
    /// <returns>A <see cref="Rectangle"/> that can be used to draw a dialogue box around the given menu.</returns>
    public static Rectangle GetDialogueBoxRectangle(this InventoryMenu menu) {
      Point last_slot = menu.GetSlotDrawPositions().Last().AsXNAPoint();

      return new Rectangle(menu.xPositionOnScreen - 36,
                           menu.yPositionOnScreen - IClickableMenu.spaceToClearTopBorder - 12,
                           last_slot.X + (IClickableMenu.borderWidth + 32) * 2 - menu.xPositionOnScreen,
                           last_slot.Y + (IClickableMenu.spaceToClearTopBorder + 10) * 2 - menu.yPositionOnScreen);
    }

    /// <summary>
    /// Gets a <see cref="Rectangle"/> that represents the content rectangle of the given dialogue box.
    /// </summary>
    /// <returns>Content <see cref="Rectangle"/> of the given dialogue box.</returns>
    public static Rectangle GetContentRectangle(this Rectangle dialogueBoxBounds) {
      return new(dialogueBoxBounds.X + 36,
                 dialogueBoxBounds.Y + IClickableMenu.spaceToClearTopBorder + 12,
                 dialogueBoxBounds.Width - IClickableMenu.borderWidth - 36,
                 dialogueBoxBounds.Height - IClickableMenu.spaceToClearTopBorder - 48);
    }

    /// <summary>
    /// Gets a <see cref="Rectangle"/> that represents the content rectangle of the <see cref="InventoryMenu"/>.
    /// </summary>
    /// <returns>Content <see cref="Rectangle"/> of the given <see cref="InventoryMenu"/>.</returns>
    public static Rectangle GetContentRectangle(this InventoryMenu menu) {
      Point last_slot = menu.GetSlotDrawPositions().Last().AsXNAPoint();

      return new Rectangle(menu.xPositionOnScreen - 4,
                           menu.yPositionOnScreen - 12,
                           last_slot.X + IClickableMenu.borderWidth + 32 - menu.xPositionOnScreen,
                           last_slot.Y + IClickableMenu.spaceToClearTopBorder - 12 - menu.yPositionOnScreen);
    }

    /// <summary>
    /// Gets the bounds of the given <see cref="IClickableMenu"/>.
    /// </summary>
    /// <returns>A <see cref="Rectangle"/> containing the bounds of the given menu.</returns>
    public static Rectangle GetBounds(this IClickableMenu menu) { return new(menu.xPositionOnScreen, menu.yPositionOnScreen, menu.width, menu.height); }

    /// <summary>
    /// Sets the given menu's X and Y position, and width and height to that of the given bounds.
    /// </summary>
    public static void SetBounds(this IClickableMenu menu, Rectangle bounds) {
      menu.xPositionOnScreen = bounds.X;
      menu.yPositionOnScreen = bounds.Y;
      menu.width             = bounds.Width;
      menu.height            = bounds.Height;
    }

    /// <summary>
    /// Sets the given menu's X and Y position, and width and height to that of the given values.
    /// </summary>
    public static void SetBounds(this IClickableMenu menu, Int32 x, Int32 y, Int32 width,
                                 Int32               height) {
      menu.xPositionOnScreen = x;
      menu.yPositionOnScreen = y;
      menu.width             = width;
      menu.height            = height;
    }

    /// <summary>
    /// Sets the given menu's and its clickable components' visibiliy to '<paramref name="isVisible"/>'.
    /// </summary>
    /// <param name="menu"></param>
    /// <param name="isVisible">Whether this menu should be visible.</param>
    public static void SetVisibleEx(this InventoryMenu menu, Boolean isVisible) { menu.inventory?.ForEach(cc => cc.visible = isVisible); }

    public static void DrawEx(this InventoryMenu menu, SpriteBatch b, Color slotBorderColour) { menu.draw(b, slotBorderColour.R, slotBorderColour.G, slotBorderColour.B); }

    public static ExtendedChest.ChestType GetChestType(this Chest chest) {
      return chest.fridge.Value       ? ExtendedChest.ChestType.Fridge :
        chest.ParentSheetIndex == 130 ? ExtendedChest.ChestType.WoodenChest :
        chest.ParentSheetIndex == 232 ? ExtendedChest.ChestType.StoneChest : ExtendedChest.ChestType.None;
    }

    public static Color GetActualColour(this Chest chest) {
      Color col = chest.playerChoiceColor.Value;

      if (col == Color.Black)
        col = chest.GetChestType() == ExtendedChest.ChestType.WoodenChest ? Color.FromNonPremultiplied(206, 120, 41, 255) : Color.FromNonPremultiplied(207, 191, 179, 255);

      return col;
    }
  }
}