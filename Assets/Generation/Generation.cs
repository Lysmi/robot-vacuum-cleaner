using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Generation;

public class Generation : MonoBehaviour
{
    public LineRenderer lineRenderer;
    // Start is called before the first frame update
    void Start()
    {
        Walls walls = new Walls();
        walls.AddCorner(new WallCorner(new Vector2(0, 0)), walls.last);
        walls.AddCorner(new WallCorner(new Vector2(0, 50)), walls.last);
        walls.AddCorner(new WallCorner(new Vector2(50, 50)), walls.last);
        walls.AddCorner(new WallCorner(new Vector2(50, 0)), walls.last);
        walls.AddConnection(walls.last, walls.head);
        walls.DebugPaint(lineRenderer);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
