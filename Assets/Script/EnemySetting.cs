using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySetting : MonoBehaviour
{
    public Image im;
    // Start is called before the first frame update
    void Start()
    {
       im =  GameObject.Find("LoadingBar").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
