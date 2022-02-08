using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    private PlayerInput playerInput;


    //store controls
    private InputAction moveAction;
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

        //alternative, when playerInput is not attached to player
        //playerInput = new PlayerInput();
        moveAction = playerInput.actions["Move"];
        

        //bool leftClickPressed = Mouse.current.leftButton.isPressed;
    }

    //private PlayerControls playerControls;

    public float camSpeed = 1f;

    
    //private void Awake()
    //{
    //    playerControls = new PlayerControls();
    //}

    //private void OnEnable()
    //{
    //    playerControls.Enable();
    //}

    //private void OnDisable()
    //{
    //    playerControls.Disable();
    //}

    void Update()
    {
        //Vector2 movement = playerInput.actions["Move"].ReadValue<Vector2>();
        Vector2 movement = moveAction.ReadValue<Vector2>();

        //Vector2 move = playerControls.Movement.Move.ReadValue<Vector2>();

        Vector3 position = transform.position;


        //if(moveAction.ReadValue<Vector2> == )
        //position += (Vector3)moveAction.ReadValue<Vector2>() * Time.deltaTime * camSpeed;
        //position += new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical)")) * Time.deltaTime * camSpeed;

        position += new Vector3(movement.x, 0, movement.y) * Time.deltaTime * camSpeed;


        //if(Input.GetKey("w"))
        //{
        //    position.z += camSpeed * Time.deltaTime;
        //}
        //if (Input.GetKey("s"))
        //{
        //    position.z -= camSpeed * Time.deltaTime;
        //}
        //if (Input.GetKey("a"))
        //{
        //    position.x -= camSpeed * Time.deltaTime;
        //}
        //if (Input.GetKey("d"))
        //{
        //    position.x += camSpeed * Time.deltaTime;
        //}


        transform.position = position;
    }
}
