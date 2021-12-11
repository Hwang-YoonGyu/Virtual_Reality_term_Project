using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneSetting : MonoBehaviour
{
    public int killCount =0;
    public bool isEnemy = true;
    public bool isLoad = true;
    public bool recuperatorOpened = false;
    public Text t;

    //---------------------------------------

    public GameObject Enemy;
    

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(Enemy, new Vector3(63, -2, 12), new Quaternion(0, 0, 0, 0));
        //t.text = "적 전차 파괴수 : " + killCount + "\n" + "장전여부 : " + isLoad;
    }

    // Update is called once per frame
    void Update()
    {
        t.text = "Kill Count : " + killCount + "\n" + "Loaded : " + isLoad + "\n" + "Recuperator Close :" + !recuperatorOpened;
        if (!isEnemy) {

           
            Instantiate(Enemy, new Vector3(63, -2, 12), new Quaternion(0, 0, 0, 0));
            isEnemy = true;

        }
    }
}
