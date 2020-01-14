using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffGreenBird : Bird
{
    public override void showSkill()
    {
        base.showSkill();
        Vector3 speed = rg.velocity;
        speed.x *= -1f;
        rg.velocity = speed;
        rg.freezeRotation = false;
    }
}
