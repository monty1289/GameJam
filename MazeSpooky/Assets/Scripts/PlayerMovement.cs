using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Animator animator;
    public Tilemap tilemap;
    public TileBase pathTile;

    private Vector3Int currentTilePosition;

    Vector2 movement;

    private void Start()
    {
        currentTilePosition = tilemap.WorldToCell(transform.position);
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        if (movement.x < 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }

        // Update the current tile position
        Vector3Int newTilePosition = tilemap.WorldToCell(transform.position);
        if (newTilePosition != currentTilePosition)
        {
            // Check if the new tile is a path tile
            if (tilemap.GetTile(newTilePosition) == pathTile)
            {
                currentTilePosition = newTilePosition;
            }
            else
            {
                // Prevent the player from moving to non-path tiles
                transform.position = tilemap.GetCellCenterWorld(currentTilePosition);
            }
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
