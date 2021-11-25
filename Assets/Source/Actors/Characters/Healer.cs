using Assets.Source.Core;
using UnityEngine;
using DungeonCrawl.Core;
using DungeonCrawl.Actors.Experience;
using System.Collections.Generic;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace DungeonCrawl.Actors.Characters
{
    public class Healer : Character
    {
        private readonly int _expNeededForHeal;
        private readonly int _hpHealed;
        public Healer()
        {
            Name = "Old Harold";
            _expNeededForHeal = 10;
            _hpHealed = 20;
        }
        public override bool OnCollision(Actor anotherActor)
        {
            if (anotherActor is Player player)
            {
                UserInterface.Singleton.ShowHealerUi(this, player, _expNeededForHeal, _hpHealed);
            }
            return false;
        }
        protected override void OnDeath() { }

        public void HealPlayer(Player player, Button healButton)
        {
            healButton.onClick.RemoveAllListeners();
            player.Experience.ExperiencePoints -= _expNeededForHeal;
            player.CurrentHealth += _hpHealed;
            UserInterface.Singleton.ShowHealerUi(this, player, _expNeededForHeal, _hpHealed, true);
        }


        public override int DefaultSpriteId { get; set; } = 78;
        public override string DefaultName => "Healer";
    }
}











