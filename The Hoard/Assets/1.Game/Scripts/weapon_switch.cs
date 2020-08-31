//Dylan G
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon_switch : MonoBehaviour 
{

    public int selected_weapon = 0;

    // Start is called before the first frame update
    void Start()
    {
        select_weapon();
    }



    public void select_weapon()
    {
        int i = 0;
        foreach(Transform weapon in transform)
        {
            if(i == selected_weapon)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            i++;
        }
    }
}
