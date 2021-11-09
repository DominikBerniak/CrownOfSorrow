using System.Collections.Generic;

namespace DungeonCrawl.Actors.Characters
{
    public class Armor : Item
    {
        private List<string> Names = new List<string>()
        {
            "dupa", "dupa2", "dupa3"
        };
        
        public override void UseItem(Player player, int armor)
        {
            //player.Armor += armor;
        }
        
        public override int DefaultSpriteId => 281;
        public override string DefaultName => "Armor";

        public override void SetName()
        {
            Name = Names[Utilities.Random.Next(Names.Count)];
        }
    }
}