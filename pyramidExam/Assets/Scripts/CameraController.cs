using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    //store controls
    private InputAction move;
    private InputAction rotate;
    private InputAction click;

    public PlayerControls playerCotrols;

    [SerializeField] private float playerSpeed = 3f;
    [SerializeField] private float playerRotationSpeed = 0.5f;

    public bool leftClickButton = false;
    public bool leftClickButtonPressed = false;

    private void Awake()
    {
        //random Player start rotation
        transform.Rotate(new Vector3(0, Random.Range(0,4)*90, 0));
        playerCotrols = new PlayerControls();
    }

    private void OnEnable()
    {
        move = playerCotrols.Game.Move;
        move.Enable();

        rotate = playerCotrols.Game.Rotate;
        rotate.Enable();

        click = playerCotrols.Game.LeftClick;
        click.Enable();
        /*click.started += clickAction;
        click.canceled += clickRelease;*/
    }

    private void OnDisable()
    {
        move.Disable();
        rotate.Disable();
        click.Disable();
    }

    void Update()
    {
        //listen for player input
        leftClickButtonPressed = click.WasPressedThisFrame();
        Vector2 movement = move.ReadValue<Vector2>();
        float rotateCamera = rotate.ReadValue<float>();
        
        Vector3 position = transform.position;
        //add movement
        Vector3 moveCamera = transform.right * movement.x + transform.forward * movement.y;
        //add rotate
        transform.Rotate(new Vector3(0, rotateCamera, 0) * playerRotationSpeed, Space.World);

        //set new position
        position += moveCamera * Time.deltaTime * playerSpeed;

        //go to new position
        transform.position = position;
    }

    /*private void clickAction(InputAction.CallbackContext context)
    {
        leftClickButton = true;
    }

    private void clickRelease(InputAction.CallbackContext context)
    {
        leftClickButton = false;
    }*/
}
