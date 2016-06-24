using UnityEngine;
using System.Collections;

public class TargetDetection : MonoBehaviour {

    public GameObject projectile;
    public float reloadTime = 1.0f;
    public float turnSpeed = 5.0f;
    public float firePauseTime = 0.25f;
    public float errorAmount = 0.001f;
    public float range = 10.0f;
    public Transform turretHead;
    public Transform[] muzzlePositions;
  
    //Variables that are used exclusively for the missle launcher.
    public bool missileLauncher;
    public bool loaded = true;
    public int nextMuzzlePosition;
    public float missileReload = 2.0f;
    private float reloadComplete;

    private ArrayList targets = new ArrayList();
    private float nextFireTime = 0.5f;
    private float nextMoveTime;
    private Quaternion desiredRotation;
    private float aimError;
    // Use this for initialization
    void Start()
    {
        GetComponent<SphereCollider>().radius = range;
        GameObject ChildGameObject1 = transform.GetChild(0).gameObject;
        ChildGameObject1.transform.localScale = new Vector3(range * 2, range * 2, range * 2);
    }
    //
    // Update is called once per frame
    void Update()
    {
        if (targets.Count > 0)
        {
            
            if (Time.time >= nextMoveTime)
            {
                CalculateAimPosition(((Transform)targets[0]).position);
                turretHead.rotation = Quaternion.Lerp(turretHead.rotation, desiredRotation, Time.deltaTime * turnSpeed);
            }
            if (Time.time >= nextFireTime)
            {
                if(loaded)
                {
                    FireProjectile();
                }
                            
            }
            if (Time.time >= reloadComplete)
            {
                loaded = true;
            }

        }
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            targets.Add(other.gameObject.transform);
            nextFireTime = Time.time + reloadTime;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(targets.Contains(other.gameObject.transform))
        {
            targets.Remove(other.gameObject.transform);
        }
    }
    void CalculateAimPosition(Vector3 targetPosition)
    {
        Vector3 aimPoint = new Vector3(targetPosition.x-transform.position.x + aimError, targetPosition.y-transform.position.y + aimError + 0.5f, targetPosition.z - transform.position.z + aimError);
        desiredRotation = Quaternion.LookRotation(aimPoint);
    }
     
    void CalculateAimError()
    {
        aimError = Random.Range(-errorAmount, errorAmount);
    }
  
    void FireProjectile()
    {
        nextFireTime = Time.time + reloadTime;
        nextMoveTime = Time.time + firePauseTime;
        GameObject projectileShot = null;
        CalculateAimError();

        if(missileLauncher)
        {
            GameObject structure = transform.GetChild(1).gameObject;
            GameObject head = structure.transform.GetChild(0).gameObject;
            GameObject missiles = head.transform.GetChild(0).gameObject;
            if (!missiles.transform.GetChild(muzzlePositions.Length - 1).gameObject.activeSelf && loaded)
            {
                nextMuzzlePosition = 0;
                for (int i = 0; i < missiles.transform.childCount; i++)
                {
                    missiles.transform.GetChild(i).gameObject.SetActive(true);
                }
            }

            projectileShot = (GameObject)Instantiate(projectile, muzzlePositions[nextMuzzlePosition].position, muzzlePositions[nextMuzzlePosition].rotation);
            missiles.transform.GetChild(nextMuzzlePosition).gameObject.SetActive(false);
            nextMuzzlePosition++;
            if(nextMuzzlePosition == muzzlePositions.Length)
            {
                loaded = false;
                reloadComplete = Time.time + missileReload;
            }
          
        }
        else
        {
            foreach (Transform muzzlePosition in muzzlePositions)
            {
                projectileShot = (GameObject)Instantiate(projectile, muzzlePosition.position, muzzlePosition.rotation);
               
            }
        }
        projectileShot.GetComponent<CannonProjectile>().range = range;


    }
}
