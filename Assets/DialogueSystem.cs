using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueSystem : MonoBehaviour
{
    public GameObject DialogueBox;
    public TextMeshProUGUI DialogueDisplayedText;
    public GameObject ContinueArrow;
    public List<Conversation> ConversationList;

    private Conversation _currentConversation;
    private int _currentIndex;

    public void Start()
    {
        StartConversation("DialogueTest");
    }

    public void StartConversation(string conversationName)
    {
        if (_currentConversation != null) return; //We're still in a conversation!

        ContinueArrow.SetActive(true);

        foreach (Conversation conversation in ConversationList)
        {
            if (conversation.ConversationName.ToLower().Trim() == conversationName.ToLower().Trim())
            {
                _currentConversation = conversation;
            }
        }

        _currentIndex = 0;
        DialogueDisplayedText.text = _currentConversation.Text[0];
        DialogueBox.SetActive(true);
        ShowHideContinueArrow();
    }

    public void ContinueConversation()
    {
        _currentIndex++;
        if (_currentIndex == _currentConversation.Text.Count)
        {
            EndConversation();
        } else
        {
            DialogueDisplayedText.text = _currentConversation.Text[_currentIndex];
            ShowHideContinueArrow();
        }
    }

    public void ShowHideContinueArrow()
    {
        ContinueArrow.SetActive(_currentConversation != null && _currentIndex < _currentConversation.Text.Count-1);
    }

    public void EndConversation()
    {
        DialogueBox.SetActive(false);
        DialogueDisplayedText.text = "";
       _currentConversation = null;
    }
}
