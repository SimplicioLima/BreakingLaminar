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

    public static bool camerasOn = true;
    private bool enterOnce = false;
    private bool enterOnce2 = false;
    public static bool AtivateCam = false;

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
        if (AtivateCam && !enterOnce2)
        {
            AtivateOnlyCams();
            enterOnce2 = true;
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

    private void AtivateOnlyCams()
    {
        foreach (var item in cctvs)
        {
            item.SetActive(true);
        }
    }

    public static bool ChangeValue(bool onOFF)
    {
        camerasOn = onOFF;
        return camerasOn;
    }
}
