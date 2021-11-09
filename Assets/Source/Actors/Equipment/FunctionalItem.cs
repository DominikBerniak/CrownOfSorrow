using System.Collections.Generic;

namespace DungeonCrawl.Actors.Characters
{
    public class FunctionalItem : Item
    {
        private List<string> Names = new List<string>()
        {
            "dupa", "dupa2", "dupa3"
        };
        
        public override void UseItem(Player player, int dupa)
        {
        }
        
        public override int DefaultSpriteId => 110;
        public override string DefaultName => "Door";
        
        public override void SetName()
        {
            Name = Names[Utilities.Random.Next(Names.Count)];
        }
    }
}