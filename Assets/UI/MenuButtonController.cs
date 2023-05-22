using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonController : MonoBehaviour
{
    Data data;

    private void Start()
    {
        data = GameObject.FindGameObjectWithTag("GameData").GetComponent<Data>();
    }
    public void OnButtonPlay()
    {
        //string currentLevel = data.GetCurrentLevelName();//TODO read current level ID from save data
        SceneManager.LoadScene("GameScene");
    }

    public void OnButtonMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void OnButtonCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void OnButtonQuit()
    {
        Application.Quit();
    }
}
