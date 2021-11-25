using System.Collections.Generic;

namespace DungeonCrawl.Actors.Static
{
    public class Floor : Actor
    {
        public override int DefaultSpriteId { get; set; } = 1;
        public override string DefaultName => "Floor";
        public override int Z => 2;
        
        public Dictionary<string, int> SpriteVariants = new Dictionary<string, int>()
        {
            {"floor", 1}, {"water", 247}, {"roadTurn", 240}, {"horizontalRoad", 148},
            {"verticalRoad1", 239}, {"verticalRoad2", 243}, {"woodenBridge", 780}, {"gateway", 341},{"grass",4},{"boat",441}
        };
        
        public override void SetSprite(Dictionary<string, int> variants, string key)
        {
            base.SetSprite(SpriteVariants, key);
        }

        public override bool Detectable => false;
    }
}
