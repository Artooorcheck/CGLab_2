using System.Drawing;
using Transform;

namespace CG_2.Render.Light.Interfaces
{
    public interface ILight
    {
        Color CalculateVertexColor(Vector3 position, Vector3 normal, Color color);
    }
}
