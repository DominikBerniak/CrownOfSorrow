using System.Collections.Generic;
using System.Linq;
using DungeonCrawl.Actors;
using DungeonCrawl.Actors.Characters;
using DungeonCrawl.Actors.Static;
using UnityEngine;
using UnityEngine.U2D;

namespace DungeonCrawl.Core
{
    /// <summary>
    ///     Main class for Actor management - spawning, destroying, detecting at positions, etc
    /// </summary>
    public class ActorManager : MonoBehaviour
    {
        /// <summary>
        ///     ActorManager singleton
        /// </summary>
        public static ActorManager Singleton { get; private set; }
        
        public string PlayerName { get; private set; }

        private SpriteAtlas _spriteAtlas;
        private HashSet<Actor> _allActors;

        private void Awake()
        {
            if (Singleton != null)
            {
                Destroy(this);
                return;
            }

            Singleton = this;

            _allActors = new HashSet<Actor>();
            _spriteAtlas = Resources.Load<SpriteAtlas>("Spritesheet");
            PlayerName = PlayerPrefs.GetString("playerName");
        }

        /// <summary>
        ///     Returns actor present at given position (returns null if no actor is present)
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public Actor GetActorAt((int x, int y) position)
        {
            return _allActors.FirstOrDefault(actor => actor.Detectable && actor.Position == position);
        }

        /// <summary>
        ///     Returns actor of specific subclass present at given position (returns null if no actor is present)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="position"></param>
        /// <returns></returns>
        public T GetActorAt<T>((int x, int y) position) where T : Actor
        {
            return _allActors.FirstOrDefault(actor => actor.Detectable && actor is T && actor.Position == position) as T;
        }

        /// <summary>
        ///     Unregisters given actor (use when killing/destroying)
        /// </summary>
        /// <param name="actor"></param>
        public void DestroyActor(Actor actor)
        {
            _allActors.Remove(actor);
            Destroy(actor.gameObject);
        }

        /// <summary>
        ///     Used for cleaning up the scene before loading a new map
        /// </summary>
        public void DestroyAllDestructibleActors()
        {
            var actors = _allActors.ToArray();

            foreach (var actor in actors)
            {
                if (actor.IsDestructible)
                {
                    DestroyActor(actor);
                }
            }
        }

        /// <summary>
        ///     Returns sprite with given ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Sprite GetSprite(int id)
        {
            return _spriteAtlas.GetSprite($"kenney_transparent_{id}");
        }

        /// <summary>
        ///     Spawns given Actor type at given position
        /// </summary>
        /// <typeparam name="T">Actor type</typeparam>
        /// <param name="position">Position</param>
        /// <param name="actorName">Actor's name (optional)</param>
        /// <returns></returns>
        public T Spawn<T>((int x, int y) position, string actorName = null) where T : Actor
        {
            return Spawn<T>(position.x, position.y, actorName);
        }

        /// <summary>
        ///     Spawns given Actor type at given position
        /// </summary>
        /// <typeparam name="T">Actor type</typeparam>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        /// <param name="actorName">Actor's name (optional)</param>
        /// <returns></returns>
        public T Spawn<T>(int x, int y, string actorName = null) where T : Actor
        {
            var go = new GameObject();
            go.AddComponent<SpriteRenderer>();

            var component = go.AddComponent<T>();

            go.name = actorName ?? component.DefaultName;
            component.Position = (x, y);
            _allActors.Add(component);
            
            return component;
        }
        
        public Wall Spawn<T>(int x, int y, string variantName, string actorName = null) where T : Wall
        {
            var go = new GameObject();
            go.AddComponent<SpriteRenderer>();
            var component = go.AddComponent<T>();
            component.SetSprite(component.SpriteVariants, variantName);
            go.name = actorName ?? component.DefaultName;
            component.Position = (x, y);
            _allActors.Add(component);
            return component;
        }
        
        public T Spawn<T>(int x, int y, string variantName, string actorName = null, int number = 0) where T : Floor
        {
            var go = new GameObject();
            go.AddComponent<SpriteRenderer>();

            var component = go.AddComponent<T>();
            component.SetSprite(component.SpriteVariants, variantName);
            go.name = actorName ?? component.DefaultName;
            component.Position = (x, y);
            _allActors.Add(component);
            
            return component;
        }
        
        public T Spawn<T>(int x, int y, string variantName, string actorName = null, int number = 0, int secondNumber = 0) where T : FunctionalItem
        {
            var go = new GameObject();
            go.AddComponent<SpriteRenderer>();
            var component = go.AddComponent<T>();
            component.SetSprite(component.SpriteVariants, variantName);
            var key = (FunctionalItem) component;
            key.StatName = "Key";
            switch (variantName)
            {
                case "blueKey":
                    component.ItemId = 1;
                    key.Name = "Blue Key";
                    key.StatPower = 1;
                    break;
                case "redKey":
                    component.ItemId = 2;
                    key.Name = "Red Key";
                    key.StatPower = 2;
                    break;
                case "axe":
                    component.ItemId = 3;
                    key.Name = "Pickaxe";
                    key.StatPower = 3;
                    break;
            }
            go.name = actorName ?? component.DefaultName;
            component.Position = (x, y);
            _allActors.Add(component);
            
            return component;
        }

        public T Spawn<T>(int x, int y, string variantName, string actorName = null, int number = 0, int secondNumber = 0, int thirdNumber = 0) where T : ClosedDoor
        {
            var go = new GameObject();
            go.AddComponent<SpriteRenderer>();
            var component = go.AddComponent<T>();
            component.SetSprite(component.SpriteVariants, variantName);
            switch (variantName)
            {
                case "blueDoor":
                    component.ItemId = 1;
                    break;
                case "redDoor":
                    component.ItemId = 2;
                    break;
                case "stoneObstacle":
                    component.ItemId = 3;
                    break;
            }
            go.name = actorName ?? component.DefaultName;
            component.Position = (x, y);
            _allActors.Add(component);
            
            return component;
        }
    }
}
