using System.Collections;
using UnityEngine;

/*
 * Move head in direction of arrow and with velocity of power meter percentage
 * 
 * extra
 *    - rotate head when moving
 */
public class HeadMovement : MonoBehaviour
{
    [SerializeField] private float headMaxSpeed = 20.0f;
    private float currentSpeed;
    private float acceleration;
    private bool isAccelerate;
    private Vector2 currentDirection;

    void Start()
    {
        acceleration = 2.0f;
        isAccelerate = true;
        currentSpeed = 0;
    }

    public IEnumerator MoveHead(float maxSpeed)
    {
        acceleration = maxSpeed;
        transform.Translate(currentDirection * currentSpeed * Time.deltaTime);
        yield return new WaitWhile(() => isMoving());

        isAccelerate = true;
    }

    //accelerate speed to maxSpeed from MoveHead(), then decelerate
    public IEnumerator LerpSpeed()
    {
        if (isAccelerate)
        {

            if (currentSpeed < acceleration)
            {
                currentSpeed = currentSpeed + acceleration * Time.deltaTime;
            }
            else
            {
                isAccelerate = false;
            }
        }
        else
        {
            if (currentSpeed > 0)
            {
                currentSpeed = currentSpeed + -acceleration * Time.deltaTime;
            }
            else
            {
                currentSpeed = 0;
            }
        }
        yield return null;
    }

    public float GetHeadMaxSpeed()
    {
        return headMaxSpeed;
    }

    public void SetDirection(Vector2 direction)
    {
        currentDirection = direction;
    }

    public bool isMoving()
    {
        if (currentSpeed > 0)
        {
            return true;
        }
        return false;
    }

    //bounce off walls and object in the room
    private void OnCollisionEnter2D(Collision2D coll)
    {
        Vector2 contactNormal = coll.contacts[0].normal;
        currentDirection = Vector2.Reflect(currentDirection, contactNormal).normalized;
    }

    //if zomboihead triggers goal area, player wins
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("goal"))
        {
            print("player wins");
        }
    }
}
