using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Button StartGameButton;

    public Button QuitGameButton;

    public GameObject PlayerNameUi;
    public void StartGame()
    {
        StartGameButton.gameObject.SetActive(false);
        QuitGameButton.gameObject.SetActive(false);
        PlayerNameUi.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PlayGame()
    {
        string playerName = GameObject.Find("UserNameInput").GetComponent<TextMeshProUGUI>().text;
        PlayerPrefs.SetString("playerName", playerName);
        SceneManager.LoadScene(1);
    }
}
