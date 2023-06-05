using CG_2.Render.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transform;

namespace Renderer.Layer
{
    public class Layer : IRenderer
    {

        private List<IRenderer> _renderObjects;

        public Layer(string name)
        {
            _renderObjects = new List<IRenderer>();
            Name = name;
        }

        public string Name { get; set; }

        public bool Raycast(Vector3 point, Vector3 direction, out HitInfo hitInfo)
        {
            bool res = false;
            hitInfo = new HitInfo();
            for (int i = 0; i < _renderObjects.Count; i++)
            {
                if (_renderObjects[i].Raycast(point, direction, out HitInfo tmp))
                {
                    if (!res || (tmp.Point - point).magnitude < (hitInfo.Point - point).magnitude)
                    {
                        hitInfo = tmp;
                        res = true;
                    }
                }
            }
            return res;
        }

        public void Add(IRenderer renderer)
        {
            _renderObjects.Add(renderer);
        }

        public void Remove(string name)
        {
            _renderObjects.RemoveAll(a => a.Name == name);
        }
    }
}
