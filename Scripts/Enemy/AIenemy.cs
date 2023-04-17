using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIenemy : MonoBehaviour
{
    [SerializeField] NavMeshAgent navMeshAgent;
    [SerializeField] Transform targetPos;
    [SerializeField] Animator _animation;

    [SerializeField] bool followPlayer = false;
    [SerializeField] bool checkPlayer = false;
    [SerializeField] float range;
    [SerializeField] LayerMask playerLayer;

    [SerializeField] float timeBTWshoot;
    [SerializeField] float shootCounter;
    [SerializeField] ParticleSystem _shootFX;

    // Start is called before the first frame update
    void Start()
    {
        _animation.SetBool("StopWalk", true);
        shootCounter = timeBTWshoot;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerMovement.instance.isActiveAndEnabled)
        {
            checkPlayer = Physics.CheckSphere(transform.position, range, playerLayer);

            if (checkPlayer)
            {
                _animation.SetBool("StopWalk", false);
                followPlayer = !followPlayer;
                range = 0f;
            }

            if (followPlayer)
            {
                navMeshAgent.SetDestination(PlayerMovement.instance.transform.position);

                if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
                {
                    if (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude <= 1f)
                    {
                        _animation.SetBool("Attack", true);
                        transform.LookAt(PlayerMovement.instance.transform);

                        /*if (shootCounter <= 0)
                        {
                            print("shoot");
                            Shoot();
                            shootCounter = timeBTWshoot;
                        }
                        else
                        {
                            shootCounter -= Time.deltaTime;
                        }*/
                    }
                    else
                    {
                        _animation.SetBool("StopWalk", false);
                    }
                }
                else
                {
                    _animation.SetBool("Attack", false);
                }
            }
        }
        else
        {
            _animation.SetBool("StopWalk", true);
        }
        
    }

    public void EnemyDeath()
    {
        //VFXManager.instance.DeathFX();
        _animation.SetTrigger("Die");
    }

    public void EnemyHitAnimation()
    {
        _animation.SetTrigger("Hit");
    }
    public void Shoot()
    {
        VFXManager.instance.EnemyFx(0);
        _shootFX.Play();
        Uimanager.instance.UpdatePlayerHealth(25);
    }
}
