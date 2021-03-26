using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreManager : MonoBehaviour
{
    public GameManager GM;
    public PlayerManager PM;
    public GameObject d6;
    public GameObject d8;
    public GameObject d10;

    public int attackModCost = 4;
    public int defenceModCost = 4;
    public int hpCost = 7;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BuyWeapon(int cost)
    {
        if (cost > PM.playerCurrency)
        {
            GM.notEnough.SetActive(true);
            StartCoroutine(DeactivateNE());
            Debug.Log("not enough currency"); //make text say it!! coroutine to remove
        }

        else
        {
            PM.playerCurrency -= cost;
            if (cost == 6)
            {
                WeaponDamage WD = d6.GetComponent<WeaponDamage>();
                WD.uses += 2;
                if (WD.uses > 0)
                {
                    d6.SetActive(true);
                }
                Debug.Log(cost + " has " + WD.uses + " uses");
            }

            if (cost == 8)
            {
                WeaponDamage WD = d8.GetComponent<WeaponDamage>();
                WD.uses += 2;
                if (WD.uses > 0)
                {
                    d8.SetActive(true);
                }
                Debug.Log(cost + " has " + WD.uses + " uses");
            }

            if (cost == 10)
            {
                WeaponDamage WD = d10.GetComponent<WeaponDamage>();
                WD.uses += 2;
                if (WD.uses > 0)
                {
                    d10.SetActive(true);
                }
                Debug.Log(cost + " has " + WD.uses + " uses");
            }

        }
        
    }

    public void BuyAttackMod()
    {
        if (attackModCost > PM.playerCurrency)
        {
            GM.notEnough.SetActive(true);
            StartCoroutine(DeactivateNE());
            Debug.Log("not enough currency"); //make text say it!! coroutine to remove
        }

        else
        {
            PM.playerCurrency -= attackModCost;
            GM.attackMod++;
            attackModCost++;
            GM.atkModCost.text = "Cost: " + attackModCost.ToString() + " currency";
            GM.attackModText.text = "ATK bonus: +" + GM.attackMod.ToString();
        }
    }

    public void BuyDefenceMod()
    {
        if (defenceModCost > PM.playerCurrency)
        {
            GM.notEnough.SetActive(true);
            StartCoroutine(DeactivateNE());
            Debug.Log("not enough currency"); //make text say it!! coroutine to remove
        }

        else
        {
            PM.playerCurrency -= defenceModCost;
            GM.defenceMod++;
            defenceModCost++;
            GM.defModCost.text = "Cost: " + defenceModCost.ToString() + " currency";
            GM.defenceModText.text = "DEF bonus: +" + GM.defenceMod.ToString();
        }
    }

    public void BuyHealth()
    {
        if (hpCost > PM.playerCurrency)
        {
            GM.notEnough.SetActive(true);
            StartCoroutine(DeactivateNE());
            Debug.Log("not enough currency"); //make text say it!! coroutine to remove
        }
        
        //else if health is already on 20

        else
        {
            PM.playerCurrency -= hpCost;

            if (PM.currentPlayerHP <= 16)
            {
                PM.currentPlayerHP += 4;
            }

            else
            {
                PM.currentPlayerHP = 20;
            }

            hpCost++;
            GM.healthCost.text = "Cost: " + hpCost.ToString() + " currency";
        }
    }

    IEnumerator DeactivateNE()
    {
        yield return new WaitForSeconds(1);
        GM.notEnough.SetActive(false);
    }
}
