using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/*
 * Sets state for head mini game
 *     1. RotateArrow: rotate arrow around zombiohead
 *     2. ActivatePowerMeter: once player clicks zomboihead, stop arrow rotation and start power meter animation
 *     3. FireZomboiHead: once player clicks F key, hide arrow and power meter, then move zomboihead
 *     4. Once zomboihead stops moving, repeat to state 1. If player reaches goal, player wins
 *     
 * Add implementations
 *     - increase rotationSpeed each time player moves
 *     - increase powerMeterSpeed
 */
public class HeadMiniGame : MonoBehaviour
{
    [SerializeField] private GameObject arrow;
    private RotateAroundPivot arrowScript;

    [SerializeField] private GameObject powerMeter;
    private ActivatePowerMeter powerMeterScript;
    private float speedPercentage;

    [SerializeField] private GameObject zHead;
    private HeadMovement headMovementScript;
    private Button headButton;

    private bool isHeadClicked;
    private bool isFClicked;

    // Start is called before the first frame update
    void Start()
    {
        isHeadClicked = false;
        isFClicked = false;
        arrowScript = arrow.GetComponent<RotateAroundPivot>();
        powerMeterScript = powerMeter.GetComponent<ActivatePowerMeter>();
        headMovementScript = zHead.GetComponent<HeadMovement>();
        headButton = zHead.GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isHeadClicked)
        {
            StartCoroutine(arrowScript.RotateArrow());
        }
        //wait for mouse click to stop arrow and activate power meter
        else
        {
            StartCoroutine(powerMeterScript.ActivateMeter());
        }
        //fire zomboi head
        if (Input.GetKeyDown(KeyCode.F))
        {
            //direction and speed for head movement
            headMovementScript.SetDirection(arrow.transform.position - zHead.transform.position);
            speedPercentage = powerMeterScript.GetPowerMeterValue();
            print("speed percentage: " + speedPercentage);

            isHeadClicked = false;
            headButton.interactable = false;
            arrow.GetComponent<SpriteRenderer>().enabled = false;
            powerMeterScript.ResetPowerMeter();
            isFClicked = true;
        }
        ////move zomboi head
        //if (isFClicked)
        //{
        //    StartCoroutine(headMovementScript.MoveHead(headMovementScript.GetHeadMaxSpeed() * speedPercentage));
        //    StartCoroutine(headMovementScript.LerpSpeed());

        //    StartCoroutine(WaitForHeadToStop());
        //}
    }

    void FixedUpdate()
    {
        //if (!isHeadClicked)
        //{
        //    StartCoroutine(arrowScript.RotateArrow());
        //}
        ////wait for mouse click to stop arrow and activate power meter
        //else
        //{
        //    StartCoroutine(powerMeterScript.ActivateMeter());
        //}
        ////fire zomboi head
        //if (Input.GetKeyDown(KeyCode.F))
        //{
        //    //direction and speed for head movement
        //    headMovementScript.SetDirection(arrow.transform.position - zHead.transform.position);
        //    speedPercentage = powerMeterScript.GetPowerMeterValue();

        //    isHeadClicked = false;
        //    arrow.GetComponent<SpriteRenderer>().enabled = false;
        //    powerMeterScript.ResetPowerMeter();
        //    isFClicked = true;
        //}
        //move zomboi head
        if (isFClicked)
        {
            StartCoroutine(headMovementScript.MoveHead(headMovementScript.GetHeadMaxSpeed() * speedPercentage));
            StartCoroutine(headMovementScript.LerpSpeed());

            StartCoroutine(WaitForHeadToStop());
        }
    }

    IEnumerator WaitForHeadToStop()
    {
        yield return new WaitWhile(() => headMovementScript.isMoving());

        isFClicked = false;
        isHeadClicked = false;
        headButton.interactable = true;
        arrow.GetComponent<SpriteRenderer>().enabled = true;
    }

    public void HeadClicked()
    {
        isHeadClicked = true;
    }
}
