using System;
using DungeonCrawl.Core;
using UnityEngine;

namespace DungeonCrawl.Actors.Characters
{
    public class Player : Character
    {
        private (int, int) _targetPosition;

        private bool _isMoving;

        private float _timeSinceLastMove;
        public Player()
        {
            Health = 100;
            AttackDmg = 5;
        }
        protected override void OnUpdate(float deltaTime)
        {
            // _timeSinceLastMove += deltaTime;
            // if (!_isMoving && Input.GetMouseButtonDown(0))
            // {
            //     _isMoving = true;
            //     var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //     var targetX = (int)Math.Round(mousePos.x);
            //     var targetY = (int)Math.Round(mousePos.y);
            //     _targetPosition = (targetX, targetY);
            // }
            //
            // if (_isMoving && _timeSinceLastMove > 0.2f)
            // {
            //     _timeSinceLastMove = 0;
            //     if (Position.x > _targetPosition.Item1)
            //     {
            //         TryMove(Direction.Left);
            //     }
            //
            //     else if (Position.x < _targetPosition.Item1)
            //     {
            //         TryMove(Direction.Right);
            //     }
            //
            //     else if (Position.y > _targetPosition.Item2)
            //     {
            //         TryMove(Direction.Down);
            //     }
            //     else if (Position.y < _targetPosition.Item2)
            //     {
            //         TryMove(Direction.Up);
            //     }
            //     else
            //     {
            //         _isMoving = false;
            //     }
            // }
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
            CameraController.Singleton.Position = Position;
        }

        public override bool OnCollision(Actor anotherActor)
        {
            ApplyDamage(1);
            return false;
        }

        protected override void OnDeath()
        {
            Debug.Log("Oh no, I'm dead!");
        }

        public override int DefaultSpriteId => 47;
        public override string DefaultName => "Player";
    }
}
