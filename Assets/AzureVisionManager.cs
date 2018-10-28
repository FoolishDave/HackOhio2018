using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AzureVisionManager : MonoBehaviour {

    public static AzureVisionManager Instance;

    private string authKey = "aee2ea5f50ad43719216f9dff0827fcf";
    private const string subKeyHeader = "Ocp-Apim-Subscription-Key";
    private string visionEndpoint = "https://eastus.api.cognitive.microsoft.com/face/v1.0/detect?returnFaceId=false&returnFaceAttributes=age,gender,emotion,facialHair,glasses";

    private void Awake()
    {
        if (Instance != null) Destroy(this);
        else
        {
            Instance = this;
        }
    }

    public IEnumerator TakeImageAnalysis(Texture2D tex)
    {
        WWWForm webForm = new WWWForm();
        using (UnityWebRequest webReq = UnityWebRequest.Post(visionEndpoint, webForm))
        {
            byte[] imageBytes = tex.EncodeToPNG();
            webReq.SetRequestHeader("Content-Type", "application/octet-stream");
            webReq.SetRequestHeader("Accept", "application/json");
            webReq.SetRequestHeader(subKeyHeader, authKey);
            webReq.downloadHandler = new DownloadHandlerBuffer();
            webReq.uploadHandler = new UploadHandlerRaw(imageBytes);
            webReq.uploadHandler.contentType = "application/octet-stream";

            yield return webReq.SendWebRequest();

            long responseCode = webReq.responseCode;

            try
            {
                string jsonResponse = null;
                jsonResponse = webReq.downloadHandler.text;

                // The response will be in Json format
                // therefore it needs to be deserialized into the classes AnalysedObject and TagData
                AnalysedObject analysedObject = new AnalysedObject();
                analysedObject = JsonUtility.FromJson<AnalysedFaces>("{\"faces\": " + jsonResponse + "}").faces[0];

                if (analysedObject.faceAttributes == null)
                {
                    Debug.Log("analysedObject.tagData is null");
                }
                else
                {
                    EmotionManager.Instance.faceAttributes = analysedObject.faceAttributes;
                    EmotionManager.Instance.EmotionsUpdated();
                }
            }
            catch (Exception exception)
            {
                Debug.Log("Json exception.Message: " + exception.Message);
            }
            yield return null;
        }
    }
}

[System.Serializable]
public class Emotion
{
    public Dictionary<string, float> emotionDict
    {
        get
        {
            return new Dictionary<string, float>
            {
                { "anger", anger },
                { "contempt", contempt },
                { "disgust", disgust },
                { "fear", fear },
                { "happiness", happiness },
                { "neutral", neutral },
                { "sadness", sadness },
                { "surprise", surprise }
            };
        }
    }

    public float anger;
    public float contempt;
    public float disgust;
    public float fear;
    public float happiness;
    public float neutral;
    public float sadness;
    public float surprise;
}

[System.Serializable]
public class FaceAttributes
{
    public string gender;
    public string glasses;
    public FacialHair facialHair;
    public int age;
    public Emotion emotion;
}

[System.Serializable]
public class FacialHair
{
    public float moustache;
    public float beard;
    public float sideburns;
}

[System.Serializable]
public class FaceRect
{
    public int top;
    public int left;
    public int width;
    public int height;
}

[System.Serializable]
public class AnalysedObject
{
    public FaceRect faceRectangle;
    public FaceAttributes faceAttributes;
}

[System.Serializable]
public class AnalysedFaces
{
    public List<AnalysedObject> faces;
}
