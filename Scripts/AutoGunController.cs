using UnityEngine;

public class AutoGunController : MonoBehaviour
{
    [Header("Components")]
    public CircleCollider2D circleCollider;
    private Vector3 mousePos;
    public GameObject bullet;
    public Transform bulletTransform;
    public SpriteRenderer gunIcon;
    public GameObject _gunChild;
    public int rotFix = 0;

    [Header("Ai Aiming")]
    public GameObject target;
    private float timerAi;
    public bool canFire;
    public float timeBetweenFiring;

    private void Awake()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        
    }

    private void Update()
    {
       AimAtEnemies();
    }

    
    private void AimAtEnemies()
    { 
        if (target != null)
        {
            Vector3 rotation = target.transform.position - transform.position;
            float distance = Vector3.Distance(transform.position, target.transform.position);

            if (distance <= circleCollider.radius)
            {
                float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, rotZ + rotFix);
                Shoot();
            }
            else
            {
                return;
            }
        }
        else if (target == null)
        {
            Vector2 currentPosition = transform.position;
            RaycastHit2D hit = Physics2D.CircleCast(currentPosition, circleCollider.radius, Vector2.zero, 1f);
            if (hit.collider.gameObject.CompareTag("Enemy"))
            {
                target = hit.collider.gameObject;
            }
            else
            {
                return;
            }
        }
    }

    private void Shoot()
    {
        if (!canFire)
        {
            timerAi += Time.deltaTime;
            if (timerAi > timeBetweenFiring)
            {
                canFire = true;
                timerAi = 0;
            }
        }
        if (canFire)
        {
            
            GameObject bulletObj = Instantiate(bullet, bulletTransform.position, transform.rotation);
            canFire = false;
            // bulletObj.transform.parent = bulletTransform;
            TurretBulletManager bulletScript = bulletObj.GetComponent<TurretBulletManager>();
            bulletScript.SetTarget(target.transform);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {        
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (collision.gameObject.GetComponent<EnemyController>().aiDead != true)
            {
                target = collision.gameObject;
            }
            else
            {
                target = null;
            }
        }
    }   
}
