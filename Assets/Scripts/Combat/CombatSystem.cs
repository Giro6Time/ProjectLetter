using System.Collections;
using System;
using System.Collections.Generic;
using UnityEditor.U2D.Common;
using UnityEngine;

public class CombatSystem : Singleton<CombatSystem>
{

    public GameObject EnemyObject;
    public GameObject ProtangonistObject;
    public Protangonist protangonist;
    public Enemy enemy;

    AnimationClip q;
    private void Start()
    {
        ProtangonistObject = GameObject.FindGameObjectWithTag("Player");
        protangonist = ProtangonistObject.GetComponent<Protangonist>();
    }

    public void SetEnemyByName(string EnemyName)
    {
        EnemyObject = GameObject.Find(EnemyName);
        enemy = EnemyObject.GetComponent<Enemy>();
    }

    public void StartCombat()
    {
        EventManager.AddEventListener("OnProtangonistDefeated",ProtangonistDefeated);
        EventManager.AddEventListener("OnEnemyDefeated",EnemyDefeated);
        StartCoroutine(DoProtangonistCombat());
       /*while(enemy.Health > 0 && protangonist.Health > 0)
        {
            //进行血量扣减的结算
            protangonist.AttackAnimation();
            enemy.Health -= protangonist.Attack;
            if (enemy.Health <= 0) break;
            enemy.PlayAttackAnimation();
            protangonist.Health -= enemy.Attack;
        }*/
    }

    IEnumerator DoProtangonistCombat()
    {
        protangonist.AttackAnimation();
        enemy.Health -= protangonist.Attack;
        yield return new WaitForSeconds(q.length);
        if (protangonist.Health <= 0)
        {
            EventManager.EventTrigger("OnEnemyDefeated");
        }
        else
        {
            StartCoroutine(DoEnemyCombat());
        }
    }
    IEnumerator DoEnemyCombat()
    {
        enemy.PlayAttackAnimation();
        protangonist.Health -= enemy.Attack;
        yield return new WaitForSeconds(q.length);
        if (protangonist.Health <= 0)
        {
            EventManager.EventTrigger("OnProtangonistDefeated");
        }
        else
        {
            StartCoroutine(DoEnemyCombat());
        }
    }
    void ProtangonistDefeated()
    {
        protangonist.ProtangonistDefeated();
        protangonist.OnProtangonistDie?.Invoke();
    }
    void EnemyDefeated()
    {
        enemy.PlayDieAnimation();
        EventManager.EventTrigger("CombatEnd");
    }
}
