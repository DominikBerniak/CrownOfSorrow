using System.Collections.Generic;

namespace DungeonCrawl.Actors.Static
{
    public class Floor : Actor
    {
        public override int DefaultSpriteId => 1;
        public override string DefaultName => "Floor";
        
        public Dictionary<string, int> SpriteVariants = new Dictionary<string, int>()
        {
            {"floor", 1}, {"water", 247}, {"roadTurn", 240}, {"horizontalRoad", 148},
            {"verticalRoad1", 239}, {"verticalRoad2", 243}, {"woodenBridge", 780}, {"gateway", 341}
        };
        
        public override void SetSprite(Dictionary<string, int> variants, string key)
        {
            base.SetSprite(SpriteVariants, key);
        }

        public override bool Detectable => false;
    }
}
