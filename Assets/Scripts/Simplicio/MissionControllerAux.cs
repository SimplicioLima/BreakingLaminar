using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionControllerAux : MonoBehaviour
{
    public List<GameObject> missionOtherLevel = new List<GameObject>();

    void Start()
    {
        foreach (var item in missionOtherLevel)
        {
            MissionController.current.missionObj.Add(item);
        }

    }
}
