using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using StardewValley.Menus;

namespace ChestEx.Types.BaseTypes {
   public partial class ICustomMenuItem : ClickableComponent, IDisposable {
      // Public:
      #region Public

      public IActionColours ActionColours { get; set; }

      public Rectangle Bounds { get => this.bounds; set => this.bounds = value; }

      public List<BasicComponent> Components { get; protected set; }

      public ICustomMenu HostMenu { get; private set; }

      public Boolean IsVisible { get; protected set; }

      public Boolean RaiseMouseClickEventOnRelease { get; protected set; }

      // Virtuals:
      #region Virtuals

      /// <summary>
      /// Base implementation sets '<see cref="IsVisible"/>' to '<paramref name="isVisible"/>'.
      /// </summary>
      /// <param name="isVisible">Whether this item should be visible.</param>
      public virtual void SetVisible(Boolean isVisible) {
         this.IsVisible = isVisible;
      }

      /// <summary>
      /// Base implementation draws the dialogue box background if '<see cref="IsVisible"/>' is true and likewise with this item's components.
      /// </summary>
      public virtual void Draw(SpriteBatch b) {
         if (!this.IsVisible)
            return;

         this.Components.ForEach((c) =>
         {
            if (c.IsVisible)
               c.Draw(b);
         });
      }

      /// <summary>Base implementation informs this item's components.</summary>
      public virtual void OnGameWindowSizeChanged(Rectangle oldBounds, Rectangle newBounds) {
         this.Components.ForEach((c) => c.OnGameWindowSizeChanged(oldBounds, newBounds));
      }

      /// <summary>Base implementation does nothing.</summary>
      public virtual void OnMouseClick(ICustomMenu.MouseStateEx mouseState) { }

      /// <summary>Base implementation does nothing.</summary>
      public virtual void OnCursorMoved(StardewModdingAPI.Events.CursorMovedEventArgs e) { }

      /// <summary>Base implementation does nothing.</summary>
      public virtual void OnButtonPressed(StardewModdingAPI.Events.ButtonPressedEventArgs e) { }

      /// <summary>Base implementation does nothing.</summary>
      public virtual void OnButtonReleased(StardewModdingAPI.Events.ButtonReleasedEventArgs e) { }

      #endregion

      #endregion

      // Constructors:
      #region Constructors

      public ICustomMenuItem(ICustomMenu hostMenu, Rectangle bounds, Boolean raiseMouseClickEventOnRelease, IActionColours actionColours, StardewValley.Item svItem) : base(bounds, svItem) {
         this.ActionColours = actionColours;
         this.Components = new List<BasicComponent>();
         this.RaiseMouseClickEventOnRelease = raiseMouseClickEventOnRelease;
         this.HostMenu = hostMenu;
         this.SetVisible(true);
      }

      public ICustomMenuItem(ICustomMenu hostMenu, Rectangle bounds, Boolean raiseMouseClickEventOnRelease, IActionColours actionColours, String svItemName, String svItemLabel) : base(bounds, svItemName, svItemLabel) {
         this.ActionColours = actionColours;
         this.Components = new List<BasicComponent>();
         this.RaiseMouseClickEventOnRelease = raiseMouseClickEventOnRelease;
         this.HostMenu = hostMenu;
         this.SetVisible(true);
      }

      public ICustomMenuItem(ICustomMenu hostMenu, Rectangle bounds, Boolean raiseMouseClickEventOnRelease, IActionColours actionColours) : this(hostMenu, bounds, raiseMouseClickEventOnRelease, actionColours, String.Empty, String.Empty) { }

      public ICustomMenuItem(ICustomMenu hostMenu, Rectangle bounds, Boolean raiseMouseClickEventOnRelease) : this(hostMenu, bounds, raiseMouseClickEventOnRelease, IActionColours.Default) { }

      public ICustomMenuItem(ICustomMenu hostMenu, Rectangle bounds) : this(hostMenu, bounds, true) { }

      #endregion

      // IDisposable:
      #region IDisposable

      /// <summary>
      /// <para>Base implementation:</para>
      /// <para>1. Calls '<see cref="SetVisible(Boolean)"/>' with 'false'.</para>
      /// <para>2. Disposes of this item's components.</para>
      /// </summary>
      public virtual void Dispose() {
         // hide
         this.SetVisible(false);
         // dispose of components
         this.Components.ForEach((c) => c.Dispose());
      }

      #endregion
   }
}
