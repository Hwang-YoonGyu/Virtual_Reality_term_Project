using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cshController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("XboxH");
        float v = Input.GetAxis("XboxV");
        Vector3 direction = new Vector3(h, v, 0.0f);
        transform.Translate(direction * 10.0f * Time.deltaTime);

        if (Input.GetButton("XboxA"))
        {
            transform.Rotate(Vector3.up * 30.0f * Time.deltaTime);
        }
    }
}
