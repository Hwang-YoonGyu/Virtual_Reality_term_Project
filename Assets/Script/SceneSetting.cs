using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSetting : MonoBehaviour
{
    public int killCount =0;
    public bool isEnemy = true;
    //---------------------------------------
    
    public GameObject Enemy;
    public bool isLoad = false;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(Enemy, new Vector3(63, -2, 12), new Quaternion(0, 0, 0, 0));
    }

    // Update is called once per frame
    void Update()
    {

        if (!isEnemy) {
            
            
            Instantiate(Enemy, new Vector3(63, -2, 12), new Quaternion(0, 0, 0, 0));
            isEnemy = true;

        }
    }
}
