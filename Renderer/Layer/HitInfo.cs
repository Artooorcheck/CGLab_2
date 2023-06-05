using System.Drawing;
using Transform;

namespace Renderer.Layer
{
    public struct HitInfo
    {
        public Vector3 Point { get; private set; }

        public Vector3 Normal { get; private set; }
        public Color Color { get; private set; }

        public HitInfo(Vector3 point, Vector3 normal, Color color)
        {
            Point = point;
            Color = color;
            Normal = normal;
        }
    }
}
