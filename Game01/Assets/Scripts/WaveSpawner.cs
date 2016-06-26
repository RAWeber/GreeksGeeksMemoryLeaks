using UnityEngine;
using System.Collections;

public class WaveSpawner : MonoBehaviour {

    public GameObject[] enemyList;
    public GameObject spawnPoint;
    public int minWaveValue;
    public int spawnInterval;
    public Transform enemies;

    private float timer;

	// Use this for initialization
	void Start () {
        timer = spawnInterval;
	}
	
	// Update is called once per frame
	void Update () {
        if(timer >= spawnInterval)
        {
            int currentValue = 0;
            int path = Random.Range(1, 4);
            while(currentValue < minWaveValue)
            {
                int enemyNumb = Random.Range(0, enemyList.Length);
                GameObject enemy = (GameObject)Instantiate(enemyList[enemyNumb], spawnPoint.transform.position, spawnPoint.transform.rotation);
                enemy.GetComponent<EnemyMovement>().path = path;
                enemy.transform.SetParent(enemies);
                currentValue += enemyList[enemyNumb].GetComponent<BaseEnemy>().price;
            }
            timer = 0;
        }
        timer += Time.deltaTime;
	}
}
