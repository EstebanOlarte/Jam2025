using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    [SerializeField] private float _speed = 1f;
    [SerializeField] private float _health = 10;

    public void SetUp(List<Transform> waypoints)
    {
        StartCoroutine(FollowPath(waypoints));
    }

    private IEnumerator FollowPath(List<Transform> waypoints)
    {
        int waypointIndex = 0;
        Transform targetWaypoint = waypoints[waypointIndex];

        while (true)
        {
            // Move towards the target waypoint
            Vector3 direction = (targetWaypoint.position - transform.position).normalized;
            transform.position += direction * _speed * Time.deltaTime;

            // Rotate to face the direction of movement
            if (direction != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10);
            }

            // Check if we have reached the target waypoint
            if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.1f)
            {
                waypointIndex++;
                if (waypointIndex >= waypoints.Count)
                {
                    // If we have reached the last waypoint, stop the coroutine
                    yield break;
                }
                targetWaypoint = waypoints[waypointIndex];
            }

            yield return null;
        }
    }
}
