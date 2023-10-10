using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using UnityEngine.UI;

public class BettingSystem : MonoBehaviour
{
    public static BettingSystem instance;

    [SerializeField] int startMoney = 50;
    int currentMoney;
    int currentBet;
    [SerializeField] TMP_Text moneyText;
    [SerializeField] GameObject betPanel;
    [SerializeField] Button fiveCoin;
    [SerializeField] Button tenCoin;
    [SerializeField] Button twentyfiveCoin;
    [SerializeField] Button fiftyCoin;
    [SerializeField] Button hundredCoin;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        currentMoney = startMoney;
        UpdateMoneyAmount();
        ActivateBetPanel(true); //Or Let the gamemanger do it
    }

    void UpdateMoneyAmount()
    {
        moneyText.text = currentMoney + " Coins";
    }

    public void ActivateBetPanel(bool on)
    {
        betPanel.SetActive(on);
        if (on)
        {
            /*if (currentMoney >= 5) fiveCoin.interactable = true;
            if (currentMoney >= 10) tenCoin.interactable = true;
            if (currentMoney >= 25) twentyfiveCoin.interactable = true;
            if (currentMoney >= 50) fiftyCoin.interactable = true;
            if (currentMoney >= 100) hundredCoin.interactable = true;*/
            fiveCoin.interactable = (currentMoney >= 5) ? true : false;
            tenCoin.interactable = (currentMoney >= 10) ? true : false;
            twentyfiveCoin.interactable = (currentMoney >= 25) ? true : false;
            fiftyCoin.interactable = (currentMoney >= 50) ? true : false;
            hundredCoin.interactable = (currentMoney >= 100) ? true : false;
        }
    }

    public void SetBet(int bet)
    {
        //Reset Last Bet
        currentMoney += currentBet;
        //Plave the New Bet
        currentBet = bet;
        currentMoney -= bet;
        UpdateMoneyAmount();
    }

    public void Deal()
    {
        if(currentBet>0)
        {
            //Start Round
            GameManager.instance.GetCards();
            ActivateBetPanel(false);
        }
        else
        {
            //Message the player to create a bet
        }
    }

    public void RoundResult(string result)
    {
        switch(result)
        {
            case "win":
                break;
            case "lose":
                break;
            case "draw":
                break;
        }
    }
}
