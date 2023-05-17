using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carmove : MonoBehaviour
{
    public float speed = 5f;
    public Transform track;

    private Vector3[] waypoints;
    private int waypointIndex = 0;

    void Start()
    {

        waypoints = new Vector3[track.childCount];
        for (int i = 0; i < waypoints.Length; i++)
        {
            waypoints[i] = track.GetChild(i).position;
        }
    }

    void Update()
    {

        transform.position = Vector3.MoveTowards(transform.position, waypoints[waypointIndex], speed * Time.deltaTime);


        if (Vector3.Distance(transform.position, waypoints[waypointIndex]) < 0.1f)
        {
            waypointIndex = (waypointIndex + 1) % waypoints.Length;
        }


        transform.LookAt(waypoints[waypointIndex]);
    }
}
