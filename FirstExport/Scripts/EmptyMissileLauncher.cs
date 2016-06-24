using UnityEngine;
using System.Collections;

public class EmptyMissileLauncher : MonoBehaviour {

    private GameObject[] missiles = new GameObject[9];
    
	void Start ()
    {
        GameObject head = transform.parent.gameObject;
        GameObject structure = head.transform.parent.gameObject;
        GameObject tower = structure.transform.parent.gameObject;
        for (int i = 0; i < transform.childCount; i++ )
        {
            missiles[i] = transform.GetChild(i).gameObject;
            tower.GetComponent<TargetDetection>().muzzlePositions[i] = missiles[i].transform;
        }
        
       
            
    }
    // Update is called once per frame
    void Update ()
    {
	
	}
}
