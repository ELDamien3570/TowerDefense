using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Unity.VisualScripting;
using static UnityEngine.GraphicsBuffer;
using System.Collections.Generic;


public class EnemyController : MonoBehaviour
{
    [Header("Gane Objects")]
    public Transform[] wayPoints;
    public Rigidbody2D aIRigidbody;
    public LevelManager levelManager;
    public EnemySpawner spawner;
    public Slider healthBar;
    //public EnemyController[] bomberRiders;

    [Header("AI Stats")]
    public float speed;
    public float randomSpeed;
    public float rotationSpeed = 720;
    public int damage;
    public int health;
    public int maxHealth;
    public int startingPoint;
    public int dollarValue;
    private int i;
    public bool aiDead = false;
    public bool aiPassed = false;
    //public bool isBomber;

    public bool justRandomedSpeed = false;

    public void Awake()
    {
        health = maxHealth;
        aIRigidbody = GetComponent<Rigidbody2D>();
        spawner = FindFirstObjectByType<EnemySpawner>();
        levelManager = FindFirstObjectByType<LevelManager>();
        healthBar = this.gameObject.GetComponentInChildren<Slider>();
        wayPoints = new Transform[spawner.wayPoints.Length];
        wayPoints = spawner.wayPoints;
        healthBar.maxValue = maxHealth;

        randomSpeed = speed;
        transform.position = wayPoints[startingPoint].position;
    }
    public void Update()
    {       
        AIMovement();
        UpdateHealthBar();

        if (health <= 0 && aiDead == false)
        {
            health = 0;
            aiDead = true;
            randomSpeed = 0;
            StartCoroutine(AiDeathTimer());
        }
        
    }
    private void AIMovement()
    {
        if (!aiDead)
        {
            if (Vector2.Distance(transform.position, wayPoints[i].position) < 0.05f)
            {
                i++;
                if (i == wayPoints.Length)
                {
                    i = 0;
                }
            }
            Vector3 movementDirection = wayPoints[i].position - transform.position;

            if (!justRandomedSpeed)
            {
                StartCoroutine(RandomizeSpeed());
            }

            transform.position = Vector2.MoveTowards(transform.position, wayPoints[i].position, randomSpeed * Time.deltaTime);
            float angle = Mathf.Atan2(movementDirection.y, movementDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
    private void UpdateHealthBar()
    {
        healthBar.value = health;   
    }
    private void EnemyTowerAttack()
    {
        health = 0;
        aiPassed = true;

    }
    IEnumerator RandomizeSpeed()
    {
        justRandomedSpeed = true;
        yield return new WaitForSeconds(Random.Range(0f, 1f));
        randomSpeed = Random.Range(speed * .7f, speed);
        justRandomedSpeed = false;
    }
    IEnumerator AiDeathTimer()
    {
        if (aiDead)
        {
            randomSpeed = 0;
            this.GetComponent<BoxCollider2D>().enabled = false;
            levelManager.currentEnemies--;
            if (!aiPassed)
            {
                // Debug.Log("Adding " + dollarValue);
                levelManager.money += dollarValue;
                levelManager.score += dollarValue;
            }
            yield return new WaitForSeconds(1);

            //if (isBomber)
            //{
            //    StartCoroutine(BomberSpawner(bomberRiders.Length));
            //}
            //else
            //{
                Destroy(gameObject);
            //}
        }
    }

    //IEnumerator BomberSpawner(int loopCount)
    //{
    //    for (int i = 0; i < loopCount; i++)
    //    {
    //        int x;
            
    //        x = Random.Range(0, bomberRiders.Length);

    //        Transform originalTransform = transform;
    //        //Debug.Log("Spawning enemy type " + x);
    //        EnemyController newAi = Instantiate(bomberRiders[x], originalTransform);
    //        newAi.transform.SetParent(originalTransform);
    //        levelManager.currentEnemies++;
    //        yield return new WaitForSeconds((x / loopCount) + (x * .05f) + .05f);
    //    }
    //    Destroy(this.gameObject);
    //    //  levelManager.waveStarted = false;
    //}
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Tower"))
        {
            if (!aiDead && health != 0)
            {
                collision.gameObject.GetComponent<TowerManager>().TakeDamage(damage);

                EnemyTowerAttack();
                //Debug.Log("Player Hit");
            }
            else
                return;
        }
    }
}
