using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControll : MonoBehaviour
{
    [Header("New Setting For Enemy")]
    [SerializeField] Rigidbody _rb;
    [SerializeField] float chasingDistance;
    [SerializeField] float stopDistance;
    [SerializeField] bool chasing;
    [SerializeField] float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!chasing)
        {
            if (Vector3.Distance(transform.position, PlayerMovement.instance.transform.position) < chasingDistance)
            {
                chasing = true;
            }
        }
        else
        {
            transform.LookAt(PlayerMovement.instance.transform.position);
            _rb.velocity = transform.forward * moveSpeed;
            if (Vector3.Distance(transform.position, PlayerMovement.instance.transform.position) > stopDistance)
            {
                chasing = false;
            }
        }
    }
}
