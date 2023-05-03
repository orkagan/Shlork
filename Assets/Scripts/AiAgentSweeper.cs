using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiAgentSweeper : AiAgent
{
    [SerializeField] private Transform[] _lookPoints;
    [SerializeField] int lookIndex;
    [SerializeField] Vector3 originalPos;

    public void Awake()
    {
        lookIndex = 0;
        originalPos = transform.position;
    }

    public override void Patrol()
    {
        _moveSpeed = _speed * 0.8f;
        Vector3 newLookDir;
        Vector3 targetDir = _lookPoints[lookIndex].position - transform.position;
        newLookDir = Vector3.RotateTowards(transform.up, targetDir, _spinSpeed * Time.deltaTime, 0f);
        if (Vector3.Angle(transform.up, newLookDir) == 0f)
        {
            lookIndex = (lookIndex + 1) % _lookPoints.Length;
        }
        transform.up = newLookDir;
    }
}
