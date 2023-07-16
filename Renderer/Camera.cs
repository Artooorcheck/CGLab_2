using CG_2.Render.Interfaces;
using CG_2.Render.Light.Interfaces;
using Renderer.Layer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
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

        public void LocalMove(Vector3 direction)
        {
            var ang = -_rotation.z;
            Position += new Vector3 ((float)(Math.Sin(ang) * direction.y + Math.Cos(ang) *direction.x),
                                     (float)(-Math.Sin(ang) * direction.x + Math.Cos(ang) * direction.y),
                                     direction.z);
        }

        public void Render(PictureBox picture, ILight light, IRenderer renderer)
        {
            var up = Up;
            var forward = Forward;
            var right = Right;
            DateTime time = DateTime.Now;
            var ang = FieldOfView * Mathf.PI / 360;
            var upAng = ang / picture.Width * picture.Height;
            var deltaRight = right * Mathf.Sin(ang) / picture.Width * 2;
            var deltaUp = up * Mathf.Sin(upAng) / picture.Height * 2;
            var startDir = Forward * Mathf.Cos(ang) - Right * Mathf.Sin(ang) + Up * Mathf.Sin(upAng);

            Bitmap bmp = new Bitmap(picture.Width, picture.Height);
            BitmapData bmd = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height),
                                  ImageLockMode.ReadWrite,
                                  bmp.PixelFormat);

            unsafe
            {
                for (int y = 0; y < bmd.Height; y++)
                {
                    byte* row = (byte*)bmd.Scan0 + (y * bmd.Stride);
                    var dirY = startDir;
                    int PixelSize = 4;

                    for (int x = 0; x < bmd.Width; x++)
                    {
                        if (renderer.Raycast(Position, dirY, out HitInfo hitInfo))
                        {
                            var color = light.CalculateVertexColor(hitInfo.Point, hitInfo.Normal, hitInfo.Color);
                            row[x * PixelSize] = color.B; 
                            row[x * PixelSize + 1] = color.G; 
                            row[x * PixelSize + 2] = color.R; 
                            row[x * PixelSize + 3] = color.A;
                        }
                        else
                        {
                            row[x * PixelSize] = 0;
                            row[x * PixelSize + 1] = 0;
                            row[x * PixelSize + 2] = 0;
                            row[x * PixelSize + 3] = 255;
                        }
                        dirY += deltaRight;
                    }
                    startDir -= deltaUp;
                }
            }

            bmp.UnlockBits(bmd);
            picture.Image = bmp;
        }
    }
}
