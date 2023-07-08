using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction { Left, Right, None}
public class MovingObject : MonoBehaviour
{
    public float speed;
    public float accelerationTime = 0.5f;
    public Direction currentDirection;
    public MovementPattern walkPattern;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
