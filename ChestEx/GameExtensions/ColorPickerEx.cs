using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using StardewValley;
using StardewValley.Menus;
using StardewValley.Objects;

namespace ChestEx {
   public class ChestColorPickerEx : IClickableMenu {
      public Boolean visible;
      public Microsoft.Xna.Framework.Color chestColor;
      public Chest dummyChest;

      private Bitmap canvas;
      private Graphics graphics;
      private LinearGradientBrush spectrumGradient, darkMixGradient, lightMixGradient;
      private Microsoft.Xna.Framework.Graphics.Texture2D canvasTexture;
      private Boolean canvasTextureUpdated;
      private Chest actualChest;

      private void setupBrushes() {
         if (this.canvas != null) {
            this.canvas.Dispose();
            this.canvas = null;
         }
         this.canvas = new Bitmap(this.width, this.height);

         if (this.graphics != null) {
            this.graphics.Dispose();
            this.graphics = null;
         }
         this.graphics = Graphics.FromImage(this.canvas);

         var blend = new ColorBlend {
            Positions = new[] { 0f, 1 / 4f, 2 / 4f, 3 / 4f, 1f },
            Colors = new[] { Color.Red, Color.Orange, Color.Green, Color.Cyan, Color.Blue }
         };

         if (this.spectrumGradient != null) {
            this.spectrumGradient.Dispose();
            this.spectrumGradient = null;
         }
         this.spectrumGradient = new LinearGradientBrush(new RectangleF(0f, 0f, this.width, this.height), Color.White, Color.White, 0f) {
            InterpolationColors = blend
         };

         if (this.darkMixGradient != null) {
            this.darkMixGradient.Dispose();
            this.darkMixGradient = null;
         }
         this.darkMixGradient = new LinearGradientBrush(new RectangleF(0f, 0f, this.width, this.height * 0.3f), Color.Black, Color.Transparent, 90f);

         if (this.lightMixGradient != null) {
            this.lightMixGradient.Dispose();
            this.lightMixGradient = null;
         }
         this.lightMixGradient = new LinearGradientBrush(new RectangleF(0f, this.height * 0.7f, this.width, this.height * 0.3f), Color.Transparent, Color.White, 90f);
      }
      private void drawPicker() {
         this.graphics.FillRectangle(this.spectrumGradient, 0f, 0f, this.width, this.height);
         this.graphics.FillRectangle(this.darkMixGradient, 0f, 0f, this.width, this.height * 0.3f);
         this.graphics.FillRectangle(this.lightMixGradient, 0f, this.height * 0.7f + 1f /* just some glitch */, this.width, this.height * 0.3f);

         this.canvasTextureUpdated = false;
      }
      private void convertPickerToTexture2D(Microsoft.Xna.Framework.Graphics.GraphicsDevice device) {
         if (this.canvasTexture != null) {
            this.canvasTexture.Dispose();
            this.canvasTexture = null;
         }
         this.canvasTexture = new Microsoft.Xna.Framework.Graphics.Texture2D(device, this.canvas.Width, this.canvas.Height, false, Microsoft.Xna.Framework.Graphics.SurfaceFormat.Color);
         BitmapData bits = this.canvas.LockBits(new Rectangle(0, 0, this.canvas.Width, this.canvas.Height), ImageLockMode.ReadOnly, this.canvas.PixelFormat);
         var bytes = new Byte[bits.Height * bits.Stride];

         System.Runtime.InteropServices.Marshal.Copy(bits.Scan0, bytes, 0, bytes.Length);
         this.canvasTexture.SetData(bytes);

         this.canvas.UnlockBits(bits);
         this.canvasTextureUpdated = true;
      }

      public override void performHoverAction(Int32 x, Int32 y) {
         if (!this.visible)
            return;

         x -= this.xPositionOnScreen;
         y -= this.yPositionOnScreen;
         if ((x >= 0 && x < this.canvasTexture.Width) &&
            (y >= 0 && y < this.canvasTexture.Height)) {
            var c = this.canvas.GetPixel(x, y);
            if (c == Color.Black) // changes the color to the default brown color
               return;

            this.dummyChest.playerChoiceColor.Value = new Microsoft.Xna.Framework.Color(c.R, c.G, c.B);
            this.dummyChest.resetLidFrame();
         } else {
            this.dummyChest.playerChoiceColor.Value = chestColor;
            this.dummyChest.resetLidFrame();
         }
      }
      public override void update(Microsoft.Xna.Framework.GameTime time) {
         base.update(time);
      }

      public override void receiveLeftClick(Int32 x, Int32 y, Boolean playSound = true) {
         if (!this.visible)
            return;
         base.receiveLeftClick(x, y, playSound);
         x -= this.xPositionOnScreen;
         y -= this.yPositionOnScreen;
         if ((x >= 0 && x < this.canvasTexture.Width) &&
            (y >= 0 && y < this.canvasTexture.Height)) {
            var c = this.canvas.GetPixel(x, y);
            if (c == Color.Black) // changes the color to the default brown color
               return;

            this.chestColor = new Microsoft.Xna.Framework.Color(c.R, c.G, c.B);

            this.actualChest.playerChoiceColor.Value = chestColor;
            this.dummyChest.playerChoiceColor.Value = chestColor;
            this.dummyChest.resetLidFrame();
         }
      }

      public override void gameWindowSizeChanged(Microsoft.Xna.Framework.Rectangle oldBounds, Microsoft.Xna.Framework.Rectangle newBounds) {
         base.gameWindowSizeChanged(oldBounds, newBounds);

         this.setupBrushes();
         this.drawPicker();
      }

      public override void draw(Microsoft.Xna.Framework.Graphics.SpriteBatch b) {
         if (this.visible) {
            if (!this.canvasTextureUpdated)
               this.convertPickerToTexture2D(b.GraphicsDevice);

            Game1.drawDialogueBox(
               this.xPositionOnScreen - IClickableMenu.borderWidth * 2 - IClickableMenu.spaceToClearSideBorder * 2 - 32,
               this.yPositionOnScreen - 134,
               64 + 64,
               92 + 128,
               false, true, null, false, true, 60, 50, 40);

            this.dummyChest.draw(b,
               this.xPositionOnScreen - IClickableMenu.borderWidth * 2 - IClickableMenu.spaceToClearSideBorder * 2,
               this.yPositionOnScreen,
               1f, true);

            Game1.drawDialogueBox(
               this.xPositionOnScreen - 32,
               this.yPositionOnScreen - 96,
               this.width + 64,
               this.height + 128,
               false, true, null, false, true, 60, 50, 40);

            b.Draw(this.canvasTexture, new Microsoft.Xna.Framework.Rectangle(this.xPositionOnScreen, this.yPositionOnScreen, canvasTexture.Width, canvasTexture.Height),
               null, Microsoft.Xna.Framework.Color.White, 0f, Microsoft.Xna.Framework.Vector2.Zero, Microsoft.Xna.Framework.Graphics.SpriteEffects.FlipHorizontally, 0f);
         }
      }

      public ChestColorPickerEx(Int32 x, Int32 y, Int32 width, Int32 height, Chest actualChest) {
         this.visible = Game1.player.showChestColorPicker;

         this.xPositionOnScreen = x;
         this.yPositionOnScreen = y;
         this.width = width;
         this.height = height;

         this.actualChest = actualChest;
         this.dummyChest = new Chest(true);
         this.dummyChest.playerChoiceColor.Value = this.chestColor = actualChest.playerChoiceColor.Value;
         this.dummyChest.resetLidFrame();

         this.setupBrushes();
         this.drawPicker();
      }
   }
}
