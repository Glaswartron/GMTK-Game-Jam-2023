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
    public float invincibilityTime;
    public float enemyBoost = 2f;
    private bool invictus;

    private bool jumping;

    private bool doubleJumpEnabled;
    private bool secondJumpUsed;

    public Stats stats;

    private Rigidbody2D playerRigidbody;
    private Collider2D playerCollider;
    private Animator playerAnimator;
    private SpriteRenderer playerSpriteRenderer;

    private float invincibilityStartTime;

    private float speedIncrement;
    private float speedDecrement;

    private float accelerationStartTime;
    private float decelerationStartTime;

    private bool decrementingSpeed = false;

    private bool blockMovement = false;
    private bool dead = false;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
        playerAnimator = GetComponent<Animator>();
        playerSpriteRenderer = GetComponent<SpriteRenderer>();

        playerRigidbody.velocity = Vector2.zero; // (0, 0)
        stats = new Stats(speed, /*walkPattern, jumpPattern,*/ health);
    }

    // Update is called once per frame
    void Update()
    {
        if(blockMovement)
        {
            playerRigidbody.velocity = Vector2.zero;
            playerAnimator.SetBool("running", false);
            return;
        }

        if (IsGrounded())
        {
            // Reset jumping stuff once grounded after jump (= jump ended)
            if (jumping)
                jumping = false;

            playerAnimator.SetBool("jumping", false);
            playerAnimator.SetBool("grounded", true);

            DetermineSpeedRampUpAndDown();

            // Jump when space is pressed
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //playerRigidbody.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
                playerRigidbody.velocity = Vector2.up * jumpSpeed;
                playerAnimator.SetBool("jumping", true);
                secondJumpUsed = false;
                jumping = true;
            }
        }
        else
        {
            playerAnimator.SetBool("grounded", false);

            // Long and short jumps
            if (Input.GetKeyUp(KeyCode.Space))
            {
                playerRigidbody.velocity = playerRigidbody.velocity *= 0.5f;
            }

            // Double jump
            if (doubleJumpEnabled)
            {
                if (Input.GetKeyDown(KeyCode.Space) && !secondJumpUsed)
                {
                    playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, jumpSpeed);
                    secondJumpUsed = true;
                }
            }
        }

        if (Input.GetKey(KeyCode.A))
        {
            // Walk left when on the ground
            if (IsGrounded())
            {
                //playerRigidbody.AddForce(new Vector2(-speed /** speedIncrement * speedDecrement*/, playerRigidbody.velocity.y) - playerRigidbody.velocity, ForceMode2D.Impulse);
                playerRigidbody.velocity = new Vector2(-speed * speedIncrement * speedDecrement, playerRigidbody.velocity.y);
                playerAnimator.SetBool("running", true);
            }
            else
                playerRigidbody.velocity += new Vector2(-speed * midAirControl * Time.deltaTime, 0);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            // Walk right when on the ground
            if (IsGrounded())
            {
                //playerRigidbody.AddForce(new Vector2(speed /** speedIncrement * speedDecrement*/, playerRigidbody.velocity.y) - playerRigidbody.velocity, ForceMode2D.Impulse);
                playerRigidbody.velocity = new Vector2(speed * speedIncrement * speedDecrement, playerRigidbody.velocity.y);
                playerAnimator.SetBool("running", true);
            }
            else
                playerRigidbody.velocity += new Vector2(speed * midAirControl * Time.deltaTime, 0);
        }
        else
        {
            if (IsGrounded() && !decrementingSpeed)
            {
                // Stop when on the ground and nothing is pressed
                //playerRigidbody.AddForce(new Vector2(-playerRigidbody.velocity.x * playerRigidbody.mass, 0), ForceMode2D.Impulse);
                playerRigidbody.velocity = new Vector2(0, playerRigidbody.velocity.y);
                playerAnimator.SetBool("running", false);
            }
        }

        if (IsGrounded())
        {
            // Flip sprite/animation in the x direction depending on movement direction
            if (playerRigidbody.velocity.x > 0)
            {
                playerSpriteRenderer.flipX = true;
            }
            else if (playerRigidbody.velocity.x < 0)
            {
                playerSpriteRenderer.flipX = false;
            }
        }
    }

    private void FixedUpdate()
    {

    }

    private bool IsGrounded()
    {
        // Ground check using boxcast below player collider
        RaycastHit2D hit = Physics2D.BoxCast(playerCollider.bounds.center, playerCollider.bounds.size, 0f, Vector2.down, .1f, ~LayerMask.NameToLayer("Ground and Platforms"));
        return hit.collider != null;
    }

    public void BoostUp()
    {
        playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, enemyBoost);
    }

    public void SpeedUp(float multiplier, float time)
    {
        speed = speed * multiplier;

        playerSpriteRenderer.color = new Color(255, 120, 0);

        Invoke("ResetSpeed", time);
    }

    public void ResetSpeed()
    {
        speed = stats.GetSpeed();

        playerSpriteRenderer.color = new Color(255, 255, 255);
    }

    public void EnableDoubleJump(float time)
    {
        doubleJumpEnabled = true;

        playerSpriteRenderer.color = Color.green;

        Invoke("DisableDoubleJump", time);
    }

    public void DisableDoubleJump()
    {
        doubleJumpEnabled = false;
        secondJumpUsed = false;

        playerSpriteRenderer.color = new Color(255, 255, 255);
    }

    private void DetermineSpeedRampUpAndDown()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            accelerationStartTime = Time.fixedTime;
            speedIncrement = 0;
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
    }

    private float SpeedFunction(float t, float accelerationTime)
    {
        if (t >= accelerationTime)
            return 1f;

        return Mathf.Pow(t, 2) / Mathf.Pow(accelerationTime, 2);
    }

    public void TakeHit(bool skipInvinc = false)
    {
        if(dead)
        {
            return;
        }

        if (!invictus || skipInvinc)
        {
            if (stats.ReduceHP()) //-> Spieler hat keine HP mehr
            {
                UIMaster.instance.LoseHeart(2);
                blockMovement = true;
                GameManager.instance.GameOver();
                dead = true;
            }
            else
            {
                //set invincibility
                invictus = true;
                invincibilityStartTime = Time.time;
                StartCoroutine(InvictusCountdown());
                StartCoroutine(BlinkCo());
                UIMaster.instance.LoseHeart(1);

                ResetSpeed();
                DisableDoubleJump();

                CancelInvoke("ResetSpeed");
                CancelInvoke("DisableDoubleJump");
            }
        }
    }

    private IEnumerator BlinkCo()
    {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemies"), true);
        while (Time.time - invincibilityStartTime < invincibilityTime - 0.5f)
        {
            playerSpriteRenderer.color = new Color(255, 0, 0, 255);
            yield return new WaitForSeconds(0.3f);
            playerSpriteRenderer.color = new Color(255, 0, 0, 0);
            yield return new WaitForSeconds(0.1f);
        }

        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemies"), false);
        playerSpriteRenderer.color = new Color(255, 255, 255, 1);
    }

    public void GainHP()
    {
        stats.IncreaseHP(health);
        UIMaster.instance.GainHeart();
    }

    private IEnumerator InvictusCountdown()
    {
        yield return new WaitForSeconds(invincibilityTime);
        invictus = false;
    }

    public Vector2 GetCurrentVelo()
    {
        return playerRigidbody.velocity;
    }

    public class Stats
    {
        private float speed;
        /*private MovementPattern walkPattern;
        private MovementPattern jumpPattern;*/
        private int health;

        public Stats(float speed, /*MovementPattern walkPattern, MovementPattern jumpPattern,*/ int health)
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
        public float GetSpeed()
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
