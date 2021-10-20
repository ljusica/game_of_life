using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOfLife : ProcessingLite.GP21
{
    GameCell[,] cells; //Our game grid matrix
    float cellSize = 0.25f; //Size of our cells
    int numberOfColums;
    int numberOfRows;
    int spawnChancePercentage = 15;

    void Start()
    {
        //Lower framerate makes it easier to test and see whats happening.
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 4;

        //Calculate our grid depending on size and cellSize
        numberOfColums = (int)Mathf.Floor(Width / cellSize);
        numberOfRows = (int)Mathf.Floor(Height / cellSize);

        //Initiate our matrix array
        cells = new GameCell[numberOfColums, numberOfRows];

        //Create all objects

        //For each row
        for (int y = 0; y < numberOfRows; ++y)
        {
            //for each column in each row
            for (int x = 0; x < numberOfColums; ++x)
            {
                //Create our game cell objects, multiply by cellSize for correct world placement
                cells[x, y] = new GameCell(x * cellSize, y * cellSize, cellSize);

                //Random check to see if it should be alive
                if (Random.Range(0, 100) < spawnChancePercentage)
                {
                    cells[x, y].alive = true;
                }
            }
        }
    }

    void Update()
    {
        //Clear screen
        Background(0);

        for (int y = 1; y < numberOfRows-1; ++y)
        {
            for (int x = 1; x < numberOfColums-1; ++x)
            {
                cells[x, y].previousGen = CalculateNeighbours(x, y);
            }
        }

        for (int y = 0; y < numberOfRows; ++y)
        {
            for (int x = 0; x < numberOfColums; ++x)
            {
                if (cells[x, y].previousGen < 2 || cells[x, y].previousGen > 3)
                {
                    cells[x, y].alive = false;
                }

                if (cells[x, y].alive == false && cells[x, y].previousGen == 3)
                {
                    cells[x, y].alive = true;
                }
            }
        }

        //Draw all cells.
        for (int y = 0; y < numberOfRows; ++y)
        {
            for (int x = 0; x < numberOfColums; ++x)
            {
                cells[x, y].Draw();
            }
        }
    }

    public int CalculateNeighbours(int x, int y)
    {
        int neighbours = 0;

        if (cells[x - 1, y].alive)
        {
            neighbours++;
        }

        if (cells[x, y - 1].alive)
        {
            neighbours++;
        }

        if (cells[x + 1, y].alive)
        {
            neighbours++;
        }

        if (cells[x, y + 1].alive)
        {
            neighbours++;
        }

        if (cells[x - 1, y - 1].alive)
        {
            neighbours++;
        }

        if (cells[x + 1, y + 1].alive)
        {
            neighbours++;
        }

        if (cells[x + 1, y - 1].alive)
        {
            neighbours++;
        }

        if (cells[x - 1, y + 1].alive)
        {
            neighbours++;
        }

        return neighbours;
    }

}

//You will probebly need to keep track of more things in this class
public class GameCell : ProcessingLite.GP21
{
    float x, y; //Keep track of our position
    float size; //our size
    public int previousGen;

    //Keep track if we are alive
    public bool alive = false;

    //Constructor
    public GameCell(float x, float y, float size)
    {
        //Our X is equal to incoming X, and so forth
        //adjust our draw position so we are centered
        this.x = x + size / 2;
        this.y = y + size / 2;

        //diameter/radius draw size fix
        this.size = size / 2;
    }


    public void Draw()
    {
        //If we are alive, draw our dot.
        if (alive)
        {
            //draw our dots
            Circle(x, y, size);
            //new Vector existing = [this.x, this.y]
        }
    }
}
