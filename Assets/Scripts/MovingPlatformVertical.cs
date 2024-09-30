using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float minY = 0f;
    [SerializeField] private float maxY = 5f;
    private Vector3 _startingPosition;
    private bool goingUp = true;

    private void Start()
    {
        _startingPosition = transform.position;
    }

    private void Update()
    {
        float movementStep = speed * Time.deltaTime;
        if (goingUp)
        {
            transform.position += Vector3.up * movementStep;
            if (transform.position.y >= _startingPosition.y + maxY)
            {
                goingUp = false;
            }
        }
        else
        {
            transform.position += Vector3.down * movementStep;
            if (transform.position.y <= _startingPosition.y + minY)
            {
                goingUp = true;
            }
        }
    }
}