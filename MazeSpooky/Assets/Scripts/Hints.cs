using UnityEngine;
using UnityEngine.Tilemaps;

public class Hints : MonoBehaviour
{
    public TileBase specialTile; // TileBase for the special tile
    public int maxSpecialTiles = 40; // Maximum number of special tiles the player can place
    public int specialTileCount = 0; // Current number of placed special tiles

    public Tilemap tilemap; // Reference to the Tilemap game object
    public Transform playerTransform; // Reference to the player's transform

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (specialTileCount < maxSpecialTiles)
            {
                // Get the player's position in the tilemap space
                Vector3Int playerTilePos = tilemap.WorldToCell(playerTransform.position);

                // Check if there is already a special tile at the player's position
                if (tilemap.GetTile(playerTilePos) == null)
                {
                    // Add the special tile to the tilemap
                    tilemap.SetTile(playerTilePos, specialTile);

                    specialTileCount++;
                }
            }
        }
    }
}
