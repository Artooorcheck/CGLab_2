using System;

namespace Transform
{
    public static class Mathf
    {
        public static float PI => (float)Math.PI;
        public static float Clamp(float value, float minValue, float maxValue)
        {
            return value > maxValue ? maxValue : value < minValue ? minValue : value;
        }

        public static float Sqrt(float value)
        {
            return (float)Math.Sqrt(value);
        }
        public static float Sin(float value)
        {
            return (float)Math.Sin(value);
        }
        public static float Cos(float value)
        {
            return (float)Math.Cos(value);
        }
    }
    
}
