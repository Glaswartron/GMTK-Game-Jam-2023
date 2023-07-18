using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FlyingPlatform : MonoBehaviour
{
    public Transform platform;
    public Transform start;
    public Transform end;
    public float speed;

    public bool waitAtPoints;
    public float waitingTime;

    [Tooltip("True: Die Plattform bewegt sich erst dann, wenn der Spieler sie berührt hat")]
    public bool moveByTrigger;
    [Tooltip("So lange wartet die Plattform nach dem Trigger mit der Bewegung")]
    public float triggerDelay;

    public bool dieAtEnd = false;

    private Transform target;
    private bool waiting = false;
    private bool semaphora = false;
    private bool triggered = false;

    // Start is called before the first frame update
    void Start()
    {
        platform.position = start.position;

        target = end;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(moveByTrigger && !triggered)
        {
            return;
        }

        if(!waiting)
            platform.position = Vector2.MoveTowards(platform.position, target.position, speed * Time.fixedDeltaTime);

        if (Vector2.SqrMagnitude(target.position - platform.position) < 0.05f)
        {
            if (waitAtPoints && !semaphora)
            {
                waiting = true;
                semaphora = true;
                StartCoroutine(WaitAtPoint());
            }
            if (!waiting)
            {
                target = target == end ? start : end;
                semaphora = false;
                if(dieAtEnd)
                {
                    Destroy(this.gameObject);
                }
            }
        }
    }

    private IEnumerator WaitAtPoint()
    {
        yield return new WaitForSeconds(waitingTime);
        waiting = false;
    }

    public void Trigger()
    {
        StartCoroutine(TriggerDelay());
    }

    private IEnumerator TriggerDelay()
    {
        yield return new WaitForSeconds(triggerDelay);
        triggered = true;
    }
}
