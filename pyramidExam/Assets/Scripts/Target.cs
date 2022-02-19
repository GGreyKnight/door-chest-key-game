using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Target : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    //private Color currentColor;

    [SerializeField] public float distanceToInteract = 2;

    [SerializeField] private PlayAnim playAnim = null;
    [SerializeField] private string targetAnimation = null;

    private bool wasHighlighted = false;
    private bool wasTargeted = false;
    private bool isHighlighted = false;

    public bool madeFromParts = false;
    [SerializeField] private MeshRenderer[] parts = null;

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
            isHighlighted = true;
            addHighlight();
        }

        if(wasHighlighted == true && distanceToInteract < Mathf.Abs(Vector3.Distance(transform.position, GameManager.Instance.cameraController.transform.position)))
        {
            isHighlighted = false;
            removeHighlight();
        }

        if(isHighlighted == true && GameManager.Instance.cameraController.leftClickButtonPressed == true)
        {
            Debug.Log("itemHitted");
            playAnim.playAnimation(targetAnimation);
            isHighlighted = false;
        }
    }

    private void addHighlight()
    {
        if(madeFromParts == false)
        {
            GetComponent<MeshRenderer>().material.color += new Color32(50, 50, 50, 0);
            wasHighlighted = true;
        }
        else
        {
            addHighlightToMulti();
        }
    }

    private void addHighlightToMulti()
    {
        for (int i = 0;i<parts.Length;i++)// MeshRenderer joint in parts)
        {
            parts[i].material.color += new Color32(50, 50, 50, 0);
        }   
        wasHighlighted = true;
    }

    private void removeHighlight()
    {
        if(madeFromParts == false)
        {
            GetComponent<MeshRenderer>().material.color -= new Color32(50, 50, 50, 0);
            wasHighlighted = false;
        }
        else
        {
            removeHighlightFromMulti();
        }
    }

    private void removeHighlightFromMulti()
    {
        for (int i = 0; i < parts.Length; i++)// MeshRenderer joint in parts)
        {
            parts[i].material.color -= new Color32(50, 50, 50, 0);
        }
        wasHighlighted = false;
    }

    void Start()
    {
        //currentColor = GetComponent<MeshRenderer>().material.color;
    }
}
