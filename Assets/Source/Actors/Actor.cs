using System.Collections.Generic;
using DungeonCrawl.Core;
using UnityEngine;

namespace DungeonCrawl.Actors
{
    public abstract class Actor : MonoBehaviour
    {
        public (int x, int y) Position
        {
            get => _position;
            set
            {
                _position = value;
                transform.position = new Vector3(value.x, value.y, Z);
            }
        }
        
        private (int x, int y) _position;
        private SpriteRenderer _spriteRenderer;
        public bool IsDestructible { get; set; } = true;
        
        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();

            SetSprite(DefaultSpriteId);
            gameObject.layer = 6;
        }

        private void Update()
        {
            OnUpdate(Time.deltaTime);
        }

        public Sprite GetSprite()
        {
            return _spriteRenderer.sprite;
        }
        public void SetSpriteVisible(bool isVisible)
        {
            _spriteRenderer.enabled = isVisible;
        }

        public virtual void SetSprite(int id)
        {
            _spriteRenderer.sprite = ActorManager.Singleton.GetSprite(id);
        }
        
        public virtual void SetSprite(Dictionary<string, int> variants, string key)
        {
            _spriteRenderer.sprite = ActorManager.Singleton.GetSprite(variants[key]);
        }


        public void TryMove(Direction direction)
        {
            var vector = direction.ToVector();
            (int x, int y) targetPosition = (Position.x + vector.x, Position.y + vector.y);

            var actorAtTargetPosition = ActorManager.Singleton.GetActorAt(targetPosition);

            if (actorAtTargetPosition == null)
            {
                // No obstacle found, just move
                Position = targetPosition;
            }
            else if (actorAtTargetPosition.OnCollision(this))
            {
                // Allowed to move
                Position = targetPosition;
            }
        }

        /// <summary>
        ///     Invoked whenever another actor attempts to walk on the same position
        ///     this actor is placed.
        /// </summary>
        /// <param name="anotherActor"></param>
        /// <returns>true if actor can walk on this position, false if not</returns>
        public virtual bool OnCollision(Actor anotherActor)
        {
            // All actors are passable by default
            return false;
        }

        /// <summary>
        ///     Invoked every animation frame, can be used for movement, character logic, etc
        /// </summary>
        /// <param name="deltaTime">Time (in seconds) since the last animation frame</param>
        protected virtual void OnUpdate(float deltaTime)
        {
        }

        /// <summary>
        ///     Can this actor be detected with ActorManager.GetActorAt()? Should be false for purely cosmetic actors
        /// </summary>
        public virtual bool Detectable { get; set; } = true;

        /// <summary>
        ///     Z position of this Actor (0 by default)
        /// </summary>
        public virtual int Z => 0;

        /// <summary>
        ///     Id of the default sprite of this actor type
        /// </summary>
        public abstract int DefaultSpriteId { get; set; }

        /// <summary>
        ///     Default name assigned to this actor type
        /// </summary>
        public abstract string DefaultName { get; }
        
        public int ItemId { get; set; }
    }
}