using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Generation;

public class Generation : MonoBehaviour
{
    public RectTransform WallContainer;
    // Start is called before the first frame update
    void Start()
    {
        Walls walls = new Walls();
        walls.AddCorner(new WallCorner(new Vector2(10, 10)), walls.last);
        walls.AddCorner(new WallCorner(new Vector2(10, 50)), walls.last);
        walls.AddCorner(new WallCorner(new Vector2(50, 50)), walls.last);
        walls.AddCorner(new WallCorner(new Vector2(50, 10)), walls.last);
        walls.AddCorner(new WallCorner(new Vector2(50, 10)), walls.last);
        walls.AddConnection(walls.last, walls.head);
        walls.DebugPaint(WallContainer);
    } 

    // Update is called once per frame
    void Update()
    {
        
    }
}
