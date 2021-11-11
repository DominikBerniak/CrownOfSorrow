using System.Collections.Generic;

namespace DungeonCrawl.Actors.Characters
{
    public class FunctionalItem : Item
    {
        private List<string> Names = new List<string>()
        {
            "dupa", "dupa2", "dupa3"
        };

        public FunctionalItem()
        {
        }

        public override int DefaultSpriteId { get; set; } = 110;

        public override string DefaultName => "Door";
        
    }
}