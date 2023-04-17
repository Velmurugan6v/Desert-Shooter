using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : MonoBehaviour
{
    public static Animation instance;

    [SerializeField] Animator gunAnim;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void GunLoadAnimation()
    {
        gunAnim.Play("Pistol_Load");
    }

    public void GunShootAnimation()
    {
        gunAnim.Play("Pistol_Shoot_anim");
    }
}
