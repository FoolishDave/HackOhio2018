using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class OpeningScene : MonoBehaviour {

    RawImage img;
    public DialogueManager mgr;

	// Use this for initialization
	void Start () {
        img = GetComponent<RawImage>();
        img.color = Color.black;
        StartCoroutine(StartCutscene());
	}
	
	IEnumerator StartCutscene()
    {
        yield return new WaitForSeconds(2.5f);
        mgr.Queue("Hello?", 3f);
        mgr.Queue("You in there?", 4f);
        mgr.Queue("Blink if you can hear me.", 3.5f);
        mgr.sequence.Append(img.DOFade(0.6f, 0.4f));
        mgr.sequence.AppendInterval(0.2f);
        mgr.sequence.Append(img.DOFade(1f, 0.2f));
        mgr.sequence.AppendInterval(0.2f);
        mgr.sequence.Append(img.DOFade(0f, 0.4f));
        mgr.sequence.AppendCallback(() => mgr.GetComponentInChildren<TextMeshProUGUI>().color = new Color(0, 0, 0, 0));
    }
}
