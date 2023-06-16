using UnityEngine;
using TMPro;

public class RemainingTilesDisplay : MonoBehaviour
{
    public Hints hints; // Reference to the TilePlacement script
    private TMP_Text textMeshPro; // Reference to the TMP component

    private void Start()
    {
        // Get the reference to the TMP component
        textMeshPro = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        // Update the remaining tiles display
        int remainingTiles = hints.maxSpecialTiles - hints.specialTileCount;
        textMeshPro.text = "Remaining Hints: " + remainingTiles.ToString();
    }
}

