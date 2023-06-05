using System;
using System.Drawing;

namespace Transform
{
    public struct Vector3
    {
        public float x { get; set; }
        public float y { get; set; }
        public float z { get; set; }


        public float magnitude { get => Mathf.Sqrt(x * x + y * y + z * z); }
        public Vector3 normalizate => this / magnitude;

        public static Vector3 zero => new Vector3(0, 0, 0);

        private static float[,] RotateMatrix(float angle, Vector3 axis)
        {
            var res = new float[3, 3];
            res[0, 0] = Mathf.Cos(angle) + (1 - Mathf.Cos(angle)) * axis.x * axis.x;
            res[0, 1] = -Mathf.Sin(angle) * axis.z + (1 - Mathf.Cos(angle)) * axis.x * axis.y;
            res[0, 2] = Mathf.Sin(angle) * axis.y + (1 - Mathf.Cos(angle)) * axis.x * axis.z;

            res[1, 0] = Mathf.Sin(angle) * axis.z + (1 - Mathf.Cos(angle)) * axis.y * axis.x;
            res[1, 1] = Mathf.Cos(angle) + (1 - Mathf.Cos(angle)) * axis.y * axis.y;
            res[1, 2] = -Mathf.Sin(angle) * axis.x + (1 - Mathf.Cos(angle)) * axis.y * axis.z;

            res[2, 0] = -Mathf.Sin(angle) * axis.y + (1 - Mathf.Cos(angle)) * axis.z * axis.x;
            res[2, 1] = Mathf.Sin(angle) * axis.x + (1 - Mathf.Cos(angle)) * axis.z * axis.y;
            res[2, 2] = Mathf.Cos(angle) + (1 - Mathf.Cos(angle)) * axis.z * axis.z;
            return res;
        }

        public static Vector3 RotateAround(Vector3 vector, Vector3 axis, float angle)
        {
            var matrix = RotateMatrix(angle, axis);
            var res = new Vector3();
            res.x = matrix[0, 0] * vector.x + matrix[0, 1] * vector.y + matrix[0, 2] * vector.z;
            res.y = matrix[1, 0] * vector.x + matrix[1, 1] * vector.y + matrix[1, 2] * vector.z;
            res.z = matrix[2, 0] * vector.x + matrix[2, 1] * vector.y + matrix[2, 2] * vector.z;
            return res.normalizate;
        }

        public static Vector3 Cross(Vector3 a, Vector3 b)
        {
            var res = new Vector3();
            res.x = a.y * b.z - a.z * b.y;
            res.y = a.z * b.x - a.x * b.z;
            res.z = a.x * b.y - a.y * b.x;
            return res.normalizate;
        }

        public static Vector3 operator +(Vector3 a, Vector3 b)
        {
            a.x += b.x;
            a.y += b.y;
            a.z += b.z;
            return a;
        }

        public static Vector3 operator -(Vector3 a, Vector3 b)
        {
            a.x -= b.x;
            a.y -= b.y;
            a.z -= b.z;
            return a;
        }

        public Vector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Vector3(Color color)
        {
            this.x = color.R/255f;
            this.y = color.G/255f;
            this.z = color.B/255f;
        }

        public Color Color => Color.FromArgb(255, (byte)(Mathf.Clamp(x * 255,0,255)), (byte)Mathf.Clamp(y * 255, 0, 255), (byte)Mathf.Clamp(z * 255, 0, 255));

        public static Vector3 operator *(Vector3 a, float b)
        {
            a.x *= b;
            a.y *= b;
            a.z *= b;
            return a;
        }
        public static Vector3 operator /(Vector3 a, float b)
        {
            a.x /= b;
            a.y /= b;
            a.z /= b;
            return a;
        }

        public static float Angle(Vector3 a, Vector3 b)
        {
            var cos = Mathf.Clamp((a.x * b.x + a.y * b.y + a.z * b.z) / (a.magnitude * b.magnitude), -1, 1);
            return (float)(Math.Acos(cos) / Math.PI * 180) ;
        }

        public static float operator *(Vector3 vector1, Vector3 vector2)
        {
            return vector1.x * vector2.x + vector1.y * vector2.y + vector1.z * vector2.z;
        }

        public override string ToString()
        {
            return $"x:{Math.Round(x,1)} y:{Math.Round(y, 1)} z:{Math.Round(z, 1)}";
        }
    }
}
