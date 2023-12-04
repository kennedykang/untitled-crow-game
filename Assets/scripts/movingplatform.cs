using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public Transform posA, posB;
    public int Speed;
    Vector2 targetPos;
    private Quaternion rotationAtPosA;
    private Quaternion rotationAtPosB;

    void Start()
    {
        targetPos = posB.position;
        rotationAtPosA = Quaternion.Euler(0, 0, 0);
        rotationAtPosB = Quaternion.Euler(0, 180, 0);
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, posA.position) < .1f)
        {
            targetPos = posB.position;
            transform.rotation = rotationAtPosB;
        }
        if (Vector2.Distance(transform.position, posB.position) < .1f)
        {
            targetPos = posA.position;
            transform.rotation = rotationAtPosA;
        }

        transform.position = Vector2.MoveTowards(transform.position, targetPos, Speed * Time.deltaTime);
    }
}
