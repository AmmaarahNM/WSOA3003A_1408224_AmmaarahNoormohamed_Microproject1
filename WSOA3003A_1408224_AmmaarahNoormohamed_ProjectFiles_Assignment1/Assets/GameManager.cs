using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameState {START, PLAYER, ENEMY, WIN, LOSE }
public class GameManager : MonoBehaviour
{
    public GameState state;
    public GameObject player;
    public GameObject enemy;
    public PlayerManager PM;
    public EnemyManager EM;
    public StoreManager SM;

    public GameObject rollDamage;
    public bool isAttacking;
    public GameObject attackButton;
    public GameObject defendButton;
    public GameObject buyButton;
    public GameObject beginButton;

    public int enemyAttack;
    public Text whosTurn;
    public Text attackRoll;
    public Text defenceRoll;
    public Text resultDisplay;
    public Text currencyUpdate;
    public Text damageResult;

    public GameObject weaponUI;
    public GameObject storeUI;
    public GameObject notEnough;

    public int attackMod;
    public int defenceMod;

    public Text attackModText;
    public Text defenceModText;

    public Text healthCost;
    public Text atkModCost;
    public Text defModCost;

    public Text d6UsesStore;
    public Text d6Uses;
    public Text d8UsesStore;
    public Text d8Uses;
    public Text d10UsesStore;
    public Text d10Uses;

    public GameObject loseUI;
    public GameObject winUI;


    private void Start()
    {
        state = GameState.START;
    }

    private void Update()
    {
        d6UsesStore.text = d6Uses.text = SM.d6.GetComponent<WeaponDamage>().uses.ToString() + " uses left";
        d8UsesStore.text = d8Uses.text = SM.d8.GetComponent<WeaponDamage>().uses.ToString() + " uses left";
        d10UsesStore.text = d10Uses.text = SM.d10.GetComponent<WeaponDamage>().uses.ToString() + " uses left";

        if (state == GameState.START)
        {
            //Introtext set active
            beginButton.SetActive(true);
        }

        if (state == GameState.PLAYER)
        {
            whosTurn.text = "Player attack turn";
            //attackButton.SetActive(true);
            //defendButton.SetActive(false);

            //if (isAttacking)
            //{
            //    buyButton.SetActive(false);
            //}

            //else
            //{
            //    buyButton.SetActive(true);
            //}

        }

        if (state == GameState.ENEMY)
        {
            whosTurn.text = "Enemy attack turn";
            buyButton.SetActive(false);
            //attackButton.SetActive(false);
            //StartCoroutine(EnemyTurn());
        }

        if (state == GameState.LOSE)
        {
            whosTurn.enabled = false;
            loseUI.SetActive(true);

        }

        if (state == GameState.WIN)
        {
            whosTurn.enabled = false;
            winUI.SetActive(true);
        }

        
    }

    public void BeginGame()
    {
        //deactivate introtext
        beginButton.SetActive(false);
        state = GameState.PLAYER;
        whosTurn.enabled = true;
        attackButton.SetActive(true);
        buyButton.SetActive(true);
    }

    public void RollAttack()
    {
        StartCoroutine(RollForAttack());
    }

    IEnumerator RollForAttack()
    {
        isAttacking = true;
        attackButton.SetActive(false);
        buyButton.SetActive(false);
        int attackScore = Random.Range(1, 20) + attackMod; // plus any player attack bonus mods
        attackRoll.enabled = true;
        attackRoll.text = "Total attack roll: " + attackScore + " ... Enemy is rolling for defence";
        //Say enemy is rolling defence
        yield return new WaitForSeconds(2);

        int defenceScore = Random.Range(1, 20); //plus any enemy defence bonus mods
        defenceRoll.enabled = true;
        defenceRoll.text = "Defence roll: " + defenceScore;
        yield return new WaitForSeconds(2);

        resultDisplay.enabled = true;
        if (attackScore > defenceScore)
        {
            resultDisplay.text = "Attack successful! Roll for damage";
            PM.playerCurrency += (attackScore - defenceScore);
            currencyUpdate.enabled = true;
            currencyUpdate.text = "+" + (attackScore - defenceScore).ToString() + " to currency";
            rollDamage.SetActive(true);
        }

        else
        {
            resultDisplay.text = "Attack failed";
            isAttacking = false;
            

            if (attackScore < defenceScore)
            {
                EM.enemyCurrency += (defenceScore - attackScore);
                state = GameState.ENEMY;
                StartCoroutine(EnemyTurn());
            }

            else
            {
                state = GameState.ENEMY;
                StartCoroutine(EnemyTurn());
            }
            
        }
        
        yield return new WaitForSeconds(1);
        
        if (state == GameState.ENEMY)
        {
            resultDisplay.enabled = false;
            currencyUpdate.enabled = false;
            attackRoll.enabled = false;
            defenceRoll.enabled = false;
            damageResult.enabled = false;
        }
        
        
    }

