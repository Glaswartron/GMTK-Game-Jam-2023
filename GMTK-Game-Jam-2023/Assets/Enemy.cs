using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MovingObject
{
    private Vector2 movement;
    private float speedIncrement;
    private Vector2 decrement;
    private float accelerationStartTime;
    private float accelerationStopTime;


    bool startedMoving = true;
    // Start is called before the first frame update
    void Start()
    {
        movement = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentDirection == Direction.Right)
        {
            if (startedMoving)
            {
                accelerationStartTime = Time.fixedTime;
                speedIncrement = 0f;
                startedMoving = false;
            }
            movement = Vector2.right;
        }
        else if (currentDirection == Direction.Left)
        {
            if (startedMoving)
            {
                accelerationStartTime = Time.fixedTime;
                speedIncrement = 0f;
                startedMoving = false;
            }
            movement = Vector2.left;
        }
        else if (currentDirection == Direction.None)
        {
            movement = Vector2.zero;
            if (!startedMoving)
            {
                startedMoving = true;
            }

        }
    }

    private void FixedUpdate()
    {
        float t = Time.fixedTime - accelerationStartTime;
        speedIncrement = SpeedFunction(t, accelerationTime);

        transform.Translate(speed * movement * speedIncrement * Time.fixedDeltaTime);
    }

    private float SpeedFunction(float t, float accelerationTime)
    {
        if (t >= accelerationTime)
            return 1f;

        return walkPattern.CalculateMovement(t) / walkPattern.CalculateMovement(accelerationTime);
    }

    public void ChangeDirection()
    {
        if(currentDirection == Direction.Left)
        {
            currentDirection = Direction.Right;
        }
        else if(currentDirection == Direction.Right)
        {
            currentDirection = Direction.Left;
        }
    }

    public void StopMovement()
    {
        currentDirection = Direction.None;
    }

    public void StartMovement()
    {
        currentDirection = Direction.Left;
    }
}
