using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    public GameManager GM;
    public int maxDamage;
    public int uses = 0; // attach a text in both store and weapon UI for this

    //public GameObject thisObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (maxDamage > 4)
        {
            if (uses <= 0)
            {
                this.gameObject.SetActive(false);
            }


            
        }

        
    }

    public void RollDamage(int maxDamage)
    {
        //GM.rollDamage.SetActive(false);
        //GM.attackRoll.enabled = false;
        //GM.resultDisplay.enabled = false;
        //GM.defenceRoll.enabled = false;
        uses--;
        GM.weaponUI.SetActive(false);
        int damage = Random.Range(1, maxDamage);
        Debug.Log("damage is " + damage);
        
        if (GM.state == GameState.ENEMY)
        {
            GM.TakeDamage(damage);
        }

        else if (GM.state == GameState.PLAYER)
        {
            GM.DealDamage(damage);
        }

        //GM.rollDamage.SetActive(false);
        
            
    }
}
