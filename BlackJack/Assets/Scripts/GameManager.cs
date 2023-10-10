using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    List<Card> playerCards = new List<Card>();
    List<Card> dealerCards = new List<Card>();
    [SerializeField] TMP_Text playerPointsText;
    [SerializeField] TMP_Text dealerPointsText;
    [SerializeField] GameObject hitStayPanel;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        hitStayPanel.SetActive(false);
        InitializeGame();
    }

    void InitializeGame()
    {
        Deck.instance.Initailize();
        BettingSystem.instance.ActivateBetPanel(true);
        MessageSystem.instance.SetMessage("Welcome to <b><color=black>Black Jack</color></b>.\nPlace Your Bet");
    }

    public void GetCards()
    {
        StartCoroutine(GetInitialCards());
    }

    IEnumerator GetInitialCards()
    {
        playerCards.Add(Deck.instance.HandOutCards(true));
        CountPlayerPoints();
        yield return new WaitForSeconds(0.4f);
        dealerCards.Add(Deck.instance.HandOutCards(false));
        yield return new WaitForSeconds(0.4f);
        playerCards.Add(Deck.instance.HandOutCards(true));
        CountPlayerPoints();
        yield return new WaitForSeconds(0.4f);
        dealerCards.Add(Deck.instance.HandOutCards(false));
        yield return new WaitForSeconds(0.4f);

        CalcuateResult(true, true);
        //CountDealerPoints();
        hitStayPanel.SetActive(true);
    }

    int CountDealerPoints()
    {
        int points = 0;
        for (int i = 0; i < dealerCards.Count; i++)
        {
            points += dealerCards[i].GetPoints();
        }
        dealerPointsText.text = points.ToString();
        return points;
    }

    int CountPlayerPoints()
    {
        int points = 0;
        for(int i=0;i<playerCards.Count;i++)
        {
            points += playerCards[i].GetPoints();
        }
        playerPointsText.text = points.ToString();
        //IF WE HAVE LOST DO SOMETHING HERE
        return points;
    }

    public void Hit()
    {
        playerCards.Add(Deck.instance.HandOutCards(true));
        CalcuateResult(true, false);
        //IF WE HAVE LOST DO SOMETHING HERE

    }
    public void Stay() //GIVE AI THE DALER THE TURN
    {
        StartCoroutine(DealerTurn());
        //hitStayPanel.SetActive(false);
    }

    IEnumerator DealerTurn()
    {
        RevealDealerCards();
        int score = CountDealerPoints();
        int scoreToStop = 17;
        while (score < scoreToStop)         //Basic rule of Dealer. if the dealer's deck count is under 17, then must hit. else stay. https://namu.wiki/w/%EB%B8%94%EB%9E%99%EC%9E%AD(%EC%B9%B4%EB%93%9C%EA%B2%8C%EC%9E%84)#s-4.5
        {
            MessageSystem.instance.SetMessage("<color=red>Dealer</color> is thinking");
            yield return new WaitForSeconds(Random.Range(0.7f,1.5f));
            MessageSystem.instance.SetMessage("<color=red>Dealer</color> draws card");
            dealerCards.Add(Deck.instance.HandOutCards(false));
            score = CountDealerPoints();
            RevealDealerCards();
        }
        //Give Out The Result
        CalcuateResult(false, false);
        //Let The Betting System Know
    }

    void CalcuateResult(bool playerRequest, bool isInitial)
    {
        int playerScore = CountPlayerPoints();
        if (playerRequest && isInitial)
        {
            if (playerScore == 21)
            {
                //Win- Black Jack
                hitStayPanel.SetActive(false);
                BettingSystem.instance.RoundResult("win", true);
                return;
            }

            if (playerScore > 21)
            {
                //Lose
                hitStayPanel.SetActive(false);
                BettingSystem.instance.RoundResult("lose", false);
                return;
            }
        }
        else
        {
            int dealerScore = CountDealerPoints();
            
            if (dealerScore > 21)
            {
                //Win
                BettingSystem.instance.RoundResult("win", false);
            }
            if (dealerScore == playerScore)
            {
                //Draw
                BettingSystem.instance.RoundResult("draw", false);
            }
            if (dealerScore < playerScore && playerScore <= 21)
            {
                //Win
                BettingSystem.instance.RoundResult("win", false);
            }
            if (dealerScore > playerScore && dealerScore <= 21)
            {
                //Lose
                BettingSystem.instance.RoundResult("lose", false);
            }
        }
        //Reset the game manager or set the game over
    }


    public void ActivateStartNewRound()
    {
        Invoke("StartNewRound", 3f);
    }

    public void StartNewRound()
    {
        Deck.instance.ResetCardDeck();

        foreach(var card in playerCards)
        {
            Destroy(card.gameObject);
        }

        foreach(var card in dealerCards)
        {
            Destroy(card.gameObject);
        }

        playerCards.Clear();
        dealerCards.Clear();

        BettingSystem.instance.ActivateBetPanel(true);
        playerPointsText.text = "--";
        dealerPointsText.text = "--";
    }

    void RevealDealerCards()
    {
        foreach(var card in dealerCards)
        {
            card.ReavealCard();
        }
    }
}
