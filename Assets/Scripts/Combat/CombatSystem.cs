using System.Collections;
using System;
using System.Collections.Generic;
using UnityEditor.U2D.Common;
using UnityEngine;

public class CombatSystem : Singleton<CombatSystem>
{

    public GameObject EnemyObject;
    public GameObject Protagonist;
    public Protagonist protagonist;
    public Enemy enemy;

    public AnimationClip protagonistAttack;
    public AnimationClip enemyAttack;
    private void Start()
    {
        Protagonist = GameObject.FindGameObjectWithTag("Player");
        protagonist = Protagonist.GetComponent<Protagonist>();
    }

    public void SetEnemyByName(string EnemyName)
    {
        EnemyObject = GameObject.Find(EnemyName);
        enemy = EnemyObject.GetComponent<Enemy>();
    }

    public void StartCombat()
    {
        EventManager.AddEventListener("OnProtagonistDefeated",ProtangonistDefeated);
        EventManager.AddEventListener("OnEnemyDefeated",EnemyDefeated);
        StartCoroutine(DoProtagonistCombat());
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

    IEnumerator DoProtagonistCombat()
    {
        protagonist.AttackAnimation();
        enemy.Health -= protagonist.Attack;
        yield return new WaitForSeconds(protagonistAttack.length);
        if (protagonist.Health <= 0)
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
        protagonist.Health -= enemy.Attack;
        yield return new WaitForSeconds(enemyAttack.length);
        if (protagonist.Health <= 0)
        {
            EventManager.EventTrigger("OnProtagonistDefeated");
        }
        else
        {
            StartCoroutine(DoEnemyCombat());
        }
    }
    void ProtangonistDefeated()
    {
        protagonist.ProtagonistDefeated();
        protagonist.OnProtangonistDie?.Invoke();
    }
    void EnemyDefeated()
    {
        enemy.PlayDieAnimation();
        EventManager.EventTrigger("CombatEnd");
    }
}
