using Bolt;
using Ludiq;
using System;
using UnityEngine;

namespace Dopetools.Tweening
{
    [RenamedFrom("Dopetools.Animation.DopeTweenUnitWidget")]
    [Widget(typeof(DopeTweenUnit))]
    public sealed class DopeTweenUnitWidget : UnitWidget<DopeTweenUnit>
    {
        public DopeTweenUnitWidget(FlowCanvas canvas, DopeTweenUnit unit) : base(canvas, unit)
        {
        }
        protected override bool showHeaderAddon => true;
        public override bool foregroundRequiresInput => true;
        protected override bool showTitle => true;
        protected override bool showSurtitle => true;

        public object lastValueStored;

        //private DopeEasingType lastEasingType;

        //private Type lastType;

        protected override void DrawHeaderAddon()
        {
            //LudiqGUI.Inspector(metadata[0], new Rect(headerAddonPosition.x, headerAddonPosition.y, headerAddonPosition.width, 18), GUIContent.none);
            //if (lastValueStored != unit.defaultValues["Type"])
            //{
            //    lastValueStored = unit.defaultValues["Type"];
            //    unit.Define();
            //}
            //if (lastEasingType != unit.dopeEasingType)
            //{
            //    lastEasingType = unit.dopeEasingType;
            //    Reposition();
            //    unit.Define();
            //    Debug.Log("Define!");
            //}

            //if (unit.defaultValues["DopeEasingTypeValue"] != null ) { unit.Define(); }

        }

       

        public override void BeforeFrame()
        {
            base.BeforeFrame();

            if (GetHeaderAddonWidth() != headerAddonPosition.width ||
                GetHeaderAddonHeight(headerAddonPosition.width) != headerAddonPosition.height)
            {
                Reposition();
            }
        }

        protected override float GetHeaderAddonWidth()
        {
            return 130;
        }

        protected override float GetHeaderAddonHeight(float width)
        {
            //if (unit.dopeEasingType == DopeEasingType.EaseInElastic ||
            //    unit.dopeEasingType == DopeEasingType.EaseOutElastic ||
            //    unit.dopeEasingType == DopeEasingType.EaseInOutElastic) return 20;
            return 0;
        }
    }
}