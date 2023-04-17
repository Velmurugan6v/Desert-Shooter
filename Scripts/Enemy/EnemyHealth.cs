using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    
    [Header("Enemy Health Var")]
    public int health;
    //Update Health mean Take Damage
    public void UpdateHealth(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            EnemyDie();
        }
    }

    void EnemyDie()
    {
        AIenemy newEnemy = GetComponent<AIenemy>();
        newEnemy.EnemyDeath();
        GetComponent<Collider>().enabled = false;
        GetComponent<Rigidbody>().useGravity = false;
        GameController.instance.enemies.Remove(newEnemy);
        Uimanager.instance.UpdateSoldierCount();
        newEnemy.enabled = false;
        Invoke("EnemyWanish", 3f);
    }

    public void EnemyWanish()
    {
        Destroy(this.gameObject);
    }
}
