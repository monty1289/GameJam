using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;             // Movement speed of the player
    public Rigidbody2D rb;                   // Reference to the Rigidbody2D component
    public Animator animator;                // Reference to the Animator component
    public Tilemap tilemap;                  // Reference to the Tilemap component
    public TileBase pathTile;                // Tile representing the valid path
    public int lightValue;

    private Vector3Int currentTilePosition;  // Current tile position of the player

    Vector2 movement;                        // Movement vector for the player


    private void Start()
    {
        currentTilePosition = tilemap.WorldToCell(transform.position);  // Get the initial tile position
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");  // Get horizontal input
        movement.y = Input.GetAxisRaw("Vertical");    // Get vertical input

        animator.SetFloat("Horizontal", movement.x);   // Set animator parameters for movement animation
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        if (movement.x < 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;  // Flip sprite if moving left
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;  // Reset sprite flip
        }

        // Calculate the target position based on current position and movement
        Vector3 targetPosition = transform.position + new Vector3(movement.x, movement.y, 0f) * moveSpeed * Time.deltaTime;

        // Convert target position to tile position
        Vector3Int targetTilePosition = tilemap.WorldToCell(targetPosition);

        // Check if the target tile is a path tile
        if (tilemap.GetTile(targetTilePosition) == pathTile)
        {
            // Update the current tile position
            currentTilePosition = targetTilePosition;
            // Move the player to the target position
            transform.position = targetPosition;
        }

    }
}
