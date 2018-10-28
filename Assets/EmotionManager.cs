using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Tilemaps;
using System.Linq;
using DG.Tweening;

public class EmotionManager : MonoBehaviour {

    public static EmotionManager Instance;

    public float TimeBetweenChecks = 5f;
    public FaceAttributes faceAttributes; 

    public List<EmotionPalette> emotionPalettes = new List<EmotionPalette>();
    public List<Tilemap> tilemaps = new List<Tilemap>();
    public Dictionary<string, float> emotions;
    public Color[] colors = new Color[5] { Color.white, Color.white, Color.white, Color.white, Color.white };
    public GameObject glasses;
    public GameObject beard;
    public GameObject moustache;
    public GameObject sideburns;
    private string currentEmotion;

    [System.Serializable]
    public struct EmotionPalette
    {
        public string name;
        public Color[] colors;
    }

    private void Awake()
    {
        if (Instance != null) Destroy(this);
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        StartCoroutine(EmotionClock());
    }

    private IEnumerator EmotionClock()
    {
        yield return new WaitForSeconds(1f);
        while (true)
        {
            Texture2D tex = ImageCapture.Instance.SnapshotToTex();
            StartCoroutine(AzureVisionManager.Instance.TakeImageAnalysis(tex));
            yield return new WaitForSeconds(TimeBetweenChecks);
        }
    }

    public void EmotionsUpdated()
    {
        emotions = faceAttributes.emotion.emotionDict;
        glasses.SetActive(faceAttributes.glasses != "NoGlasses");
        beard.SetActive(faceAttributes.facialHair.beard > 0.5f);
        moustache.SetActive(faceAttributes.facialHair.moustache > 0.5f);
        sideburns.SetActive(faceAttributes.facialHair.sideburns > 0.5f);
    
        string emotion = faceAttributes.emotion.emotionDict.Aggregate((l, r) => l.Value > r.Value ? l : r).Key;
        if (currentEmotion != emotion)
        {
            currentEmotion = emotion;
            Color[] blendedPalette = new Color[5] { Color.black, Color.black, Color.black, Color.black, Color.black };

            foreach (KeyValuePair<string, float> pair in faceAttributes.emotion.emotionDict)
            {
                EmotionPalette emotionPalette = emotionPalettes.First(p => p.name == emotion);
                for (int i = 0; i < 5; i++)
                {
                    blendedPalette[i] += emotionPalette.colors[i] * pair.Value;
                }
            }

            for (int i = 0; i < 5; i++)
            {
                Tilemap map = tilemaps[i];
                Color color = blendedPalette[i];
                DOTween.To(() => map.color, x => map.color = x, color, 0.5f);
            }
        }
    }
}
