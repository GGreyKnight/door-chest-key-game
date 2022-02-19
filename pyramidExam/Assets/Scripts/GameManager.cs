using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public CameraController cameraController;

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if(Instance == null) { Instance = this; }
        else { Destroy(gameObject); }

        cameraController = FindObjectOfType<CameraController>();
        
        //Debug.Log("Im working");
        UnlockCursor();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
