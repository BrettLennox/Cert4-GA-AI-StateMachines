using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour
{
    private int _waypointIndex;
    [SerializeField] private List<GameObject> _waypoints;
    [SerializeField] private float _moveToDistance = 1f;
    [SerializeField] private float _speed = 3f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        AIMoveTowards(_waypoints[_waypointIndex].transform);
    }

    private void AIMoveTowards(Transform goal)
    {
        Vector2 AIPosition = transform.position;
        if (Vector3.Distance(AIPosition, goal.position) > _moveToDistance)
        {
            Vector2 directionToWaypoint = (goal.position - transform.position);
            directionToWaypoint.Normalize();
            transform.position += (Vector3)directionToWaypoint * _speed * Time.deltaTime;
        }
        else
        {
            WaypointUpdate();
        }
    }

    private void WaypointUpdate()
    {
        if(_waypointIndex < _waypoints.Count - 1)
        {
            _waypointIndex++;
        }
        else
        {
            _waypointIndex = 0;
        }
    }
}
