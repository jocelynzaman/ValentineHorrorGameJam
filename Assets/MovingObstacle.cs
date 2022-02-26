using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    [SerializeField] private bool isMoveY;
    [SerializeField] private float speed;
    [SerializeField] private float maxY;
    [SerializeField] private float minY;
    [SerializeField] private GameObject pivotObject;
    private bool isMoveUp;

    // Start is called before the first frame update
    void Start()
    {
        isMoveUp = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoveY)
        {
            if (isMoveUp)
            {
                transform.Translate(Vector2.up * speed * Time.deltaTime);
                if (transform.position.y >= maxY)
                {
                    isMoveUp = false;
                }
            }
            else
            {
                transform.Translate(Vector2.up * -speed * Time.deltaTime);
                if (transform.position.y <= minY)
                {
                    isMoveUp = true;
                }
            }
        }
        else
        {
            MoveObstacleInCircles();
        }
    }

    private void MoveObstacleInCircles()
    {
        transform.RotateAround(pivotObject.transform.position, Vector3.forward, speed * Time.deltaTime);
    }
}
