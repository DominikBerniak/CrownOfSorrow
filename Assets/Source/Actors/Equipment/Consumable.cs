namespace DungeonCrawl.Actors.Characters
{
    public class Consumable: Item
    {
        private string Name { get; set; }
        
        public Consumable(string name)
        {
            this.Name = name;
            
        }

        public override void UseItem(Player player, int health)
        {
            player.Health += health;
        }
    }
}