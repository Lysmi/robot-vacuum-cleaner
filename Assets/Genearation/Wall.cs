using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public Vector2 position = new Vector2();
    List<Wall> neightbors = new List<Wall>();
    Wall(Vector2 pos, Wall wall)
    {
        this.position = pos;
        neightbors.Add(wall);
    }

    public void addNeightbor(Wall wall)
    {
        neightbors.Add(wall);
    }


}
