using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertMark : MonoBehaviour
{
    StateMachine stateMachine;
    SpriteRenderer spriteRen;

    private void Start()
    {
        stateMachine = GetComponentInParent<StateMachine>();
        spriteRen = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        transform.up = Vector3.up;

        switch (stateMachine.state)
        {
            case StateMachine.State.Patrol:
                spriteRen.enabled = false;
                break;
            case StateMachine.State.Chase:
                spriteRen.enabled = true;
                break;
            case StateMachine.State.Hunt:
                spriteRen.enabled = false;
                break;
            default:
                break;
        }
    }
}