    public void DealDamage(int damage)
    {
        currencyUpdate.enabled = false;
        damageResult.enabled = true;
        damageResult.text = "Enemy takes " + damage + " points of damage";
        EM.currentEnemyHP -= damage;
        isAttacking = false;
        StartCoroutine(EndAttack());
    }

    IEnumerator EndAttack()
    {
        yield return new WaitForSeconds(3);
        damageResult.enabled = false;
        if (EM.currentEnemyHP > 0)
        {
            state = GameState.ENEMY;
            StartCoroutine(EnemyTurn());
        }

        else
        {
            state = GameState.WIN;
        }
    }

    IEnumerator EnemyTurn()
    {
        //attackRoll.enabled = false;
        //Enemy choices randomised
        Debug.Log("Enemy turn begins");
        isAttacking = true;
        int attackScore = Random.Range(1, 20); //plus any enemy attack bonus mods
        enemyAttack = attackScore;

        yield return new WaitForSeconds(1.5f);
        attackRoll.enabled = true;
        attackRoll.text = "Attack roll: " + enemyAttack + " ...Roll for defence";
        defendButton.SetActive(true);
    }

    public void RollDefence()
    {
        StartCoroutine(RollForDefence());
    }

    IEnumerator RollForDefence()
    {
        int defenceScore = Random.Range(1, 20) + defenceMod;
        defenceRoll.enabled = true;
        defenceRoll.text = "Total defence roll: " + defenceScore;
        defendButton.SetActive(false);
        yield return new WaitForSeconds(2);

        resultDisplay.enabled = true;

        if (enemyAttack > defenceScore)
        {
            EM.enemyCurrency += (enemyAttack - defenceScore);
            int damage = Random.Range(1, 4);
            resultDisplay.text = "Failed to defend! You take " + damage + " points of damage";
            TakeDamage(damage);
        }

        else
        {
            resultDisplay.text = "Your defence holds strong";
            

            if (enemyAttack < defenceScore)
            {
                PM.playerCurrency += (defenceScore - enemyAttack);
                currencyUpdate.enabled = true;
                currencyUpdate.text = "+" + (defenceScore - enemyAttack).ToString() + " to currency";
                isAttacking = false;
                state = GameState.PLAYER;
            }

            else
            {
                isAttacking = false;
                state = GameState.PLAYER;
            }

            
        }

        yield return new WaitForSeconds(2);
        resultDisplay.enabled = false;
        currencyUpdate.enabled = false;
        attackRoll.enabled = false;
        defenceRoll.enabled = false;
        if (state == GameState.PLAYER)
        {
            attackButton.SetActive(true);
            buyButton.SetActive(true);
            //defendButton.SetActive(false);
        }
        
    }

    public void TakeDamage(int damage)
    {
        PM.currentPlayerHP -= damage;
        isAttacking = false;

        if (PM.currentPlayerHP > 0)
        {
            state = GameState.PLAYER;
        }

        else
        {
            state = GameState.LOSE;
        }
    }

    public void ChooseWeapon()
    {
        weaponUI.SetActive(true);
        rollDamage.SetActive(false);
        attackRoll.enabled = false;
        resultDisplay.enabled = false;
        defenceRoll.enabled = false;
    }

    public void BuyItem()
    {
        storeUI.SetActive(true);
        notEnough.SetActive(false);
        
    }

    public void ReturnToBattle()
    {
        storeUI.SetActive(false);
    }



}
