using System;
using System.IO;

using ChestEx.Types.BaseTypes;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ChestEx.Types.CustomTypes.ChestExMenu.Items {
   public partial class ConfigPanel {
      private class ConfigButton : BaseTypes.ICustomItemGrabMenuItem.BasicComponent {
         // Protected:
         #region Protected

         // Overrides:
         #region Overrides

         protected override Texture2D GetTexture(GraphicsDevice device) {
            if (this.texture is null) {
               // create texture
               using var stream = new MemoryStream();
               //ChestEx.Properties.Resources.ConfigIcon.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
               this.texture = Texture2D.FromStream(StardewValley.Game1.graphics.GraphicsDevice, stream);

               // set bounds
               this.Bounds = new Rectangle(
                  this.Bounds.X - this.texture.Width + 8,
                  this.Bounds.Y - this.texture.Height - 8,
                  this.texture.Width,
                  this.texture.Height);
            }

            return this.texture;
         }

         #endregion

         #endregion

         // Constructors:
         #region Constructors

         public ConfigButton(ICustomItemGrabMenuItem hostMenuItem, Point point, String componentName = "", EventHandler<ICustomMenu.MouseStateEx> onMouseClick = null, String hoverText = "", BaseTypes.IActionColours textureTintColours = null)
            : base(hostMenuItem, point, true, componentName, onMouseClick, hoverText, textureTintColours) { }

         #endregion
      }
   }
}
