using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ActivatePowerMeter : MonoBehaviour
{
    private Slider powerMeter;
    [SerializeField] private float powerMeterSpeed;

    // Start is called before the first frame update
    void Start()
    {
        powerMeter = GetComponent<Slider>();
        ResetPowerMeter();
    }

    public IEnumerator ActivateMeter()
    {
        powerMeter.value = Mathf.PingPong(Time.time * powerMeterSpeed, 1.0f);
        yield return null;
    }

    public void ResetPowerMeter()
    {
        powerMeter.value = 0.0f;
    }

    public float GetPowerMeterValue()
    {
        return powerMeter.value;
    }

    public void IncreaseSpeed(float speed)
    {
        powerMeterSpeed += speed;
    }
}
