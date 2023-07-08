using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionJumpingEnemy : Enemy
{
    public float jumpingPower;
    [Tooltip("True: Gegner springt immer, wenn er wieder Boden unter den F¸ﬂen hat | False: Gegner springt in gewissen Intervallen")]
    public bool jumpAlways;
    [Tooltip("Das Sprungintervall, wenn jumpAlways = false")]
    public float jumpRate;

    private float jumpStartTime;
    private float lastJump;

    private bool jumping = false;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        lastJump = Time.fixedTime;
    }

    // Update is called once per frame
    protected void Update()
    {
        BaseEnemyUpdate(); //in Enemy

        if(jumpAlways)
        {
            if(!jumping)
            {
                if(IsGrounded()) //Am Boden
                {
                    Jump();
                }
            }
            else //jumping
            {
                if (IsGrounded() && Time.fixedTime - jumpStartTime > 0.2f)
                {
                    jumping = false;
                    return;
                }
            }
        }
        else if(!jumpAlways) //else w¸rde reichen, aber lesbarkeit
        {
            if(!jumping)
            {
                if(IsGrounded())
                {
                    if(Time.fixedTime - lastJump >= jumpRate)
                    {
                        Jump();
                    }
                }
            }
            else
            {
                if(IsGrounded() && Time.fixedTime - jumpStartTime > 0.2f)
                {
                    jumping = false;
                    lastJump = Time.fixedTime;
                }
            }
        }
    }

    /// <summary>
    /// Popul‰rer Song von Van Halen aus dem Jahr 1984. Der Song erschien erstmals auf dem gleichnamigen Album 1984. Ja, das Album heiﬂt 1984.
    /// 1984 ist das sechste Album der amerikanischen Hard-Rock Band Van Halen.Jump ist der zweite Song des Alums, der Song geht 4 Minuten und 4 Sekunden lang
    /// </summary>
    protected void Jump()
    {
        jumpStartTime = Time.fixedTime;
        jumping = true;

        enemyRigidBody.velocity = Vector2.up * jumpingPower;
    }
}
