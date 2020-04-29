using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShuttleController : MonoSingleton<ShuttleController>
{
    GameObject activeMover = null;
    [SerializeField] Text objectiveText;

    public void MoveToNextPhase(GameObject poi, string text)
    {
        activeMover = poi;
        objectiveText.text = text;
    }

    void Update()
    {
        if (activeMover == null) return;
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, activeMover.transform.position, 1 * Time.deltaTime);
        gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, activeMover.transform.rotation, 1 * Time.deltaTime);
    }
}
