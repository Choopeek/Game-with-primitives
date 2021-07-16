using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmadaShip : MonoBehaviour
{
    private float leftBound = -18.0f;
    private float armadaShipSpeed;
    private float armadaShipMinSpeed = 5;
    private float armadaShipMaxSpeed = 10;

    // Start is called before the first frame update
    void Start()
    {
        armadaShipSpeed = Random.Range(armadaShipMinSpeed, armadaShipMaxSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        //ship constantly flies to the left and than vanishes
        transform.Translate(Vector3.forward * armadaShipSpeed * Time.deltaTime);
        if (transform.position.x < leftBound)
        {
            Destroy(gameObject);
        }
    }
}
