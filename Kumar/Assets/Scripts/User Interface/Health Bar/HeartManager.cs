using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manage Heart Component (v1.0) 
/// by Rui Jiang 2022-07-16
/// 
/// Require a Fill Image to as normal displayment
/// & a Hidden Image for delaying effct.
/// & a Healing Image for healing effect
/// </summary>

public class HeartManager : MonoBehaviour
{
    [Header("Props.")]
    // max amount of the heart
    const byte maxHealth = 4;
    // current amount of the heart
    [SerializeField] byte currentHealth = 0;
    // health to be healed
    [SerializeField] byte healingHealth = 0;

    [Header("Refs.")]
    [SerializeField] Image fillImageRef;
    [SerializeField] Image hiddenImageRef;
    [SerializeField] Image healingImageRef;
    // Start is called before the first frame update
    void Start()
    {
        // TODO: handle by HealthBar Manager later
        currentHealth = maxHealth;
        // find requried components
        fillImageRef = gameObject.transform.Find("Fill Image").gameObject.GetComponent<Image>();
        hiddenImageRef = gameObject.transform.Find("Hidden Image").gameObject.GetComponent<Image>();
        healingImageRef = gameObject.transform.Find("Healing Image").gameObject.GetComponent<Image>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && currentHealth > 0)
            UpdateHealth(1);
        else if (Input.GetMouseButtonDown(1) && currentHealth < 4)
            UpdateHealingHealth(1);
    }

    // update health displayment
    public void UpdateHealth(byte damage)
    {
        // Debug.Log("@UpdateHealth()");
        currentHealth -= damage;
        float finalPercentage = (float)currentHealth / (float)maxHealth;
        fillImageRef.fillAmount = finalPercentage;
        StartCoroutine(DecreaseHiddenImage(finalPercentage));
        StartCoroutine(DecreaseHealingImage(finalPercentage));
    }

    // update healing health displayment
    public void UpdateHealingHealth(byte heal)
    {
        // Debug.Log("@UpdateHealingHealth()");
        healingHealth += heal;
        StartCoroutine(IncreaseHealingImage((float)healingHealth / maxHealth));
    }

    // decrease Hidden Image with simple animation
    IEnumerator DecreaseHiddenImage(float percentage, float decreaseSpeed = 0.002f)
    {
        // Debug.Log("@DecreaseHiddenImage()");
        yield return new WaitForSeconds(0.2f);
        while (hiddenImageRef.fillAmount > percentage)
        {
            hiddenImageRef.fillAmount -= decreaseSpeed;
            yield return new WaitForSeconds(0.004f);
        }
    }

    // increase Healing Image with simple animation
    IEnumerator IncreaseHealingImage(float percentage, float increaseSpeed = 0.002f)
    {
        // Debug.Log("@IncreaseHealingImage()");
        yield return new WaitForSeconds(0.4f);
        while (healingImageRef.fillAmount < fillImageRef.fillAmount + percentage)
        {
            healingImageRef.fillAmount += increaseSpeed;
            yield return new WaitForSeconds(0.002f);
        }
    }

    // decrease Healing Image with simple animation
    IEnumerator DecreaseHealingImage(float percentage, float decreaseSpeed = 0.002f)
    {
        // Debug.Log("@DecreaseHealingImage()");
        yield return new WaitForSeconds(0.4f);
        while (healingImageRef.fillAmount > fillImageRef.fillAmount + (float)healingHealth / maxHealth)
        {
            healingImageRef.fillAmount -= decreaseSpeed;
            yield return new WaitForSeconds(0.002f);
        }
    }
}
