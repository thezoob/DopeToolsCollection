using UnityEngine;

namespace Dopetools.Tweening
{ 

    #region TweeningLibrary

        public class Linear
        {
            public static float Lin(float t)
            {
                return t;
            }
        }

        public class Quadratic
        {
            public static float In(float t)
            {
                return t * t;
            }

            public static float Out(float t)
            {
                return t * (2f - t);
            }

            public static float InOut(float t)
            {
                if ((t *= 2f) < 1f) return 0.5f * t * t;
                return -0.5f * ((t -= 1f) * (t - 2f) - 1f);
            }

            /* 
             * Quadratic.Bezier(t,0) behaves like Quadratic.In(t)
             * Quadratic.Bezier(t,1) behaves like Quadratic.Out(t)
             *
             * If you want to learn more check Alan Wolfe's post about it http://www.demofox.org/bezquad1d.html
             */
            public static float Bezier(float t, float c)
            {
                return c * 2 * t * (1 - t) + t * t;
            }
        };

        public class Cubic
        {
            public static float In(float t)
            {
                return t * t * t;
            }

            public static float Out(float t)
            {
                return 1f + ((t -= 1f) * t * t);
            }

            public static float InOut(float t)
            {
                if ((t *= 2f) < 1f) return 0.5f * t * t * t;
                return 0.5f * ((t -= 2f) * t * t + 2f);
            }
        };

        public class Quartic
        {
            public static float In(float t)
            {
                return t * t * t * t;
            }

            public static float Out(float t)
            {
                return 1f - ((t -= 1f) * t * t * t);
            }

            public static float InOut(float t)
            {
                if ((t *= 2f) < 1f) return 0.5f * t * t * t * t;
                return -0.5f * ((t -= 2f) * t * t * t - 2f);
            }
        };

        public class Quintic
        {
            public static float In(float t)
            {
                return t * t * t * t * t;
            }

            public static float Out(float t)
            {
                return 1f + ((t -= 1f) * t * t * t * t);
            }

            public static float InOut(float t)
            {
                if ((t *= 2f) < 1f) return 0.5f * t * t * t * t * t;
                return 0.5f * ((t -= 2f) * t * t * t * t + 2f);
            }
        };

        public class Sinusoidal
        {
            public static float In(float t)
            {
                return 1f - Mathf.Cos(t * Mathf.PI / 2f);
            }

            public static float Out(float t)
            {
                return Mathf.Sin(t * Mathf.PI / 2f);
            }

            public static float InOut(float t)
            {
                return 0.5f * (1f - Mathf.Cos(Mathf.PI * t));
            }
        };

        public class Exponential
        {
            public static float In(float t)
            {
                return t == 0f ? 0f : Mathf.Pow(1024f, t - 1f);
            }

            public static float Out(float t)
            {
                return t == 1f ? 1f : 1f - Mathf.Pow(2f, -10f * t);
            }

            public static float InOut(float t)
            {
                if (t == 0f) return 0f;
                if (t == 1f) return 1f;
                if ((t *= 2f) < 1f) return 0.5f * Mathf.Pow(1024f, t - 1f);
                return 0.5f * (-Mathf.Pow(2f, -10f * (t - 1f)) + 2f);
            }
        };

        public class Circular
        {
            public static float In(float t)
            {
                return 1f - Mathf.Sqrt(1f - t * t);
            }

            public static float Out(float t)
            {
                return Mathf.Sqrt(1f - ((t -= 1f) * t));
            }

            public static float InOut(float t)
            {
                if ((t *= 2f) < 1f) return -0.5f * (Mathf.Sqrt(1f - t * t) - 1);
                return 0.5f * (Mathf.Sqrt(1f - (t -= 2f) * t) + 1f);
            }
        };

        public class Elastic
        {
            public static float In(float t, float smooth = .4f, float spring = 2f)
            {
                if (t == 0) return 0;
                if (t == 1) return 1;
                return -Mathf.Pow(2f, 10f * (t -= 1f)) * Mathf.Sin((t - 0.1f) * (Mathf.Abs(spring) * Mathf.PI) / Mathf.Abs(smooth));
            }

            public static float Out(float t, float smooth = .4f, float spring = 2f)
            {
                if (t == 0) return 0;
                if (t == 1) return 1;
                return Mathf.Pow(2f, -10f * t) * Mathf.Sin((t - 0.1f) * (Mathf.Abs(spring) * Mathf.PI) / Mathf.Abs(smooth)) + 1f;
            }

            public static float InOut(float t, float smooth = .4f, float spring = 2f)
            {
                if ((t *= 2f) < 1f) return -0.5f * Mathf.Pow(2f, 10f * (t -= 1f)) * Mathf.Sin((t - 0.1f) * (Mathf.Abs(spring) * Mathf.PI) / Mathf.Abs(smooth));
                return Mathf.Pow(2f, -10f * (t -= 1f)) * Mathf.Sin((t - 0.1f) * (Mathf.Abs(spring) * Mathf.PI) / Mathf.Abs(smooth)) * 0.5f + 1f;
            }
        };

        public class Back
        {
            static float s = 1.70158f;
            static float s2 = 2.5949095f;

            public static float In(float t)
            {
                return t * t * ((s + 1f) * t - s);
            }

            public static float Out(float t)
            {
                return (t -= 1f) * t * ((s + 1f) * t + s) + 1f;
            }

            public static float InOut(float t)
            {
                if ((t *= 2f) < 1f) return 0.5f * (t * t * ((s2 + 1f) * t - s2));
                return 0.5f * ((t -= 2f) * t * ((s2 + 1f) * t + s2) + 2f);
            }
        };

        public class Bounce
        {
            public static float In(float t)
            {
                return 1f - Out(1f - t);
            }

            public static float Out(float t)
            {
                if (t < (1f / 2.75f))
                {
                    return 7.5625f * t * t;
                }
                else if (t < (2f / 2.75f))
                {
                    return 7.5625f * (t -= (1.5f / 2.75f)) * t + 0.75f;
                }
                else if (t < (2.5f / 2.75f))
                {
                    return 7.5625f * (t -= (2.25f / 2.75f)) * t + 0.9375f;
                }
                else
                {
                    return 7.5625f * (t -= (2.625f / 2.75f)) * t + 0.984375f;
                }
            }

            public static float InOut(float t)
            {
                if (t < 0.5f) return In(t * 2f) * 0.5f;
                return Out(t * 2f - 1f) * 0.5f + 0.5f;
            }
        };
    #endregion

    public static class Re
    {
        public static float Map(this float from, float fromMin, float fromMax, float toMin, float toMax)
        {
            var fromAbs = from - fromMin;
            var fromMaxAbs = fromMax - fromMin;

            var normal = fromAbs / fromMaxAbs;

            var toMaxAbs = toMax - toMin;
            var toAbs = toMaxAbs * normal;

            var to = toAbs + toMin;

            return to;
        }
    }

} //NameSpace



