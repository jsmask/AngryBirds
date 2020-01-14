using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : MonoBehaviour
{
    public float maxSpeed = 10;
    public float minSpeed = 3;
    public Sprite hurt;
    public GameObject boom;
    public GameObject pigScore;
    public bool isPig = false;

    public AudioClip pigDeadClip;
    public AudioClip pigCollisionClip;
    public AudioClip woodCollisionClip;
    public AudioClip woodDeadClip;

    private SpriteRenderer render;


    private void Awake()
    {
        render = transform.GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.magnitude >= maxSpeed)
        {       
            Dead();
        }
        else if(collision.relativeVelocity.magnitude < maxSpeed && collision.relativeVelocity.magnitude >= minSpeed)
        {
            maxSpeed = Mathf.Max(maxSpeed - collision.relativeVelocity.magnitude, minSpeed);
            render.sprite = hurt;
            if (isPig)
            {
                playAudio(pigCollisionClip);
            }
            else
            {
                playAudio(woodCollisionClip);
            }
        }
    }

    public void Dead()
    {
        if (isPig)
        {
            GameManager._interface.pigs.Remove(this);
            Instantiate(pigScore, transform.position + new Vector3(0, 0, 0), Quaternion.identity);
            playAudio(pigDeadClip);
        }
        else
        {
            playAudio(woodDeadClip);
        }
       
        Instantiate(boom, transform.position, Quaternion.identity);
        Destroy(gameObject);
        
    }

    public void playAudio(AudioClip clip)
    {
        try
        {
            AudioSource.PlayClipAtPoint(clip, transform.position);
        }
        catch
        {

        }
        
    }

}
