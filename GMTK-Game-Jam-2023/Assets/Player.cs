using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MovingObject
{
    public int health;
    public MovementPattern jumpPattern;

    public Stats stats;

    private Vector2 movement;
    private float speedIncrement;
    private float speedDecrement;
    private float accelerationStartTime;
    private float accelerationStopTime;

    private bool decrementingSpeed = false;

    // Start is called before the first frame update
    void Start()
    {
        movement = Vector2.zero; // (0, 0)
        stats = new Stats(speed, walkPattern, jumpPattern, health);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            accelerationStartTime = Time.fixedTime;
            speedIncrement = 0;
        }
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            decrementingSpeed = true;
            accelerationStopTime = Time.fixedTime;
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
            if (!decrementingSpeed)
            {
                movement = Vector2.zero; // (0, 0)
                return;
            }
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            movement *= 2;
        }
        
    }

    private void FixedUpdate()
    {
        float t = Time.fixedTime - accelerationStartTime;
        float speedDecrement = 1;
        speedIncrement = SpeedFunction(t, accelerationTime);
        if (decrementingSpeed)
        {
            float s = Time.fixedTime - accelerationStopTime;
            speedDecrement = 1 - SpeedFunction(s, accelerationTime);
            if(speedDecrement <= 0.5)
            {
                decrementingSpeed = false;
            }
        }
        transform.Translate(movement * speed * speedDecrement * speedIncrement * Time.fixedDeltaTime);
    }

    private float SpeedFunction(float t, float accelerationTime)
    {
        if (t >= accelerationTime)
            return 1f;

        return walkPattern.CalculateMovement(t) / walkPattern.CalculateMovement(accelerationTime);
    }


    public class Stats
    {
        private int speed;
        private MovementPattern walkPattern;
        private MovementPattern jumpPattern;
        private int health;

        public Stats(int speed, MovementPattern walkPattern, MovementPattern jumpPattern, int health)
        {
            this.speed = speed;
            this.walkPattern = walkPattern;
            this.jumpPattern = jumpPattern;
            this.health = health;
        }

        public void SetSpeed(int speed)
        {
            this.speed = speed;
        }
        public int GetSpeed()
        {
            return speed;
        }

        public bool ReduceHP(int i = 1)
        {
            health -= i;
            return health <= 0;
        }
        
        public void IncreaseHP(int maxHealth, int i = 1)
        {
            health += i;
            if (health > maxHealth)
                health = maxHealth;
        }

        public void ChangeWalkPattern(MovementPattern wp)
        {
            walkPattern = wp;
        }
        public void ChangeJumpPattern(MovementPattern jp)
        {
            jumpPattern = jp;
        }
    }
}
