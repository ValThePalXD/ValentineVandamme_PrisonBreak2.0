using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class AIBehaviour : MonoBehaviour
{
    private INode _startNode;
    private NavMeshAgent _agent;
    private Animator _animator;
    private EnemyController _enemyController;

    private bool _caughtPlayer = false;
    private bool _isDead = false;
   
    private readonly float _maxRoamDistance = 10.0f;   
    
    void Start()
    {    
        _animator = gameObject.GetComponent<Animator>();
        _agent = gameObject.GetComponent<NavMeshAgent>();

        _startNode = new SelectorNode
        (
            new SequenceNode
            (
                new ConditionNode(IsDead),
                new ActionNode(StopMoving)
            ),
             new SequenceNode
            (
                new ConditionNode(Found),
                new ActionNode(Aim)
            ),
             new SequenceNode
            (
                new ConditionNode(Free),
                new ActionNode(Patrolling)
            ));
        StartCoroutine(RunTree());
    }

    IEnumerator RunTree()
    {
        while(Application.isPlaying)
        {
            yield return _startNode.Tick();
        }
    }

    private void Update()
    {
        if (_animator.GetBool("Found"))
        {            
            _caughtPlayer = true;
        }

        if (_animator.GetBool("FallDead"))
        {
            _isDead = true;           
        }               
    }

    bool IsDead()
    {
        return _isDead;
    }
    
    bool Free()
    {
        return true;
    }

    bool Found()
    {      
        return _caughtPlayer;
    }


    IEnumerator<NodeResult> StopMoving()
    {      
        _agent.velocity = Vector3.zero;
        _agent.enabled = false;

        yield return NodeResult.Succes;
    }

    IEnumerator<NodeResult> Patrolling()
    {
        AIAnimation();
        _agent.speed = 2;
        Debug.Log("poop");
        if (_agent.remainingDistance <= _agent.stoppingDistance)
        {
            float newTarget = Random.Range(0, 100);
            if (newTarget >= 99)
            {
                Vector3 newPosition = transform.position + Random.insideUnitSphere * _maxRoamDistance;
                NavMeshHit hit;
                NavMesh.SamplePosition(newPosition, out hit, Random.Range(0, _maxRoamDistance), 1);

                _agent.SetDestination(hit.position);
            }
        }

        yield return NodeResult.Succes;
    }


    IEnumerator<NodeResult> Aim()
    {              
        _agent.velocity = Vector3.zero;
        _agent.enabled = false;

        yield return NodeResult.Succes;
    }    
    
    private void AIAnimation()
    {
        Vector3 transformedVelocity = transform.TransformDirection(_agent.velocity);
        _animator.SetFloat("InputX", transformedVelocity.z);
        _animator.SetFloat("InputY", transformedVelocity.x);

    }

}
