using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarManager : MonoBehaviour
{
    [Header("Props.")]
    // max health amount of the player (quaternary)
    [SerializeField] byte maxHealth = 20;
    // current health amount of the player
    [SerializeField] byte currentHealth = 0;
    // prefab to instantiate a heart
    [SerializeField] GameObject heartPrefab;
    [SerializeField] List<GameObject> hearts = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        // TODO: Load from the json date files
        currentHealth = maxHealth;
        initHealthBar();
    }

    void initHealthBar()
    {
        for (sbyte i = -1; i < maxHealth / 4 + 1; i++)
        {
            Vector3 instPos = gameObject.transform.position;
            instPos.x += i * 40;
            // instantiate hearts
            var heart = Instantiate(heartPrefab, instPos,
                Quaternion.identity, gameObject.transform);
            heart.name = "Heart " + i;
            hearts.Add(heart);
        }
        // disable the new first and last sibling
        setActiveForFirstAndLastSibling(false);
    }
/*

    void UpdateHeartPos()
    {
        for (sbyte i = 0; i < hearts.Count; i++)
        {
            Vector3 newPos = gameObject.transform.position;
            newPos.x += (i - 1) * 40;
            hearts[i].gameObject.GetComponent<RectTransform>().
                SetPositionAndRotation(newPos, Quaternion.identity);
        }
    }

    private void Update()
    {
        byte damage = 1;
        if (Input.GetKeyDown(KeyCode.D))
        {
            TakeDamge(damage);
        }
    }

    public void TakeDamge(byte damage)
    {
        int temp = damage / 4;
        int count = damage % 4 == 0 ? temp : temp + 1;
        Debug.Log(count);
        for (byte i = 0; i < count; i++) { 
            ShiftLeft(damage);
            damage -= 4;
        }
    }

    public void ShiftLeft(byte damage)
    {
        // shift objs
        List<GameObject> tempHearts = new List<GameObject>();
        // enable the first and last sibling first
        setActiveForFirstAndLastSibling(true);
        // shift them to the right
        tempHearts.Add(hearts[0]);
        hearts[0].gameObject.GetComponent<RectTransform>().SetAsLastSibling();
        hearts.RemoveAt(0);
        // add it to the list
        hearts.AddRange(tempHearts);
        // disable the new first and last sibling
        setActiveForFirstAndLastSibling(false);
        UpdateHeartPos();
    }
*/
    void setActiveForFirstAndLastSibling(bool isActive)
    {
        hearts[0].SetActive(isActive);
        hearts[maxHealth / 4 + 1].SetActive(isActive);
    }
}
