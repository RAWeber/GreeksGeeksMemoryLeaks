using UnityEngine;
using System.Collections;

public class CannonProjectile : MonoBehaviour {

    public float speed = 10.0f;
    public float range = 10.0f;
    public float damage = 10.0f;
    public GameObject explosion;
    private float distanceTraveled;
    public Transform target;
	


	void Update ()
    {
        //Vector3 unitVector = Vector3.Normalize(new Vector3(target.position.x - transform.position.x, target.position.y - transform.position.y + 1, target.position.z - transform.position.z));
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
        //transform.Translate(unitVector * Time.deltaTime);
        distanceTraveled += Time.deltaTime * speed;
        if(distanceTraveled >= range)
        {
            Explode();
        }
       
	}
    void Explode()
    {
        if (explosion != null)
        {
            Instantiate(explosion, gameObject.transform.position, gameObject.transform.rotation);
        }
       
        Destroy(gameObject);
    }
    void OnTriggerEnter(Collider other)
    {
        string tag = other.gameObject.tag;
        if (tag == "Enemy")
        {
            Explode();
        }
        if (tag == "Environment")
        {
            Explode();
        }
    }
}
