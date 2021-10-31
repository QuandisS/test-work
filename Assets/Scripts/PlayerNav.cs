using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerNav : MonoBehaviour
{
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private Pack[] packs;

    private readonly Queue<Transform> _waypointsQueue = new Queue<Transform>();
    private NavMeshAgent _navMeshAgent;
    private Animator _animator;
    private static readonly int IsStopped = Animator.StringToHash("isStopped");
    private bool _isNavigationEverStarted;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        foreach (var wp in waypoints)
        {
            _waypointsQueue.Enqueue(wp);
        }

        foreach (var pack in packs)
        {
            pack.PackCleared += MoveToNextWaypoint;
        }
        
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !_isNavigationEverStarted)
        {
            MoveToNextWaypoint();
            _isNavigationEverStarted = true;
        }

        _animator.SetBool(IsStopped, _navMeshAgent.remainingDistance < _navMeshAgent.stoppingDistance);
    }

    private void MoveToNextWaypoint()
    {
        _navMeshAgent.destination = _waypointsQueue.Dequeue().position;
    }
}
