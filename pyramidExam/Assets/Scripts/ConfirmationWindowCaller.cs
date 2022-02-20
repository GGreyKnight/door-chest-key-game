using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmationWindowCaller : MonoBehaviour
{
    [SerializeField] private ConfirmationWindow myConfirmationWindow;

    void Start()
    {
        //OpenConfirmationWindow("Are you sure?");
    }

    public void OpenConfirmationWindow(string message)
    {
        myConfirmationWindow.gameObject.SetActive(true);
        myConfirmationWindow.yesButton.onClick.AddListener(YesClicked);
        myConfirmationWindow.noButton.onClick.AddListener(NoClicked);
        myConfirmationWindow.messageText.text = message;
    }

    public void YesClicked()
    {
        myConfirmationWindow.gameObject.SetActive(false);
        Debug.Log("Yes clicked");
    }

    public void NoClicked()
    {
        myConfirmationWindow.gameObject.SetActive(false);
        Debug.Log("No clicked");
    }
}
