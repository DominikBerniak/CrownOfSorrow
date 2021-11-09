using System.Collections.Generic;
using DungeonCrawl.Actors.Characters;
using UnityEngine;
using UnityEngine.U2D;

namespace Source.Core
{
    public class EquipmentManager : MonoBehaviour
    {
        public static EquipmentManager Singleton { get; private set; }

        private List<Item> _items;
        private SpriteAtlas _spriteAtlas;
        private void Awake()
        {
            if (Singleton != null)
            {
                Destroy(this);
                return;
            }

            Singleton = this;
            _spriteAtlas = Resources.Load<SpriteAtlas>("Spritesheet");
        }

        public void SpawnItems(List<Item> items)
        {
            foreach (var item in items)
            {
                var go = new GameObject();
                go.name = item.Name;
                var spriteRenderer = go.AddComponent<SpriteRenderer>();
                spriteRenderer.sprite = GetCorrectSprite(item);
                var component = go.AddComponent<Item>();
                _items.Add(component);
            }
        }

        private Sprite GetCorrectSprite(Item item)
        {
            var id = 323;
            return _spriteAtlas.GetSprite($"kenney_transparent_{id}");
        }
        
    }
}