using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    public string enemyName;
    public int enemyAttack;
    public int enemyDefence;
    public int enemyDamage; //weapon damage?
    public int maxEnemyHP;
    public int currentEnemyHP;
    public int enemyCurrency;

    //public Image enemyHP;
    public Slider enemyHPslider;
    public Text enemyHealth;
 
    // Start is called before the first frame update
    void Start()
    {
        currentEnemyHP = maxEnemyHP;
    }

    // Update is called once per frame
    void Update()
    {
        //enemyHP.fillAmount = (currentEnemyHP / maxEnemyHP);
        enemyHPslider.value = currentEnemyHP;
        enemyHPslider.maxValue = maxEnemyHP;

        //update UI

        if (currentEnemyHP < 0)
        {
            currentEnemyHP = 0;
        }

        
        enemyHealth.text = currentEnemyHP.ToString();
    }

    
}
