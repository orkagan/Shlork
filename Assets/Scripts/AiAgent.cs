using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiAgent : MonoBehaviour
{
    [SerializeField] protected float _speed = 5f;
    [SerializeField] protected float _spinSpeed = 50f;
    protected float _moveSpeed;
    [SerializeField] protected GameObject _player;
    private Vector3 _lastSeen;

    [SerializeField] protected Transform[] _waypoints;
    [SerializeField] protected int _index;
    //[SerializeField] private float _viewRange = 5f;
    protected FieldOfView fov;

    private void Start()
    {
        fov = GetComponent<FieldOfView>();
        _player = GameObject.FindGameObjectWithTag("Player");
    }
    public bool IsPlayerInRange()
    {
        //Ternary operator "variable = (condition) ? expressionTrue :  expressionFalse;"
        //bool _inRange = (Vector2.Distance(transform.position, _player.transform.position)<_viewRange) ? true : false;
        //return _inRange;
        if (_player == null) return false;
        if (fov.visibleTargets.Contains(_player.transform))
        {
            return true;
        }
        return false;
    }
    
    public void ChasePlayer()
    {
        if (_player != null)
        {
            _moveSpeed = _speed * 1.2f;
            MoveToPoint(_player.transform.position);
            _lastSeen = _player.transform.position;
        }
    }

    public void HuntForPlayer()
    {
        if (Vector2.Distance(transform.position, _lastSeen) > 0.1f)
        {
            MoveToPoint(_lastSeen);
        }
        else
        {
            transform.eulerAngles += new Vector3(0, 0, _spinSpeed * Time.deltaTime);
        }
    }

    public virtual void Patrol()
    {
        _moveSpeed = _speed * 0.8f;
        MoveToPoint(_waypoints[_index].position);

        if (Vector2.Distance(transform.position, _waypoints[_index].position) < 0.1f)
        {
            _index = (_index + 1) % (_waypoints.Length);
        }
    }

    protected void MoveToPoint(Vector2 target)
    {
        Vector2 directionToPlayer = target - (Vector2)transform.position;
        if (directionToPlayer.magnitude > 0.1f)
        {
            directionToPlayer.Normalize();
            directionToPlayer *= _moveSpeed * Time.deltaTime;
            transform.position += (Vector3)directionToPlayer;

            transform.up = new Vector3(target.x,target.y,0) - transform.position;
        }
    }

    public void Search()
    {
        for (int i = 0; i < _waypoints.Length; i++)
        {
            if (Vector2.Distance(transform.position, _waypoints[_index].position) > Vector2.Distance(transform.position, _waypoints[i].position))
            {
                _index = i;
            }
        }
    }
}
