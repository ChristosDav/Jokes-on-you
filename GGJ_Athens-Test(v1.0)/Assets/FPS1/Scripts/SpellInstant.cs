using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellInstant : Spell
{
    [SerializeField] Transform casterT;
    bool follow;
    public override void Setup(SpellCast caster)
    {
        base.Setup(caster);

        follow = true;
    }

    public override void Activate()
    {
        base.Activate();

        follow = false;
    }

    void Update()
    {
        if (!follow) return;
        Quaternion q = casterT.rotation;
        q.x = 0;
        q.z = 0;
        transform.rotation = q;
        //Debug.Log(q);
    }
}
