using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class DialogueManager : MonoBehaviour {

    public TextMeshProUGUI narratorText;

    public float transitionTime = 0.5f;
    public Sequence sequence;

    public void Display(string message, float displayTime)
    {
        sequence.Complete();
        Queue(message, displayTime);
    }

    public void Display(List<KeyValuePair<string, float>> messages)
    {
        sequence.Complete();
        Queue(messages);
    }

    public void Queue(string message, float displayTime)
    {
        if (sequence == null)
        {
            sequence = DOTween.Sequence();
            sequence.OnKill(() => sequence = null); 
        }
        sequence.AppendCallback(() => Debug.Log("Displaying: " + message));
        sequence.AppendCallback(() => narratorText.text = message);
        sequence.Append(narratorText.DOFade(1f, transitionTime/2));
        sequence.AppendInterval(displayTime);
        sequence.Append(narratorText.DOFade(0f, transitionTime / 2));
    }

    public void Queue(List<KeyValuePair<string, float>> messages)
    {
        foreach (KeyValuePair<string, float> pairs in messages)
        {
            Queue(pairs.Key, pairs.Value);
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

[System.Serializable]
public class EmotionDialogue
{
    public string dialogue;
    public float length;
    public string emotion;
    public float emotionThresh;
}