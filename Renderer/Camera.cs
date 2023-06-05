using CG_2.Render.Interfaces;
using CG_2.Render.Light.Interfaces;
using Renderer.Layer;
using System;
using System.Drawing;
using Transform;

namespace CG_2.Render
{
    public class Camera
    {
        public Vector3 Position { get; set; }
        public Vector3 Rotation { 
            get=>_rotation/Mathf.PI*180; 
            set
            {
                _rotation = value * Mathf.PI / 180;
            }
        }

        private Vector3 _rotation;

        public Vector3 Forward => Vector3.RotateAround(Vector3.RotateAround(
            Vector3.RotateAround(new Vector3(0, 1, 0), 
                new Vector3(1, 0, 0), _rotation.x),
                new Vector3(0, 1, 0), _rotation.y),
                new Vector3(0, 0, 1), _rotation.z);

        public Vector3 Right => Vector3.RotateAround(Vector3.RotateAround(
            Vector3.RotateAround(new Vector3(1, 0, 0),
                new Vector3(1, 0, 0), _rotation.x),
                new Vector3(0, 1, 0), _rotation.y),
                new Vector3(0, 0, 1), _rotation.z);

        public Vector3 Up => Vector3.Cross(Right, Forward);

        public float FieldOfView { get; set; }

        public float Distance { get; set; }

        public Camera(Vector3 position = default(Vector3), Vector3 rotation = default(Vector3), float fieldOfView = 60, float distance = float.PositiveInfinity)
        {
            Position = position;
            Rotation = rotation;
            FieldOfView = fieldOfView;
            Distance = distance;
        }

        public void Render(Graphics graphics, ILight light, IRenderer renderer)
        {
            var up = Up;
            var forward = Forward;
            var right = Right;
            DateTime time = DateTime.Now;
            var ang = FieldOfView * Mathf.PI / 360;
            var upAng = ang / graphics.VisibleClipBounds.Width * graphics.VisibleClipBounds.Height;
            var deltaRight = right * Mathf.Sin(ang) / graphics.VisibleClipBounds.Width * 2;
            var deltaUp = up * Mathf.Sin(upAng) / graphics.VisibleClipBounds.Height * 2;
            var startDir = Forward * Mathf.Cos(ang) - Right * Mathf.Sin(ang) + Up * Mathf.Sin(upAng);
            graphics.Clear(Color.Black);
            var pen=new Pen(Color.Black);
            for (int x = 0; x < graphics.VisibleClipBounds.Width; x++)
            {
                var dirY = startDir;
                for (int y = 0; y < graphics.VisibleClipBounds.Height; y++)
                {
                    if(renderer.Raycast(Position, dirY, out HitInfo hitInfo))
                    {
                        pen.Color = hitInfo.Color;
                        pen.Color = light.CalculateVertexColor(hitInfo.Point, hitInfo.Normal, hitInfo.Color);
                        graphics.DrawRectangle(pen, x, y, 1, 1);
                    }
                    dirY -= deltaUp;
                }
                startDir += deltaRight;
            }
            //graphics.DrawRectangle(pen, 93, 93, 3,3);
            double t = (DateTime.Now - time).TotalSeconds;
            Console.WriteLine(t);
            //if (renderer.Raycast(Position, ))
        }
    }
}
