using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [SerializeField] AIenemy enemy;
    // Start is called before the first frame update
    public void Shoot()
    {
        enemy.Shoot();
    }
}
