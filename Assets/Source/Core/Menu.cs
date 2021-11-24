using DungeonCrawl.Core;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Button StartGameButton;

    public Button QuitGameButton;

    public GameObject PlayerNameUi;
    
    private GameObject _musicGameObject;
    private AudioSource _musicAudioSource;
    public void Awake()
    {
        _musicGameObject = new GameObject("BackgroundMusic");
        _musicAudioSource = _musicGameObject.AddComponent<AudioSource>();
        PlayMainMenuMusic();
    }

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
    
    public void PlayMainMenuMusic()
    {
        _musicAudioSource.Stop();
        _musicAudioSource.clip = Resources.Load<AudioClip>("AudioClips/backgroundMusic/main_menu");
        _musicAudioSource.volume = 0.06f;
        _musicAudioSource.Play();
    }
}
