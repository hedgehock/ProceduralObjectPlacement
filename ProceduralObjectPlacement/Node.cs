using System;
using System.Collections.Generic;
using System.Text;

namespace ProceduralObjectPlacement
{
    class Node
    {
        public string type;

        public int x;
        public int y;

        public Node(string _type, int _x, int _y)
        {
            x = _x;
            y = _y;
            type = _type;
        }
    }
}
