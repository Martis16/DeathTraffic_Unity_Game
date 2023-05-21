using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carAI : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 5.0f;
    public float rotationSpeed = 2.0f;
    public float raycastDistance = 5.0f;
    public float avoidForce = 15.0f;

    private int currentWaypointIndex = 0;

    void Update()
    {
        if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position) < 1)
        {

            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
        else
        {

            Vector3 direction = (waypoints[currentWaypointIndex].position - transform.position).normalized;

            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, raycastDistance))
            {

                Vector3 avoidDirection = transform.forward + hit.normal * avoidForce;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(avoidDirection), rotationSpeed * Time.deltaTime);
            }
            else
            {

                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
            }


            transform.position += transform.forward * speed * Time.deltaTime;
        }
    }
}
