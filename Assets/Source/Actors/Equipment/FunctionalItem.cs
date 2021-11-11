using System.Collections.Generic;

namespace DungeonCrawl.Actors.Characters
{
    public class FunctionalItem : Item
    {
        private List<string> Names = new List<string>()
        {
            "dupa", "dupa2", "dupa3"
        };
        
        public override void UseItem()
        {
        }
        
        public override int DefaultSpriteId => 110;
        public override string DefaultName => "Door";
        
    }
}