using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction { Left, Right}
public class MovingObject : MonoBehaviour
{
    public int speed;
    public float accelerationTime = 0.5f;
    public Direction currentDirection;
    public MovementPattern walkPattern;

    public void ApplyMovement()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
