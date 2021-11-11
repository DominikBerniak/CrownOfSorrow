using System.Collections.Generic;

namespace DungeonCrawl.Actors.Characters
{
    public class FunctionalItem : Item
    {
        public Dictionary<string, int> SpriteVariants = new Dictionary<string, int>()
        {
            {"axe", 282}, {"redKey", 561}, {"blueKey", 560}
        };
        
        public override void SetSprite(Dictionary<string, int> variants, string key)
        {
            base.SetSprite(SpriteVariants, key);
        }
        
        public override void UseItem()
        {
        }

        public override int DefaultSpriteId { get; set; }

        public override string DefaultName => "Key";
    }
}