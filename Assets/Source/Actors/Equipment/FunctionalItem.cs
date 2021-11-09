namespace DungeonCrawl.Actors.Characters
{
    public class FunctionalItem : Item
    {
        private string Name { get; set; }
        
        public FunctionalItem(string name)
        {
            this.Name = name;
        }
        
        public override void UseItem(Player player, int dupa)
        {
        }
    }
}