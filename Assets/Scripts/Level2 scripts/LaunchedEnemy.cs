using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchedEnemy : MonoBehaviour


    
{
    public float enemySpeed = 10f;
    private float destroyBoundZ = -24f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.CompareTag("LaunchedEnemy"))
        {
            transform.Translate(Vector3.back * enemySpeed * Time.deltaTime);
        }

        if (transform.position.z < destroyBoundZ)
        {
            Destroy(gameObject);
        }
    }
}
