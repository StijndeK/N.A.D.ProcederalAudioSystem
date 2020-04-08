using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridManager : MonoBehaviour
{
    // amount tile cells
    private int rows = 4;
    private int cols = 8;

    // tile sizes
    private float tileSizeRows = 1;
    private float tileSizeCols = 1;

    // canvas size
    // TODO: find/set this dyanmically
    private float width = 17.75f;
    private float height = 10;
    // component size
    private float componentWidth;
    private float componentHeight;

    // scale of tile
    private float scale = 0.71f;

    // buttons
    public Button addXButton;
    public Button addYButton;
    public Button minXButton;
    public Button minYButton;

    void Start()
    {
        // listeners
        addXButton.onClick.AddListener(addXButtonPressed);
        addYButton.onClick.AddListener(addYButtonPressed);
        minXButton.onClick.AddListener(minXButtonPressed);
        minYButton.onClick.AddListener(minYButtonPressed);

        GenerateGrid();
    }

    private void minYButtonPressed()
    {
        rows -= 1;
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        GenerateGrid();
    }

    private void minXButtonPressed()
    {
        cols -= 1;
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        GenerateGrid();
    }

    private void addYButtonPressed()
    {
        rows += 1;
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        GenerateGrid();
    }

    private void addXButtonPressed()
    {
        cols += 1;
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        // set component size (relative to size)
        componentWidth = width;
        componentHeight = height / 2;

        // Dynamic tilesize
        tileSizeRows = componentWidth / cols;
        tileSizeCols = componentHeight / rows;

        // get tile
        GameObject referenceTile = (GameObject)Instantiate(Resources.Load("GrabableBorder"));

        // Scale tile to tilesize
        referenceTile.transform.localScale = new Vector2(scale * tileSizeRows, scale * tileSizeCols);

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                GameObject tile = (GameObject)Instantiate(referenceTile, transform);

                // create dynamic position
                float posX = col * tileSizeRows;
                float posY = row * -tileSizeCols;

                // move from center to top left in screen by removing the distance to the topleft minus half the size of the sprite
                posX -= (componentWidth / 2) - ((tileSizeRows) / 2);
                posY += (componentHeight / 2) - ((tileSizeCols) / 2);

                // account for positioning on canvas of sequencer component
                posY -= componentHeight / 2;

                // set position
                tile.transform.position = new Vector2(posX, posY);
            }
        }

        Destroy(referenceTile);
    }
}
