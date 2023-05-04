using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AiAgent))]
public class StateMachine : MonoBehaviour
{
    public enum State
    {
        Patrol, 
        Chase, 
        Hunt,
    }

    [SerializeField] public State state;
    [SerializeField] private float _huntTime = 5f;

    private AiAgent _aiAgent;
    private GameObject _alert;
    //private SpriteRenderer _spriteRenderer;
    private void Start()
    {
        _aiAgent = GetComponent<AiAgent>();
        //_spriteRenderer = GetComponent<SpriteRenderer>();

        NextState();
    }
    private void NextState()
    {
        switch (state)
        {
            case State.Patrol:
                StartCoroutine(PatrolState());
                break;
            case State.Chase:
                StartCoroutine(ChaseState());
                break;
            case State.Hunt:
                StartCoroutine(HuntState());
                break;
            default:
                Debug.Log("404 State not found, State does not exist in NextState() function, stopping statemachine \nstate: " + state);
                break;
        }
    }
    private IEnumerator PatrolState()
    {
        //_spriteRenderer.color = Color.yellow;
        _aiAgent.Search();
        while (state == State.Patrol) //while loop runs once per game step
        {
            //run this code
            _aiAgent.Patrol();
            if (_aiAgent.IsPlayerInRange()) { state = State.Chase;}
            yield return null;
        }
        Debug.Log("State: Exiting Patrol");
        NextState();
    }

    private IEnumerator ChaseState()
    {
        //_spriteRenderer.color = Color.red;
        while (state == State.Chase)
        {
            //run this code
            _aiAgent.ChasePlayer();
            if (!_aiAgent.IsPlayerInRange()) { state = State.Hunt; }
            yield return null;
        }
        Debug.Log("State: Exiting Alert");
        NextState();
    }
    private IEnumerator HuntState()
    {
        //_spriteRenderer.color = Color.magenta;
        float timer = _huntTime;
        while (state == State.Hunt && timer>0)
        {
            //run this code
            _aiAgent.HuntForPlayer();
            timer -= Time.deltaTime;
            if (_aiAgent.IsPlayerInRange())
            {
                state = State.Chase;
                break;
            }
            yield return null;
        }
        if (!_aiAgent.IsPlayerInRange()) { state = State.Patrol; }
        Debug.Log("State: Exiting Hunt");
        NextState();
    }
}