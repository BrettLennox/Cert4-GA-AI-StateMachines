using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AIMovement : MonoBehaviour
{
    #region Movement/Waypoints
    [Header("Movement/Waypoints")]
    public int waypointIndex;
    [Tooltip("List of waypoints for the AI to move to")]
    public List<GameObject> waypoints;
    [Tooltip("The distance the AI needs to be from the waypoint before moving onto the next waypoint")]
    [SerializeField] private float _moveToDistance = 1f;
    [Tooltip("The speed in which the AI will move")]
    [SerializeField] private float _speed = 3f;
    #endregion
    #region Player/Chase
    [Header("Player/Chase")]
    public GameObject player;
    [Tooltip("The distance the player needs to be within for the AI to start chase")]
    [SerializeField] private float _chaseDistance = 3f;
    #endregion
    

    public void AIMoveTowards(Transform goal)
    {
        Vector2 AIPosition = transform.position;
        //checks if the AI is not within the moveToDistance
        if (Vector3.Distance(AIPosition, goal.position) > _moveToDistance)
        {
            //calculates the direction the Ai needs to move in to reach the waypoint
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
        if (waypointIndex < waypoints.Count - 1)
        {
            waypointIndex++;
        }
        else
        {
            waypointIndex = 0;
        }
    }

    public bool PlayerInRange()
    {
        //returns true if the player is within the chase distance
        return Vector3.Distance(transform.position, player.transform.position) <= _chaseDistance;
    }

    
}
