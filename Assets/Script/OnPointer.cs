using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Collider))]
public class OnPointer : MonoBehaviour
{
    public enum Role { Enemy, Screen, ExitScreen}
    public Role role;
    public Image LoadingBar;
    private bool IsOn;
    private float barTime = 0.0f;

    public SceneSetting st;
    //------------------------------------------------------
    //Enemy 전용

    public GameObject EnemyTank;
    public GameObject EnemyTankTurret;
    public GameObject BombPrefab;

    //------------------------------------------------------
    // Screen, ExitScreen 전용 전용

    public GameObject PlayerGunner;
    public GameObject PlayerInner;
    public GameObject GunnerCanvas;
    public GameObject InnerCanvas;
    //-----------------------------------------------------
    bool enemyIsAlive = true;
    void Start()
    {
        st = GameObject.Find("SceneSetting").GetComponent<SceneSetting>();


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
            //Debug.Log("In");
        }
        else
        {
            //Debug.Log("Out");
            LoadingBar.fillAmount = 0;
        }
    }


    void DoSomeThing() {
        
        switch (role)
        {
            case Role.Enemy:
                DistroyEnemy();
                break;
            case Role.Screen:
                GotoAim();
                break;
            case Role.ExitScreen:
                ExitScreen();
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
            barTime = 0.0f; // 바로 상호작용하는것을 막기위해 barTime 초기화
            enemyIsAlive = false; 
            
        }
        else if (!enemyIsAlive) 
        {
            Destroy(gameObject);
            barTime = 0.0f;
            st.isEnemy = false; //적전차가 파괴되었고 게임상에서도 삭제되었음을 SceneSetting에게 알림
            st.killCount++; // SceneSetting의 killCount를 1 추가
        }
    }

    void GotoAim() {
        Debug.Log("Goto Aim");
        PlayerGunner.SetActive(true);
        GunnerCanvas.SetActive(true);
        PlayerInner.SetActive(false);
        InnerCanvas.SetActive(false);
    }
    void ExitScreen()
    {
        Debug.Log("Exit Screen");
        PlayerInner.SetActive(true);
        InnerCanvas.SetActive(true);
        PlayerGunner.SetActive(false);
        GunnerCanvas.SetActive(false);

    }
}