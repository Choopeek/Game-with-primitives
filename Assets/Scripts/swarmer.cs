using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swarmer : MonoBehaviour
{
    float range = 5f;
    float speed = 10f;
    float returnSpeed = 1f;
    GameObject hive;

    // Start is called before the first frame update
    void Start()
    {
        hive = GameObject.Find("HIVE");
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.LookAt(hive.transform);
        
            transform.Translate(Vector3.left * speed * Time.deltaTime, Space.Self);
        
            
        if (Vector3.Distance(hive.transform.position, transform.position) > range)
        {
            
            transform.Translate(Vector3.forward * returnSpeed * Time.deltaTime, Space.Self);
        }

        if (Vector3.Distance(hive.transform.position, transform.position) < range)
        {
            
            transform.Translate(Vector3.back * returnSpeed * Time.deltaTime, Space.Self);
        }
    }
}
