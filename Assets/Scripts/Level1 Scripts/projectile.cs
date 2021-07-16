using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    private Rigidbody projectileRB;
    private float flySpeed = 10.0f;
    //particle effects
    public ParticleSystem enemyExplodeParticle;
    public ParticleSystem projectileHitWallParticle;
    public GameObject bulletHitWallSound;
    public GameObject enemyDeathSound;
    
    // Start is called before the first frame update
    void Start()
    {
        projectileRB = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * flySpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            Instantiate(enemyExplodeParticle, transform.position, enemyExplodeParticle.transform.rotation);
            Instantiate(enemyDeathSound, transform.position, transform.rotation);
            Invoke("BulletHit", 0);
        }

        if (other.CompareTag("wall"))
        {
            Instantiate(projectileHitWallParticle, transform.position, projectileHitWallParticle.transform.rotation);
            Instantiate(bulletHitWallSound, transform.position, transform.rotation);
        }
    }
    private void BulletHit()
    {
        Destroy(gameObject);
    }
}
