using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class Menu : MonoBehaviour
{
    public GameObject startMenu;
    public GameObject gameMenu;

    private void Start()
    {
        /*
         * When game start:
        1. Set timescale to 0
        2. Show start menu UI
        3. Hide main game UI

         * ShowMenu():
        1. Set timescale to 0
        2. Show start menu UI (Set StartMenuUI.scene to active)
        3. Hide game UI (Set MainGameUI.scene to unactive)

         * StartMenu():
        1. Set timescale to 1
        2. Hide start menu UI (Set StartMenuUI.scene to unactive)
        3. Show game UI (Set MainGameUI.scene to active)
         */
        ShowMenu();
    }

    public void StartGame()
    {
        Time.timeScale = 1;
        startMenu.SetActive(false);
        gameMenu.SetActive(true);
    }

    public void ShowMenu()
    {
        Time.timeScale = 0;
        startMenu.SetActive(true);
        gameMenu.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
