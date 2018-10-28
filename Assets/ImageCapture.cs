using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageCapture : MonoBehaviour {

    public static ImageCapture Instance;
    public RawImage rawImage;
    public RawImage previousCapture;

    private WebCamTexture webTex;

    private void Awake()
    {
        if (Instance != null) Destroy(this);
        else
        {
            Instance = this;
            webTex = new WebCamTexture();
            //rawImage.texture = webTex;
            //rawImage.material.mainTexture = webTex;
            webTex.Play();
        }
    }
	
    public Texture2D SnapshotToTex()
    {
        Texture2D snapshot = new Texture2D(webTex.width, webTex.height);
        snapshot.SetPixels(webTex.GetPixels());
        snapshot.Apply();
        return snapshot;
    }

	// Update is called once per frame
	void Update () {
	}
}
