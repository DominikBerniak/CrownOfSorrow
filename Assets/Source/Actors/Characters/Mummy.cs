using Assets.Source.Core;
using UnityEngine;
using System.Collections.Generic;

namespace DungeonCrawl.Actors.Characters
{
    public class Mummy : Character
    {
        private List<string> Names = new List<string>()
        {
            "Scary Spooky Mummy", "Crazy Wrapper", "Moooo", "Terrifying Mummy"
        };

        public Mummy()
        {
            Level.Number = 1;
            Experience.ExperiencePoints = 0;
            Name = Names[Utilities.Random.Next(Names.Count)];
            MaxHealth = Utilities.Random.Next(10, 51);
            CurrentHealth = MaxHealth;
            AttackDmg = Utilities.Random.Next(5, 16);
            Armor = Utilities.Random.Next(11);
        }
        public override bool OnCollision(Actor anotherActor)
        {
            if (anotherActor is Player player)
            {
                if (player.IsChristmasTreeEquipped())
                {
                    return true;
                }
                Level.GuesWhoAndChangeLevel(player,this);
                Experience.SetExperiencePoints(this);
                UpdateMonsterStats(player, this);
                UserInterface.Singleton.ShowFightScreen(player, this);
                return CurrentHealth <= 0;
            }
            return false;
        }

        protected override void OnDeath()
        {
            Experience.DropExperience(this);
            DropItem();
            Debug.Log("Wooooooo...");
        }

        public override int DefaultSpriteId { get; set; } = 317;
        public override string DefaultName => "Mummy";
    }
}











