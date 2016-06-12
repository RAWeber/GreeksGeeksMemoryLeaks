using UnityEngine;
using System.Collections;

public class MoveTo : MonoBehaviour {

    public Transform goal;
    [Range(1,3)]
    public int path;    //1 - Left, 2 - Center, 3 - Right
    NavMeshAgent agent;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        //Uses bitvavalues for navmesh path
        agent.areaMask = 4 + 8 * ((int)Mathf.Pow(2, path - 1)) + 64;
    }
    
    void Start()
    {
        agent.destination = goal.position;
    }
}
