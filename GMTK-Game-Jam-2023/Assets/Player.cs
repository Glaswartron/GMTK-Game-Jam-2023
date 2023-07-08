using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;

public class Player : MovingObject
{
    public int health;
    public float jumpSpeed = 3f;
    public float midAirControl = 1f;

    public Stats stats;

    private Rigidbody2D playerRigidbody;
    private Collider2D playerCollider;

    private Vector2 movement;

    private bool jumping;
    private bool grounded = true;

    private float speedIncrement;
    private float speedDecrement;

    private float accelerationStartTime;
    private float decelerationStartTime;

    private float jumpStartTime;

    private bool decrementingSpeed = false;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();

        movement = Vector2.zero; // (0, 0)
        stats = new Stats(speed, /*walkPattern, jumpPattern,*/ health);
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
                if(Input.GetKeyDown(KeyCode.A))
                {
                    GetComponent<SpriteRenderer>().flipX = false;
                }
                else
                {
                    GetComponent<SpriteRenderer>().flipX = true;
                }
            }
            if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
            {
                decrementingSpeed = true;
                decelerationStartTime = Time.fixedTime;
            }

            float t = Time.fixedTime - accelerationStartTime;
            speedIncrement = SpeedFunction(t, accelerationTime);
            speedDecrement = 1;
            if (decrementingSpeed)
            {
                float s = Time.fixedTime - decelerationStartTime;
                speedDecrement = 1 - SpeedFunction(s, accelerationTime / 2);

                if (speedDecrement <= 0)
                    decrementingSpeed = false;
            }

            if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
            {
                jumpStartTime = Time.fixedTime;
                jumping = true;

                playerRigidbody.velocity = Vector2.up * jumpSpeed;
            }
        }

        if (Input.GetKey(KeyCode.A))
        {
            if (IsGrounded())
                playerRigidbody.velocity = new Vector2(-speed * speedIncrement * speedDecrement, playerRigidbody.velocity.y);
            else
            {
                playerRigidbody.velocity += new Vector2(-speed * midAirControl * Time.deltaTime, 0);
                playerRigidbody.velocity = new Vector2(Mathf.Clamp(playerRigidbody.velocity.x, -speed, speed), playerRigidbody.velocity.y);
            }
        }
        else if (Input.GetKey(KeyCode.D))
        {
            if (IsGrounded())
                playerRigidbody.velocity = new Vector2(speed * speedIncrement * speedDecrement, playerRigidbody.velocity.y);
            else
            {
                playerRigidbody.velocity += new Vector2(speed * midAirControl * Time.deltaTime, 0);
                playerRigidbody.velocity = new Vector2(Mathf.Clamp(playerRigidbody.velocity.x, -speed, speed), playerRigidbody.velocity.y);
            }
        }
        else
        {
            if (IsGrounded() && !decrementingSpeed)
                playerRigidbody.velocity = new Vector2(0, playerRigidbody.velocity.y);
        }

        if (jumping)
        {
            if (IsGrounded() && Time.fixedTime - jumpStartTime > 0.2f)
            {
                jumping = false;
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

    }

    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.BoxCast(playerCollider.bounds.center, playerCollider.bounds.size, 0f, Vector2.down, .1f, ~LayerMask.NameToLayer("Ground and Platforms"));
        return hit.collider != null;
    }

    private float SpeedFunction(float t, float accelerationTime)
    {
        if (t >= accelerationTime)
            return 1f;

        return walkPattern.CalculateMovement(t) / walkPattern.CalculateMovement(accelerationTime);
    }

    public void TakeHit()
    {
        if(stats.ReduceHP()) //-> Spieler hat keine HP mehr
        {
            //GameOver oder so
        }
    }

    public class Stats
    {
        private int speed;
        /*private MovementPattern walkPattern;
        private MovementPattern jumpPattern;*/
        private int health;

        public Stats(int speed, /*MovementPattern walkPattern, MovementPattern jumpPattern,*/ int health)
        {
            this.speed = speed;
            /*this.walkPattern = walkPattern;
            this.jumpPattern = jumpPattern;*/
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

        /*public void ChangeWalkPattern(MovementPattern wp)
        {
            walkPattern = wp;
        }
        public void ChangeJumpPattern(MovementPattern jp)
        {
            jumpPattern = jp;
        }*/
    }
}
