using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMonitor : MonoBehaviour
{
    [SerializeField] Text canvasText;
    private Vector3 lastPos = new Vector3(0, 0, 0);
    private float timeSinceUpdated = 0.0f;
    private float cumulativeSpeedTicker = 0.0f;

    private void Update()
    {
        timeSinceUpdated += Time.deltaTime;
        if (timeSinceUpdated > 1.0f)
        {
            canvasText.text = "MOVING AT " + (cumulativeSpeedTicker * 60).ToString("0.000") + " MPH";
            cumulativeSpeedTicker = 0.0f;
            timeSinceUpdated = 0.0f;
        }

        Vector3 newPos = ShuttleController.Instance.gameObject.transform.position;
        cumulativeSpeedTicker += Vector3.Distance(lastPos, newPos);
        lastPos = newPos;
    }
}
