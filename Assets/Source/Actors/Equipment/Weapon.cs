using System.Collections.Generic;

namespace DungeonCrawl.Actors.Characters
{
    public class Weapon : Item
    {
        private List<string> Names = new List<string>()
        {
            "dupa", "dupa2", "dupa3"
        };
            
        public override void UseItem(Player player, int damage)
        {
            player.AttackDmg += damage;
        }

        public override int DefaultSpriteId => 281;
        public override string DefaultName => "Weapon";

        public override void SetName()
        {
            Name = Names[Utilities.Random.Next(Names.Count)];
        }
    }
}