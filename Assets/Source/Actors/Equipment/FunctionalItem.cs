using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEditor.U2D;

namespace DungeonCrawl.Actors.Characters
{
    public class FunctionalItem : Item
    {
        /*private List<string> Names = new List<string>()
        {
            "axe", "nextLevelKey", "nextStageKey"
        };*/
        
        public Dictionary<string, int> SpriteVariants = new Dictionary<string, int>()
        {
            {"axe", 282}, {"redKey", 561}, {"blueKey", 560}
        };
        
        private List<string> Names = new List<string>()
        {
            "Axe", "RedKey", "BlueKey"
        };
        
        public Dictionary<int, int> KeyesId = new Dictionary<int, int>()
        {
            {282, 1}, {561, 2}, {560, 3}
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