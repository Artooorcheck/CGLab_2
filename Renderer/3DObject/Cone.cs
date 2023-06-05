using CG_2.Render.Interfaces;
using Renderer.Layer;
using System.Drawing;
using Transform;

namespace Renderer._3DObject
{
    public class Cone : IRenderer
    {
        public string Name { get; set; }
        public Vector3 Position { get; set; }

        public Color Color { get; set; }

        private float _height;
        private float _radius;

        public Cone(string name, Vector3 position, Color color, float height, float radius)
        {
            Name = name;
            Position = position;
            Color = color;
            _height = height;
            _radius = radius / height;
        }

        public Cone(string name, Vector3 position, float height, float radius)
        {
            Name = name;
            Position = position;
            Color = Color.White;
            _height = height;
            _radius = radius / height;
        }

        public Cone(string name, float height, float radius)
        {
            Name = name;
            Position = Vector3.zero;
            _height = height;
            _radius = radius / height;
        }

        public bool Raycast(Vector3 point, Vector3 direction, out HitInfo hitInfo)
        {
            hitInfo = new HitInfo();
            float r2 = _radius * _radius;
            float z0 = _height + Position.z;
            float a = direction.x * direction.x + direction.y * direction.y - direction.z * direction.z * r2;
            float b = 2 * point.x * direction.x * direction.y - 2 * point.y * direction.x * direction.x - 2 * direction.x * Position.x * direction.y + 2 * r2 * point.y * direction.z * direction.z -
                2 * r2 * point.z * direction.z * direction.y + 2 * r2 * z0 * direction.z * direction.y - 2 * Position.y * direction.y * direction.y;
            float c = point.y * point.y * direction.x * direction.x - 2 * point.y * point.x * direction.x * direction.y + 2 * point.y * direction.x * Position.x * direction.y + point.x * point.x * direction.y * direction.y
                - 2 * Position.x * point.x * direction.y * direction.y + Position.x * Position.x * direction.y * direction.y + Position.y * Position.y * direction.y * direction.y - r2 * (point.y * point.y * direction.z * direction.z - 2 * point.y * direction.z * point.z * direction.y
                + 2 * point.y * direction.z * z0 * direction.y + point.z * point.z * direction.y * direction.y - 2 * point.z * z0 * direction.y * direction.y + z0 * z0 * direction.y * direction.y);
            float d = b * b - 4 * a * c;

            if (d < 0)
            {
                return false;
            }

            var hitPoint1 = new Vector3(0, (-b + Mathf.Sqrt(d)) / (2 * a), 0);
            var hitPoint2 = new Vector3(0, (-b - Mathf.Sqrt(d)) / (2 * a), 0);

            hitPoint1.x = (hitPoint1.y - point.y) * direction.x / direction.y + point.x;
            hitPoint1.z = (hitPoint1.y - point.y) * direction.z / direction.y + point.z;
            hitPoint2.x = (hitPoint2.y - point.y) * direction.x / direction.y + point.x;
            hitPoint2.z = (hitPoint2.y - point.y) * direction.z / direction.y + point.z;

            //a = direction.x * direction.x + direction.z * direction.z - r2 * direction.z * direction.z;
            //b = 2 * direction.x * point.x * direction.z - 2 * point.z * direction.x * direction.x - 2 * direction.x * Position.x * direction.z + 2 * r2 * z0;
            //c = point.z * point.z * direction.x * direction.x - 2 * point.z * direction.x * point.x * direction.z + 2 * point.z * direction.x * Position.x * direction.z + point.x * point.x * direction.z * direction.z
            //    - 2 * point.x * Position.x * direction.z * direction.z + Position.x * Position.x * direction.z * direction.z - r2 * Position.z * Position.z * direction.z * direction.z + Position.y * Position.y * direction.z * direction.z;

            //hitPoint1.z = (-b + Mathf.Sqrt(b * b - 4 * a * (c + hitPoint1.y * hitPoint1.y * direction.z * direction.z - 2 * Position.y * hitPoint1.y * direction.z * direction.z))) / (2 * a);
            //hitPoint2.z = (-b + Mathf.Sqrt(b * b - 4 * a * (c + hitPoint2.y * hitPoint2.y * direction.z * direction.z - 2 * Position.y * hitPoint2.y * direction.z * direction.z))) / (2 * a);


            if ((hitPoint1 - point).magnitude > (hitPoint2 - point).magnitude && hitPoint2.z > Position.z && hitPoint2.z < _height + Position.z&&Vector3.Angle(direction, hitPoint2-point)<90)
            {
                var imag = Position + new Vector3(0, _height, 0) - hitPoint2;
                Vector3 normal = Vector3.Cross(Vector3.Cross(Position - hitPoint2, imag), imag);
                if (hitPoint1.z < Position.z&& (hitPoint1 - point).magnitude < (hitPoint2 - point).magnitude)
                {
                    var zz = (Position.z - hitPoint2.z) / direction.z;
                    hitPoint2 = new Vector3(zz * direction.x + hitPoint2.x, zz * direction.y + hitPoint2.y, Position.z);
                    normal = new Vector3(0, 0, -1);
                }
                hitInfo = new HitInfo(hitPoint2, normal, Color);
                return true;
            }

            if (hitPoint1.z > Position.z && hitPoint1.z < _height + Position.z && Vector3.Angle(direction, hitPoint1 - point) < 90)
            {
                var imag = Position + new Vector3(0, _height, 0) - hitPoint1;
                Vector3 normal = Vector3.Cross(Vector3.Cross(Position - hitPoint1, imag), imag);
                if (hitPoint2.z < Position.z&& (hitPoint1 - point).magnitude > (hitPoint2 - point).magnitude)
                {
                    var zz = (Position.z - hitPoint1.z) / direction.z;
                    hitPoint1 = new Vector3(zz * direction.x + hitPoint1.x, zz * direction.y + hitPoint1.y, Position.z);
                    normal = new Vector3(0, 0, -1);
                }
                hitInfo = new HitInfo(hitPoint1, normal, Color);
                return true;
            }

            return false;
        }
    }
}
