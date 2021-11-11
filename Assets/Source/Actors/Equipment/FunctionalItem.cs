namespace DungeonCrawl.Actors.Characters
{
    public class FunctionalItem : Item
    {
        private int ItemId { get; set; }
        private string Name { get; set;  }
        
        public FunctionalItem(string Name, int itemId)
        {
            this.Name = Name;
            this.ItemId = itemId;
        }
        
        public override void UseItem(Player player, int dupa)
        {
        }
    }
}