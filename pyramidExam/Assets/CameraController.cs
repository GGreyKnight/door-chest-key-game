using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float camSpeed = 1f;

    void Update()
    {
        Vector3 position = transform.position;

        if(Input.GetKey("w"))
        {
            position.z += camSpeed * Time.deltaTime;
        }
        if (Input.GetKey("s"))
        {
            position.z -= camSpeed * Time.deltaTime;
        }
        if (Input.GetKey("a"))
        {
            position.x -= camSpeed * Time.deltaTime;
        }
        if (Input.GetKey("d"))
        {
            position.x += camSpeed * Time.deltaTime;
        }

        transform.position = position;
    }
}
