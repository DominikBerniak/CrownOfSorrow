using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Core
{
    /// <summary>
    ///     Class for handling text on user interface (UI)
    /// </summary>
    public class UserInterface : MonoBehaviour
    {
        public enum TextPosition : byte
        {
            TopLeft,
            TopCenter,
            TopRight,
            MiddleLeft,
            MiddleCenter,
            MiddleRight,
            BottomLeft,
            BottomCenter,
            BottomRight
        }

        /// <summary>
        ///     User Interface singleton
        /// </summary>
        public static UserInterface Singleton { get; private set; }

        public GameObject fightScreen;
        
        private TextMeshProUGUI[] _textComponents;
        

        private void Awake()
        {
            if (Singleton != null)
            {
                Destroy(this);
                return;
            }
            
            Singleton = this;

            _textComponents = GetComponentsInChildren<TextMeshProUGUI>();
        }

        /// <summary>
        ///     Changes text at given screen position
        /// </summary>
        /// <param name="text"></param>
        /// <param name="textPosition"></param>
        public void SetText(string text, TextPosition textPosition)
        {
            _textComponents[(int) textPosition].text = text;
        }

        public void ShowFightScreen(string monsterName)
        {
            fightScreen.SetActive(true);
            var text = fightScreen.GetComponentInChildren<TextMeshProUGUI>().text = $"It's a {monsterName}!";
        }

        public void HideFightScreen()
        {
            fightScreen.SetActive(false);
            
        }
    }
}
