using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu_UI : MonoBehaviour
{
    public GameObject menu;
    public GameObject loadingScreen;
    public GameObject video;

    List<AsyncOperation> scenesToLoad = new List<AsyncOperation>();
    Load loadGame = new Load();
    
    public void NewGame()
    {
        loadingScreen.gameObject.SetActive(true);
        SceneManager.LoadScene(1);
    }

    public void Credits()
    {
        SceneManager.LoadScene(4);
    }

    public void ExitBtnClick()
    {
        Application.Quit();
    }

    public void LoadLevel()
    {
        HideMenu();
        showLoad();
        scenesToLoad.Add(SceneManager.LoadSceneAsync(1));
        StartCoroutine(loadGame.LoadSomeLevel(1));
    }

    public void HideMenu()
    {
        menu.SetActive(false);
        video.SetActive(false);
    }

    public void showLoad()
    {
        loadingScreen.SetActive(true);
    }
}
