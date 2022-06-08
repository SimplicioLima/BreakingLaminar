using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.SceneManagement;
public class Climb : MonoBehaviour
{
    [SerializeField] private GameObject UiGoUp;
    [SerializeField] private GameObject UiGoDown;
    [HideInInspector] public GameObject loadingScreen;

    private bool activeUI;

    [Header("Can i? :")]
    public bool goUp = false;
    [SerializeField] private int UpperLevel = 1;
    public bool goDown = false;
    [SerializeField] private int LowerLevel = 0;

    public GameObject player;
    public Transform pos1;
    public Transform pos2;
    public Transform pos3;

    public GameObject level2;
    public GameObject level1;

    void Update()
    {

        if(MissionController.current.mission3_Cam)//Por a missao que se pretenda que ele possa sair do top level
        {
            player = GameObject.FindWithTag("Player");
            //loadingScreen = GameObject.FindWithTag("loadingScreen");
            //Go up
            if (goUp && Input.GetKeyDown(KeyCode.Alpha1))
            {
                Debug.Log("IM HERE 1");
                level1.SetActive(true);
                if (player.transform.position == pos1.position)
                    level2.SetActive(false);
                else
                {
                    GameManager.current.MovePlayer(pos1.position);
                    level2.SetActive(false);
                }
                //if (UpperLevel != SceneManager.sceneCountInBuildSettings)
                //{
                //    if (goUp) UiGoUp.SetActive(false);
                //    if (goDown) UiGoDown.SetActive(false);
                //    //showLoad();
                //    SceneManager.LoadScene(UpperLevel);
                //}
            }
            //Go down
            if (goDown && Input.GetKeyDown(KeyCode.Alpha2))
            {
                Debug.Log("IM HERE");
                level2.SetActive(true);
                if (player.transform.position == pos2.position)
                    level1.SetActive(false);
                else
                {
                    GameManager.current.MovePlayer(pos2.position);
                    level1.SetActive(false);
                }
                //if (UpperLevel != SceneManager.sceneCountInBuildSettings)
                //{
                //    if (goUp) UiGoUp.SetActive(false);
                //    if (goDown) UiGoDown.SetActive(false);
                //    //showLoad();
                //    SceneManager.LoadScene(LowerLevel);
                //}
            }
        }
    }

    public void showLoad()
    {
        loadingScreen.SetActive(true);
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
