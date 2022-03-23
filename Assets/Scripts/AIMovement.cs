using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AIMovement : MonoBehaviour
{
    public int waypointIndex;
    public List<GameObject> waypoints;
    [SerializeField] private float _moveToDistance = 1f;
    [SerializeField] private float _speed = 3f;
    public GameObject player;
    [SerializeField] private float _chaseDistance = 3f;
    [SerializeField] private float _stopDistance = 0.5f;
    [SerializeField] private Text _confusionText;

    public void AIMoveTowards(Transform goal)
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
        return Vector3.Distance(transform.position, player.transform.position) <= _chaseDistance;
    }

    public void DisplayConfusionText()
    {
        _confusionText.gameObject.SetActive(true);
    }

    public void HideConfusionText()
    {
        _confusionText.gameObject.SetActive(false);
    }
}
