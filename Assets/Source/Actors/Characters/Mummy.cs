using Assets.Source.Core;
using UnityEngine;
using DungeonCrawl.Core;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace DungeonCrawl.Actors.Characters
{
    public class Mummy : Character
    {
        private float DelayCounter { get; set; }

        private List<string> Names = new List<string>()
        {
            "Scary Spooky Mummy", "Crazy wrapper", "Muuuuuu", "Terrifying Mummy"
        };

        public Mummy()
        {
            Level.Number = 1;
            Name = Names[Utilities.Random.Next(Names.Count)];
            MaxHealth = Utilities.Random.Next(10, 51);
            CurrentHealth = MaxHealth;
            AttackDmg = Utilities.Random.Next(5, 16);
            Armor = Utilities.Random.Next(11);
        }
        public override bool OnCollision(Actor anotherActor)
        {
            if (anotherActor is Player)
            {
                Player player = (Player)anotherActor;
                if (player.IsChristmasTreeEquipped())
                {
                    return true;
                }
                UserInterface.Singleton.ShowFightScreen(player, this);
            }
            return false;
        }

        protected override void OnDeath()
        {
            DropItem();
            Debug.Log("Wooooooo...");
        }

        public override int DefaultSpriteId { get; set; } = 71;
        public override string DefaultName => "Mummy";
    }
}











