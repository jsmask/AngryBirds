using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BlackBird : Bird
{
    
    public List<Pig> blocks = new List<Pig>();

    /// <summary>
    /// 进入触发区域
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            blocks.Add(collision.gameObject.GetComponent<Pig>());
        }
    }

    /// <summary>
    /// 离开触发区域
    /// </summary>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            blocks.Remove(collision.gameObject.GetComponent<Pig>());
        }
    }

    

    public override void showSkill()
    {
        base.showSkill();
        if (blocks.Count > 0 && blocks != null)
        {
            for (int i = 0; i < blocks.Count; i++)
            {
                try { blocks[i].Dead(); }
                catch { }
            }
        }
       OnClear();
    }

    void OnClear()
    {
        rg.velocity = Vector3.zero;
        Instantiate(boom, transform.position, Quaternion.identity);
        render.enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;
        myTrail.hideTrail();
        Invoke("Next", 2.5f);
    }

    protected override void Next()
    {
        GameManager._interface.birds.Remove(this);
        Destroy(gameObject);
        GameManager._interface.NextBird();
    }


    
}
