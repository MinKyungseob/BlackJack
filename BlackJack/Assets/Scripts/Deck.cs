using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;

public class Deck : MonoBehaviour
{
    [System.Serializable]
    public class DeckCard
    {
        public string number;
        public string symbol;

        public DeckCard(string _number, string _symbol)
        {
            number = _number;
            symbol = _symbol;
        }
    }

    public List<DeckCard> cardDeck = new List<DeckCard>();

    string[] numberList = new string[13] { "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A" };
    string[] symbolList = new string[4] { "spades", "clover", "diamonds", "hearts" };

    public GameObject cardPrefab;
    [SerializeField] Transform playerHand;
    [SerializeField] Transform dealerHand;
    // Start is called before the first frame update
    void Start()
    {
        InitializeDeck();
        ShuffleDack(cardDeck);
        HandOutCards(true);
        HandOutCards(false);
    }

    void InitializeDeck()
    {
        for (int i = 0; i < symbolList.Length; i++) 
        {
            for (int j = 0; j < numberList.Length; j++)
            {
                cardDeck.Add(new DeckCard(numberList[j], symbolList[i]));
            }
        }
    }

    void ShuffleDack<T>(IList<T> list)
    {
        int n = list.Count;
        System.Random range = new System.Random();
        while (n > 1) 
        {
            n--;
            int val = range.Next(n + 1);
            T value = list[val];
            list[val] = list[n];
            list[n] = value;
        }
    }

    void HandOutCards(bool isPlayer)
    {
        for (int i = 0; i < 2; i++)
        {
            GameObject newCard = Instantiate(cardPrefab);
            DeckCard temp = cardDeck[0];
            newCard.GetComponent<Card>().SetUpCard(temp.number, temp.symbol, isPlayer);    //PLAYER's
            cardDeck.Remove(temp);
            newCard.transform.SetParent(isPlayer?playerHand:dealerHand, false);
        }
    }
}
