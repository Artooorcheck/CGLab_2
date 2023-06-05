using Renderer.Layer;
using Transform;

namespace CG_2.Render.Interfaces
{
    public interface IRenderer
    {
        string Name { get; set; }
        bool Raycast(Vector3 point, Vector3 direction, out HitInfo hitInfo);
    }
}
