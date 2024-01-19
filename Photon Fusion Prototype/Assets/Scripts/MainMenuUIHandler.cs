using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUIHandler : MonoBehaviour
{
    [SerializeField] private TMP_InputField nickField;
    [SerializeField] private TMP_InputField roomField;
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject loadingPanel;
    private void Start()
    {
        if (PlayerPrefs.HasKey("PlayerNickname"))
        {
            nickField.text = PlayerPrefs.GetString("PlayerNickname");
        }
    }

    public void OnJoinClicked()
    {
        LoadScene();
    }

    private void LoadScene()
    {
        PlayerPrefs.SetString("PlayerNickname", nickField.text);
        PlayerPrefs.Save();

        var operation = SceneManager.LoadSceneAsync("GameScene", LoadSceneMode.Additive);
        operation.completed += (s) =>
        {
            SceneManager.UnloadSceneAsync("MainMenu");
            Singleton<GameHandler>.instance.CreateRoom(roomField.text);
        };
        mainMenuPanel.SetActive(false);
        loadingPanel.SetActive(true);
    }

}
