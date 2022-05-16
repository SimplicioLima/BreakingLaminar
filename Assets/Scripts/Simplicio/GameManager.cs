using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //Debuging
    [SerializeField] private bool inDebug = true;
    public static GameManager current { get; private set; }

    //Camera mission;
    [HideInInspector] public bool cctvOff = false;
    [HideInInspector] public bool turnCamOff = true;

    public GameObject player;
    [Header("Inventory Canvas")]
    [Space]
    //Inventory
    [SerializeField] private GameObject _invCanvas;
    private bool _isActive = false;

    //Throw Obj
    [SerializeField] private Transform rightHandPos;
    private GameObject currentHand;
    private int id = 0;
    private bool _hasThrowObj = false;

    //Credencials
    [HideInInspector] public bool haveBasicAccess = false;
    [HideInInspector] public bool haveCaptainAccess = false;

    void Start()
    {
        if (current != null && current != this)
        {
            Destroy(gameObject);
        }
        else
        {
            current = this;
        }
        
        DontDestroyOnLoad(gameObject);

        //Inventory
        _invCanvas.SetActive(false);

        currentHand = null;
    }

    // Update is called once per frame
    void Update()
    {
        CctvDeativate(); //Desativar as cctvs           Letra Z
        ActivateInv(); //Abrir e fechar o inventario    Letra T
        PickTrowableFromInv(); //inv obj Throw          Letra Y
        VerefyCredencials(); //verefica credenciais
        MissionController.current.VerefyWhatCanIdo();
    }


    #region Inventory
    private void PickTrowableFromInv()
    {
        if (Input.GetKeyDown(KeyCode.Y)) OnClickObjectToThrow();
    }

    public bool CanShoot()
    {
        if (currentHand != null && currentHand.gameObject != null)
        {
            return true;
        }
        return false;
    }

    public GameObject ChangeFireObject()
    {
        return InventorySystem.current.slotItem[id].data.prefab;
    }

    public void RemoveFireObjFromInv()
    {
        Destroy(rightHandPos.transform.GetChild(0).gameObject);
        foreach (var item in InventorySystem.current.inventory)
        {
            if (item.data.prefab == InventorySystem.current.slotItem[id].data.prefab)
            {
                InventorySystem.current.Remove(item.data);
                _hasThrowObj = true;
                break;
            }
        }
        
    }

    //Obj from inv to Hand
    public void OnClickObjectToThrow()
    {
        if (currentHand == null && InventorySystem.current.slotItem != null && InventorySystem.current.slotItem.Count > 0)
        {
            id = 0;
            HasHandObj(InventorySystem.current.slotItem[0]);
            if (_hasThrowObj)
            {
                _hasThrowObj = false;
            }
        }
        else if (currentHand != null && InventorySystem.current.slotItem.Count > 0)
        {
            Destroy(rightHandPos.transform.GetChild(0).gameObject);
            id++;
            if (id >= InventorySystem.current.slotItem.Count) id = 0;

            HasHandObj(InventorySystem.current.slotItem[id]);
            if (_hasThrowObj)
            {
                _hasThrowObj = false;
            }
        }
    }

    public void HasHandObj(InventoryItem item)
    {
        if (inDebug) Debug.Log(item.data.prefab);
        ///Rever
        currentHand = null;

        currentHand = Instantiate(item.data.prefab);  //transform o obj do inv;
        currentHand.transform.SetParent(rightHandPos.transform, true);
        currentHand.GetComponent<Collider>().isTrigger = false;
        currentHand.GetComponent<Rigidbody>().useGravity = false;

        currentHand.transform.position = rightHandPos.position;
        currentHand.transform.parent = rightHandPos;
        currentHand.transform.localEulerAngles = Vector3.one;
        currentHand.GetComponent<Rigidbody>().isKinematic = true;
    }

    public void ActivateInv()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (!InventoryManager._isActive)
            {
                _invCanvas.SetActive(true);
                InventoryManager._isActive = true;
            }
            else
            {
                InventoryManager._isActive = false;
                _invCanvas.SetActive(false);
            }
        }
    }
    #endregion

    #region CCTVS
    public void CctvDeativate()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            turnCamOff = false;
            CCTVController.ChangeValue(turnCamOff);
            cctvOff = true;
            if (inDebug) Debug.Log("CCTVController.camerasOn :" + CCTVController.camerasOn);
        }
    }
    #endregion

    #region Doors Credencials
    //Verefica credencial
    private void VerefyCredencials()
    {
        if (!haveBasicAccess || !haveCaptainAccess)
        {
            foreach (var item in InventorySystem.current.inventory)
            {
                if (item.data.displayName == "Basic Credencial")
                {
                    haveBasicAccess = true;
                }

                if (item.data.displayName == "Captain Credencial")
                {
                    haveCaptainAccess = true;
                }
            }
        }
    }
    #endregion

}
