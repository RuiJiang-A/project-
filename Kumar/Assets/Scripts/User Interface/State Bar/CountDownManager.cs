using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDownManager : MonoBehaviour
{
    [SerializeField] float duration = 3f;
    [SerializeField] float remaining = 0f;

    Image fillMaskImageRef;
    // Start is called before the first frame update
    void Start()
    {
        fillMaskImageRef = gameObject.GetComponent<Image>();
        StartCoroutine(CountDown());
    }

    public void StartCountDown(float duration)
    {
        ExtendState(duration - 3f);
    }

    IEnumerator CountDown()
    {
        remaining = duration;
        while (remaining > 0)
        {
            remaining -= Time.deltaTime;
            fillMaskImageRef.fillAmount = remaining / duration;
            yield return null;
        }
        StateBarManager sb = gameObject.transform.GetComponentInParent<StateBarManager>();
        sb.RemoveState(this);
    }

    public void ExtendState(float extention)
    {
        duration += extention;
        remaining += extention;
    }
}
