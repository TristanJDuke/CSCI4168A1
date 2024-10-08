using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    //vars for the platform variability
    [SerializeField] private float speed = 2f;
    [SerializeField] private float minY = 0f;
    [SerializeField] private float maxY = 5f;
    private Vector3 _startingPosition;
    //used this so it can go up and then also can go down :)
    private bool goingUp = true;

    private void Start()
    {
        //identifies the start position for the bounceback
        _startingPosition = transform.position;
    }

    private void Update()
    {
        //movement math
        float movementStep = speed * Time.deltaTime;
        if (goingUp) //going up (duh)
        {
            transform.position += Vector3.up * movementStep;
            if (transform.position.y >= _startingPosition.y + maxY)
            {
                goingUp = false;
            }
        }
        else // going down
        {
            transform.position += Vector3.down * movementStep;
            if (transform.position.y <= _startingPosition.y + minY)
            {
                goingUp = true;
            }
        }
    }
}