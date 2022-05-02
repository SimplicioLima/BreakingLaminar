using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Climb : MonoBehaviour
{
    [SerializeField] private GameObject UiGoUp;
    [SerializeField] private GameObject UiGoDown;

    private bool activeUI;

    [Header("Can i? :")]
    public bool goUp = false;
    [SerializeField] private int UpperLevel = 0;
    public bool goDown = false;
    [SerializeField] private int LowerLevel = 0;

    // Start is called before the first frame update
    void Start()
    {
        activeUI = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(activeUI)
        {
            //Go up
            if (goUp && Input.GetKeyDown(KeyCode.Alpha1))
            {
                if (UpperLevel != SceneManager.sceneCountInBuildSettings)
                {
                    if (goUp) UiGoUp.SetActive(false);
                    if (goDown) UiGoDown.SetActive(false);
                    SceneManager.LoadScene(UpperLevel);
                }
            }
            //Go down
            if (goDown &&Input.GetKeyDown(KeyCode.Alpha2))
            {
                if (UpperLevel != SceneManager.sceneCountInBuildSettings)
                {
                    if (goUp) UiGoUp.SetActive(false);
                    if (goDown) UiGoDown.SetActive(false);
                    SceneManager.LoadScene(LowerLevel);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.transform.tag == "Player")
        {
            activeUI = true;
            if (goUp) UiGoUp.SetActive(true);
            if (goDown) UiGoDown.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Player")
        {
            activeUI = false;
            if (goUp) UiGoUp.SetActive(false);
            if (goDown) UiGoDown.SetActive(false);
        }
    }
}