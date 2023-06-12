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

        // Update the current tile position
        Vector3Int newTilePosition = tilemap.WorldToCell(transform.position);  // Convert world position to tile position
        if (newTilePosition != currentTilePosition)  // Check if the player has moved to a new tile
        {
            // Check if the new tile is a path tile
            if (tilemap.GetTile(newTilePosition) == pathTile)
            {
                currentTilePosition = newTilePosition;  // Update the current tile position
            }
            else
            {
                // Prevent the player from moving to non-path tiles
                transform.position = tilemap.GetCellCenterWorld(currentTilePosition);  // Reset player position to the center of the current tile
            }
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);  // Move the player using physics in FixedUpdate
    }
}
