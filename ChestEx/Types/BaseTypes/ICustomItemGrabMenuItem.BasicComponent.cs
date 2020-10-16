using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ChestEx.Types.BaseTypes {
   public partial class ICustomItemGrabMenuItem {
      public new class BasicComponent : ICustomMenuItem.BasicComponent {
         // Public:
         #region Public

         // Shadowed:
         #region Shadowed

         public new ICustomItemGrabMenuItem HostMenuItem { get; private set; }

         #endregion

         #endregion

         // Constructors:
         #region Constructors

         public BasicComponent(ICustomItemGrabMenuItem hostMenuItem, Rectangle bounds, Boolean raiseMouseClickEventOnRelease = true, String componentName = "", EventHandler<ICustomMenu.MouseStateEx> onMouseClickHandler = null, String hoverText = "", IActionColours textureTintColours = null) : base(hostMenuItem, bounds, raiseMouseClickEventOnRelease, componentName, onMouseClickHandler, hoverText, textureTintColours) {
            this.HostMenuItem = hostMenuItem;
         }

         public BasicComponent(ICustomItemGrabMenuItem hostMenuItem, Point point, Boolean raiseMouseClickEventOnRelease = true, String componentName = "", EventHandler<ICustomMenu.MouseStateEx> onMouseClickHandler = null, String hoverText = "", IActionColours textureTintColours = null) : this(hostMenuItem, new Rectangle(point.X, point.Y, -1, -1), raiseMouseClickEventOnRelease, componentName, onMouseClickHandler, hoverText, textureTintColours) { }

         #endregion
      }
   }
}
