using Assets.Source.Core;
using UnityEngine;
using DungeonCrawl.Actors.Experience;
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
            Level.Number = 0;
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
                Level.IfLevelUp(this.Experience,player);
                Level.GuesWhoAndChangeLevel(player,this);
                this.Experience.SetExperiencePoints(this);              
                UserInterface.Singleton.ShowFightScreen(player, this);
                this.Experience.DropExperience(player,this);
                Level.IfLevelUp(this.Experience,player);
                return CurrentHealth <= 0;
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











