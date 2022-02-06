using System.Collections;
using UnityEngine;

/*
 * Rotate object around a pivot object
 */
public class RotateAroundPivot : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 50;
    public GameObject pivotObject;

    public IEnumerator RotateArrow()
    {
        transform.RotateAround(pivotObject.transform.position, Vector3.forward, rotationSpeed * Time.deltaTime);
        yield return null;
    }

    public void IncreaseSpeed(float speed)
    {
        rotationSpeed += speed;
    }
}
