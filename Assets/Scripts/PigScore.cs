using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigScore : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        destroy();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void destroy()
    {
        Destroy(gameObject, 1f);
    }
}
