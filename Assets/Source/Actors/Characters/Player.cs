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


        public override int DefaultSpriteId { get; set; } = 26;
        public override string DefaultName => "Player";


        public Player()
        {
            Name = ActorManager.Singleton.PlayerName;
            Level.Number = 1;
            MaxHealth = 100;
            CurrentHealth = MaxHealth;
            _baseAttackDmg = Utilities.Random.Next(5, 11);
            _baseArmor = 0;
            IsDestroyable = false;
        }

        protected override void OnUpdate(float deltaTime)
        {
            if (Input.GetKeyDown(KeyCode.Escape) && !UserInterface.Singleton.IsFightScreenOn)
            {
                UserInterface.Singleton.TogglePauseMenu();
            }
            if (Input.GetKeyDown(KeyCode.I) && !UserInterface.Singleton.IsFightScreenOn && !UserInterface.Singleton.IsPauseMenuOn)
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
            UpdatePlayerSprite();
            UserInterface.Singleton.UpdatePlayerInfo(this);
        }

        public void UpdatePlayerStats()
        {
            AttackDmg = _baseAttackDmg + Equipment.GetEquippedWeaponPower();
            Armor = _baseArmor + Equipment.GetEquippedArmorPower();
        }

        private void UpdatePlayerSprite()
        {
            switch (GetEquippedItems())
            {
                case EquippedItems.None:
                    SetSprite(24);
                    break;
                case EquippedItems.Weapon:
                    SetSprite(26);
                    break;
                case EquippedItems.Armor:
                    SetSprite(29);
                    break;
                case EquippedItems.ArmorAndWeapon:
                    SetSprite(27);
                    break;
                case EquippedItems.ChristmasTree:
                    SetSprite(47);
                    break;
            }
        }

        public override bool OnCollision(Actor anotherActor)
        {
            UserInterface.Singleton.ShowFightScreen(this, (Character)anotherActor);
            return false;
        }

        protected override void OnDeath()
        {
        }

        private EquippedItems GetEquippedItems()
        {
            if (Equipment.IsChristmasTreeEquipped())
            {
                return EquippedItems.ChristmasTree;
            }
            if (Equipment.IsArmorEquipped() && Equipment.IsWeaponEquipped())
            {
                return EquippedItems.ArmorAndWeapon;
            }
            if (Equipment.IsArmorEquipped())
            {
                return EquippedItems.Armor;
            }
            if (Equipment.IsWeaponEquipped())
            {
                return EquippedItems.Weapon;
            }

            return EquippedItems.None;
        }
        enum EquippedItems
        {
            None,
            Armor,
            Weapon,
            ArmorAndWeapon,
            ChristmasTree
        }
    }
}