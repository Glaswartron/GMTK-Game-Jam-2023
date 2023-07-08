using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionJumpingEnemy : Enemy
{
    public float jumpingPower;
    [Tooltip("True: Gegner springt immer, wenn er wieder Boden unter den Füßen hat | False: Gegner springt in gewissen Intervallen")]
    public bool jumpAlways;
    [Tooltip("Das Sprungintervall, wenn jumpAlways = false")]
    public float jumpRate;

    private float jumpStartTime;

    private bool jumping = false;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        BaseEnemyUpdate(); //in Enemy

        if(jumpAlways)
        {
            if(!jumping)
            {
                if(IsGrounded())
                {
                    jumpStartTime = Time.fixedTime;
                    jumping = true;

                    enemyRigidBody.velocity = Vector2.up * jumpingPower;
                }
            }
            else
            {
                if (IsGrounded() && Time.fixedTime - jumpStartTime > 0.2f)
                {
                    jumping = false;
                    return;
                }
            }
        }
        else
        {

        }
    }
}
