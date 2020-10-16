using System;

using Microsoft.Xna.Framework;

namespace ChestEx.Types.BaseTypes {
   public partial class ICustomItemGrabMenuItem : ICustomMenuItem {
      // Public:
      #region Public

      // Shadowed:
      #region Shadowed

      public new ICustomItemGrabMenu HostMenu { get; private set; }

      #endregion

      #endregion

      // Constructors:
      #region Constructors

      public ICustomItemGrabMenuItem(ICustomItemGrabMenu hostMenu, Rectangle bounds, Boolean raiseMouseClickEventOnRelease, IActionColours actionColours, StardewValley.Item svItem) : base(null, bounds, raiseMouseClickEventOnRelease, actionColours, svItem) {
         this.HostMenu = hostMenu;
      }

      public ICustomItemGrabMenuItem(ICustomItemGrabMenu hostMenu, Rectangle bounds, Boolean raiseMouseClickEventOnRelease, IActionColours actionColours, String svItemName, String svItemLabel) : base(null, bounds, raiseMouseClickEventOnRelease, actionColours, svItemName, svItemLabel) {
         this.HostMenu = hostMenu;
      }

      public ICustomItemGrabMenuItem(ICustomItemGrabMenu hostMenu, Rectangle bounds, Boolean raiseMouseClickEventOnRelease, IActionColours actionColours) : this(hostMenu, bounds, raiseMouseClickEventOnRelease, actionColours, String.Empty, String.Empty) { }

      public ICustomItemGrabMenuItem(ICustomItemGrabMenu hostMenu, Rectangle bounds, Boolean raiseMouseClickEventOnRelease) : this(hostMenu, bounds, raiseMouseClickEventOnRelease, IActionColours.Default) { }

      public ICustomItemGrabMenuItem(ICustomItemGrabMenu hostMenu, Rectangle bounds) : this(hostMenu, bounds, true) { }

      #endregion
   }
}
