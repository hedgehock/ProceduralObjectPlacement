using System.Collections.Generic;
using System.Threading;
using Raylib_cs;

namespace ProceduralObjectPlacement
{
    class Render
    {
        Algorithm alghorithm;

        public int windowSize;
        public int gridSize;
        public int gap;

        public List<Node> nodes = new List<Node>();

        public Render(int _windowSize, int _gridSize)
        {
            alghorithm = new Algorithm(this);

            windowSize = _windowSize;
            gridSize = _gridSize;
            gap = windowSize / gridSize;

            CreateList();
        }

        void CreateList()
        {
            for (int x = 0; x < gridSize; x++)
            {
                for (int y = 0; y < gridSize; y++)
                {
                    nodes.Add(new Node("Empty", x, y));
                }
            }

            foreach (Node node in nodes)
            {
                if (node.x == 25 && node.y == 25)
                {
                    node.type = "UnCalculated";
                }
            }

            Thread algorithmThread = new Thread(new ThreadStart(alghorithm.Start));
            algorithmThread.Start();
        }

        public void RenderContext()
        {
            Raylib.ClearBackground(Color.WHITE);

            foreach (Node node in nodes)
            {
                if (node.type == "Empty")
                    Raylib.DrawRectangle(gap * node.x, gap * node.y, gap, gap, Color.LIGHTGRAY);
                else if (node.type == "UnCalculated")
                    Raylib.DrawRectangle(gap * node.x, gap * node.y, gap, gap, Color.GREEN);
                else if (node.type == "Calculated")
                    Raylib.DrawRectangle(gap * node.x, gap * node.y, gap, gap, Color.RED);
                else if (node.type == "Barrier")
                    Raylib.DrawRectangle(gap * node.x, gap * node.y, gap, gap, Color.SKYBLUE);
            }

            for (int x = 0; x < gridSize; x++)
            {
                Raylib.DrawLine(gap * x, 0, gap * x, windowSize, Color.WHITE);

                for (int y = 0; y < gridSize; y++)
                {
                    Raylib.DrawLine(0, gap * y, windowSize, gap * y, Color.WHITE);
                }
            }
        }
    }
}
