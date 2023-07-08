using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MovingObject
{
    public int HP = 1;

    protected Vector2 movement;
    protected float speedIncrement;
    protected Vector2 decrement;
    protected float accelerationStartTime;
    protected float accelerationStopTime;

    protected bool startedMoving = true;
    protected bool decrementingSpeed = false;

    protected Rigidbody2D enemyRigidBody;
    protected Collider2D enemyCollider;

    public float invincibilityTime = .2f;
    protected bool invictus;

    public bool doesDamage = true;
    // Start is called before the first frame update
    protected void Start()
    {
        movement = Vector2.zero;
        enemyRigidBody = GetComponent<Rigidbody2D>();
        enemyCollider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        BaseEnemyUpdate();
    }

    protected void BaseEnemyUpdate()
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
            if (!decrementingSpeed)
            {
                movement = Vector2.zero;
                if (!startedMoving)
                {
                    startedMoving = true;
                }
            }

        }
    }

    protected void FixedUpdate()
    {
        float t = Time.fixedTime - accelerationStartTime;
        speedIncrement = SpeedFunction(t, accelerationTime);

        float speedDecrement = 1f;
        if(decrementingSpeed)
        {
            float s = Time.fixedTime - accelerationStopTime;
            speedDecrement = 1 - SpeedFunction(s, accelerationTime / 2);

            if(speedDecrement <= 0)
            {
                decrementingSpeed = false;
            }
        }

        transform.Translate(speed * movement * speedIncrement * speedDecrement * Time.fixedDeltaTime);
    }

    private float SpeedFunction(float t, float accelerationTime)
    {
        if (t >= accelerationTime)
            return 1f;

        return Mathf.Pow(t,2) / Mathf.Pow(accelerationTime, 2);
    }

    public virtual void ChangeDirection()
    {
        decrementingSpeed = true;
        accelerationStopTime = Time.fixedTime;

        if (currentDirection == Direction.Left)
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
        decrementingSpeed = true;
        accelerationStopTime = Time.fixedTime;

        currentDirection = Direction.None;
    }

    public void StartMovement()
    {
        currentDirection = Direction.Left;
    }

    public bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.BoxCast(enemyCollider.bounds.center, enemyCollider.bounds.size, 0f, Vector2.down, .1f, ~LayerMask.NameToLayer("Ground and Platforms"));
        return hit.collider != null;
    }

    public void TakeHit(int dmg = 1)
    {
        if (!invictus)
        {
            Debug.Log("Hi, TakeHit hier");
            HP -= dmg;
            if (HP <= 0)
            {
                movement = Vector2.zero;
                GetComponent<Rigidbody2D>().gravityScale = 0;

                doesDamage = false;
                GetComponent<Animator>().Play("Die"); //Dort muss das GO ausgemacht werden
                //PlayDeathAnimation;
                //Wait for Animation Done
                //gameObject.SetActive(false);
            }
            else
            {
                StartCoroutine(InvinctusCountdown(invincibilityTime));
                DamageBehaviour();
            }
        }
    }

    private IEnumerator InvinctusCountdown(float time)
    {
        yield return new WaitForSeconds(time);
        invictus = false;
    }

    public void SelfInvic(float time)
    {
        invictus = true;
        StartCoroutine(InvinctusCountdown(time));
    }
    protected virtual void DamageBehaviour()
    {
        //Muss überschrieben werden
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
