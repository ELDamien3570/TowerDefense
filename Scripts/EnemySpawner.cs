using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform[] wayPoints;
    public EnemyController[] enemyTypes;
    public LevelManager levelManager;

    private void LateUpdate()
    {
        
    }

    public void SpawnEnemies(int enemyCount)
    {
        //Debug.Log("Enemy Length is " + enemyTypes.Length);
       StartCoroutine(SpawnDelay(enemyCount));
    }

    IEnumerator SpawnDelay(int loopCount)
    {
        for (int i = 0; i < loopCount; i++)
        {
            int x = 0;

            if (loopCount <= 36)
            {
                x = Random.Range(0, 3);
            }
            else if (loopCount <= 100)
            {
                x = Random.Range(0, 4);
            }
            else if (loopCount <= 400 && loopCount > 100)
            {
                x = Random.Range(0, 7);
            }
            else
            {
                x = Random.Range(0, enemyTypes.Length);
            }

            //Debug.Log("Spawning enemy type " + x);
            Instantiate(enemyTypes[x], this.transform);
            levelManager.currentEnemies++;
            yield return new WaitForSeconds((x / loopCount) + (x * .01f) + .1f);
            
            
        }
        levelManager.waveStarted = false;
    }
}
