using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 1;
    public float accelerationTime = 0.5f;

    private Vector2 movement;
    private float speedIncrement;

    private float accelerationStartTime;

    // Start is called before the first frame update
    void Start()
    {
        movement = Vector2.zero; // (0, 0)
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            accelerationStartTime = Time.fixedTime;
            speedIncrement = 0;
        }

        if (Input.GetKey(KeyCode.A))
        {
            movement = Vector2.left; // (-1, 0)
        }
        else if (Input.GetKey(KeyCode.D))
        {
            movement = Vector2.right; // (0, 1)
        }
        else
        {
            movement = Vector2.zero; // (0, 0)
            return;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            movement *= 2;
        }
    }

    private void FixedUpdate()
    {
        float t = Time.fixedTime - accelerationStartTime;
        speedIncrement = SpeedFunction(t, accelerationTime);

        transform.Translate(movement * speed * speedIncrement * Time.fixedDeltaTime);
    }

    private float SpeedFunction(float t, float accelerationTime)
    {
        if (t >= accelerationTime)
            return 1f;

        return BaseSpeedFunction(t) / BaseSpeedFunction(accelerationTime);
    }

    private float BaseSpeedFunction(float x)
    {
        return Mathf.Pow(x, 2);
    }
}
