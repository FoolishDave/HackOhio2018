using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CamSwitchTrigger : MonoBehaviour {

    private static List<CinemachineVirtualCamera> cameras;
    public CinemachineVirtualCamera cameraToFocus;
    private void Awake()
    {
        if (cameras == null)
        {
            cameras = Resources.FindObjectsOfTypeAll<CinemachineVirtualCamera>().ToList();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        cameras.ForEach(cam => cam.Priority = 0);
        if (cameraToFocus != null)
        {
            cameraToFocus.Priority = 1;
        }else
            GetComponentInParent<CinemachineVirtualCamera>().Priority = 1;
    }
}
