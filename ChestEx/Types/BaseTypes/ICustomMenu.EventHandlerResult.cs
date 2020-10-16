namespace ChestEx.Types.BaseTypes {
   public partial class ICustomMenu {
      public enum InformStatus {
         /// <summary>
         /// Let this menu inform its items.
         /// </summary>
         InformItems,
         /// <summary>
         /// Block this menu from informing its items.
         /// </summary>
         DontInformItems
      }
   }
}
