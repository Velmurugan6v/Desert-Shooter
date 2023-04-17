using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    [Header("Enemy Var")]
    public Rigidbody rb;
    public float MoveSpeed;


    [Header("New Setting For Enemy")]
    [SerializeField] float attackRange;
    [SerializeField] float shootDistance;
    [SerializeField] Animator anim;

    [Header("Enemy Shoot Var")]
    [SerializeField] float timeBTWfire;
    [SerializeField] float fireCounter;

    public bool isDeath;
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        fireCounter = timeBTWfire;
    }

    void Update()
    {
        /*if (!isDeath)
        {
            if (!chasing)
            {
                Vector3 targetPos = new Vector3(PlayerMovement.instance.transform.position.x, 0, PlayerMovement.instance.transform.position.z);

                if (Vector3.Distance(transform.position, targetPos) < chasingDistance)
                {
                    chasing = true;
                }
            }
            else
            {
                LookAtPlayer();
                anim.SetBool("IsWalk", true);
                rb.velocity = transform.forward * MoveSpeed;

                if (Vector3.Distance(transform.position, PlayerMovement.instance.transform.position) > stopDistance)
                {
                    chasing = false;
                    anim.SetBool("IsWalk", false);
                }
            }

            Debug.DrawRay(transform.position, transform.forward * 10);

        }
        else
        {
            return;
        }*/
        if(Vector3.Distance(transform.position, PlayerMovement.instance.transform.position) < attackRange)
        {
            
            if (Vector3.Distance(transform.position, PlayerMovement.instance.transform.position) <= shootDistance)
            {
                LookAtPlayer();
                rb.velocity = transform.forward * 0;
                
                //Shoot Fun
                if (fireCounter <= 0)
                {
                    print("Shoot");
                    anim.SetTrigger("Shoot");
                    fireCounter = timeBTWfire;
                }
                else
                {
                    fireCounter -= Time.deltaTime;
                }

            }
            else
            {
                LookAtPlayer();
                anim.SetBool("IsWalk", true);
                rb.velocity = transform.forward * MoveSpeed;
            }
        }
        else
        {
            anim.SetBool("IsWalk", false);
        }
    }

    public void EnemyDeath()
    {
        anim.SetTrigger("Die");
    }
    void LookAtPlayer()
    {
        transform.LookAt(PlayerMovement.instance.transform);
    }

}
