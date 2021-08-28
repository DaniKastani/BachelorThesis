using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReSpawn : MonoBehaviour
{
    public Transform spawnPoint;
    public float minHeightForRespawn;
    public GameObject soap;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(soap.transform.position.y <= minHeightForRespawn)
        {
            soap.transform.position = spawnPoint.position;
        }
    }
}
