/*
 * 
 * https://gist.github.com/HilariousCow/560db765cf24eb589b00
 * 
using System;
using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace UnityEngine
{
    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = true)]
    public class RecommendedAttribute : PropertyAttribute
    {
        public readonly string RecommendedValue;

        public RecommendedAttribute(string recommendedValue)
        {
            this.RecommendedValue = recommendedValue;
        }
    }

    [System.Serializable]
    public class RecommendedValue
    {
        public string Recommendation;
    }

    [CustomPropertyDrawer(typeof(RecommendedAttribute))]
    public class RecommendedDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return base.GetPropertyHeight(property, label) + 16;
        }

        // Draw the property inside the given rect
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // Now draw the property as a Slider or an IntSlider based on whether it’s a float or integer.
            if (property.type != typeof(string).ToString())
                Debug.LogWarning("Use only with string type");
            else
            {
                RecommendedAttribute value = attribute as RecommendedAttribute;
                SerializedProperty val = property.FindPropertyRelative("Recommendation");
                float newMin = minValue.intValue;
                float newMax = maxValue.intValue;

                float xDivision = position.width * 0.4f;
                float xLabelDiv = xDivision * 0.125f;

                float yDivision = position.height * 0.5f;
                EditorGUI.LabelField(new Rect(position.x, position.y, xDivision, yDivision)
                , label);


                Rect mmRect = new Rect(position.x + xDivision + xLabelDiv, position.y, position.width - (xDivision + xLabelDiv * 2), yDivision);

                EditorGUI.MinMaxSlider(mmRect, ref newMin, ref newMax, range.MinLimit, range.MaxLimit);

                //to deal with rounding on negative values:
                int newMinI = (int)(newMin - (float)range.MinLimit) + range.MinLimit;
                int newMaxI = (int)(newMax - (float)range.MinLimit) + range.MinLimit;

                //left label
                Rect minRangeRect = new Rect(position.x + xDivision, position.y, xLabelDiv, yDivision);
                minRangeRect.x += xLabelDiv * 0.5f - 12;
                minRangeRect.width = 24;
                EditorGUI.LabelField(minRangeRect, range.MinLimit.ToString());

                //right label
                Rect maxRangeRect = new Rect(minRangeRect);
                maxRangeRect.x = mmRect.xMax;
                maxRangeRect.x = mmRect.xMax + xLabelDiv * 0.5f - 12;
                maxRangeRect.width = 24;
                EditorGUI.LabelField(maxRangeRect, range.MaxLimit.ToString());

                int totalRange = Mathf.Max(range.MaxLimit - range.MinLimit, 1);
                Rect minLabelRect = new Rect(mmRect);
                minLabelRect.x = minLabelRect.x + minLabelRect.width * ((newMin - range.MinLimit) / totalRange);
                minLabelRect.x -= 12;
                minLabelRect.y += yDivision;
                minLabelRect.width = 24;
                newMinI = Mathf.Clamp(EditorGUI.IntField(minLabelRect, newMinI), range.MinLimit, newMaxI);
                //EditorGUI.LabelField(minLabelRect, newMin.ToString());//old style non moving label

                Rect maxLabelRect = new Rect(mmRect);
                maxLabelRect.x = maxLabelRect.x + maxLabelRect.width * ((newMax - range.MinLimit) / totalRange);
                maxLabelRect.x -= 12;
                maxLabelRect.x = Mathf.Max(maxLabelRect.x, minLabelRect.xMax);
                maxLabelRect.y += yDivision;
                maxLabelRect.width = 24;
                newMaxI = Mathf.Clamp(EditorGUI.IntField(maxLabelRect, newMaxI), newMinI, range.MaxLimit);
                //EditorGUI.LabelField(maxLabelRect, newMax.ToString());//old style non moving label


                minValue.intValue = (int)newMin;
                maxValue.intValue = (int)newMax;
            }
        }
    }
}


//Place anywhere in your unity project



//Place anywhere in your unity project

public class IntRangeAttribute : PropertyAttribute
{
    public int MinLimit, MaxLimit;

    public IntRangeAttribute(int minLimit, int maxLimit)
    {
        this.MinLimit = minLimit;
        this.MaxLimit = maxLimit;
    }
}

[System.Serializable]
public class IntRange
{
    public int RangeStart, RangeEnd;

    private int GetRandomValue()
    {
        return Random.Range(RangeStart, RangeEnd);
    }

    public static implicit operator int(IntRange d)
    {
        return d.GetRandomValue();
    }

}

[CustomPropertyDrawer(typeof(IntRangeAttribute))]
public class IntRangeDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return base.GetPropertyHeight(property, label) + 16;
    }

    // Draw the property inside the given rect
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Now draw the property as a Slider or an IntSlider based on whether it’s a float or integer.
        if (property.type != typeof(IntRange).ToString())
            Debug.LogWarning("Use only with IntRange type");
        else
        {
            IntRangeAttribute range = attribute as IntRangeAttribute;
            SerializedProperty minValue = property.FindPropertyRelative("RangeStart");
            SerializedProperty maxValue = property.FindPropertyRelative("RangeEnd");
            float newMin = minValue.intValue;
            float newMax = maxValue.intValue;

            float xDivision = position.width * 0.4f;
            float xLabelDiv = xDivision * 0.125f;

            float yDivision = position.height * 0.5f;
            EditorGUI.LabelField(new Rect(position.x, position.y, xDivision, yDivision)
            , label);


            Rect mmRect = new Rect(position.x + xDivision + xLabelDiv, position.y, position.width - (xDivision + xLabelDiv * 2), yDivision);

            EditorGUI.MinMaxSlider(mmRect, ref newMin, ref newMax, range.MinLimit, range.MaxLimit);

            //to deal with rounding on negative values:
            int newMinI = (int)(newMin - (float)range.MinLimit) + range.MinLimit;
            int newMaxI = (int)(newMax - (float)range.MinLimit) + range.MinLimit;

            //left label
            Rect minRangeRect = new Rect(position.x + xDivision, position.y, xLabelDiv, yDivision);
            minRangeRect.x += xLabelDiv * 0.5f - 12;
            minRangeRect.width = 24;
            EditorGUI.LabelField(minRangeRect, range.MinLimit.ToString());

            //right label
            Rect maxRangeRect = new Rect(minRangeRect);
            maxRangeRect.x = mmRect.xMax;
            maxRangeRect.x = mmRect.xMax + xLabelDiv * 0.5f - 12;
            maxRangeRect.width = 24;
            EditorGUI.LabelField(maxRangeRect, range.MaxLimit.ToString());

            int totalRange = Mathf.Max(range.MaxLimit - range.MinLimit, 1);
            Rect minLabelRect = new Rect(mmRect);
            minLabelRect.x = minLabelRect.x + minLabelRect.width * ((newMin - range.MinLimit) / totalRange);
            minLabelRect.x -= 12;
            minLabelRect.y += yDivision;
            minLabelRect.width = 24;
            newMinI = Mathf.Clamp(EditorGUI.IntField(minLabelRect, newMinI), range.MinLimit, newMaxI);
            //EditorGUI.LabelField(minLabelRect, newMin.ToString());//old style non moving label

            Rect maxLabelRect = new Rect(mmRect);
            maxLabelRect.x = maxLabelRect.x + maxLabelRect.width * ((newMax - range.MinLimit) / totalRange);
            maxLabelRect.x -= 12;
            maxLabelRect.x = Mathf.Max(maxLabelRect.x, minLabelRect.xMax);
            maxLabelRect.y += yDivision;
            maxLabelRect.width = 24;
            newMaxI = Mathf.Clamp(EditorGUI.IntField(maxLabelRect, newMaxI), newMinI, range.MaxLimit);
            //EditorGUI.LabelField(maxLabelRect, newMax.ToString());//old style non moving label


            minValue.intValue = (int)newMin;
            maxValue.intValue = (int)newMax;
        }
    }
}
*/