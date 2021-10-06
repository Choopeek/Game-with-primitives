using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerLVL2 : MonoBehaviour
{
    [SerializeField] GameObject soundEnemyDeath;
    [SerializeField] GameObject soundPlayerDeath;
    [SerializeField] GameObject soundBulletBonk;
    

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void EnemyDeathSound(Vector3 coordinates)
    {
        Instantiate(soundEnemyDeath, coordinates, transform.rotation);
    }
    public void PlayerDeathSound(Vector3 coordinates)
    {

    }

    public void BulletBonkSound(Vector3 coordinates)
    {
        Instantiate(soundBulletBonk, coordinates, transform.rotation);
    }
}
