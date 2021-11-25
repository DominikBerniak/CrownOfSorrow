using System.IO;
using DungeonCrawl.Core;
using DungeonCrawl.DAO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Button StartGameButton;

    public Button LoadGameButton;
    
    public Button QuitGameButton;

    public GameObject PlayerNameUi;
    
    private GameObject _musicGameObject;
    private AudioSource _musicAudioSource;
    public void Awake()
    {
        _musicGameObject = new GameObject("BackgroundMusic");
        _musicAudioSource = _musicGameObject.AddComponent<AudioSource>();
        PlayMainMenuMusic();
        string loadFilePath = Directory.GetCurrentDirectory() + "/SavedGames/game_save.json";
        LoadGameButton.interactable = File.Exists(loadFilePath);
        LoadGameButton.GetComponentInChildren<TextMeshProUGUI>().enableVertexGradient = File.Exists(loadFilePath);
    }

    public void NewGame()
    {
        StartGameButton.gameObject.SetActive(false);
        QuitGameButton.gameObject.SetActive(false);
        LoadGameButton.gameObject.SetActive(false);
        PlayerNameUi.SetActive(true);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(1);
        PlayerPrefs.SetString("gameStatus", "loadedGame");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PlayGame()
    {
        string playerName = GameObject.Find("UserNameInput").GetComponent<TextMeshProUGUI>().text;
        PlayerPrefs.SetString("playerName", playerName);
        PlayerPrefs.SetString("gameStatus", "newGame");
        SceneManager.LoadScene(1);
    }

    public void GoBack()
    {
        StartGameButton.gameObject.SetActive(true);
        QuitGameButton.gameObject.SetActive(true);
        LoadGameButton.gameObject.SetActive(true);
        PlayerNameUi.SetActive(false);
    }
    
    public void PlayMainMenuMusic()
    {
        _musicAudioSource.Stop();
        _musicAudioSource.clip = Resources.Load<AudioClip>("AudioClips/backgroundMusic/main_menu");
        _musicAudioSource.volume = 0.06f;
        _musicAudioSource.Play();
    }
}
