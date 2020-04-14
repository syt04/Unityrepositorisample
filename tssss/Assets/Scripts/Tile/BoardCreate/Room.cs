﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room
{
    public int xPos;
    public int yPos;
    public int roomWidth;
    public int roomHeight;
    public Direction enteringCorridor;

    public void SetupRoom(IntRange widthRange, IntRange heightRange, int columns, int rows) {

        roomWidth  = widthRange.Random;
        roomHeight = heightRange.Random;

        xPos = Mathf.RoundToInt(columns / 2f - roomWidth / 2f);
        yPos = Mathf.RoundToInt(rows / 2f - roomHeight / 2f);
       
    }

    public void SetupRoom(IntRange widthRange, IntRange heightRange, int columns, int rows,Corridor corridor) {

        enteringCorridor = corridor.direction;

        roomWidth = widthRange.Random;
        roomHeight = heightRange.Random;

        switch (corridor.direction)
        {
            case Direction.N:

                roomHeight = Mathf.Clamp(roomHeight, 1, rows - corridor.EndPositionY);

                yPos = corridor.EndPositionY;

                xPos = Random.Range(corridor.EndPositionX - roomWidth + 1, corridor.EndPositionX);
                xPos = Mathf.Clamp(xPos,0,columns-roomWidth);
                break;
            case Direction.E:

                roomWidth = Mathf.Clamp(roomWidth, 1, columns - corridor.EndPositionX);
                xPos = corridor.EndPositionX;

                yPos = Random.Range(corridor.EndPositionY - roomHeight + 1, corridor.EndPositionY);
                yPos = Mathf.Clamp(yPos, 0, rows - roomHeight);
                break;
            case Direction.S:
                roomWidth = Mathf.Clamp(roomHeight, 1, columns - corridor.EndPositionY);
                yPos = corridor.EndPositionY - roomHeight +1;

                xPos = Random.Range(corridor.EndPositionX - roomWidth + 1, corridor.EndPositionX);
                xPos = Mathf.Clamp(xPos, 0, rows - roomWidth);
                break;

            case Direction.W:
                roomWidth = Mathf.Clamp(roomWidth, 1, corridor.EndPositionX);
                xPos = corridor.EndPositionX  -roomWidth+ 1;

                yPos = Random.Range(corridor.EndPositionY - roomHeight + 1, corridor.EndPositionY);
                yPos = Mathf.Clamp(yPos, 0, rows - roomHeight);
                break;
        }

    }
}