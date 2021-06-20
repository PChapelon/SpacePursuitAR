using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UseButton()
    {
        Vector3 v = transform.position;
        v.y += 0.05f;
        transform.position = v;
    }
}
