using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Target : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    //messages
    [SerializeField] public ConfirmationWindow confirmationWindow;
    [SerializeField] private string messageAsk;
    [SerializeField] private string messageKeyNeeded;

    [SerializeField] public float distanceToInteract = 2.5f;

    [SerializeField] private PlayAnim playAnim = null;
    [SerializeField] private string targetAnimation = null;
    [SerializeField] private bool hasSound = false;
    [SerializeField] private AudioSource audioSource = null;

    private bool wasHighlighted = false;
    private bool wasTargeted = false;
    private bool isHighlighted = false;
    private bool opened = false;

    [SerializeField] private bool pickable = false;
    [SerializeField] private bool keyToOpen = false;

    public bool madeFromParts = false;
    [SerializeField] private MeshRenderer[] parts = null;

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        wasTargeted = true;
        if (wasHighlighted == false && distanceToInteract >= Mathf.Abs(Vector3.Distance(transform.position, GameManager.Instance.cameraController.transform.position)))
        {
            isHighlighted = true;
            AddHighlight();
        }
    }
    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        wasTargeted = false;
        isHighlighted = false;
        if(wasHighlighted == true)
        {
            RemoveHighlight();
        }
    }

    private void Update()
    {
        if (wasTargeted == true && wasHighlighted == false && distanceToInteract >= Mathf.Abs(Vector3.Distance(transform.position, GameManager.Instance.cameraController.transform.position)))
        {
            isHighlighted = true;
            AddHighlight();
        }

        if(wasHighlighted == true && distanceToInteract < Mathf.Abs(Vector3.Distance(transform.position, GameManager.Instance.cameraController.transform.position)))
        {
            isHighlighted = false;
            RemoveHighlight();
        }

        if (isHighlighted == true && GameManager.Instance.cameraController.leftClickButtonPressed == true && opened == false)
        {
            if (keyToOpen == true)
            {
                if(KeyInventory.hasGoldKey == false)
                {
                    OpenConfirmationWindow(messageKeyNeeded);
                }
                else
                {
                    OpenConfirmationWindow(messageAsk);
                }
            }
            else
            {
                OpenConfirmationWindow(messageAsk);
            }
        }
    }

    private void AddHighlight()
    {
        if(madeFromParts == false)
        {
            GetComponent<MeshRenderer>().material.color += new Color32(50, 50, 50, 0);
            wasHighlighted = true;
        }
        else
        {
            AddHighlightToMulti();
        }
    }

    private void AddHighlightToMulti()
    {
        for (int i = 0;i<parts.Length;i++)
        {
            parts[i].material.color += new Color32(50, 50, 50, 0);
        }   
        wasHighlighted = true;
    }

    private void RemoveHighlight()
    {
        if(madeFromParts == false)
        {
            GetComponent<MeshRenderer>().material.color -= new Color32(50, 50, 50, 0);
            wasHighlighted = false;
        }
        else
        {
            RemoveHighlightFromMulti();
        }
    }

    private void RemoveHighlightFromMulti()
    {
        for (int i = 0; i < parts.Length; i++)
        {
            parts[i].material.color -= new Color32(50, 50, 50, 0);
        }
        wasHighlighted = false;
    }

    public void OpenConfirmationWindow(string message)
    {
        if(keyToOpen == true && KeyInventory.hasGoldKey == false)
        {
            confirmationWindow.gameObject.SetActive(true);
            confirmationWindow.yesButton.gameObject.SetActive(false);
            confirmationWindow.noButton.gameObject.SetActive(false);
            confirmationWindow.okButton.gameObject.SetActive(true);
            confirmationWindow.okButton.onClick.AddListener(OkClicked);
            confirmationWindow.messageText.text = message;
        }
        else
        {
            confirmationWindow.gameObject.SetActive(true);
            confirmationWindow.yesButton.gameObject.SetActive(true);
            confirmationWindow.yesButton.onClick.AddListener(YesClicked);
            confirmationWindow.noButton.gameObject.SetActive(true);
            confirmationWindow.noButton.onClick.AddListener(NoClicked);
            confirmationWindow.okButton.gameObject.SetActive(false);
            confirmationWindow.messageText.text = message;
        }
    }

    public void OkClicked()
    {
        confirmationWindow.gameObject.SetActive(false);
        isHighlighted = false;
    }

    public void YesClicked()
    {
        confirmationWindow.gameObject.SetActive(false);
        if(pickable == true)
        {
            KeyInventory.hasGoldKey = true;
        }

        playAnim.PlayAnimation(targetAnimation);

        if(hasSound == true)
        {
            audioSource.Play();
        }

        if(pickable == true)
        {
            StartCoroutine(DestroyOnAnimationEnd());
            ShakeCamera shake = FindObjectOfType<ShakeCamera>();
            shake.start = true;
        }

        opened = true;
        isHighlighted = false;
    }

    public void NoClicked()
    {
        confirmationWindow.gameObject.SetActive(false);
        isHighlighted = false;
    }

    private IEnumerator DestroyOnAnimationEnd()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }

    void Start()
    {
        confirmationWindow = FindObjectOfType<ConfirmationWindow>(true);
    }
}
