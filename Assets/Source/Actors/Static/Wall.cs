using System.Collections.Generic;

namespace DungeonCrawl.Actors.Static
{
    public class Wall : Actor
    {
        public override int DefaultSpriteId { get; set; } = 825;

        public override string DefaultName => "Wall";

        public Dictionary<string, int> SpriteVariants = new Dictionary<string, int>()
        {
            {"wall", 825}, {"water", 22}
        };

        public override void SetSprite(Dictionary<string, int> variants, string key)
        {
            base.SetSprite(SpriteVariants, key);
        }
    }
}
