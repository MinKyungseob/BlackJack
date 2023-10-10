using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class MessageSystem : MonoBehaviour
{
    public static MessageSystem instance;
    [SerializeField] TMP_Text messageText;

    private void Awake()
    {
        instance = this;
    }

    public void SetMessage(string message)
    {
        messageText.text = message;
    }
}
