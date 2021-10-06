using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2MusicObject : MonoBehaviour
{

    private MusicController musicController;
    private float destructTimer = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        musicController = GameObject.Find("Music").GetComponent<MusicController>();
        gameObject.transform.parent = musicController.transform;
    }


    public void SelfDestruct()
    {
        StartCoroutine(SeldDestructTimer());
    }

    IEnumerator SeldDestructTimer()
    {
        yield return new WaitForSeconds(destructTimer);
        Destroy(gameObject);
    }

}
