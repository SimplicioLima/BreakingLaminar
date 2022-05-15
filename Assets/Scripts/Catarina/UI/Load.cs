using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class Load : MonoBehaviour
{
    public static Load instance;

    [SerializeField]
    private GameObject loadingBarHolder;

    private float progress_Value = 0f;
    private float progress_Multiplier_1 = 0.5f;
    private float progress_Multiplier_2 = 0.07f;
    public float loadLevelTime = 6f;
    public int sceneIndex;
    void Awake()
    {
        MakeSingleton();
    }

    void Start()
    {
        StartCoroutine(LoadSomeLevel(sceneIndex));
    }

    void Update()
    {
        ShowLoadingScreen();
        Debug.Log(progress_Value);
    }

    private void MakeSingleton()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else{
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void LoadLevel(int sceneIndex)
    {
        // activate loading screen
        loadingBarHolder.SetActive(true);

        progress_Value = 0f;

        // pára o tempo de jogo
        Time.timeScale = 0f;

    }

    public void ShowLoadingScreen()
    {
        if (progress_Value < 1f)
        {
            progress_Value += progress_Multiplier_1 * progress_Multiplier_2;
            

            if (progress_Value >= 1f)
            {
                progress_Value = 1.1f;

                loadingBarHolder.SetActive(false);

                Time.timeScale = 1f;
            }
        }
    }

    public IEnumerator LoadSomeLevel(int sceneIndex)
    {
        yield return new WaitForSeconds(loadLevelTime);

        LoadLevel(sceneIndex);
    }

   
}
