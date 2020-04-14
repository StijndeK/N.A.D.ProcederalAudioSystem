using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridManager : MonoBehaviour
{
    // amount tile cells
    public int rows = 2;
    public int cols = 2;

    // tile sizes
    public float tileSizeRows = 100;
    public float tileSizeCols = 100;

    // component size
    private float componentWidth = 1078;
    private float componentHeight = 431;

    // buttons
    public Button addXButton;
    public Button addYButton;
    public Button minXButton;
    public Button minYButton;

    // gridsize object
    public GameObject gridSize;

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
        if (rows > 2)
        {
            rows -= 1;
            foreach (Transform child in gridSize.transform)
            {
                Destroy(child.gameObject);
            }
            gameObject.GetComponentInParent<SequencerSystem>().parameterSettingSliderY.maxValue -= 1;
            GenerateGrid();
        }
    }

    private void minXButtonPressed()
    {
        if (cols > 2)
        {
            cols -= 1;
            foreach (Transform child in gridSize.transform)
            {
                Destroy(child.gameObject);
            }
            gameObject.GetComponentInParent<SequencerSystem>().parameterSettingSliderX.maxValue -= 1;
            GenerateGrid();
        }
    }

    private void addYButtonPressed()
    {
        rows += 1;
        foreach (Transform child in gridSize.transform)
        {
            Destroy(child.gameObject);
        }
        gameObject.GetComponentInParent<SequencerSystem>().parameterSettingSliderY.maxValue += 1;
        GenerateGrid();
    }

    private void addXButtonPressed()
    {
        cols += 1;
        foreach (Transform child in gridSize.transform)
        {
            Destroy(child.gameObject);
        }
        gameObject.GetComponentInParent<SequencerSystem>().parameterSettingSliderX.maxValue += 1;
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        // calculate tilesize
        tileSizeRows = componentWidth / cols;
        tileSizeCols = componentHeight / rows;

        // get tile component
        GameObject referenceTile = (GameObject)Instantiate(Resources.Load("LayerRoom"));

        // Scale tile to tilesize
        referenceTile.transform.localScale = new Vector2(tileSizeRows / 100, tileSizeCols / 100);

        // for every cell create and set position
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                // create dynamic position
                float posX = col * tileSizeRows;
                float posY = row * -tileSizeCols;

                // account for middle pivot
                posX += tileSizeRows / 2;
                posY -= tileSizeCols / 2;

                // spawn cell
                GameObject Box = Instantiate(referenceTile, new Vector3(posX, posY, 0), Quaternion.identity) as GameObject;
                Box.transform.SetParent(GameObject.FindGameObjectWithTag("gridSize").transform, false);
            }
        }
    }
}
