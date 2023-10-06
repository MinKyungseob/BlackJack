using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class GameManager : MonoBehaviour
{
    List<Card> playerCards = new List<Card>();
    List<Card> dealerCards = new List<Card>();
    [SerializeField] TMP_Text playerPointsText;
    [SerializeField] TMP_Text dealerPointsText;
    // Start is called before the first frame update
    void Start()
    {
        InitializeGame();
    }

    void InitializeGame()
    {
        Deck.instance.Initailize();
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

        CountPlayerPoints();
        //CountDealerPoints();
    }

    void CountDealerPoints()
    {
        int points = 0;
        for (int i = 0; i < dealerCards.Count; i++)
        {
            points += dealerCards[i].GetPoints();
        }
        dealerPointsText.text = points.ToString();

    }

    void CountPlayerPoints()
    {
        int points = 0;
        for(int i=0;i<playerCards.Count;i++)
        {
            points += playerCards[i].GetPoints();
        }
        playerPointsText.text = points.ToString();
        //IF WE HAVE LOST DO SOMETHING HERE
    }

    public void Hit()
    {
        playerCards.Add(Deck.instance.HandOutCards(true));
        CountPlayerPoints();
        //IF WE HAVE LOST DO SOMETHING HERE

    }
    public void Stay() //GIVE AI THE DALER THE TURN
    {
    }
}
