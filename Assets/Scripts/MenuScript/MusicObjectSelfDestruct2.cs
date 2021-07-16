using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicObjectSelfDestruct2 : MonoBehaviour
{
    private MusicController musicController;
    private float destructTimer = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        musicController = GameObject.Find("Music").GetComponent<MusicController>();
        gameObject.transform.parent = musicController.transform;
    }

    // Update is called once per frame
    void Update()
    {
        //checking if the dynamic music stopped. If yes - terminate the object
        if (!musicController.dynamicMusicBool)
        {
            StartCoroutine(SelfDestruct());
        }
    }
    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(destructTimer);
        Destroy(gameObject);
    }
}
