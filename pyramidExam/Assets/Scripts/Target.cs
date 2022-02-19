using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Target : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    //private Color currentColor;

    [SerializeField]
    public float distanceToInteract = 2;

    private bool wasHighlighted = false;
    private bool wasTargeted = false;

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Enter");
        wasTargeted = true;
    }
    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Exit");
        wasTargeted = false;
        if(wasHighlighted == true)
        {
            removeHighlight();
        }
    }

    private void Update()
    {
        if (wasTargeted == true && wasHighlighted == false && distanceToInteract >= Mathf.Abs(Vector3.Distance(transform.position, GameManager.Instance.cameraController.transform.position)))
        {
            addHighlight();
        }

        if(wasHighlighted == true && distanceToInteract < Mathf.Abs(Vector3.Distance(transform.position, GameManager.Instance.cameraController.transform.position)))
        {
            removeHighlight();
        }
    }

    private void addHighlight()
    {
        GetComponent<MeshRenderer>().material.color += new Color32(50, 50, 50, 0);
        wasHighlighted = true;
    }

    private void removeHighlight()
    {
        GetComponent<MeshRenderer>().material.color -= new Color32(50, 50, 50, 0);
        wasHighlighted = false;
    }

    void Start()
    {
        //currentColor = GetComponent<MeshRenderer>().material.color;
    }
}
