using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;

public class Card : MonoBehaviour
{
    [SerializeField] Sprite cardBack;
    [SerializeField] Sprite cardFront;
    [SerializeField] Image cardBackground;
    [SerializeField] GameObject spades;
    [SerializeField] GameObject clovers;
    [SerializeField] GameObject diamonds;
    [SerializeField] GameObject hearts;
    [SerializeField] TMP_Text numberText;

    int pointsOfCard;
    string symbol;
    string number;

    public void SetUpCard(string _number, string _symbol,bool isPlayer)
    {
        number = _number;
        numberText.text = number;
        symbol = _symbol;
        //VISUALIZE CONTENT
        switch(_symbol)
        {
            case "spades":
                spades.SetActive(true);
                clovers.SetActive(false);
                diamonds.SetActive(false);
                hearts.SetActive(false);
                break;
            case "clovers":
                spades.SetActive(false);
                clovers.SetActive(true);
                diamonds.SetActive(false);
                hearts.SetActive(false);
                break;
            case "diamonds":
                spades.SetActive(false);
                clovers.SetActive(false);
                diamonds.SetActive(true);
                hearts.SetActive(false);
                break;
            case "hearts":
                spades.SetActive(false);
                clovers.SetActive(false);
                diamonds.SetActive(false);
                hearts.SetActive(true);
                break;
        }

        //CALCULATE POINTS
        switch (_number)
        {
            case "2":
                pointsOfCard = 2;
                break;
            case "3":
                pointsOfCard = 3;
                break;
            case "4":
                pointsOfCard = 4;
                break;
            case "5":
                pointsOfCard = 5;
                break;
            case "6":
                pointsOfCard = 6;
                break;
            case "7":
                pointsOfCard = 7;
                break;
            case "8":
                pointsOfCard = 8;
                break;
            case "9":
                pointsOfCard = 9;
                break;
            case "10":
                pointsOfCard = 10;
                break;
            case "J":
                pointsOfCard = 10;
                break;
            case "Q":
                pointsOfCard = 10;
                break;
            case "K":
                pointsOfCard = 10;
                break;
            case "A":
                pointsOfCard = 11;
                break;
        }

        if(!isPlayer)
        {
            cardBackground.sprite = cardBack;
            numberText.gameObject.SetActive(false);
            spades.transform.parent.gameObject.SetActive(false);
        }
    }

    public int GetPoints()
    {
        return pointsOfCard;
    }
    
}
