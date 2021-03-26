using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public string playerName;
    public int playerAttack;
    public int playerDefence;
    public int playerDamage; //weapon damage?
    public int maxPlayerHP;
    public int currentPlayerHP;
    public int playerCurrency;

    public Text currencyUI;
    //public Image playerHP;
    public Text playerHealth;
    public Slider playerHPslider;


    // Start is called before the first frame update
    void Start()
    {
        currentPlayerHP = maxPlayerHP;
    }

    // Update is called once per frame
    void Update()
    {
        //playerHP.fillAmount = (currentPlayerHP / maxPlayerHP);
        playerHPslider.value = currentPlayerHP;
        playerHPslider.maxValue = maxPlayerHP;
        //update UI
        currencyUI.text = "Currency: " + playerCurrency.ToString();

        if (currentPlayerHP < 0)
        {
            currentPlayerHP = 0;
        }

        
        playerHealth.text = currentPlayerHP.ToString();
        
    }


}
