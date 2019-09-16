using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainScreenAnimations : MonoBehaviour
{
    [SerializeField]
    private Image nutridayLogo;

    [SerializeField]
    private Image nutridayDron;

    private void OnEnable()
    {
        nutridayLogo.transform.localScale = Vector3.zero;
        nutridayDron.transform.localScale = Vector3.zero;

        LeanTween.scale(nutridayLogo.gameObject, Vector3.one, 0.8f).setEase(LeanTweenType.easeOutBounce).setOnComplete(AnimateDrone);
    }

    private void AnimateDrone ()
    {
        LeanTween.scale(nutridayDron.gameObject, Vector3.one, 0.8f).setEase(LeanTweenType.easeOutBounce).setOnComplete(DroneLoop);
    }

    private void DroneLoop ()
    {
        LeanTween.moveLocalY(nutridayDron.gameObject,-5f, 0.8f).setLoopPingPong();
    }

}
