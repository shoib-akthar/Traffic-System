using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Car_AI : MonoBehaviour
{
    public float safeDistance = 2f;
    public float carSpeed = 5f;
    public string[] tags;
    
    public GameObject currentTrafficRoute;
    public GameObject nextWaypoint;
    public int currentWapointNumber;

    private NavMeshAgent _carNavmesh;

    private void Start()
    {
        _carNavmesh = this.gameObject.GetComponent<NavMeshAgent>();
        _carNavmesh.speed = carSpeed;
    }

    private void Update()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position,transform.forward, out hit, safeDistance);

        if(hit.transform)
        {
            for(int i=0;i<tags.Length;i++)
            {
                if(hit.transform.tag != tags[i])
                {
                    //Debug.Log(hit.transform.name);
                    Stop();
                }
            }
        }
        else
        {
            Move();
        }
    }


    void Stop()
    {
        _carNavmesh.speed = 0;
    }

    void Move()
    {
        //transform.position += new Vector3(0, 0, carSpeed * Time.deltaTime);
        //Debug.Log("Current Point:" + currentWapointNumber + "Current Traffic Route: " + currentTrafficRoute.transform.name + "Next Waypoint : " + nextWaypoint.transform.name);

        if (nextWaypoint == null)
        {
            _carNavmesh.speed = 0;
        }

        if (currentWapointNumber > 0)
        {
            if (_carNavmesh.speed == 0)
                _carNavmesh.speed = carSpeed;

            _carNavmesh.SetDestination(currentTrafficRoute.transform.GetChild(currentWapointNumber - 1).transform.position);
        }
        else
        {
            if (nextWaypoint != null)
            {
                if (_carNavmesh.speed == 0)
                    _carNavmesh.speed = carSpeed;
                _carNavmesh.SetDestination(nextWaypoint.transform.position);
            }
        }

        if (currentWapointNumber > 0)
        {
            float distance = Vector3.Distance(transform.position, currentTrafficRoute.transform.GetChild(currentWapointNumber - 1).transform.position);
            if (distance <= 1)
                currentWapointNumber -= 1;
        }
        else
        {
            if (nextWaypoint != null)
            {
                float distance = Vector3.Distance(transform.position, nextWaypoint.transform.position);
                if (distance <= 1)
                {
                    currentWapointNumber = 5;
                    currentTrafficRoute = nextWaypoint.transform.parent.gameObject;
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
       // Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + safeDistance, transform.position.y + safeDistance, transform.position.z + safeDistance));
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * safeDistance);

    }
}
