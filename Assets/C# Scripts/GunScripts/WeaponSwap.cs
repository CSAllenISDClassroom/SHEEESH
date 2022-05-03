using System.Collections;
using UnityEngine;

public class WeaponSwap : MonoBehaviour
{

    public int selectedWeapon;

    // Start is called before the first frame update
    public void Start()
    {
        StartCoroutine(SelectWeapon());
    }

    // Update is called once per frame
    public void Update()
    {
        int previousSelectedWeapon = selectedWeapon;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedWeapon = 0;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
        {
            selectedWeapon = 1;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3)
        {
            selectedWeapon = 2;
        }

        if (Input.GetKeyDown(KeyCode.Alpha4) && transform.childCount >= 4)
        {
            selectedWeapon = 3;
        }

        if (previousSelectedWeapon != selectedWeapon)
        {
            StartCoroutine(SelectWeapon());
        }
    }

    IEnumerator SelectWeapon()
    {
        int i = 0;
        yield return new WaitForSeconds(.56f);
        foreach (Transform weapon in transform)
        {
            if (i == selectedWeapon)
            {
                
                weapon.gameObject.SetActive(true);
            }
            else
                weapon.gameObject.SetActive(false);
            i++;
        }
    }
}
