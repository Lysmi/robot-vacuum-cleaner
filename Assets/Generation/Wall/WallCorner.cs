using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Generation
{
    public class WallCorner
    {
        public List<WallCorner> neightbors;
        public Vector2 position;

        public WallCorner()
        {
            this.position = new Vector2();
            this.neightbors = new List<WallCorner>();
        }


        public WallCorner(Vector2 position)
        {
            this.position = position;
            this.neightbors = new List<WallCorner>();
        }
    }
}