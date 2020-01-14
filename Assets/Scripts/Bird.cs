using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    
    public float maxDis = 1.5f;
    public Transform toolRightPos;
    public Transform toolLeftPos;
    public LineRenderer rightLine;
    public LineRenderer leftLine;
    public GameObject boom;
    public Sprite hurt;

    public AudioClip collisionClip;
    public AudioClip selectClip;
    public AudioClip flyClip;

    private bool isClick = false;
    [HideInInspector]
    public SpringJoint2D sp;
    protected Rigidbody2D rg;
    protected TestMyTrail myTrail;
    protected SpriteRenderer render;
    private bool isplay = false;
    private bool canMove = false;
    private bool isfly = false;

    private void Awake()
    {
        sp = transform.GetComponent<SpringJoint2D>();
        rg = transform.GetComponent<Rigidbody2D>();
        myTrail = transform.GetComponent<TestMyTrail>();
        render = transform.GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        canMove = true;
    }

    void Update()
    {
        if (isClick)
        {
            Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition); // 鼠标坐标转换成世界坐标
            point.z = transform.position.z;
            transform.position = point;

            if (Vector3.Distance(transform.position, toolRightPos.position) > maxDis)
            {
                Vector3 pos = (transform.position - toolRightPos.position).normalized; //单位化向量
                pos *= maxDis;
                transform.position = pos + toolRightPos.position;
            }

            lineShow();   
        }


        //相机跟随
        float posX = transform.position.x;
        Camera.main.transform.SetPositionAndRotation(
            Vector3.Lerp(Camera.main.transform.position, new Vector3(Mathf.Clamp(posX,0,13.2f), Camera.main.transform.position.y, Camera.main.transform.position.z),0.3f),
            Quaternion.identity);

        if (isfly)
        {
            if (Input.GetMouseButtonDown(0))
            {
                showSkill();
            }
        }
    }

    private void OnMouseDown()
    {
        if (!canMove) return;
        rg.isKinematic = true;
        isClick = true;
        playAudio(selectClip);
    }

    protected virtual void Next()
    {
        if (gameObject)
        {
            GameManager._interface.birds.Remove(this);
            Instantiate(boom, transform.position, Quaternion.identity);
            Destroy(gameObject);
            GameManager._interface.NextBird();
        }
    }

    private void OnMouseUp()
    {
        if (!canMove) return;
        rg.isKinematic = false;
        isClick = false;
        Invoke("Fly", 0.2f);
        lineHide();
        canMove = false; 
    }
    private void Fly()
    {
        sp.enabled = false;
        myTrail.showTrail();
        isplay = true;
        playAudio(flyClip);
        isfly = true;
        Invoke("Next", 7.5f);
    }

    private void lineShow()
    {
        rightLine.SetPosition(0, toolRightPos.position);
        rightLine.SetPosition(1, transform.position);

        leftLine.SetPosition(0, toolLeftPos.position);
        leftLine.SetPosition(1, transform.position);
    }

    private void lineHide()
    {
        rightLine.SetPosition(0, toolRightPos.position);
        rightLine.SetPosition(1, toolRightPos.position);

        leftLine.SetPosition(0, toolLeftPos.position);
        leftLine.SetPosition(1, toolLeftPos.position);
    }

    private void OnCollisionEnter2D()
    {
        myTrail.hideTrail();
        if (isplay)
        {
            Invoke("Next", 2.5f);
            playAudio(collisionClip);
            render.sprite = hurt;
        }
    }

    public void playAudio(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, transform.position);
    }

    public virtual void showSkill()
    {
        isfly = false;
        
    }

}
