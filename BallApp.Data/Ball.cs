// Ball - Warstwa danych - Model

namespace Data
{
    public class Ball
    {
        private static int nextId = 1;

        public int ID { get; private set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float Vel_X { get; set; }
        public float Vel_Y { get; set; }
        public float Diameter { get; set; }
        public float Weight { get; set; }

        public Ball()
        {
            ID = nextId++;
        }
    }

    public class Rect
    {
        public float width { get; set; }
        public float height { get; set; }

        public Rect(float width, float height)
        {
            this.width = width;
            this.height = height;
        }
    }
}
