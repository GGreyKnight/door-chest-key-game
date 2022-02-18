using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Target : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    //private Color currentColor;

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Enter");
        GetComponent<MeshRenderer>().material.color += new Color32(50, 50, 50, 0);
    }
    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Exit");
        GetComponent<MeshRenderer>().material.color -= new Color32(50, 50, 50, 0);
    }

    void Start()
    {
        //currentColor = GetComponent<MeshRenderer>().material.color;
    }
}
