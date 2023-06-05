using CG_2.Render.Light.Interfaces;
using System.Drawing;
using Transform;

namespace CG_2.Render.Light
{
    public class Light : ILight
    {
        public Vector3 Position { get; set; }

        public Color Color { get; set; }

        public float Fade { get; set; }

        public float Intensivity { get; set; }


        public Light(Vector3 position, float fade = 1, float intensivity = 1)
        {
            Position = position;
            Color = Color.White;
            Fade = fade;
            Intensivity = intensivity;
        }

        public Light(Vector3 position, Color color, float fade = 1, float intensivity = 1)
        {
            Position = position;
            Color = color;
            Fade = fade;
            Intensivity = intensivity;
        }

        public Color CalculateVertexColor(Vector3 position, Vector3 normal, Color color)
        {
            var ldir = (Position - position);
            var dist = ldir.magnitude;
            var A = 1 / (Fade * dist * dist);

            return (new Vector3(Color.R*color.R, color.G*Color.G, color.B*Color.B)/(255f*255f) * (normal * ldir) * A * Intensivity).Color;
        }
    }
}
