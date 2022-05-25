using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MissionController : MonoBehaviour
{
    [SerializeField] private bool inDebug = true;
    public static bool MissionComplete = false;
    public static MissionController current { get; private set; }

    [HideInInspector] public Dictionary<string, string> missionsDiscription = new Dictionary<string, string>();
    //[HideInInspector] public Dictionary<string, bool> missionsComplete = new Dictionary<string, bool>();
    [HideInInspector] public List<string> missionName;
    [HideInInspector] public List<string> discription;

    [HideInInspector] public string currentMission;

    public bool mission1_Key = false;
    public bool mission2_Base = false;
    public bool mission3_Cam = false;
    public bool mission4_Cargo = false;
    public bool mission5_Emp = false;
    public bool mission6_Server = false;
    public bool mission7_DoorKeys = false;
    public bool mission8_OpenServer = false;
    public bool mission9_KARENoff = false;

    public List<bool> missionsSequence = new List<bool>();
    public List<GameObject> missionObj = new List<GameObject>();

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

        NumberOfMissions();
        SetMissionsNames();

        currentMission = missionName[0]; //iniciar a mission inicial;

        DontDestroyOnLoad(gameObject);
    }

    #region Set Missions
    private void NumberOfMissions()
    {
        missionsSequence.Add(mission1_Key);
        missionsSequence.Add(mission2_Base);
        missionsSequence.Add(mission3_Cam);
        missionsSequence.Add(mission4_Cargo);
        missionsSequence.Add(mission5_Emp);
        missionsSequence.Add(mission6_Server);
        missionsSequence.Add(mission7_DoorKeys);
        missionsSequence.Add(mission8_OpenServer);
        missionsSequence.Add(mission9_KARENoff);
    }

    //Atribuir nome e descriçao a cada missao
    private void SetMissionsNames()
    {
        //1
        missionName.Add("Escapar da Sala de Controlo");
        discription.Add("O jogador deve escapar da sala de controlo sem ser detetado pelo robô.");
        //2
        missionName.Add("Contactar a Base Naval");
        discription.Add("Encontrar a Sala de Comunicações e contactar a base naval para informá-los " +
            "da situação do navio e receber informações do próximo objetivo.");
        //3
        missionName.Add("Desativar as Câmaras");
        discription.Add("Encontrar a sala de videovigilância e desativar as câmaras de forma a que" +
            " a K.A.R.E.N. não consiga perseguir o jogador.");
        //4
        missionName.Add("Investigar a Área de Carga");
        discription.Add("Dirigir-se à Área de Carga/Armazém e investigar a zona. Ao investigar, " +
            "o jogador irá recolher peças e encontrar o soldador.");
        //5
        missionName.Add("Construir EMP’s");
        discription.Add("Encontrar o laboratório de robótica e usar o soldador e peças adquiridas " +
            "ao investigar a área de carga para construir o primeiro EMP.");
        //6
        missionName.Add("Chegar à Sala do Servidor");
        discription.Add("Chegar à sala do servidor, onde o jogador se depara com uma porta que " +
            "necessita de 3 chaves para ser aberta.");
        //7
        missionName.Add("Encontrar as Chaves");
        discription.Add("O jogador deve procurar as 3 chaves pelo mapa de forma a abrir a sala do servidor.");
        //8
        missionName.Add("Abrir a Sala do Servidor");
        discription.Add("Entrar na sala do servidor e dirigir-se ao terminal de controlo central.");
        //9
        missionName.Add("Desativar o Servidor que Contém a K.A.R.E.N.");
        discription.Add("Inserir o código de linha de comandos que desativa o servidor onde a K.A.R.E.N. está alojada.");


        SetDiscription();
    }

    //Por as listas direitas com a info toda
    private void SetDiscription() 
    {
        for (int i = 0; i < missionName.Count; i++)
        {
            missionsDiscription.Add(missionName[i].ToString(), discription[i].ToString());
            //missionsComplete.Add(missionName[i].ToString(), false);
        }
    }
    #endregion

    public void VerefyWhatCanIdo()
    {
        //Mission 1
        if (mission1_Key == false)
        {
            if (missionObj[0] != null && currentMission == missionName[0]) missionObj[0].tag = "Collectible";

            foreach (var item in InventorySystem.current.inventory)
            {
                if (item.data.id == 11)
                {
                    mission1_Key = true;
                    currentMission = missionName[1];
                    missionObj[1].GetComponent<Doors>().doorsUnlock = true;
                }
            }
        }
        //Mission 2
        else if (mission2_Base == false && mission1_Key == true) //missao 2 obj apartir do [2]
        {
            //if (missionObj[1] != null && currentMission == missionName[1]) missionObj[1].tag = "Collectible";

            foreach (var item in InventorySystem.current.inventory)
            {
                if (item.data.id == 13)
                {
                    mission2_Base = true;
                    currentMission = missionName[2];
                }
            }
        }
        //Mission 3
        else if (mission3_Cam == false && mission2_Base == true)
        {
            if(GameManager.current.turnCamOff == false)
            {
                mission3_Cam = true;
                GameManager.current.cctvOff = true;
                currentMission = missionName[3];
            }
            
        }
        //Mission 4
        else if (mission4_Cargo == false && mission3_Cam == true)
        {
            int sceneIndex = SceneManager.GetActiveScene().buildIndex;
            if (sceneIndex == 3 && missionObj[3].gameObject.GetComponent<Doors>()._isOpen == true)
            {
                mission4_Cargo = true;
                currentMission = missionName[4];
            }
        }
        //Mission 5
        else if (mission5_Emp== false && mission4_Cargo == true)
        {
            foreach (var item in InventorySystem.current.inventory)
            {
                if (item.data.id == 6 || item.data.id == 7)
                {
                    mission5_Emp = true;
                    currentMission = missionName[5];
                }
            }
        }
        //Mission 6
        else if (mission6_Server == false && mission5_Emp == true)
        {
            missionObj[4].gameObject.GetComponent<CheckServerDoor>().UnlockTrigger = true;
            //if (inDebug) Debug.Log("Door Checked: " + missionObj[4].gameObject.GetComponent<CheckServerDoor>().UnlockTrigger);
            int sceneIndex = SceneManager.GetActiveScene().buildIndex;
            //if (inDebug) Debug.Log("Scene: " + sceneIndex);
            if (sceneIndex == 3 && missionObj[4].gameObject.GetComponent<CheckServerDoor>().doorFound == true)
            {
                mission6_Server = true;
                currentMission = missionName[6];
            }
        }
        //Mission 7
        else if (mission7_DoorKeys == false && mission6_Server == true)
        {
            foreach (var item in InventorySystem.current.inventory)
            {
                if (item.data.id == 14 && item.stackSize == 3)
                {
                    mission7_DoorKeys = true;
                    currentMission = missionName[7];
                }
            }
        }
        //Mission 8
        else if (mission8_OpenServer == false && mission7_DoorKeys == true)
        {
            missionObj[5].gameObject.GetComponent<Doors>().doorsUnlock = true;
            if (missionObj[5].gameObject.GetComponent<Doors>()._isOpen == true)
            {
                mission8_OpenServer = true;
                currentMission = missionName[8];
            }
        }
        //Mission 9
        else if (mission9_KARENoff == false && mission8_OpenServer == true)
        {
            //
            if (GameManager.current.KarenOff) mission9_KARENoff = true;
            //GameOver
        }
        else
        {
            Debug.Log("GameOver!!");
        }
    }
}