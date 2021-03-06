using System;
using System.Runtime.InteropServices;
using DungeonCrawl.Core;

namespace DungeonCrawl.Actors.Characters
{
    public class Item : Actor
    {
        public string Name { get; set; }
        public string StatName { get; set; }
        public int StatPower { get; set; }
        public virtual void UseItem(){}

        public Character Owner { get; set; }
        public bool IsEquipped;

        public override bool OnCollision(Actor anotherActor)
        {
            if (anotherActor is Player player)
            {
                if (player.Equipment.Items.Count == 24)
                {
                    return false;
                }
                AudioManager.Singleton.PlayItemPickedUpSound();
                player.Equipment.AddItem(this);
                Owner = player;
                IsDestructible = false;
                SetSpriteVisible(false);
                Detectable = false;
                return true;
            }

            return false;
        }
        public override string DefaultName { get; }

        public override int Z => -1;
        public override int DefaultSpriteId { get; set; }
    }
}