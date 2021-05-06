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

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using StardewValley.Objects;

using static ChestEx.Types.BaseTypes.ICustomItemGrabMenu;

namespace ChestEx.Types.CustomTypes.ExtendedSVObjects {
  public class ExtendedChest : Chest {
    // Public:
    #region Public

    // Consts:
    #region Consts

    public const Int32 CONST_CHEST_SPRITE_SIZE = 16;

    #endregion

    public Color ChestColour {
      get { return this.playerChoiceColor.Value; }
      set { this.playerChoiceColor.Value = value; }
    }

    public Color HingesColour { get; set; }

    public Single ChestScale { get; set; }

    public ItemGrabMenuSourceType ChestType { get; set; }

    public Vector2 ChestSize {
      get {
        return new Vector2(
           ExtendedChest.CONST_CHEST_SPRITE_SIZE * this.ChestScale,
           ExtendedChest.CONST_CHEST_SPRITE_SIZE * this.ChestScale * 12);
      }
    }

    // Inaccessible 'Chest + bases' fields exposed through reflection properties:
    #region Inaccessible 'Chest + bases' fields exposed through reflection properties

    public Int32 sv_currentLidFrame {
      get { return Harmony.Traverse.Create(this).Field<Int32>("currentLidFrame").Value; }
      set { Harmony.Traverse.Create(this).Field<Int32>("currentLidFrame").Value = value; }
    }

    #endregion

    public void Draw(SpriteBatch b, Vector2 position, Single alpha = 1.0f) {
      // bottom half
      b.Draw(StardewValley.Game1.bigCraftableSpriteSheet,
             position,
             StardewValley.Game1.getSourceRectForStandardTileSheet(StardewValley.Game1.bigCraftableSpriteSheet,
             this.ChestType == ItemGrabMenuSourceType.WoodenChest ? 168 : 232, 16, 32),
             this.ChestColour * alpha, 0f, Vector2.Zero, this.ChestScale, SpriteEffects.None, 0.9f);

      // top half
      b.Draw(StardewValley.Game1.bigCraftableSpriteSheet,
             position,
             StardewValley.Game1.getSourceRectForStandardTileSheet(StardewValley.Game1.bigCraftableSpriteSheet,
             this.sv_currentLidFrame + (this.ChestType == ItemGrabMenuSourceType.WoodenChest ? 38 : 0), 16, 32),
             this.ChestColour * alpha, 0f, Vector2.Zero, this.ChestScale, SpriteEffects.None, 0.9f);

      // bottom 'metal hinge'
      b.Draw(StardewValley.Game1.bigCraftableSpriteSheet,
             new Vector2(position.X, (position.Y + (21 * this.ChestScale))),
             new Rectangle(0, ((((this.ChestType == ItemGrabMenuSourceType.WoodenChest) ? 168 : 232) / 8) * 32) + 53, 16, 8),
             this.HingesColour * alpha, 0f, Vector2.Zero, this.ChestScale, SpriteEffects.None, 0.91f);

      // top 'metal hinge'
      b.Draw(StardewValley.Game1.bigCraftableSpriteSheet,
             position,
             StardewValley.Game1.getSourceRectForStandardTileSheet(StardewValley.Game1.bigCraftableSpriteSheet,
             this.sv_currentLidFrame + (this.ChestType == ItemGrabMenuSourceType.WoodenChest ? 46 : 8), 16, 32),
             this.HingesColour * alpha, 0f, Vector2.Zero, this.ChestScale, SpriteEffects.None, 0.91f);
    }

    public void Draw(SpriteBatch b, Rectangle bounds, Single alpha = 1.0f) {
      this.Draw(b, new Vector2(bounds.X, bounds.Y), alpha);
    }

    #endregion

    // Constructors:
    #region Constructors

    public ExtendedChest(Single chestScale, Color hingesColour, ItemGrabMenuSourceType chestType) : base(false) {
      this.ChestScale = chestScale;
      this.ChestType = chestType;
      this.HingesColour = hingesColour;
      this.ParentSheetIndex = this.ChestType == ItemGrabMenuSourceType.WoodenChest ? 130 : 232;

      this.startingLidFrame.Value = this.ParentSheetIndex + 1;
      this.resetLidFrame();
    }

    public ExtendedChest(Rectangle bounds, ItemGrabMenuSourceType chestType = ItemGrabMenuSourceType.WoodenChest)
      : this((Single)bounds.Width / ExtendedChest.CONST_CHEST_SPRITE_SIZE, Color.White, chestType) { }

    #endregion
  }
}
