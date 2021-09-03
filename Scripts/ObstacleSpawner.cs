using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] obstacles;
  Vector3 spawnpos;
  [SerializeField] float spawnTimeRate;
    void Start()
    {
        StartCoroutine(nameof(SpawnObstacles));
        spawnpos = obstacles[0].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
     void Spawn()
    {
        int UpDownPos = Random.Range(0, 2);
        int randObstscle = Random.Range(0, obstacles.Length);

        if (UpDownPos == 0)
        {
            spawnpos.y = obstacles[randObstscle].transform.position.y;
            obstacles[randObstscle].transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        if (UpDownPos == 1)
        {
            spawnpos.y = obstacles[randObstscle].transform.position.y * -1;
            obstacles[randObstscle].transform.rotation= Quaternion.Euler(0, 0, 180);
        }

      
        Instantiate(obstacles[randObstscle],spawnpos, obstacles[randObstscle].transform.rotation);
    }

    IEnumerator SpawnObstacles()
    {
        yield return new WaitForSeconds(3f);
        while (true)
        {
            Spawn();
            GameManager.instance.UpdateScore();
            yield return new WaitForSeconds(spawnTimeRate);
        }
    }
}
