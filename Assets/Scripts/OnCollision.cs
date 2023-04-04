using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnCollision : MonoBehaviour
{
    public string hitTag = "";
    public bool destroySelf = false;
    public bool destroyOther = false;
    public UnityEvent onEnter;

    void HandleCollision(Collider2D collider)
    {
        if (hitTag == collider.tag)
        {
            if (destroyOther)
            {
                Destroy(collider.gameObject);
            }
            if (destroySelf)
            {
                Destroy(gameObject);
            }
            onEnter.Invoke();
        }
    }
    private void OnTriggerEnter2D(Collider2D trigger)
    {
        HandleCollision(trigger);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        HandleCollision(collision.collider);
    }
}
