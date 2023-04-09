using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CamStyle
{
    OribitalCam = 0,
    TRFCam,
}

public class UnitCamController : MonoBehaviour
{
    public Animator CamAnim;

    [SerializeField] private CinemachineVirtualCamera OribitalCam;
    [SerializeField] private CinemachineVirtualCamera TRFCam;


    public void ActiveCamera(CamStyle style)
    {
        DisActiveCamera();

        switch (style)
        {
            case CamStyle.OribitalCam:
                OribitalCam.gameObject.SetActive(true);
                CamAnim.SetTrigger("tOrbital");
                break;
            case CamStyle.TRFCam:
                TRFCam.gameObject.SetActive(true); ;
                CamAnim.SetTrigger("tTPF");
                break;
            default:
                break;
        }
    }

    public void DisActiveCamera()
    {
        OribitalCam.gameObject.SetActive(false);
        OribitalCam.gameObject.SetActive(false);
    }  

}
