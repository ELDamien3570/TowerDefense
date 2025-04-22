using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    private Vector3 mousePos;
    private Transform target;
    private Rigidbody2D rb;
    public float force;
    public int damage;
    

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 direction = mousePos - transform.position;
        Vector3 rotation = transform.position - mousePos;
        rb.linearVelocity = new Vector2(direction.x, direction.y).normalized * force;
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);
        //transform.position += new Vector3(0, 0, 0);


        StartCoroutine(DeleteBullet());
    }
    public void SetTarget(Transform _target)
    {
        target = _target;
    }
    private void Update()
    {
        

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
        else
        {
            damage = 0;
        }
    }
    public IEnumerator DeleteBullet()
    {
        yield return new WaitForSeconds(2);
        Destroy(this.gameObject);
    }
}
