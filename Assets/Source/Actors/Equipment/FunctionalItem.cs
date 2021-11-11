using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEditor.U2D;

namespace DungeonCrawl.Actors.Characters
{
    public class FunctionalItem : Item
    {
        public Dictionary<string, int> SpriteVariants = new Dictionary<string, int>()
        {
            {"axe", 282}, {"redKey", 561}, {"blueKey", 560}
        };
        
        public FunctionalItem()
        {
            StatPower = ItemId;
        }

        public override void SetSprite(Dictionary<string, int> variants, string key)
        {
            base.SetSprite(SpriteVariants, key);
        }
        
        public override void UseItem()
        {
        }
        
        public override int DefaultSpriteId => 559;
        public override string DefaultName => "Key";
        
    }
}