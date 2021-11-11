using System.Collections.Generic;

namespace DungeonCrawl.Actors.Static
{
    public class Floor : Actor
    {
        public override int DefaultSpriteId => 1;
        public override string DefaultName => "Floor";
        
        public Dictionary<string, int> SpriteVariants = new Dictionary<string, int>()
        {
            {"wall", 825}, {"water", 22}, {"roadturn", 240}, {"roadhorizontal", 148},
            {"roadvertical", 229}
        };

        public override void SetSprite(Dictionary<string, int> variants, string key)
        {
            base.SetSprite(SpriteVariants, key);
        }

        public override bool Detectable => false;
    }
}
