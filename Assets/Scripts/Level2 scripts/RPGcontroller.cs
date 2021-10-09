using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGcontroller : MonoBehaviour
{
    
    void Start()
    {
        StartCoroutine(Enlarge());
    }

    IEnumerator Enlarge()
    {
        Debug.Log("transformingRPG");  
        transform.localScale = new Vector3(0.2f, 0.0f, 0.2f);
        bool enlarge = true;
        while (enlarge)
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y + 0.002f, transform.localScale.z);
            if (transform.localScale.y >= 0.6f)
            {
                enlarge = false;
            }
            yield return null;
        }
        yield return null;
    }
    
}
