using System.Collections.Generic;
using DungeonCrawl.Actors.Characters;
using DungeonCrawl.Core;
using UnityEditor;

namespace DungeonCrawl.Actors.Static
{
    public class ClosedDoor : Actor
    {
        public override int DefaultSpriteId { get; set; }
        public override string DefaultName => "Door";

        public Dictionary<string, int> SpriteVariants = new Dictionary<string, int>()
        {
            {"blueDoor", 431} , {"redDoor", 532}, {"stoneObstacle", 640}, {"openedBlueDoor", 433}, {"openedRedDoor", 548}
        };
        
        public override void SetSprite(Dictionary<string, int> variants, string key)
        {
            base.SetSprite(SpriteVariants, key);
        }

        public override bool OnCollision(Actor anotherActor)
        {
            if (anotherActor is Player)
            {
                foreach (Item element in ((Player) anotherActor).Equipment.Items)
                {
                    if (element is FunctionalItem && ItemId == ((FunctionalItem) element).ItemId)
                    {
                        switch (ItemId)
                        {
                            case 1:
                                SetSprite(SpriteVariants, "openedBlueDoor");
                               // ((Player) anotherActor).Equipment.RemoveItem(element);
                                Detectable = false;
                                return true;
                            case 2:
                                SetSprite(SpriteVariants, "openedRedDoor");
                              // ((Player) anotherActor).Equipment.RemoveItem(element);
                                Detectable = false;
                                return true;
                            case 3:
                                ActorManager.Singleton.DestroyActor(this);
                               // ((Player) anotherActor).Equipment.RemoveItem(element);
                                break;
                        }
                    }
                }
            }
            return false;
        }
        
        public override bool Detectable => true;
    }
}