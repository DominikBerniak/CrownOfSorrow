using DungeonCrawl.DAO;
using UnityEngine;

namespace DungeonCrawl.Core
{
    /// <summary>
    ///     Loads the initial map and can be used for keeping some important game variables
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        private void Start()
        {
            string gameStatus = PlayerPrefs.GetString("gameStatus");
            switch (gameStatus)
            {
                case "newGame":
                    MapLoader.LoadMap();
                    break;
                case "loadedGame":
                    SaveManager.LoadGame();
                    break;
            }
        }
    }
}
