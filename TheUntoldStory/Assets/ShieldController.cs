using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldController : MonoBehaviour
{

    public float timeDestroy;
    private PlayerController player;
    float speed;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, timeDestroy);
        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get the player's position
        Vector3 targetPosition = player.transform.position;

        // Create a new position with the fixed Y-coordinate
        Vector3 newPosition = new Vector3(targetPosition.x, targetPosition.y, targetPosition.z);

        // Move the enemy towards the new position
        transform.position = Vector3.MoveTowards(transform.position, newPosition, player.moveSpeed);
    }
}
