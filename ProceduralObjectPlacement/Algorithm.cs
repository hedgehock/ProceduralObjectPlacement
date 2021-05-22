using System;
using System.Threading;
using System.Numerics;

namespace ProceduralObjectPlacement
{
    class Algorithm
    {
        public Render render;
        Random random;

        public Algorithm(Render _render)
        {
            render = _render;
            random = new Random();
        }

        public void Start()
        {
            foreach (Node node in render.nodes)
            {
                if (node.type == "UnCalculated")
                {
                    StartCalculating(node);
                    break;
                }
            }
        }

        Vector2 GetNewPos(Node _node)
        {
            Vector2 pos = new Vector2();

            pos.X = _node.x + random.Next(-5, 6);
            pos.Y = _node.y + random.Next(-5, 6);

            return pos;
        }

        bool IsValid(Vector2 _pos, Node _node)
        {
            foreach (Node node in render.nodes)
            {
                if (node.type == "Barrier" || node.type == "UnCalculated" || node.type == "Calculated")
                {
                    if (_pos.X == node.x && _pos.Y == node.y)
                    {
                        return false;
                    }
                }
            }

            if (_pos.X > render.gridSize || _pos.X < 0 || _pos.Y > render.gridSize || _pos.Y < 0)
            {
                return false;
            }

            return true;
        }

        void MakeBarriers(Node _node)
        {
            foreach (Node node in render.nodes)
            {
                if (!(node.x == _node.x && node.y == _node.y))
                {
                    if (node.x > (_node.x - 3) && node.x < (_node.x + 3) && node.y > (_node.y - 3) && node.y < (_node.y + 3))
                    {
                        node.type = "Barrier";
                    }
                }
            }
        }

        void SetNodeType(Vector2 _pos, string _type)
        {
            foreach (Node node in render.nodes)
            {
                if (_pos.X == node.x && _pos.Y == node.y)
                {
                    node.type = _type;

                    if (node.type == "UnCalculated")
                    {
                        MakeBarriers(node);
                    }
                }
            }
        }

        void Calculate(Node _node)
        {
            // Set calculated
            foreach (Node node in render.nodes)
            {
                if (node.x == _node.x && node.y == _node.y)
                    node.type = "Calculated";
            }

            Vector2 pos = GetNewPos(_node);

            if (IsValid(pos, _node))
            {
                SetNodeType(pos, "UnCalculated");
            }
        }

        void CalculateNextNode()
        {
            foreach (Node node in render.nodes)
            {
                if (node.type == "UnCalculated")
                {
                    for (int x = 0; x < 50; x++)
                    {
                        Calculate(node);
                        Thread.Sleep(1);
                    }
                    CalculateNextNode();
                    break;
                }
            }
        }

        void StartCalculating(Node _node)
        {
            MakeBarriers(_node);

            for (int x = 0; x < 50; x++)
            {
                Calculate(_node);
            }

            foreach (Node node in render.nodes)
            {
                if (node.type == "UnCalculated")
                {
                    for (int x = 0; x < 50; x++)
                    {
                        Calculate(node);
                    }
                    CalculateNextNode();
                    break;
                }
            }
        }
    }
}
