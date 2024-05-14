// Ball - Warstwa danych - Model

namespace Data
{
    public class Ball
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Vel_X { get; set; }
        public float Vel_Y { get; set; }
        public float Diameter { get; set; }
        public float Weight { get; set; }
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
