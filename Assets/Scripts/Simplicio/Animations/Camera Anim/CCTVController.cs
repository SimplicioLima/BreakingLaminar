using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CCTVController : MonoBehaviour
{
    [SerializeField] private bool inDebug = true;

    [SerializeField] private List<GameObject> cctvs = new List<GameObject>(); //objeto cameras
    [SerializeField] private List<Animator> animCCTVs = new List<Animator>(); //objeto com anim
    [SerializeField] private List<MeshRenderer> screens = new List<MeshRenderer>(); //screens
    [SerializeField] private Material noSignal;

    float currCountdownValue;

    public static bool camerasOn = true;
    private bool enterOnce = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!camerasOn && !enterOnce)
        {
            ShutDownCameras();
            DeativateCam();
            enterOnce = true;
        }
    }

    private async void ShutDownCameras()
    {
        foreach (var item in screens)
        {
            //Desativa a camera
            item.material = noSignal;
            await Task.Delay(500);
        }
    }


    private void DeativateCam()
    {
        //Desativa o animator
        foreach (var item in animCCTVs)
        {
            item.GetComponent<Animator>().enabled = false;
        }
        //Desativar cameras **Less lagg i tink**
        foreach (var item in cctvs)
        {
            item.SetActive(false);
        }
    }

    public static bool ChangeValue(bool onOFF)
    {
        camerasOn = onOFF;
        return camerasOn;
    }

    public IEnumerator StartCountdown(float countdownValue = 1.5f)
    {
        currCountdownValue = countdownValue;
        while (currCountdownValue > 0)
        {
            Debug.Log("Countdown: " + currCountdownValue);
            yield return new WaitForSeconds(0.5f);
            currCountdownValue--;
        }
    }
}
