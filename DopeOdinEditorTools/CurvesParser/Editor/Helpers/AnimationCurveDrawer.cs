using Sirenix.OdinInspector.Editor;

using UnityEditor;

using UnityEngine;

public class AnimationCurveDrawer : OdinAttributeDrawer<AnimationCurveHeightAttribute, AnimationCurve>
{
    protected override void DrawPropertyLayout(GUIContent label)
    {
        var rect = EditorGUILayout.GetControlRect(true, this.Attribute.height);

        var animationCurve = this.ValueEntry.SmartValue;

        animationCurve = EditorGUI.CurveField(rect, animationCurve);

        this.ValueEntry.SmartValue = animationCurve;
    }
}