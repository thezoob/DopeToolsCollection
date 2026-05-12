using Ludiq;
using Bolt;
using UnityEngine;

namespace Dopetools.Tweening
{


    [Descriptor(typeof(DopeTweenUnit))]
    public class DopeTweenUnitDescriptor : UnitDescriptor<DopeTweenUnit>
    {   
        //finds path to root of DopeTween folder. Thanks Jason from LASM. Requires a file in the root folder of DopeTween      
        private static string PathOf(string fileName) 
        {
            var files = UnityEditor.AssetDatabase.FindAssets(fileName);
            if (files.Length == 0) return string.Empty;
            var assetPath = UnityEditor.AssetDatabase.GUIDToAssetPath(files[0]).Replace(fileName, string.Empty);
            return assetPath;
        }


        public DopeTweenUnitDescriptor(DopeTweenUnit target) : base(target)
        {
        }

        protected override string DefaultSummary()
        {
            return "Tween anything that can be tweened!";
        }

        // Unit Summary
        protected override string DefinedSummary()
        {
            return "Tween anything that can be tweened!";
        }

        protected override string DefaultTitle()
        {
            return target.dopeTweenInputValueType.ToString();
        }

        protected override string DefinedShortTitle()
        {
            return target.dopeTweenInputValueType.ToString();
        }

        private DopeTweenInputValueType InputValueType;
        
        //Custom Icon
        private Texture2D texture;
        private readonly string icon = PathOf("dopetweenroot") + "Editor/Resources/DopeTween.png"; //returns image path by using the r

        protected override EditorTexture DefaultIcon()
        {
            if (texture == null)
            {
                texture = UnityEditor.AssetDatabase.LoadAssetAtPath<Texture2D>(icon);
            }

            return EditorTexture.Single(texture);
        }

        protected override EditorTexture DefinedIcon()
        {
            if (texture == null)
            {
                texture = UnityEditor.AssetDatabase.LoadAssetAtPath<Texture2D>(icon);
            }

            return EditorTexture.Single(texture);
        }

        // Port Summary

        protected override void DefinedPort(IUnitPort port, UnitPortDescription description)
        {
            base.DefinedPort(port, description);

            if (port == target.A)
            {
                description.summary = "Start Value.";
                description.showLabel = true;
            }
            else if (port == target.B)
            {
                description.summary = "End Value.";
                description.showLabel = true;
            }
            else if (port == target.ResultValue)
            {
                description.summary = "Tweened Value.";
                description.showLabel = false;
                description.label = "Tweened Value.";
            }

            else if (port == target.elapsedSeconds)
            {
                description.summary = "Elapsed tween time in seconds.";
                description.showLabel = true;
                description.label = "Elapsed Seconds";
            }

            else if (port == target.elapsedRatio)
            {
                description.summary = "Elapsed tween progress from 0 to 1.";
                description.showLabel = true;
                description.label = "Elapsed Ratio";
            }

            else if (port == target.remainingSeconds)
            {
                description.summary = "Remaining tween time in seconds.";
                description.showLabel = true;
                description.label = "Remaining Seconds";
            }

            else if (port == target.remainingRatio)
            {
                description.summary = "Remaining tween progress from 1 to 0.";
                description.showLabel = true;
                description.label = "Remaining Ratio";
            }

            else if (port == target.duration)
            {
                description.summary = "Duration of Tween in seconds.";
                description.showLabel = true;
            }

            else if (port == target.start)
            {
                description.summary = "Start Tween.";
                description.showLabel = false;
            }

            else if (port == target.pause)
            {
                description.summary = "Pause the active Tween.";
                description.showLabel = true;
            }

            else if (port == target.resume)
            {
                description.summary = "Resume a paused Tween.";
                description.showLabel = true;
            }

            else if (port == target.cancel)
            {
                description.summary = "Cancel the active Tween without firing Done.";
                description.showLabel = true;
            }

            else if (port == target.started)
            {
                description.summary = "Event fired when Tween starts.";
                description.showLabel = true;
            }

            else if (port == target.paused)
            {
                description.summary = "Event fired after Pause is called.";
                description.showLabel = true;
            }

            else if (port == target.resumed)
            {
                description.summary = "Event fired after Resume is called.";
                description.showLabel = true;
            }

            else if (port == target.cancelled)
            {
                description.summary = "Event fired after Cancel is called.";
                description.showLabel = true;
            }

            else if (port == target.tick)
            {
                description.summary = "Connect this to the value you want to Tween.";
                description.showLabel = true;
            }

            else if (port == target.completed)
            {
                description.summary = "Event fired when Tweening is completed";
                description.showLabel = true;
                description.label = "Done";
            }

            else if (port == target.DopeEasingTypeValue)
            {
                description.summary = "The Type of Easing Value to be applied to the tween.";
                description.showLabel = false;
                description.label = "Easing Type";
            }

        }
    }

}
