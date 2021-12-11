using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Collider))]
public class OnPointer : MonoBehaviour
{
    public enum Role { Enemy, Screen, ExitScreen, Loader, Recuperator, }
    public Role role;
    public Image LoadingBar;
    private bool IsOn;
    private float barTime = 0.0f;
    private float time = 0.0f;
    public SceneSetting st;
    //------------------------------------------------------
    //Enemy 전용

    public GameObject EnemyTank;
    public GameObject EnemyTankTurret;
    public GameObject BombPrefab;
    public AudioSource Audio;

    //------------------------------------------------------
    // Screen, ExitScreen 전용

    public GameObject Player;
    public GameObject PlayerTurret;
    public GameObject GunnerCanvas;

    //-------------------------------------------------------
    //Loader 전용
    public GameObject Bullet;
    private GameObject copyBullet;
    public GameObject barrel;
    private int rot = 1;
    private bool copyBulletMove1 = false;
    private bool copyBulletMove2 = false;
    private bool copyBulletMove3 = false;
    private bool copyBulletMove4 = false;


    //-----------------------------------------------------
    //Recuperator 전용
    public GameObject Recuperator;
    private bool recuperatorOpen = false;
    private bool recuperatorClose = false;


    //-----------------------------------------------------
    private bool enemyIsAlive = true;






    void Start()
    {
        st = GameObject.Find("SceneSetting").GetComponent<SceneSetting>();
        LoadingBar = GameObject.Find("LoadingBar").GetComponent<Image>();
        Audio = GameObject.Find("player Camera").GetComponent<AudioSource>();
        IsOn = false;
        LoadingBar.fillAmount = 0;
    }

    void Update()
    {
        if (IsOn)
        {
            if (barTime <= 2.0f)
            {
                barTime += Time.deltaTime;
            }
            else if (barTime > 2.0f) 
            {
                DoSomeThing();
                barTime = 0;
            }
            LoadingBar.fillAmount = barTime / 2.0f;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (recuperatorOpen) 
        {
            if (Recuperator.transform.position.z < 1.0f)
            {
                Recuperator.transform.position += new Vector3(0, 0, 0.01f);
                
            }
            else if (Recuperator.transform.position.z >= 1.0f)
            {
                recuperatorOpen = false;
                st.recuperatorOpened = true;
            }
        }
        if (recuperatorClose) 
        {
            if (Recuperator.transform.position.z > 0.0f)
            {
                Recuperator.transform.position -= new Vector3(0, 0, 0.01f);

            }
            else if (Recuperator.transform.position.z <= 0.0f)
            {
                recuperatorClose = false;
                st.recuperatorOpened = false;
            }
        }
        if (copyBulletMove1 == true) 
        {
            if (copyBullet.transform.position.y <= barrel.transform.position.y)
            {
                copyBullet.transform.position += new Vector3(0, 0.01f, 0);
            }
            else if (copyBullet.transform.position.y > barrel.transform.position.y) 
            {
                copyBulletMove1 = false;
                copyBulletMove2 = true;
            }
        }
        if (copyBulletMove2 == true) 
        {
            
            copyBullet.transform.rotation = Quaternion.Slerp(copyBullet.transform.rotation, Quaternion.Euler(new Vector3(-90, 0, 0)), 0.01f);
            copyBulletMove3 = true;
        }
        if (copyBulletMove3 == true) 
        {
            if (copyBullet.transform.position.z <= barrel.transform.position.z)
            {
                copyBullet.transform.position += new Vector3(0, 0, 0.01f);
            }
            else if (copyBullet.transform.position.z > barrel.transform.position.z)
            {
                copyBulletMove3 = false;
                copyBulletMove2 = false;
                copyBulletMove4 = true;
            }
        }
        if (copyBulletMove4 == true)
        {
            if (copyBullet.transform.position.x >= barrel.transform.position.x)
            {
                copyBullet.transform.position -= new Vector3(0.01f, 0, 0);
            }
            else if (copyBullet.transform.position.x < barrel.transform.position.x)
            {
                copyBulletMove4 = false;
                copyBulletMove2 = false;
                
                st.isLoad = true;
                
            }
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
            case Role.Loader:
                OnLoad();
                break;
            case Role.Recuperator:
                recuperator();
                break;
        }

    }

    void DistroyEnemy()
    {
        Debug.Log("Distroy Enemy");
        if (enemyIsAlive)
        {
            if (st.isLoad && !st.recuperatorOpened)
            {
                Instantiate(BombPrefab, EnemyTank.GetComponent<Transform>().position, EnemyTank.GetComponent<Transform>().rotation);
                Audio.Play();
                EnemyTankTurret.GetComponent<Rigidbody>().AddForce(new Vector3(20, 300, 20));
                barTime = 0.0f; // 바로 상호작용하는것을 막기위해 barTime 초기화
                enemyIsAlive = false;
                st.isLoad = false;
                st.killCount++; // SceneSetting의 killCount를 1 추가
                GameObject b = GameObject.Find("bullet_low (8)(Clone)");
                Destroy(b);
            }
            else {
                Debug.Log("But, No load yet");
            }
        }
        else if (!enemyIsAlive) 
        {
            Destroy(gameObject);
            barTime = 0.0f;
            st.isEnemy = false; //적전차가 파괴되었고 게임상에서도 삭제되었음을 SceneSetting에게 알림
            
        }
    }

    void GotoAim() {
        
        Debug.Log("Goto Aim");
        Player.transform.position = new Vector3(72.67604f, -2.164f, 13.46046f);
        PlayerTurret.SetActive(true);
        GunnerCanvas.SetActive(true);
        
    }
    void ExitScreen()
    {
        Debug.Log("Exit Screen");
        Player.transform.position = new Vector3(0.928f, 3.499f, 0.018f);
        PlayerTurret.SetActive(false);
        GunnerCanvas.SetActive(false);
    }
    void OnLoad()
    {
        copyBullet = Instantiate(Bullet,Bullet.transform.position,Bullet.transform.rotation);
        copyBulletMove1 = true;

    }
    void BulletRot() 
    {
        copyBullet.transform.rotation = Quaternion.Slerp(copyBullet.transform.rotation, Quaternion.Euler(new Vector3(-90, 0, 0)), 0.01f);
    }
    void recuperator()
    {
        if (st.recuperatorOpened)
        {
            st.recuperatorOpened = false;
            recuperatorClose = true;
        }
        else if (!st.recuperatorOpened) 
        {
            st.recuperatorOpened = true;
            recuperatorOpen = true;
        }
    }
}