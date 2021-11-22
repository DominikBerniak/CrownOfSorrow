using System;
using Assets.Source.Core;
using DungeonCrawl.Actors.Experience;
using DungeonCrawl.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DungeonCrawl.Actors.Characters
{
    public class Player : Character
    {
        public (int, int) _targetPosition;

        private bool _isMoving;

        private float _timeSinceLastMove;

        private int _baseArmor;

        private int _baseAttackDmg;
        
        public Player()
        {
            Name = ActorManager.Singleton.PlayerName;
            Level.Number = 1;
            MaxHealth = 100;
            CurrentHealth = MaxHealth;
            _baseAttackDmg = Utilities.Random.Next(5,11);
            _baseArmor = 0;
            IsDestroyable = false;
        }

        protected override void OnUpdate(float deltaTime)
        {
            if (Input.GetKeyDown(KeyCode.I) && !UserInterface.Singleton.IsFightScreenOn)
            {
                // Show / hide equipment
                if (Equipment.IsEquipmentOnScreen)
                {
                    Equipment.HideEquipment();    
                }
                else
                {
                    Equipment.ShowEquipment();    
                }
            }
            if (PauseControl.Singleton.IsGamePaused)
            {
                return;
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                // Move up
                TryMove(Direction.Up);
            }
            
            if (Input.GetKeyDown(KeyCode.S))
            {
                // Move down
                TryMove(Direction.Down);
            }
            
            if (Input.GetKeyDown(KeyCode.A))
            {
                // Move left
                TryMove(Direction.Left);
            }
            
            if (Input.GetKeyDown(KeyCode.D))
            {
                // Move right
                TryMove(Direction.Right);
            }

            if (Input.GetKeyDown(KeyCode.Home))
            {
                CurrentHealth = MaxHealth;
                _baseAttackDmg = 100;
            }
            
            CameraController.Singleton.Position = Position;
            UpdatePlayerStats();
            UserInterface.Singleton.UpdatePlayerInfo(this);
        }

        public void UpdatePlayerStats()
        {
            AttackDmg = _baseAttackDmg + (Equipment.EquippedWeapon != null ? Equipment.EquippedWeapon.StatPower : 0);
            Armor = _baseArmor + (Equipment.EquippedArmor != null ? Equipment.EquippedArmor.StatPower : 0);
        }

        public override bool OnCollision(Actor anotherActor)
        {
            UserInterface.Singleton.ShowFightScreen(this, (Character)anotherActor);
            return CurrentHealth <= 0;
        }

        protected override void OnDeath()
        {
        }

        public override int DefaultSpriteId { get; set; } = 26;
        public override string DefaultName => "Player";

    }
}
