using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFinderAttack : MonoBehaviour
{
    private FPS_Shooter shooter;
    private SpellCast caster;
    // Start is called before the first frame update
    void Start()
    {
        shooter = GetComponentInChildren<FPS_Shooter>();
        caster = GetComponentInChildren<SpellCast>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attack()
    {
        shooter.Shoot();
    }

    public void Cast()
    {
        caster.AnimationEnd();
    }
}
