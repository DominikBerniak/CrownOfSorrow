using UnityEditor;

namespace DungeonCrawl.Actors.Characters
{
    public class Armor : Item
    {
        private string Name { get; set; }
        
        public Armor(string name)
        {
            this.Name = name;
        }
        
        public override void UseItem(Player player, int armor)
        {
            player.Armor += armor;
        }
    }
}