using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       // Out of bounds - if it goes 1000 unit distance, destroy it
       if(Vector3.Distance(transform.position, GameObject.Find("Earth").transform.position) > 1000 ) Destroy(gameObject);

    }


}
