using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;

public class Player : MovingObject
{
    public int health;
    public float jumpSpeed;
    public MovementPattern jumpPattern;
    public AnimationCurve jumpCurve;

    public Stats stats;

    private Rigidbody2D playerRigidbody;
    private Collider2D playerCollider;

    private Vector2 movement;

    private bool jumping;
    private bool grounded = true;

    private float speedIncrement;
    private float jumpIncrement;

    private float speedDecrement;

    private float accelerationStartTime;
    private float accelerationStopTime;

    private float jumpStartTime;
    private float jumpBaseY;

    private bool decrementingSpeed = false;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();

        movement = Vector2.zero; // (0, 0)
        stats = new Stats(speed, walkPattern, jumpPattern, health);
    }

    // Update is called once per frame
    void Update()
    {
        if (!jumping)
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
                movement = Vector2.left; // (-1, 0)
            else if (Input.GetKey(KeyCode.D))
                movement = Vector2.right; // (0, 1)
            else
            {
                if (!decrementingSpeed)
                    movement = Vector2.zero; // (0, 0)
            }

            if (Input.GetKeyDown(KeyCode.Space) && grounded)
            {
                jumping = true;
                jumpStartTime = Time.fixedTime;
                jumpBaseY = transform.position.y;
            }
        }

        if (jumping)
        {
            playerRigidbody.gravityScale = 0;

            if (Input.GetKey(KeyCode.A))
            {
                movement += Vector2.left * Time.fixedDeltaTime;
            } 
            else if (Input.GetKey(KeyCode.D))
            {
                movement += Vector2.right * Time.fixedDeltaTime;
            }
        }
        else
            playerRigidbody.gravityScale = 1;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            movement *= 2;
        }

        // Ground check
        RaycastHit2D hit = Physics2D.Raycast((Vector2)transform.position - Vector2.up / 4, -Vector2.up, 0.35f, layerMask: ~LayerMask.NameToLayer("Ground and Platforms"));
        Debug.DrawRay((Vector2)transform.position - Vector2.up / 4, -Vector2.up * 0.35f,  hit.collider != null ? Color.green : Color.red);
        if (hit.collider != null)
        {
            if (hit.transform.CompareTag("Ground") || hit.transform.CompareTag("Platform"))
            {
                grounded = true;

                if (jumping && Time.fixedTime - jumpStartTime > 0.2f)
                    jumping = false;
            }
            else
            {
                grounded = false;
            }
        }
    }

    private void FixedUpdate()
    {
        float t = Time.fixedTime - accelerationStartTime;
        speedDecrement = 1;
        speedIncrement = SpeedFunction(t, accelerationTime);
        if (decrementingSpeed)
        {
            float s = Time.fixedTime - accelerationStopTime;
            speedDecrement = 1 - SpeedFunction(s, accelerationTime / 2);

            if (speedDecrement <= 0)
                decrementingSpeed = false;
        }

        if (jumping)
        {
            jumpIncrement = jumpCurve.Evaluate(Time.fixedTime - jumpStartTime);

            if (jumpIncrement <= 0 && Time.fixedTime - jumpStartTime > 0.2f)
            {
                jumping = false;
                playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, 2 * (Time.fixedTime - jumpStartTime));
            }

            transform.Translate(new Vector2(0, (jumpCurve.Evaluate(Time.fixedTime - jumpStartTime) - jumpCurve.Evaluate(Time.fixedTime - jumpStartTime - Time.fixedDeltaTime)) / Time.fixedDeltaTime) * Time.fixedDeltaTime);
            //actualMovement += Vector2.up * jumpSpeed * jumpIncrement * Time.fixedDeltaTime;
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
