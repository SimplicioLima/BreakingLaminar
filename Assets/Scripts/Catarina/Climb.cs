using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
                Debug.Log("IM HERE");
                level1.SetActive(true);
                Debug.Log(player.transform.position);
                player.transform.position = pos1.position;
                Debug.Log(player.transform.position + " +  pos1" + pos1.position);
                level2.SetActive(false);
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
                Debug.Log(player.transform.position);
                player.transform.position = new Vector3(pos2.position.x, pos2.position.y, pos2.position.z);
                Debug.Log(player.transform + " +  pos1" + pos1);
                level1.SetActive(false);
                Debug.Log("IM HERE");
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
