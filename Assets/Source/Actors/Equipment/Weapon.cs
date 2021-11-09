namespace DungeonCrawl.Actors.Characters
{
    public class Weapon : Item
    {
        private string Name { get; set; }
        
        public Weapon(string name)
        {
            this.Name = name;
        }
        
        public override void UseItem(Player player, int damage)
        {
            player.AttackDmg += damage;
        }
    }
}