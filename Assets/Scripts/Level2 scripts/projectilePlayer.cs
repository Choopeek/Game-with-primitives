using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectilePlayer : MonoBehaviour
{
    private float projectileSpeed = 10f;

    private float boundZ = 34f;

    private Rigidbody bulletRB;

    [SerializeField] ParticleSystem enemyDeathParticle;

    SpawnManagerLVL2 spawnManager;
    void Start()
    {
        bulletRB = GetComponent<Rigidbody>();
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManagerLVL2>();
        
    }

    
    void Update()
    {
        transform.Translate(Vector3.forward * projectileSpeed * Time.deltaTime);

        if (transform.position.z >= boundZ)
        {
            DestroyBullet();
        }

    }

    void DestroyBullet()
    {
        Destroy(gameObject);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyGO"))
        {
            Instantiate(enemyDeathParticle, other.transform.position, other.transform.rotation);
            spawnManager.EnemyDeathSound(other.transform.position);            
            Destroy(other.gameObject);
            DestroyBullet();
        }
        if (other.CompareTag("EnemyWaitingToBeLaunched"))
        {
            spawnManager.BulletBonkSound(transform.position);
            DestroyBullet();
        }
    }
}
