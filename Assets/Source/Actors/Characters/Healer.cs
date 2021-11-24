using Assets.Source.Core;
using UnityEngine;
using DungeonCrawl.Core;
using DungeonCrawl.Actors.Experience;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace DungeonCrawl.Actors.Characters
{
    public class Healer : Character
    {
        private float DelayCounter { get; set; }

        public Healer()
        {
        }
        public override bool OnCollision(Actor anotherActor)
        {
            if (anotherActor is Player)
            {
                Player player = (Player)anotherActor;
                player.CurrentHealth = player.MaxHealth;
                UserInterface.Singleton.ShowFightScreen(player, this);
            }
            return false;
        }
        protected override void OnDeath() { }



        public override int DefaultSpriteId { get; set; } = 78;
        public override string DefaultName => "Healer";
    }
}











