using UnityEngine;

using System.Collections;

public class DeleteParticleEffect : MonoBehaviour
{
    public float waitTime;
    private float deleteTime;
    void Start()
    {
        deleteTime = Time.time + waitTime;
    }

    void Update()
    {
        if (Time.time >= deleteTime)
        {
            Destroy(gameObject);
        }
        
    }
}
