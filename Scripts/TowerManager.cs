using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    [Header("Components")]
    public LevelManager levelManager;
    public GameObject particalSystem;

    [Header("Tower Stats")]
    public int health;
    public int maxHealth;
    private bool justAddedHealth = false;

    private void Update()
    {
        if (health <= 0)
        {
            particalSystem.SetActive(true);
            levelManager.playerDead = true;
        }       
        else
        {
            levelManager.playerDead = false;
            particalSystem.SetActive(false);
        }

        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }
    public void AddHealth()
    {
        if (!justAddedHealth && health < maxHealth)
        {
            StartCoroutine(HealthTicker());
        }
    }

    public void TakeDamage(in int incomingDamage)
    {
        health -= incomingDamage;   
    }

    IEnumerator HealthTicker()
    {
        justAddedHealth = true;
        yield return new WaitForSeconds(30);
        if (health < maxHealth)
            health++;
        justAddedHealth = false;

    }
}
