using UnityEngine;
using Bolt;
using Ludiq;

namespace Dopetools.Animation
{
    /* 
     * Functions borrowed from Tween.js - Licensed under the MIT license
     * at https://github.com/sole/tween.js
     * https://gist.github.com/Fonserbc/3d31a25e87fdaa541ddf - C# Easing Functions here
     */


    [UnitTitle("Evaluate")]
    [UnitSurtitle("DopeEase")]
    [UnitCategory("DopeTools/Animation")]
    [TypeIcon(typeof(AnimationCurve))]


    public sealed class DopeEasingUnit : Unit 
    {

        public DopeEasingUnit() : base() { }

        public DopeEasingUnit(DopeEasingType dopeEasingType) : base()
        {
            this.dopeEasingType = dopeEasingType;
        }

        public DopeEasingUnit(DopeEasingUnit dopeEasingUnit)
        {
            this.dopeEasingUnit = dopeEasingUnit;
        }

        private DopeEasingUnit dopeEasingUnit;

        [DoNotSerialize, PortLabelHidden]
        public ControlInput Enter;

        [DoNotSerialize]
        public ValueInput TimeValue;

        //[Serialize, PortLabel("Type")]
        //[Inspectable, InspectorToggleLeft, UnitHeaderInspectable]
        //public DopeEasingType EasingType;

        [DoNotSerialize, PortLabelHidden]
        public ValueInput DopeEasingTypeValue;


        [DoNotSerialize, PortLabelHidden]
        public ControlOutput Exit;

        [DoNotSerialize, PortLabelHidden]
        public ValueOutput EasedValue;

        [Serialize]
        public DopeEasingType dopeEasingType;

        private float inputTime;


