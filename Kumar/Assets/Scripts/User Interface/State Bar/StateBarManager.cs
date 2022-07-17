using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StateBarManager : MonoBehaviour
{
    [Header("Props.")]
    [SerializeField] Image[] icons = null;
    [SerializeField] GameObject statePrefab;
    // [SerializeField] byte stateCount = 0;
    [SerializeField] List<CountDownManager> states = new List<CountDownManager>();
    // Start is called before the first frame update
    void Start()
    {

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
            CreateState();
    }

    // TODO: Add index of the icon
    // TODO: use duration to init a state
    public void CreateState(float duration = 5f)
    {
        Vector3 instPos = gameObject.transform.position;
        instPos.x += states.Count * 40;

        var state = Instantiate(statePrefab, instPos,
            Quaternion.identity, gameObject.transform);
        CountDownManager cdm = state.gameObject.transform.Find("CountDown Image").
            gameObject.GetComponent<CountDownManager>();
        float d = Random.Range(1, 5);
        state.name = "State " + d + "f";
        states.Add(cdm);
        cdm.StartCountDown(d);
    }

    public void RemoveState(CountDownManager cdm)
    {
        states.Remove(cdm);
        Destroy(cdm.gameObject.transform.parent.gameObject);
        for (sbyte i = 0; i < states.Count; i++)
        {
            Vector3 newPos = gameObject.transform.position;
            newPos.x += i * 40;
            states[i].transform.parent.gameObject.GetComponent<RectTransform>().
                SetPositionAndRotation(newPos, Quaternion.identity);
        }
    }
}
