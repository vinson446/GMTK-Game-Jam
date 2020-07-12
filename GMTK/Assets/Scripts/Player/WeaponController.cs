using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{

    public int selectedWeapon = 0;

    PlayerStats playerStats;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();

        SelectedWeapon();
    }

    // Update is called once per frame
    void Update()
    {

        int perviousSelectedWeapon = selectedWeapon;



        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (selectedWeapon >= transform.childCount - 1)
                selectedWeapon = 0;
            else
                selectedWeapon++; ;
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (selectedWeapon > 0)
                selectedWeapon--;
            else
                selectedWeapon = transform.childCount - 1;
        }

        if (perviousSelectedWeapon != selectedWeapon)
        {
            SelectedWeapon();
        }
    }



    void SelectedWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == selectedWeapon)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            i++;
        }

        if (selectedWeapon == 0)
        {
            Sword sword = transform.GetChild(0).GetComponent<Sword>();
            playerStats.UpdatePlayerStats(sword.damage);
        }
        else if (selectedWeapon == 1)
        {
            Axe axe = transform.GetChild(1).GetComponent<Axe>();
            playerStats.UpdatePlayerStats(axe.damage);
        }
        else if (selectedWeapon == 2)
        {
            Spear spear = transform.GetChild(2).GetComponent<Spear>();
            playerStats.UpdatePlayerStats(spear.damage);
        }
    }
}