        //override Deinifition for Bolt Unit
        protected override void Definition()
        {
            isControlRoot = true;

            DopeEasingTypeValue = ValueInput("Type", dopeEasingType);
            TimeValue = ValueInput<float>("Time", 0f); 

            Enter = ControlInput("Enter", (flow) => 
            {
                inputTime = flow.GetValue<float>(TimeValue);
                dopeEasingType = flow.GetValue<DopeEasingType>(DopeEasingTypeValue);
                return Exit;
            });

            Exit = ControlOutput("Exit");

            EasedValue = ValueOutput<float>("EasedValue", (flow) =>
            {
                if(dopeEasingType == DopeEasingType.Linear)
                {
                    float ease = Linear.Lin(inputTime);
                    return ease;
                }
                if(dopeEasingType == DopeEasingType.EaseInBack)
                {
                    float ease = Back.In(inputTime);
                    return ease;
                }

                if (dopeEasingType == DopeEasingType.EaseOutBack)
                {
                    float ease = Back.Out(inputTime);
                    return ease;
                }

                if (dopeEasingType == DopeEasingType.EaseInOutBack)
                {
                    float ease = Back.InOut(inputTime);
                    return ease;
                }


                if (dopeEasingType == DopeEasingType.EaseInBounce)
                {
                    float ease = Bounce.In(inputTime);
                    return ease;
                }

                if (dopeEasingType == DopeEasingType.EaseOutBounce)
                {
                    float ease = Bounce.Out(inputTime);
                    return ease;
                }

                if (dopeEasingType == DopeEasingType.EaseInOutBounce)
                {
                    float ease = Bounce.InOut(inputTime);
                    return ease;
                }


                if (dopeEasingType == DopeEasingType.EaseInCirc)
                {
                    float ease = Circular.In(inputTime);
                    return ease;
                }

                if (dopeEasingType == DopeEasingType.EaseOutCirc)
                {
                    float ease = Circular.Out(inputTime);
                    return ease;
                }

                if (dopeEasingType == DopeEasingType.EaseInOutCirc)
                {
                    float ease = Circular.InOut(inputTime);
                    return ease;
                }


                if (dopeEasingType == DopeEasingType.EaseInCubic)
                {
                    float ease = Cubic.In(inputTime);
                    return ease;
                }

                if (dopeEasingType == DopeEasingType.EaseOutCubic)
                {
                    float ease = Cubic.Out(inputTime);
                    return ease;
                }

                if (dopeEasingType == DopeEasingType.EaseInOutCubic)
                {
                    float ease = Cubic.InOut(inputTime);
                    return ease;
                }


                if (dopeEasingType == DopeEasingType.EaseInElastic)
                {
                    float ease = Elastic.In(inputTime);
                    return ease;
                }

                if (dopeEasingType == DopeEasingType.EaseOutElastic)
                {
                    float ease = Elastic.Out(inputTime);
                    return ease;
                }

                if (dopeEasingType == DopeEasingType.EaseInOutElastic)
                {
                    float ease = Elastic.InOut(inputTime);
                    return ease;
                }


                if (dopeEasingType == DopeEasingType.EaseInExpo)
                {
                    float ease = Exponential.In(inputTime);
                    return ease;
                }

                if (dopeEasingType == DopeEasingType.EaseOutExpo)
                {
                    float ease = Exponential.Out(inputTime);
                    return ease;
                }

                if (dopeEasingType == DopeEasingType.EaseInOutExpo)
                {
                    float ease = Exponential.InOut(inputTime);
                    return ease;
                }


                if (dopeEasingType == DopeEasingType.EaseInQuad)
                {
                    float ease = Quadratic.In(inputTime);
                    return ease;
                }

                if (dopeEasingType == DopeEasingType.EaseOutQuad)
                {
                    float ease = Quadratic.Out(inputTime);
                    return ease;
                }

                if (dopeEasingType == DopeEasingType.EaseInOutQuad)
                {
                    float ease = Quadratic.InOut(inputTime);
                    return ease;
                }


                if (dopeEasingType == DopeEasingType.EaseInQuart)
                {
                    float ease = Quartic.In(inputTime);
                    return ease;
                }

                if (dopeEasingType == DopeEasingType.EaseOutQuart)
                {
                    float ease = Quartic.Out(inputTime);
                    return ease;
                }

                if (dopeEasingType == DopeEasingType.EaseInOutQuart)
                {
                    float ease = Quartic.InOut(inputTime);
                    return ease;
                }


                if (dopeEasingType == DopeEasingType.EaseInQuint)
                {
                    float ease = Quintic.In(inputTime);
                    return ease;
                }

                if (dopeEasingType == DopeEasingType.EaseOutQuint)
                {
                    float ease = Quintic.Out(inputTime);
                    return ease;
                }

                if (dopeEasingType == DopeEasingType.EaseInOutQuint)
                {
                    float ease = Quintic.InOut(inputTime);
                    return ease;
                }


                if (dopeEasingType == DopeEasingType.EaseInSine)
                {
                    float ease = Sinusoidal.In(inputTime);
                    return ease;
                }

                if (dopeEasingType == DopeEasingType.EaseOutSine)
                {
                    float ease = Sinusoidal.Out(inputTime);
                    return ease;
                }

                if (dopeEasingType == DopeEasingType.EaseInOutSine)
                {
                    float ease = Sinusoidal.InOut(inputTime);
                    return ease;
                }

                return 0f;
            });

            Requirement(TimeValue, Enter);
            Requirement(DopeEasingTypeValue, Enter);

            Succession(Enter,Exit);
            //Succession(Enter, EasedValue);
            
            
        }

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
            public static float In(float t)
            {
                if (t == 0) return 0;
                if (t == 1) return 1;
                return -Mathf.Pow(2f, 10f * (t -= 1f)) * Mathf.Sin((t - 0.1f) * (2f * Mathf.PI) / 0.4f);
            }

            public static float Out(float t)
            {
                if (t == 0) return 0;
                if (t == 1) return 1;
                return Mathf.Pow(2f, -10f * t) * Mathf.Sin((t - 0.1f) * (2f * Mathf.PI) / 0.4f) + 1f;
            }

            public static float InOut(float t)
            {
                if ((t *= 2f) < 1f) return -0.5f * Mathf.Pow(2f, 10f * (t -= 1f)) * Mathf.Sin((t - 0.1f) * (2f * Mathf.PI) / 0.4f);
                return Mathf.Pow(2f, -10f * (t -= 1f)) * Mathf.Sin((t - 0.1f) * (2f * Mathf.PI) / 0.4f) * 0.5f + 1f;
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
    }

}