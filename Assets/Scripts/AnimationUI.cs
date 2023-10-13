using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationUI : MonoBehaviour
{
    [SerializeField] private Transform[] orignTranPos;
    [SerializeField] private Vector3[] orignPos;
    // [SerializeField] private GameObject[] allCard;
    [SerializeField] private Transform targetPos;
    private GameObject placeIm;

    [ContextMenu("Set")]
    public void SetVector()
    {
        orignPos = new Vector3[orignTranPos.Length];

        for (int i = 0; i < orignTranPos.Length; i++)
        {
            orignPos[i] = orignTranPos[i].position;
        }
    }
    public void Hover(GameObject gameObject)
    {
        LeanTween.scale(gameObject, new Vector3(1.2f, 1.2f, 0f), .5f).setEase(LeanTweenType.easeOutElastic);
    }

    public void ExitHover(GameObject gameObject)
    {
        LeanTween.scale(gameObject, Vector3.one, .5f).setEase(LeanTweenType.easeOutElastic);
    }

    public void MoveToTarget(GameObject myCard)
    {
        if (placeIm != null)
        {
            for (int i = 0; i < orignPos.Length; i++)
            {
                if (placeIm.name == orignTranPos[i].gameObject.name)
                {
                    Debug.Log("Change card..");
                    LeanTween.move(placeIm, orignPos[i], .5f).setEase(LeanTweenType.easeOutSine);
                    LeanTween.move(myCard, targetPos, .5f).setEase(LeanTweenType.easeOutSine).setOnComplete(() =>
                    {
                        placeIm = myCard;
                    });

                    //  Debug.Log(allCard[i] + "______" + orignPos[i]);
                }

            }
        }
        else
        {
            placeIm = myCard;
            LeanTween.move(myCard, targetPos, .5f).setEase(LeanTweenType.easeOutSine);

        }
    }
    public void MoveToOrigin()
    {
        if (placeIm != null)
        {
            for (int i = 0; i < orignPos.Length; i++)
            {
                if (placeIm.name == orignTranPos[i].gameObject.name)
                {
                    Debug.Log("MoveBack..");
                    LeanTween.move(placeIm, orignPos[i], .5f).setEase(LeanTweenType.easeOutSine);

                    //  Debug.Log(allCard[i] + "______" + orignPos[i]);
                }

            }
        }
    }
    public void ScaleBigImCom(GameObject gameObject)
    {
        LeanTween.scale(gameObject, new Vector3(1.2f, 1.2f, 1.2f), .3f).setEase(LeanTweenType.easeOutElastic).setOnComplete(() =>
        {
            LeanTween.scale(gameObject, Vector3.one, .3f);
        });

    }

    public void ScaleSmallImCom(GameObject gameObject)
    {
        LeanTween.scale(gameObject, new Vector3(0, 0, 0), .3f);
    }

    public void AnimationPressText(GameObject pressText)
    {
        LeanTween.scale(pressText, new Vector3(1.2f, 1.2f, 1.2f), .3f).setEase(LeanTweenType.easeInOutSine).setOnComplete(() =>
        {
            LeanTween.scale(pressText, Vector3.one, .3f).setEase(LeanTweenType.easeInOutSine);
        }).setLoopPingPong();
    }
}
