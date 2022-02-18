using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    private PlayerInput playerInput;

    //store controls
    private InputAction moveAction;
    private InputAction rotateAction;

    [SerializeField]
    private float playerSpeed = 3f;
    [SerializeField]
    private float playerRotationSpeed = 0.5f;


    private void Awake()
    {
        //random Player start rotation
        transform.Rotate(new Vector3(0, Random.Range(0,4)*90, 0));

        playerInput = GetComponent<PlayerInput>();

        moveAction = playerInput.actions["Move"];
        rotateAction = playerInput.actions["Rotate"];

        //bool leftClickPressed = Mouse.current.leftButton.isPressed;
    }

    void Update()
    {
        //listen for player input
        Vector2 movement = moveAction.ReadValue<Vector2>();
        float rotate = rotateAction.ReadValue<float>();

        Vector3 position = transform.position;

        //add movement
        Vector3 move = transform.right * movement.x + transform.forward * movement.y;
        //add rotate
        transform.Rotate(new Vector3(0, rotate, 0) * playerRotationSpeed, Space.World);


        position += move * Time.deltaTime * playerSpeed;


        transform.position = position;
    }
}
