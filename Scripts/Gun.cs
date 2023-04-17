using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    //Gun Settings
    public float fireRate = 0.1f;
    public int clipSize = 7;
    public int reservedAmmoCapacity = 50;

    //Gun Variables
    private bool _canShoot;
    private int _currentAmmoInClip;
    private int _ammoInReserve;

    [Header("Aim Var")]
    public Vector3 normalGunPosition;
    public Vector3 aimPosition;
    public int aimSpeed;

    [Header("Gun Recoil")]
    [SerializeField] bool recoil;
    [SerializeField] bool camRecoil;

    //Bullet Damage
    [SerializeField] int _bulletDamage;
    [SerializeField] ParticleSystem _shootVFX;

    //TPP Aiming
    public Vector3 normalTppPosition;
    public Vector3 tppAimPosition;
    [SerializeField] ParticleSystem _ttpShootVFX;

    // Start is called before the first frame update
    void Start()
    {
        _currentAmmoInClip = clipSize;
        _ammoInReserve = reservedAmmoCapacity;
        _canShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerMovement.instance._ttp)
        {
            TppAiming();
        }
        else
        {
            Aimimg();
        }

        //When i press MLB for Shoot
        if(Input.GetMouseButton(0) && _canShoot && _currentAmmoInClip > 0)
        {
            //Shoot Animation
            Animation.instance.GunShootAnimation();
            _canShoot = false;
            _currentAmmoInClip--;
            Uimanager.instance.UpdateAmmo(_currentAmmoInClip, _ammoInReserve);
            StartCoroutine(Shoot());
        }

        //Reload
        else if(Input.GetKeyDown(KeyCode.R) && _currentAmmoInClip < clipSize && _ammoInReserve>0)
        {
            //Pistol Load Anim
            VFXManager.instance.PlayerFX(1);
            Animation.instance.GunLoadAnimation();
            int ammoNeed = clipSize - _currentAmmoInClip;

            if (ammoNeed >= _ammoInReserve)
            {
                _currentAmmoInClip += _ammoInReserve;
                _ammoInReserve -= ammoNeed;
            }
            else
            {
                _currentAmmoInClip = clipSize;
                _ammoInReserve -= ammoNeed;
            }

            if (_ammoInReserve <= 0)
            {
                _ammoInReserve = 0;
            }

            Uimanager.instance.UpdateAmmo(_currentAmmoInClip, _ammoInReserve);
        }

        //Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward+new Vector3(0,0,20), Color.red);
    }

    IEnumerator Shoot()
    {
        

        if (PlayerMovement.instance._ttp)
        {
            PlayerMovement.instance._playerAnimator.SetTrigger("Shoot");
            RayCostShoot();
            Recoil();
            _ttpShootVFX.Play();
            VFXManager.instance.PlayerFX(0);
            yield return new WaitForSeconds(fireRate);
            _canShoot = true;
        }
        else
        {
            RayCostShoot();
            Recoil();
            _shootVFX.Play();
            VFXManager.instance.PlayerFX(0);
            yield return new WaitForSeconds(fireRate);
            _canShoot = true;
        }
    }

    void Aimimg()
    {
        Vector3 targetPosition;
        Vector3 desiredPosition;
        float targetAimValue;
        float desiredAimValue;

        if (Input.GetMouseButton(1))
        {
            targetPosition = aimPosition;
            targetAimValue = 45f;
        }
        else
        {
            targetPosition = normalGunPosition;
            targetAimValue = 60f;
        }

        desiredPosition = Vector3.Lerp(transform.localPosition, targetPosition, aimSpeed * Time.deltaTime);
        desiredAimValue = Mathf.Lerp(Camera.main.fieldOfView, targetAimValue, aimSpeed * Time.deltaTime);

        transform.localPosition = desiredPosition;
        Camera.main.fieldOfView = desiredAimValue;
    }

    void TppAiming()
    {
        Vector3 targetPosition;
        Vector3 desiredPosition;
        float targetAimValue;
        float desiredAimValue;

        if (Input.GetMouseButton(1))
        {
            targetPosition = tppAimPosition;
            targetAimValue = 45f;
        }
        else
        {
            targetPosition = normalTppPosition;
            targetAimValue = 60f;
        }

        desiredPosition = Vector3.Lerp(transform.localPosition, targetPosition, aimSpeed * Time.deltaTime);
        desiredAimValue = Mathf.Lerp(Camera.main.fieldOfView, targetAimValue, aimSpeed * Time.deltaTime);

        transform.localPosition = desiredPosition;
        Camera.main.fieldOfView = desiredAimValue;
    }

    void Recoil()
    {
        if (recoil)
        {
            transform.localPosition -= Vector3.forward * 0.2f;
        }
        else if (camRecoil)
        {
            Camera.main.transform.localPosition -= Vector3.up * 0.2f;
        }
    }

    void RayCostShoot()
    {
        RaycastHit hitInfo;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward,out hitInfo,15))
        {
            if (hitInfo.collider.gameObject.tag == "Enemy")
            {
                print("Enemy");
                EnemyHealth enemyHealth = hitInfo.collider.GetComponent<EnemyHealth>();
                enemyHealth.UpdateHealth(_bulletDamage);
            }
        }
    }
}
