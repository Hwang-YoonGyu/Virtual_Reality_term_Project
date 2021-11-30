using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Collider))]
public class OnPointer : MonoBehaviour
{
    public enum Role { Enemy }
    public Role role;
    public Image LoadingBar;
    private bool IsOn;
    private float barTime = 0.0f;


    //-----------------------------------------------------
    
    public GameObject EnemyTank;
    public GameObject EnemyTankTurret;
    public GameObject BombPrefab;


    //------------------------------------------------------

    bool enemyIsAlive = true;
    void Start()
    {
        LoadingBar = GameObject.Find("LoadingBar").GetComponent<Image>();
        IsOn = false;
        LoadingBar.fillAmount = 0;
    }

    void Update()
    {
        if (IsOn)
        {
            if (barTime <= 5.0f)
            {
                barTime += Time.deltaTime;
            }
            else if (barTime > 5.0f) 
            {
                DoSomeThing();
                barTime = 0;
            }
            LoadingBar.fillAmount = barTime / 5.0f;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    public void SetGazedAt(bool gazedAt)
    {
        IsOn = gazedAt;
        barTime = 0.0f;
        if (gazedAt)
        {
            Debug.Log("In");
        }
        else
        {
            Debug.Log("Out");
            LoadingBar.fillAmount = 0;
        }
    }


    void DoSomeThing() {
        
        switch (role)
        {
            case Role.Enemy:
                DistroyEnemy();
                break;

        }

    }

    void DistroyEnemy()
    {
        Debug.Log("Distroy Enemy");
        if (enemyIsAlive)
        {
            Instantiate(BombPrefab, EnemyTank.GetComponent<Transform>().position, EnemyTank.GetComponent<Transform>().rotation);
            EnemyTankTurret.GetComponent<Rigidbody>().AddForce(new Vector3(20, 300, 20));
            enemyIsAlive = false;
        }
        else if (!enemyIsAlive) 
        {

            //Instantiate(EnemyTank, EnemyTank.GetComponent<Transform>().position, EnemyTank.GetComponent<Transform>().rotation);
            Destroy(gameObject);
        }
    }
}