using System;

using ChestEx.Types.BaseTypes;

using Microsoft.Xna.Framework;

namespace ChestEx.Types.CustomTypes.ChestExMenu.Items {
   public partial class ConfigPanel : BaseTypes.ICustomItemGrabMenuItem {
      // Private:
      #region Private

      // Component event handlers:
      #region Component event handlers

      private void _componentOnClick(Object sender, ICustomMenu.MouseStateEx mouseState) {
         switch ((sender as BaseTypes.ICustomItemGrabMenuItem.BasicComponent).Name) {
            case "configBTN":
               GlobalVars.SMAPIMonitor.Log("configbtn");
               break;
         }
      }

      #endregion

      #endregion

      // Constructors:
      #region Constructors

      public ConfigPanel(MainMenu hostMenu) : base(hostMenu, GlobalVars.GameViewport, true, BaseTypes.IActionColours.Default) {
      }

      #endregion

      // IDisposable:
      #region IDisposable

      public override void Dispose() {
         base.Dispose();
      }

      #endregion
   }
}
