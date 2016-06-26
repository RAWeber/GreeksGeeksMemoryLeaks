using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

    [Range(1,3)]
    public int path;    //1 - Left, 2 - Center, 3 - Right
    NavMeshAgent agent;
    GameObject pathWayPoints;
    int currentWaypoint;
    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        //Uses bitvavalues for navmesh path
        agent.areaMask = 4 + 8 * ((int)Mathf.Pow(2, path - 1)) + 64;

        switch (path)
        {
            case 1:
                pathWayPoints = GameObject.Find("TopPath");
                break;
            case 2:
                pathWayPoints = GameObject.Find("CenterPath");
                break;
            case 3:
                pathWayPoints = GameObject.Find("BottomPath");
                break;
        }

        agent.destination = pathWayPoints.transform.GetChild(0).position;
        currentWaypoint = 1;
        //agent.destination = pathWayPoints.transform.GetChild(pathWayPoints.transform.childCount - 1).position;
        //currentWaypoint = pathWayPoints.transform.childCount;
    }

    void Update()
    {

        if (!agent.pathPending && agent.remainingDistance <= 5)
        {
            if (currentWaypoint < pathWayPoints.transform.childCount)
            {
                agent.destination = pathWayPoints.transform.GetChild(currentWaypoint++).position;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }
}
