using UnityEngine;

namespace DungeonCrawl.Core
{
    public class PauseControl : MonoBehaviour
    {
        public static PauseControl Singleton { get; private set; }

        public bool IsGamePaused
        {
            get => _isGamePaused;
            set => _isGamePaused = value;
        }

        private bool _isGamePaused;
        private void Awake()
        {
            if (Singleton != null)
            {
                Destroy(this);
                return;
            }
            
            Singleton = this;
        }

        public void PauseGame()
        {
            IsGamePaused = true;
        }

        public void ResumeGame()
        {
            IsGamePaused = false;
        }

    }
}