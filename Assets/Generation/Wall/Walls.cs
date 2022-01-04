using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Generation
{
    public class Walls
    {
        public WallCorner head = null;
        public WallCorner last = null;


        public WallCorner AddCorner(WallCorner newCorner, WallCorner parent) //Добавляет новый узел newCorner, связывая с узлом parent
        {
            if (head == null)
            {
                head = newCorner;
            }
            else
            {
                UseToFirstConditionalCorner(it => { it.neightbors.Add(newCorner); newCorner.neightbors.Add(parent); }, it => it == parent) ;
            }
            last = newCorner;
            return last;
        }

        public void AddConnection(WallCorner firstCorner, WallCorner secondCorner)  //Добавляет соединение между двумя узлами графа
        {
            firstCorner.neightbors.Add(secondCorner);
            secondCorner.neightbors.Add(firstCorner);
        }

        public void DebugPaint(RectTransform container)
        {
            if (head != null)
            {
                List<WallCorner> path = new List<WallCorner>();
                _DebugPaintRecursion(head, container, path);
            }
        }

        public void _DebugPaintRecursion(WallCorner currCorner, RectTransform container, List<WallCorner> path)
        {
            path.Add(currCorner);
            foreach (var currNeightbor in currCorner.neightbors)
            {
                if (!path.Exists(it => it == currNeightbor))
                {
                    GameObject wall = new GameObject("wall", typeof(UnityEngine.UI.Image));
                    UnityEngine.UI.Image image = wall.GetComponent<UnityEngine.UI.Image>();
                    image.color = new Color(0, 0, 0);
                    wall.transform.SetParent(container);
                    RectTransform transform = wall.GetComponent<RectTransform>();
                    Vector2 dir = (currNeightbor.position - currCorner.position).normalized;
                    float dist = Vector2.Distance(currNeightbor.position, currCorner.position);
                    float thick = 3f;
                    transform.anchorMin = new Vector2(0, 0);
                    transform.anchorMax = new Vector2(0, 0);
                    transform.sizeDelta = new Vector2(dist, thick);
                    transform.anchoredPosition = currCorner.position;
                    transform.pivot = new Vector2(0, 0.5f);
                    transform.localScale = new Vector2(1, 1);
                    transform.localEulerAngles = new Vector3(0, 0, Vector2.SignedAngle(Vector2.right, dir));
                }
            }
            foreach (var currNeightbor in currCorner.neightbors)
            {
                if (!path.Exists(it => it == currNeightbor))
                {
                    _DebugPaintRecursion(currNeightbor, container, path);
                }
            }
        }

        #region SystemCode
        private void UseToAllCorner(UseToCorner @usingFunction) //Применяет делегат @usingFunction ко всем объектам графа 
        {
            if (head != null)
            {
                List<WallCorner> path = new List<WallCorner>();
                path.Add(head);
                @usingFunction(head);
                _UseToAllCornerRecursion(@usingFunction, head, path);
            }
        }

        private void _UseToAllCornerRecursion(UseToCorner @usingFunction, WallCorner currCorner, List<WallCorner> path)
        {
            @usingFunction.Invoke(currCorner);
            path.Add(currCorner);
            foreach (var currNeightbor in currCorner.neightbors)
            {
                if (!path.Exists(it => it == currNeightbor))
                {
                    _UseToAllCornerRecursion(@usingFunction, currNeightbor, path);
                }
            }
        }

        private void UseToFirstConditionalCorner(UseToCorner @usingFunction, Predicate<WallCorner> predicate) //Применяет делегат @usingFunction к первому ужовлетворяющему условию объекту в графе 
        {
            if (head != null)
            {
                List<WallCorner> path = new List<WallCorner>();
                path.Add(head);
                if (predicate(head))
                {
                    @usingFunction(head);
                }
                else
                {
                    _UseToFirstCornerRecursion(@usingFunction, predicate, head, path);
                }
            }
        }

        private bool _UseToFirstCornerRecursion(UseToCorner @usingFunction, Predicate<WallCorner> predicate, WallCorner currCorner, List<WallCorner> path)
        {
            if (predicate(currCorner))
            {
                @usingFunction(currCorner);
                return true;
            }             
            else
            {
                path.Add(currCorner);
                foreach (var currNeightbor in currCorner.neightbors)
                {
                    if (!path.Exists(it => it == currNeightbor))
                    {
                        if (_UseToFirstCornerRecursion(@usingFunction, predicate, currNeightbor, path))
                            return true;
                    }
                }
                return false;
            }
        }
    }

    delegate void UseToCorner(WallCorner currCorner);
    #endregion
}

