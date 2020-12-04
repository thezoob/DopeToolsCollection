using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using Sirenix.Utilities.Editor;


public class CurvesParser : OdinEditorWindow
{

    [MenuItem("Tools/DopeCurves/AnimationCurveParser")]
    private static void OpenWindow()
    {
        GetWindow<CurvesParser>().Show();
    }

    [BoxGroup("AnimationCurve", true)]
    [ShowInInspector, Required, HideLabel]
    [OnValueChanged("Parse")]
    [OnValueChanged("ClearTextField")]
    [OnValueChanged("GenerateProperty")]
    [Title("Name")]
    [InlineButton("Clear")]
    private string AnimationCurveName = string.Empty;

    private void Clear()
    {
        AnimationCurveName = string.Empty;
        GeneratedProperty = string.Empty;
        GeneratedCode = string.Empty;
    }

    [BoxGroup("AnimationCurve", true)]
    [AnimationCurveHeight(100)]
    [LabelWidth(1f)]
    //[OnValueChanged("Parse")]
    [OnInspectorGUI("Parse")]
    [SerializeField]
    [ShowInInspector]
    private AnimationCurve animationCurve;

    [BoxGroup("Generated Code", true)]
    [ShowInInspector]
    [InlineButton("CopyProperty"), GUIColor(0, .8f, .4f)]
    [HideLabel]
    private string GeneratedProperty = string.Empty;

    private void CopyProperty()
    {
        if(!GeneratedProperty.Equals(string.Empty))
        Clipboard.Copy(GeneratedProperty);
    }
         
    [BoxGroup("Generated Code", true)]
    [HideLabel]
    [SerializeField]
    [ShowInInspector]
    [TextArea(3, 200)]
    private string GeneratedCode = string.Empty;

    [BoxGroup("Generated Code",true)]
    [Button("Copy", ButtonSizes.Large), GUIColor(0, .8f, .4f)]
    private void Copy()
    {
        if(!GeneratedCode.Equals(string.Empty))
        Clipboard.Copy(GeneratedCode);
    }

    protected override void OnEnable()
    {
        GeneratedCode = string.Empty;
        AnimationCurveName = string.Empty;
        animationCurve = null;
        GeneratedProperty = string.Empty;
    }

    private void ClearTextField()
    {
        if(AnimationCurveName == string.Empty)
        {
            GeneratedCode = string.Empty;
            GeneratedProperty = string.Empty;
            Clipboard.Clear();
            Debug.Log("Cleared!");

        }
    }

    private void GenerateProperty()
    {
        if(!AnimationCurveName.Equals(string.Empty))
        GeneratedProperty = "private AnimationCurve " + AnimationCurveName + ";";
    }

    //[BoxGroup("Parsed Keyframe List")]
    //[Button("Parse", ButtonSizes.Large, ButtonStyle.FoldoutButton), GUIColor(0, .8f, .4f)]
    private List<Dictionary<string, float>> Parse()
    {
        var keys = animationCurve.keys;
        var list = new List<Dictionary<string, float>>();
        string generateNewKeyframe = "else if (dopeCurvesType == DopeCurvesType." + AnimationCurveName + ")" + "\n" + "{" + "\n" + AnimationCurveName + " = new AnimationCurve();" + "\n";
        string generateReturnKeyframe = "\n" + "return " + AnimationCurveName + ";" + "\n" + "}";

        if (!AnimationCurveName.Equals(string.Empty) && !animationCurve.Equals(null))
            foreach (Keyframe keyframe in keys)
                {
                    list.Add(AddKeyframeValues(keyframe));
                    GeneratedCode = generateNewKeyframe + string.Concat(generateAnimationCurve(list)) + generateReturnKeyframe;
                }
            return list;
    }


    private Dictionary<string, float> AddKeyframeValues(Keyframe keyframes)
    {
        var pairs = new Dictionary<string, float>
        {
            { "Time", keyframes.time },
            { "Value", keyframes.value },
            { "inTangent", keyframes.inTangent },
            { "OutTangent", keyframes.outTangent },
            { "InWt", keyframes.inWeight },
            { "OutWt", keyframes.outWeight },
            
        };

        return pairs;
    }

    private List<string> generateAnimationCurve(List<Dictionary<string, float>> genList)
    {
        List<string> generatedString = new List<string>();
        foreach (Dictionary<string, float> item in genList)
        {
            float time;
            float value;
            float inTangent;
            float outTangent;
            float inWt;
            float outwt;
            string stringObject;

            string generateKeyframeTextBegin = ".AddKey(new Keyframe(";
            string generateKeyframeTextEnd = "));";

            item.TryGetValue("Time", out time);
            item.TryGetValue("Value", out value);
            item.TryGetValue("inTangent", out inTangent);
            item.TryGetValue("OutTangent", out outTangent);
            item.TryGetValue("InWt", out inWt);
            item.TryGetValue("OutWt", out outwt);

            stringObject = string.Concat(AnimationCurveName,generateKeyframeTextBegin,time,"f, ",value, "f, ",inTangent, "f, ", outTangent, "f, ", inWt, "f, ",outwt, "f", generateKeyframeTextEnd, "\n" );
            generatedString.Add(stringObject);
        }
        return generatedString;
        
    }
}
