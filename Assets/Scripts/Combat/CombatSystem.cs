using System.Collections;
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
        yield return new WaitForSeconds(protagonistAttack.length/2);
        enemy.Health -= protagonist.Attack;
        if (enemy.Health <= 0)
        {
            EventManager.EventTrigger("OnEnemyDefeated");
        }
        else
        {
        yield return new WaitForSeconds(protagonistAttack.length/2-0.3f);
            StartCoroutine(DoEnemyCombat());
        }
    }
    IEnumerator DoEnemyCombat()
    {
        enemy.PlayAttackAnimation();
        yield return new WaitForSeconds(enemyAttack.length/2);
        protagonist.Health -= enemy.Attack;
        if (protagonist.Health <= 0)
        {
            EventManager.EventTrigger("OnProtagonistDefeated");
        }
        else
        {
        yield return new WaitForSeconds(enemyAttack.length/2-0.1f);
            StartCoroutine(DoProtagonistCombat());
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
