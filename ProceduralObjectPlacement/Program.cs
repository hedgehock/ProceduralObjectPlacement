using Raylib_cs;

namespace ProceduralObjectPlacement
{
    class Program
    {
        public Render render;

        static void Main(string[] args)
        {
            Program program = new Program();
        }

        public Program()
        {
            render = new Render(1000, 50);

            Raylib.SetTraceLogLevel(TraceLogType.LOG_ERROR);

            Raylib.InitWindow(render.windowSize, render.windowSize, "Procedural Object Placement");

            while (!Raylib.WindowShouldClose())
            {
                Raylib.BeginDrawing();

                render.RenderContext();

                Raylib.EndDrawing();
            }
        }
    }
}
