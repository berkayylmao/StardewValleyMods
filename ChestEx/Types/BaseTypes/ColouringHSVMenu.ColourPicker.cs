﻿#region License

// 
//    ChestEx (StardewValleyMods)
//    Copyright (c) 2021 Berkay Yigit <berkaytgy@gmail.com>
// 
//    This program is free software: you can redistribute it and/or modify
//    it under the terms of the GNU Affero General Public License as published
//    by the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
// 
//    This program is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY, without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
//    GNU Affero General Public License for more details.
// 
//    You should have received a copy of the GNU Affero General Public License
//    along with this program. If not, see <https://www.gnu.org/licenses/>.

#endregion

using System;
//using System.Drawing;
//using System.Drawing.Drawing2D;
//using System.Drawing.Imaging;
using System.IO;

using SkiaSharp;

using ChestEx.LanguageExtensions;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using StardewModdingAPI;

using StardewValley;

using Color = Microsoft.Xna.Framework.Color;
using Point = Microsoft.Xna.Framework.Point;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace ChestEx.Types.BaseTypes {
  public partial class ColouringHSVMenu {
    public class ColourPicker : CustomClickableTextureComponent {
      // Consts:
    #region Consts

      private const Int32 CONST_SELECTOR_SIZE = 12;

    #endregion

      // Private:
    #region Private

      private readonly Action<Color> onColourHovered;
      private readonly Action<Color> onColourChanged;

      private SKBitmap  bitmap;
      private Boolean selectorIsVisible = true;
      private Point   selectorPos       = Point.Zero;
      private Point   selectorActivePos = Point.Zero;
      private Color   hueColour         = Color.Red;

      private Texture2D getOrCreateTexture(GraphicsDevice device) {
        if (this.bitmap is null || this.texture is null) {
          this.bitmap?.Dispose();
          this.texture?.Dispose();

          this.bitmap = new SKBitmap(this.mBounds.Width, this.mBounds.Height);
          using var ms = new MemoryStream();
          SKManagedWStream writeableStream = new(ms);
          SKCanvas canvas = new(bitmap);
          SKRect rect = new SKRect(0, 0, this.mBounds.Width, this.mBounds.Height);
          SKPoint midpoint = new SKPoint(rect.MidX, rect.MidY);
          float radius = Math.Min(rect.Height, rect.Width) / 1.8f;

          using (SKPaint paint = new()) {
            paint.Shader = SKShader.CreateLinearGradient(
              new SKPoint(0, rect.Top),
              new SKPoint(0, rect.Bottom),
              new SKColor[] {SKColors.Red,
                             SKColors.Yellow,
                             SKColors.Lime,
                             SKColors.Blue,
                             SKColors.Magenta,
                             SKColors.Purple,
                             SKColors.Red
                            },
              new float[] {0, 1/6f, 2/6f, 3/6f, 4/6f, 5/6f, 1},
              SKShaderTileMode.Clamp
              );
            canvas.DrawPaint(paint);
          }

          using (SKPaint paint2 = new()) {
            paint2.Shader = SKShader.CreateLinearGradient(
              new SKPoint(0, 0),
              new SKPoint(rect.Right, 0),
              new SKColor[] { SKColors.White, SKColors.Transparent, SKColors.Black },
              new float[] {0.1f,0.5f,0.9f},
              SKShaderTileMode.Clamp
              );
            canvas.DrawPaint(paint2);
          }

          bitmap.Encode(writeableStream, SKEncodedImageFormat.Png, 100);
          ms.Seek(0, SeekOrigin.Begin);
          this.texture = Texture2D.FromStream(device, ms);
        }

        return this.texture;
      }

      private Color getColourAt(Point position) {
        SKColor mycolor = this.bitmap.GetPixel(Math.Max(0, Math.Min(position.X, this.mBounds.Width - 1)), Math.Max(0, Math.Min(position.Y, this.mBounds.Height - 1)));
        return new Color(mycolor.Red, mycolor.Green, mycolor.Blue, mycolor.Alpha);      
      }

      private void setSelectorPos(Point position) {
        if (this.bitmap is null) return;

        this.selectorPos = new Point(Math.Min(this.mBounds.Width - CONST_SELECTOR_SIZE, Math.Max(0, position.X - 4)),
                                     Math.Min(this.mBounds.Height - CONST_SELECTOR_SIZE, Math.Max(0, position.Y - 4)));
        this.onColourHovered?.Invoke(this.getColourAt(position));
      }

    #endregion

      // Public:
    #region Public

      public void SetHueColour(Color colour) {
        if (this.hueColour == colour) return;
        this.hueColour = colour;

        this.bitmap?.Dispose();
        this.texture?.Dispose();
        this.bitmap  = null;
        this.texture = null;
      }

      public void Reset() { this.selectorActivePos = this.selectorPos = Point.Zero; }

      public void SetSelectorVisible(Boolean isVisible) { this.selectorIsVisible = isVisible; }

      public void SetSelectorActivePos(Point position, Boolean fireEvents = true) {
        this.selectorActivePos = this.selectorPos = new Point(Math.Min(this.mBounds.Width - CONST_SELECTOR_SIZE, Math.Max(0, position.X - 4)),
                                                              Math.Min(this.mBounds.Height - CONST_SELECTOR_SIZE, Math.Max(0, position.Y - 4)));
        if (this.bitmap is null) {
          if (Game1.graphics.GraphicsDevice is null) return;
          this.getOrCreateTexture(Game1.graphics.GraphicsDevice);
        }

        Color colour = this.getColourAt(position);
        if (colour != Color.Black && fireEvents) this.onColourChanged?.Invoke(colour);
      }

      // Overrides:
    #region Overrides

      public override void Draw(SpriteBatch spriteBatch) {
        this.texture = this.getOrCreateTexture(spriteBatch.GraphicsDevice);
        base.Draw(spriteBatch);

        // Draw the selector
        if (this.selectorIsVisible) {
          SKColor skColor = this.bitmap.GetPixel(this.selectorPos.X, this.selectorPos.Y);
          Color newcolor = new(skColor.Red, skColor.Green, skColor.Blue, skColor.Alpha);
          spriteBatch.Draw(TexturePresets.gCursorsGrayScale,
                           new Rectangle(this.mBounds.X + this.selectorPos.X, this.mBounds.Y + this.selectorPos.Y, CONST_SELECTOR_SIZE, CONST_SELECTOR_SIZE),
                           new Rectangle(205, 1888, 12, 12),
                           newcolor.ContrastColour());
        }
      }

      public override void OnCursorMoved(Vector2 cursorPosition) {
        if (this.mBounds.Contains(cursorPosition.AsXNAPoint()))
          this.setSelectorPos((cursorPosition - this.mBounds.ExtractXYAsXNAVector2()).AsXNAPoint());
        else
          this.selectorPos = this.selectorActivePos;
      }

      public override void OnMouseClick(InputStateEx inputState) {
        if (inputState.mButton != SButton.MouseLeft) return;
        this.SetSelectorActivePos((inputState.mCursorPos - this.mBounds.ExtractXYAsXNAVector2()).AsXNAPoint());
      }

      // Disable cursor hover scaling
      public override void OnGameTick() { }

    #endregion

    #endregion

      // Constructors:
    #region Constructors

      public ColourPicker(Rectangle bounds, Colours colours, Action<Color> onColourHovered, Action<Color> onColourChanged)
        : base(bounds, colours) {
        this.onColourHovered = onColourHovered;
        this.onColourChanged = onColourChanged;
      }

    #endregion

      // IDisposable:
    #region IDisposable

      public override void Dispose() {
        base.Dispose();
        this.bitmap?.Dispose();
        this.bitmap  = null;
        this.texture = null;
      }

    #endregion
    }
  }
}
