using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] Button buttonStartGame;
    [SerializeField] Button buttonQuit;

    private void Start()
    {
        buttonStartGame.onClick.AddListener(StartGame);
    }

    void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    void Settings()
    {

    }

    void OpenMenu()
    {

    }

    void Quit()
    {
        Application.Quit();
    }
}
