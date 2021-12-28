using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House
{
    public List<Vector2> wallCorners = new List<Vector2>();
    public List<Room> rooms = new List<Room>();

    House(float AR)
    {
        float h = Random.Range(8, 15);
        float w = 0f; 
        if (Random.Range(0, 1) > 0.5)
        {
            w = h * AR;
        }
        else
        {
            w = h / AR;
        }
        wallCorners.Add(new Vector2(0f, 0f));
        wallCorners.Add(new Vector2(h, 0f));
        wallCorners.Add(new Vector2(0f, w));
        wallCorners.Add(new Vector2(h, w));
    }


}
