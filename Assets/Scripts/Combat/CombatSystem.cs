using System.Collections;
using System;
using System.Collections.Generic;
using UnityEditor.U2D.Common;
using UnityEngine;

public class CombatSystem : MonoBehaviour
{
    public GameObject EnemyObject;
    public GameObject ProtangonistObject;
    public Protangonist protangonist;
    public Enemy enemy;

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
        
        while(enemy.Health > 0 && protangonist.Health > 0)
        {
            //进行血量扣减的结算
            protangonist.AttackAnimation();
            enemy.Health -= protangonist.Attack;
            if (enemy.Health <= 0) break;
            enemy.PlayAttackAnimation();
            protangonist.Health -= enemy.Attack;
        }
    }

    IEnumerator DoCombat()
    {

        yield return null;
    }

}
