using CG_2.Render.Light.Interfaces;
using System.Collections.Generic;
using System.Drawing;
using Transform;

namespace CG_2.Render.Light
{
    public class MultiLight : ILight
    {
        private List<Light> _lights;

        public MultiLight()
        {
            _lights = new List<Light>();
        }

        public Color CalculateVertexColor(Vector3 position, Vector3 normal, Color color)
        {
            var sumColor = Color.Black;
            for (int i = 0; i < _lights.Count; i++)
            {
                var lightColor = _lights[i].CalculateVertexColor(position, normal, color);
                sumColor = Color.FromArgb(
                    (int)Mathf.Clamp(sumColor.A + lightColor.A, 0, 255),
                    (int)Mathf.Clamp(sumColor.R + lightColor.R, 0, 255),
                    (int)Mathf.Clamp(sumColor.G + lightColor.G, 0, 255),
                    (int)Mathf.Clamp(sumColor.B + lightColor.B, 0, 255));
            }
            return sumColor;
        }

        public void Add(Light light)
        {
            _lights.Add(light);
        }

        public void Remove(Light light)
        {
            _lights.Remove(light);
        }
    }
}
