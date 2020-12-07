using UnityEngine;
using Bolt;
using Ludiq;

namespace Dopetools.Animation
{
    /* 
     * Most functions taken from Tween.js - Licensed under the MIT license
     * at https://github.com/sole/tween.js
     * Quadratic.Bezier by @fonserbc - Licensed under WTFPL license
     */
    //public delegate float EasingFunction(float k);

    [UnitTitle("Evaluate")]
    [UnitSurtitle("DopeEaseing")]
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
            TimeValue = ValueInput<float>("Time"); 

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
            public static float Lin(float k)
            {
                return k;
            }
        }

        public class Quadratic
        {
            public static float In(float k)
            {
                return k * k;
            }

            public static float Out(float k)
            {
                return k * (2f - k);
            }

            public static float InOut(float k)
            {
                if ((k *= 2f) < 1f) return 0.5f * k * k;
                return -0.5f * ((k -= 1f) * (k - 2f) - 1f);
            }

            /* 
             * Quadratic.Bezier(k,0) behaves like Quadratic.In(k)
             * Quadratic.Bezier(k,1) behaves like Quadratic.Out(k)
             *
             * If you want to learn more check Alan Wolfe's post about it http://www.demofox.org/bezquad1d.html
             */
            public static float Bezier(float k, float c)
            {
                return c * 2 * k * (1 - k) + k * k;
            }
        };

        public class Cubic
        {
            public static float In(float k)
            {
                return k * k * k;
            }

            public static float Out(float k)
            {
                return 1f + ((k -= 1f) * k * k);
            }

            public static float InOut(float k)
            {
                if ((k *= 2f) < 1f) return 0.5f * k * k * k;
                return 0.5f * ((k -= 2f) * k * k + 2f);
            }
        };

        public class Quartic
        {
            public static float In(float k)
            {
                return k * k * k * k;
            }

            public static float Out(float k)
            {
                return 1f - ((k -= 1f) * k * k * k);
            }

            public static float InOut(float k)
            {
                if ((k *= 2f) < 1f) return 0.5f * k * k * k * k;
                return -0.5f * ((k -= 2f) * k * k * k - 2f);
            }
        };

        public class Quintic
        {
            public static float In(float k)
            {
                return k * k * k * k * k;
            }

            public static float Out(float k)
            {
                return 1f + ((k -= 1f) * k * k * k * k);
            }

            public static float InOut(float k)
            {
                if ((k *= 2f) < 1f) return 0.5f * k * k * k * k * k;
                return 0.5f * ((k -= 2f) * k * k * k * k + 2f);
            }
        };

        public class Sinusoidal
        {
            public static float In(float k)
            {
                return 1f - Mathf.Cos(k * Mathf.PI / 2f);
            }

            public static float Out(float k)
            {
                return Mathf.Sin(k * Mathf.PI / 2f);
            }

            public static float InOut(float k)
            {
                return 0.5f * (1f - Mathf.Cos(Mathf.PI * k));
            }
        };

        public class Exponential
        {
            public static float In(float k)
            {
                return k == 0f ? 0f : Mathf.Pow(1024f, k - 1f);
            }

            public static float Out(float k)
            {
                return k == 1f ? 1f : 1f - Mathf.Pow(2f, -10f * k);
            }

            public static float InOut(float k)
            {
                if (k == 0f) return 0f;
                if (k == 1f) return 1f;
                if ((k *= 2f) < 1f) return 0.5f * Mathf.Pow(1024f, k - 1f);
                return 0.5f * (-Mathf.Pow(2f, -10f * (k - 1f)) + 2f);
            }
        };

        public class Circular
        {
            public static float In(float k)
            {
                return 1f - Mathf.Sqrt(1f - k * k);
            }

            public static float Out(float k)
            {
                return Mathf.Sqrt(1f - ((k -= 1f) * k));
            }

            public static float InOut(float k)
            {
                if ((k *= 2f) < 1f) return -0.5f * (Mathf.Sqrt(1f - k * k) - 1);
                return 0.5f * (Mathf.Sqrt(1f - (k -= 2f) * k) + 1f);
            }
        };

        public class Elastic
        {
            public static float In(float k)
            {
                if (k == 0) return 0;
                if (k == 1) return 1;
                return -Mathf.Pow(2f, 10f * (k -= 1f)) * Mathf.Sin((k - 0.1f) * (2f * Mathf.PI) / 0.4f);
            }

            public static float Out(float k)
            {
                if (k == 0) return 0;
                if (k == 1) return 1;
                return Mathf.Pow(2f, -10f * k) * Mathf.Sin((k - 0.1f) * (2f * Mathf.PI) / 0.4f) + 1f;
            }

            public static float InOut(float k)
            {
                if ((k *= 2f) < 1f) return -0.5f * Mathf.Pow(2f, 10f * (k -= 1f)) * Mathf.Sin((k - 0.1f) * (2f * Mathf.PI) / 0.4f);
                return Mathf.Pow(2f, -10f * (k -= 1f)) * Mathf.Sin((k - 0.1f) * (2f * Mathf.PI) / 0.4f) * 0.5f + 1f;
            }
        };

        public class Back
        {
            static float s = 1.70158f;
            static float s2 = 2.5949095f;

            public static float In(float k)
            {
                return k * k * ((s + 1f) * k - s);
            }

            public static float Out(float k)
            {
                return (k -= 1f) * k * ((s + 1f) * k + s) + 1f;
            }

            public static float InOut(float k)
            {
                if ((k *= 2f) < 1f) return 0.5f * (k * k * ((s2 + 1f) * k - s2));
                return 0.5f * ((k -= 2f) * k * ((s2 + 1f) * k + s2) + 2f);
            }
        };

        public class Bounce
        {
            public static float In(float k)
            {
                return 1f - Out(1f - k);
            }

            public static float Out(float k)
            {
                if (k < (1f / 2.75f))
                {
                    return 7.5625f * k * k;
                }
                else if (k < (2f / 2.75f))
                {
                    return 7.5625f * (k -= (1.5f / 2.75f)) * k + 0.75f;
                }
                else if (k < (2.5f / 2.75f))
                {
                    return 7.5625f * (k -= (2.25f / 2.75f)) * k + 0.9375f;
                }
                else
                {
                    return 7.5625f * (k -= (2.625f / 2.75f)) * k + 0.984375f;
                }
            }

            public static float InOut(float k)
            {
                if (k < 0.5f) return In(k * 2f) * 0.5f;
                return Out(k * 2f - 1f) * 0.5f + 0.5f;
            }
        };
    }

}