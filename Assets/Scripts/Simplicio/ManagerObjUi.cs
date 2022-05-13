using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerObjUi : MonoBehaviour
{
    public Image prefabUi;
    private Image uiUse;
    [SerializeField] private float distancia;

    void Start()
    {
        uiUse = Instantiate(prefabUi, FindObjectOfType<Canvas>().transform).GetComponent<Image>();
        uiUse.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float dist = Vector3.Distance(this.transform.position, Camera.main.transform.position);

        if (dist < distancia)
        {
            uiUse.gameObject.SetActive(true);
            uiUse.transform.position = Camera.main.WorldToScreenPoint(transform.position);
        }
        else
        {
            uiUse.gameObject.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        if(uiUse != null)
        uiUse.gameObject.SetActive(false);
    }
}
//remover se o item for destruido