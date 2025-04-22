using UnityEngine;
using System.Collections;


public class TurretBulletManager : MonoBehaviour
{
    private Vector3 mousePos;
    private Transform target;
    private Rigidbody2D rb;
    public float force;
    public int damage;
    public int bulletRotation;
    public float liveTime;
    public float scale;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        StartCoroutine(DeleteBullet(liveTime));
    }
    public void SetTarget(Transform _target)
    {
        target = _target;
    }
    private void Update()
    {
        if (!target) return;

        Vector2 direction = (target.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.linearVelocity = direction * force;
        //this.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Euler(0, 0, angle + bulletRotation);
        transform.localScale = new Vector3(scale, scale, 1);
        CheckForDead(); 
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        // Debug.Log("Collision Detected");
        if (collision.gameObject.CompareTag("Enemy"))
        {

            EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();
            enemy.health = enemy.health - damage;
            Destroy(gameObject);
            // Debug.Log("Ai was hit, current hp" + enemy.health);           
        }
        else if (collision.gameObject.CompareTag("Bullet"))
        {
            StartCoroutine(DeleteBullet(0f));
            ShowExplosion();
        }
        else
        {
            damage = 0;
        }
    }
    private void ShowExplosion()
    {

    }
    private void CheckForDead()
    {
        float stillnessThreshold = 0.1f;
        if (rb.linearVelocity.magnitude <= stillnessThreshold)
        {
            Destroy(gameObject);
        }
    }
    public IEnumerator DeleteBullet(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(this.gameObject);
    }
}
