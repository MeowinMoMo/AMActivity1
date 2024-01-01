using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
/*    [SerializeField]
    int _playerSpeed;
    float GetHor;
    float GetFor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetHor = Input.GetAxis("Horizontal");
        GetFor = Input.GetAxis("Vertical");
        Vector3 Direction = new Vector3(GetHor, 0, GetFor);
        transform.Translate(Direction * _playerSpeed * Time.deltaTime);
    }*/


    public float moveSpeed = 5f;
    public float goalRange = 1f;
    public float towerRange = 3f;

    private bool isGameOver = false;

    private void Update()
    {
        if (!isGameOver)
        {
            MovePlayer();
            CheckForGoal();
            CheckForTower();
        }
    }

    private void MovePlayer()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Move the player based on input
        transform.Translate(new Vector3(horizontalInput, verticalInput, 0f) * moveSpeed * Time.deltaTime);
    }

    private void CheckForGoal()
    {
        // Replace "Goal" with the tag or layer you assign to the goal object
        GameObject goal = GameObject.FindGameObjectWithTag("Goal");

        if (goal != null)
        {
            float distanceToGoal = CalculateDistance(transform.position, goal.transform.position);

            // Check if the player is close enough to the goal
            if (distanceToGoal < goalRange)
            {
                Debug.Log("Goal Reached! Game Over.");
                isGameOver = true;
                // You can implement a game over screen or any other logic here
            }
        }
        else
        {
            Debug.LogError("Goal object not found. Please assign the correct tag or layer to the goal object.");
        }
    }

    private void CheckForTower()
    {
        GameObject[] towers = GameObject.FindGameObjectsWithTag("Tower");

        foreach (GameObject tower in towers)
        {
            float distanceToTower = CalculateDistance(transform.position, tower.transform.position);

            // Check if the player is in front and in range of a tower
            if (distanceToTower < towerRange && IsPlayerInFront(transform.position, transform.forward, tower.transform.position))
            {
                Debug.Log("Player caught by a tower! Game Over.");
                isGameOver = true;
                // You can implement a game over screen or any other logic here
            }
        }
    }

    private float CalculateDistance(Vector3 point1, Vector3 point2)
    {
        return Mathf.Sqrt(Mathf.Pow(point1.x - point2.x, 2) + Mathf.Pow(point1.y - point2.y, 2) + Mathf.Pow(point1.z - point2.z, 2));
    }

    private bool IsPlayerInFront(Vector3 playerPosition, Vector3 playerForward, Vector3 targetPosition)
    {
        // Check if the target is in front of the player
        Vector3 toTarget = targetPosition - playerPosition;
        float angle = Vector3.Angle(playerForward, toTarget);

        return Mathf.Abs(angle) < 90f;
    }

}
