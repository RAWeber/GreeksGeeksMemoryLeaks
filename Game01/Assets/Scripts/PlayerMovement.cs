using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray mouseClick = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            NavMeshHit navHit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                Debug.Log("Prehit");
                Debug.DrawLine(mouseClick.origin, hit.point, Color.red, 60, false);

                if (NavMesh.SamplePosition(hit.point, out navHit, 1, NavMesh.AllAreas))
                {
                    agent.destination = navHit.position;
                    Debug.Log("hit");
                }
            }
        }
        //else if (!agent.pathPending && agent.remainingDistance <= 5)
        //{
        //    agent.ResetPath();
        //}
    }
}
