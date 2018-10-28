using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {

    public List<EmotionDialogue> dialogue;
    public DialogueManager dialMan;
    public bool playOnce;
    private bool played = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (played && playOnce) return;
        played = true; 
        for (int i = 0; i < dialogue.Count; i++)
        {
            if (dialogue[i].emotion == "" || EmotionManager.Instance.emotions[dialogue[i].emotion] > dialogue[i].emotionThresh)
            {
                dialMan.Queue(dialogue[i].dialogue, dialogue[i].length);
            }
        }
    }
}
