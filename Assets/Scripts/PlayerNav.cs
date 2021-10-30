using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerNav : MonoBehaviour
{
    [SerializeField] private Transform[] waypoints;

    private readonly Queue<Transform> _waypointsQueue = new Queue<Transform>();
    private NavMeshAgent _navMeshAgent;
    private Animator _animator;
    private static readonly int IsStopped = Animator.StringToHash("isStopped");

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
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MoveToNextWaypoint();
        }

        _animator.SetBool(IsStopped, _navMeshAgent.remainingDistance < _navMeshAgent.stoppingDistance);
    }

    private void MoveToNextWaypoint()
    {
        if (_waypointsQueue.Count == 0) SceneManager.ReloadScene();
        else
        {
            _navMeshAgent.destination = _waypointsQueue.Dequeue().position;
        }
    }
    
    
}
