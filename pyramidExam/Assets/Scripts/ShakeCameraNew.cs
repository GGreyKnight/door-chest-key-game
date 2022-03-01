using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCameraNew : MonoBehaviour
{

    public float power = 0.3f;
    public float duration = 2f;
    private new Transform camera;
    public float slowDownAmount = 1f;
    public bool shouldShake = false;

    private Vector3 startPosition;
    private float initialDuration;

    void Start()
    {
        camera = Camera.main.transform;
        startPosition = camera.localPosition;
        initialDuration = duration;
    }

    // Update is called once per frame
    void Update()
    {
        if(shouldShake)
        {
            if(duration > 0)
            {
                camera.localPosition = startPosition + Random.insideUnitSphere * power;
                duration -= Time.deltaTime * slowDownAmount;
            }
            else
            {
                shouldShake = false;
                duration = initialDuration;
                camera.localPosition = startPosition;
            }
        }
    }
}
