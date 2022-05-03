using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;
    public float impactForce = 30f;

    public float reloadSoundDelay;
    public int weaponPlacementInList = 0;
    public int maxAmmo = 10;
    protected int currentAmmo;
    private int selectedWeapon;
    public float reloadTime = 1f;
    private bool isReloading = false;
    private bool isFiring = false;
    public bool isAutomatic;
    private bool isSwitching = false;

    public bool isPaused = false;
    public Camera fpsCam;
    private PlayerHUD hud;
    public GameObject impactEffect;

    private float nextTimeToFire = 0f;

    public Animator animator;
    
    private void Awake()
    {
        hud = GameObject.Find("HUD").GetComponent<PlayerHUD>();
    }
    void Start()
    {
        
        currentAmmo = maxAmmo;
    }

    void OnEnable()
    {
        isFiring = false;
        animator.SetBool("Firing", false);
        isReloading = false;
        animator.SetBool("Reloading", false);
        


    }

 
    public void gamePaused()
    {
        if (isPaused == true)
        {
           // hud.enabled = false;
            isPaused = false;
        }
        else
        {
           // hud.enabled = true;
            isPaused = true;
        }
    }

    // Update is called once per frame
    public void Update()
    {
        //Weapon Reload
        if (isReloading)
            return;

        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isPaused == true)
                isPaused = false;
            else
                isPaused = true;
        }

        if (Input.GetKeyDown(KeyCode.R) && currentAmmo != maxAmmo)
        {
            StartCoroutine(Reload());
        }

        //Weapon switching detection 
        if (isSwitching == false)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) && weaponPlacementInList != 0)
            {
                selectedWeapon = 0;

            }
            if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2 && weaponPlacementInList != 1)
            {
                selectedWeapon = 1;


            }

            if (Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3 && weaponPlacementInList != 2)
            {
                selectedWeapon = 2;
            }

            if (Input.GetKeyDown(KeyCode.Alpha4) && transform.childCount >= 4 && weaponPlacementInList != 3)
            {
                selectedWeapon = 3;
            }
        }
        if (weaponPlacementInList != selectedWeapon)
        {
            
            StartCoroutine(Switching());
            
        }
        hud.UpdateAmmo(currentAmmo, maxAmmo);

        if (isPaused == false)
        {
            if (isAutomatic == true)
            {
                if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire && isReloading == false && isSwitching == false)
                {

                    nextTimeToFire = Time.time + 1.5f / fireRate;
                    StartCoroutine(Shoot());
                }
            }
            if (isAutomatic != true)
            {
                if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire && isReloading == false && isSwitching == false)
                {
                    nextTimeToFire = Time.time + 1.5f / fireRate;
                    StartCoroutine(Shoot());
                }
            }
        }
    }

    IEnumerator Switching()
    {
        isSwitching = true;

        animator.SetBool("Switching", true);
        yield return new WaitForSeconds(.25f);
        animator.SetBool("Switching", false);
        yield return new WaitForSeconds(.5f);

        selectedWeapon = weaponPlacementInList;
        
        isSwitching = false;
        if (selectedWeapon == 1)
            FindObjectOfType<AudioManager>().Play("Kriss Vector Draw");
        yield return new WaitForSeconds(.5f);


        if (weaponPlacementInList == selectedWeapon)
            hud.UpdateAmmo(currentAmmo, maxAmmo);

    } 

    IEnumerator Reload()
    {
        isReloading = true;

        animator.SetBool("Reloading", true);

        yield return new WaitForSeconds(reloadSoundDelay);

        if (selectedWeapon == 0)
            FindObjectOfType<AudioManager>().Play("FN57 Reload");
        if (selectedWeapon == 1)
            FindObjectOfType<AudioManager>().Play("Kriss Vector Reload");


        animator.SetBool("Reloading", false);
        yield return new WaitForSeconds(.25f);

        currentAmmo = maxAmmo;
        if (currentAmmo == maxAmmo)
            hud.UpdateAmmo(currentAmmo, maxAmmo);
        isReloading = false;
    }

    public virtual IEnumerator Shoot()
    {
        isFiring = true;
        if (selectedWeapon == 0)
            FindObjectOfType<AudioManager>().Play("FN57 Fire");
        if (selectedWeapon == 1)
            FindObjectOfType<AudioManager>().Play("Kriss Vector Fire");
        animator.SetBool("Firing", true);


        
        currentAmmo--;
        if (Input.GetButton("Fire1"))
            hud.UpdateAmmo(currentAmmo, maxAmmo);
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));

            Destroy(impactGO, .5f);

        }

        yield return new WaitForSeconds(.01f);
        animator.SetBool("Firing", false);
        yield return new WaitForSeconds(.01f);
        isFiring = false;
    }
}
