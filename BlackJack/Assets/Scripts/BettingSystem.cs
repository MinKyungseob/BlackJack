using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class BettingSystem : MonoBehaviour
{
    [SerializeField] int startMoney = 50;
    int currentMoney;
    [SerializeField] TMP_Text moneyText;
    void Start()
    {
        currentMoney = startMoney;
    }
}
