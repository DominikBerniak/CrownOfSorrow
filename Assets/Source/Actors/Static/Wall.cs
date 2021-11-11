using System.Collections.Generic;

namespace DungeonCrawl.Actors.Static
{
    public class Wall : Actor
    {
        public override int DefaultSpriteId => 825;
        public override string DefaultName => "Wall";

        public Dictionary<string, int> SpriteVariants = new Dictionary<string, int>()
        {
            {"wall", 825}, {"water", 22}, {"trees", 50}, {"necklace", 428}, {"fence", 780}, {"tree1", 99}, {"roof1", 729},
            {"roof2", 730}, {"roof3", 731}, {"wallhouseleft", 777}, {"wallhouseright", 779}, {"tombstone", 672}, {"fireplace", 493}, 
            {"woodenobstacle", 288}, {"crown", 139},
            {"crossup", 600}, {"crossdown", 648}, {"door2", 340}, {"entrance", 1},
            
        };

        public override void SetSprite(Dictionary<string, int> variants, string key)
        {
            base.SetSprite(SpriteVariants, key);
        }
    }
}
