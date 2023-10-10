using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using UnityEngine.UI;

public class BettingSystem : MonoBehaviour
{
    public static BettingSystem instance;

    [SerializeField] int currentMoney = 50;
    int currentBet;
    [SerializeField] TMP_Text moneyText;
    [SerializeField] GameObject betPanel;
    [SerializeField] Button fiveCoin;
    [SerializeField] Button tenCoin;
    [SerializeField] Button twentyfiveCoin;
    [SerializeField] Button fiftyCoin;
    [SerializeField] Button hundredCoin;

    [SerializeField] GameObject gameOverUI;

    bool lostGame;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        UpdateMoneyAmount();
        ActivateBetPanel(true); //Or Let the gamemanger do it
        gameOverUI.SetActive(false);
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
            currentBet = 0;
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
        MessageSystem.instance.SetMessage("Your Bet is:<color=yellow>" + currentBet + " Coins</color>");
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
            MessageSystem.instance.SetMessage("<color=red>Place a Bet first</color>, before you deal");
        }
    }

    public void RoundResult(string result, bool isBlackJack)
    {
        switch (result)
        {
            case "win":
                //Let the player knows he got Win
                currentMoney += currentBet * 2;
                UpdateMoneyAmount();
                if (isBlackJack)
                {
                    MessageSystem.instance.SetMessage("<color=black>Black</color> <color=red>Jack</color>");
                }
                else
                {
                    MessageSystem.instance.SetMessage("You <color=yellow>Won</color> this Round!");
                }
                break;
            case "lose":
                //Let the player knows he got Lost
                MessageSystem.instance.SetMessage("You <color=red>Lost</color> this Round!");
                //If Game Over
                if (currentMoney <= 0)
                {
                    lostGame = true;
                    MessageSystem.instance.SetMessage("<color=red>Game Over</color>");
                }
                break;
            case "draw":
                currentMoney += currentBet;
                UpdateMoneyAmount();
                MessageSystem.instance.SetMessage("You <color=blue>Draw</color> this Round!");
                break;
        }
        //ActivateBetPanel(true);
        //Reset all stuff in game manager and Card Deck
        if (!lostGame)
        {
            GameManager.instance.ActivateStartNewRound();
        }
        else
        {
            //Show UI or Button to back to menu
            gameOverUI.SetActive(true);
        }
    }
}
