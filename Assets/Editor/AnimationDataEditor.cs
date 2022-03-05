using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(AnimationData))]
public class AnimationDataEditor : Editor
{

    private SerializedProperty m_aniName;

    private SerializedProperty m_colliderInfo;

    private SerializedProperty m_isLoop;

    private SerializedProperty m_speed;

    private SerializedProperty m_frameList;

    private SerializedProperty m_switchingConditions;

    private SerializedProperty m_curFrameData;

    private SerializedProperty m_curFrameSprite;

    private int m_frameIndex = 1;

    private int m_conditionIndex = 1;



    public void OnEnable()
    {
        m_aniName = serializedObject.FindProperty("aniName");
        m_colliderInfo = serializedObject.FindProperty("colliderInfo");
        m_isLoop = serializedObject.FindProperty("isLoop");
        m_speed = serializedObject.FindProperty("speed");
        m_frameList = serializedObject.FindProperty("frameList");
        m_switchingConditions = serializedObject.FindProperty("switchingConditions");
    }

    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        serializedObject.Update();
        EditorGUILayout.Space(1);

        EditorGUILayout.BeginHorizontal();
        EditorGUIUtility.labelWidth = 60;
        m_aniName.stringValue = EditorGUILayout.TextField("动画名称：",m_aniName.stringValue,GUILayout.Width(200));
        EditorGUILayout.Space(5);
        EditorGUILayout.PropertyField(m_colliderInfo,new GUIContent("碰撞信息："), GUILayout.Width(200));
        EditorGUILayout.Space(5);
        m_isLoop.boolValue = EditorGUILayout.Toggle("是否循环：", m_isLoop.boolValue, GUILayout.Width(200));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space(10);
        EditorGUILayout.BeginHorizontal();
        m_speed.floatValue = EditorGUILayout.FloatField("速度比率：",m_speed.floatValue, GUILayout.Width(100));
        m_speed.floatValue = GUILayout.HorizontalSlider(m_speed.floatValue,0.1f, 3f, GUILayout.Width(98));
        m_speed.floatValue = LimitFloatValue(m_speed.floatValue, 0.1f, 3f);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space(10);
        GUI.Label(new Rect(16, 60, 100, 22), "增减帧数：");
        if (GUI.Button(new Rect(120, 60, 200, 22), "+"))
        {
            int index = m_frameList.arraySize == 0 ? 0 : m_frameList.arraySize - 1;
            m_frameList.InsertArrayElementAtIndex(index);
        }

        if (GUI.Button(new Rect(350, 60, 200, 22), "-"))
        {
            if (m_frameList.arraySize >= m_frameIndex && m_frameList.arraySize > 0)
            {
                m_frameList.DeleteArrayElementAtIndex(m_frameIndex - 1);

            }
        }

        RefreshFramData();
        RefreshIndex();
        RefreshTexture();
        RefreshFrameInfo();
        RefreshConditionInfo();


        serializedObject.ApplyModifiedProperties();
    }

    private void RefreshIndex()
    {
        if (m_frameIndex < 1)
        {
            m_frameIndex = 1;
        }
        else if (m_frameIndex > m_frameList.arraySize)
        {
            m_frameIndex = m_frameList.arraySize;
        }
        m_curFrameData = m_frameIndex <= m_frameList.arraySize && m_frameList.arraySize > 0 ? m_frameList.GetArrayElementAtIndex(m_frameIndex - 1) : null;
    }

    #region 绘制当前帧信息
    private void RefreshTexture()
    {
        if (m_curFrameData == null) return;
        m_curFrameSprite = m_curFrameData.FindPropertyRelative("sprite");
        if (m_curFrameSprite.objectReferenceValue == null) return;

        Sprite sprite = m_curFrameSprite.objectReferenceValue as Sprite;
        Texture texture = sprite.texture;

        //计算中心点偏移
        float pivotX = sprite.pivot.x;
        float pivotY = sprite.pivot.y;
        float texturePosX = 180 - pivotX;
        float texturePosY = 200 - (texture.height - pivotY);

        GUI.DrawTexture(new Rect(texturePosX, texturePosY, texture.width, texture.height), texture);
    }

    private void EventBtn(SerializedProperty frameEvent)
    {
        //GUI.Label(new Rect(350, 90, 100, 22), "动画切换条件:");
        if (GUI.Button(new Rect(120, 280, 70, 22), "+"))
        {
            int index = frameEvent.arraySize == 0 ? 0 : frameEvent.arraySize - 1;
            frameEvent.InsertArrayElementAtIndex(index);
        }

        if (GUI.Button(new Rect(230, 280, 70, 22), "-"))
        {
            if (frameEvent.arraySize >= m_conditionIndex && frameEvent.arraySize > 0)
            {
                frameEvent.DeleteArrayElementAtIndex(m_conditionIndex - 1);

            }
        }
    }

    private void RefreshFrameInfo()
    {
        if (m_curFrameData == null) return;
        EditorGUI.PropertyField(new Rect(120,250,180,22), m_curFrameSprite, new GUIContent("精灵图片："));


        SerializedProperty frameEvent = m_curFrameData.FindPropertyRelative("frameEvent");
        EventBtn(frameEvent);
        for (int i = 0; i < frameEvent.arraySize; i++)
        {
            float offset = i * 115 + 25;
            SerializedProperty evet = frameEvent.GetArrayElementAtIndex(i);

            SerializedProperty eventType = evet.FindPropertyRelative("eventType");
            eventType.enumValueIndex = EditorGUI.Popup(new Rect(120, 280 + offset, 180, 22), "帧事件：", eventType.enumValueIndex, Enum.GetNames(typeof(EventDefine)));

            if (eventType.enumValueIndex != 0)
            {
                SerializedProperty paramType = evet.FindPropertyRelative("paramType");
                paramType.enumValueIndex = EditorGUI.Popup(new Rect(120, 310 + offset, 180, 22), "参数类型：", paramType.enumValueIndex, Enum.GetNames(typeof(EventParamDefine)));

                if (paramType.enumValueIndex != 0)
                {
                    EventParamDefine eventParam = (EventParamDefine)paramType.enumValueIndex;
                    switch (eventParam)
                    {
                        case EventParamDefine.Bool:
                            SerializedProperty parameterBool = evet.FindPropertyRelative("parameterBool");
                            parameterBool.boolValue = EditorGUI.Toggle(new Rect(120, 340 + offset, 180, 22), "布尔值：", parameterBool.boolValue);
                            break;
                        case EventParamDefine.Int:
                            SerializedProperty parameterInt = evet.FindPropertyRelative("parameterInt");
                            parameterInt.intValue = EditorGUI.IntField(new Rect(120, 340 + offset, 180, 22), "整型：", parameterInt.intValue);
                            break;
                        case EventParamDefine.Float:
                            SerializedProperty parameterFloat = evet.FindPropertyRelative("parameterFloat");
                            parameterFloat.floatValue = EditorGUI.FloatField(new Rect(120, 340 + offset, 180, 22), "浮点型：", parameterFloat.floatValue);
                            break;
                        case EventParamDefine.String:
                            SerializedProperty parameterString = evet.FindPropertyRelative("parameterString");
                            parameterString.stringValue = EditorGUI.TextField(new Rect(120, 340 + offset, 180, 22), "字符串：", parameterString.stringValue);
                            break;
                        default:
                            break;
                    }
                }

                float tempY = paramType.enumValueIndex != 0 ? 370 : 340;
                SerializedProperty frameEventLoop = m_curFrameData.FindPropertyRelative("frameEventLoop");
                frameEventLoop.boolValue = EditorGUI.Toggle(new Rect(120, tempY + offset, 180, 22), "每帧调用：", frameEventLoop.boolValue);
            }

        }
    }
    #endregion

    #region 绘制帧列表
    private void RefreshFramData()
    {
        int leng = m_frameList.arraySize;

        float rectHegt = leng > 3 ? leng * 60 + 80 : 240;

        GUILayoutUtility.GetRect(300, rectHegt);
        GUI.Box(new Rect(20, 100, 50, leng * 60), string.Empty, EditorStyles.helpBox);
        GUI.Box(new Rect(70, 100, 35, leng * 60), string.Empty, EditorStyles.helpBox);

        for (int i = 0; i < leng; i++)
        {
            DrawFrameBox(i + 1, i == leng - 1);
            DrawIntervalBox(i + 1, i == leng - 1);
        }

    }

    private void DrawFrameBox(int index, bool isLast)
    {
        Rect boxRect = new Rect(32, 60 + index * 60, 25, 25);
        Rect numRect = new Rect(38, 60 + index * 60, 25, 25);
        Rect arrowRect = new Rect(40, 100 + index * 60, 8, 8);

        Color normalColor = GUI.color;
        GUI.color = m_frameIndex == index ? Color.black : normalColor;

        GUI.Box(boxRect, string.Empty, EditorStyles.helpBox);
        GUI.Label(numRect, index.ToString());
        if (!isLast)
            GUI.DrawTexture(arrowRect, EditorGUIUtility.Load("ArrowDown_on.png") as Texture);

        //选中
        if (boxRect.Contains(Event.current.mousePosition))
        {
            if (Event.current.type == EventType.MouseDown)
            {
                m_frameIndex = index;

            }
        }
        GUI.color = normalColor;
    }

    private void DrawIntervalBox(int index, bool isLast)
    {
        SerializedProperty frameData = m_frameList.GetArrayElementAtIndex(index - 1);
        SerializedProperty interval = frameData.FindPropertyRelative("interval");

        if (!isLast)
        {
            Rect boxRect = new Rect(75, 92 + index * 60, 25, 20);
            interval.floatValue = EditorGUI.FloatField(boxRect, interval.floatValue);

        }
    }

    private float LimitFloatValue(float value,float min, float max)
    {
        if (value > max)
            value = max;
        else if (value < min)
            value = min;

        return value;
    }
    #endregion

    #region 绘制动画切换条件
    private void RefreshConditionInfo()
    {
        ConditionBtn();

        int leng = m_switchingConditions.arraySize;

        float rectHegt = leng > 3 ? leng * 200 + 80 : 500;

        GUILayoutUtility.GetRect(300, rectHegt);

        for (int i = 0; i < leng; i++)
        {
            DrawConditionSwitchBox(i + 1);
        }

    }

    private float CalculateConditionSwitchBoxPos(int index)
    {
        float posY = 140 * (index - 1);

        for (int i = 1; i < index; i++)
        {
            SerializedProperty conditionList = m_switchingConditions.GetArrayElementAtIndex(i - 1);
            SerializedProperty conditions = conditionList.FindPropertyRelative("conditions");
            posY += conditions.arraySize * 23;
        }

        return posY;
    }

    private void DrawConditionSwitchBox(int index)
    { 
        SerializedProperty conditionList = m_switchingConditions.GetArrayElementAtIndex(index - 1);
        SerializedProperty conditions = conditionList.FindPropertyRelative("conditions");

        float condHeght = conditions.arraySize * 23 + 107;
        float posY = CalculateConditionSwitchBoxPos(index);

        Rect boxRect = new Rect(350, 120 + posY, 200, condHeght);
        Rect areaRect = new Rect(350, 120 + posY, 200, condHeght + 20);
        Color normalColor = GUI.color;
        GUI.color = m_conditionIndex == index ? Color.gray : normalColor;

        GUI.Box(boxRect, string.Empty, EditorStyles.helpBox);

        //选中
        if (boxRect.Contains(Event.current.mousePosition))
        {
            if (Event.current.type == EventType.MouseDown)
            {
                m_conditionIndex = index;
            }
        }
        GUI.color = normalColor;

        GUILayout.BeginArea(areaRect);

        SerializedProperty frameArea = conditionList.FindPropertyRelative("frame");
        GUI.Label(new Rect(10, 18, 120, 22), "检测范围");
        int beginValue = frameArea.vector2IntValue.x + 1;
        int endValue = frameArea.vector2IntValue.y + 1;
        beginValue = EditorGUI.IntSlider(new Rect(80, 10, 105, 18), beginValue, 1, m_frameList.arraySize);
        endValue = EditorGUI.IntSlider(new Rect(80, 30, 105, 18), endValue, 1, m_frameList.arraySize);
        if (endValue < beginValue) endValue = beginValue;
        frameArea.vector2IntValue = new Vector2Int(beginValue - 1, endValue - 1);


        SerializedProperty animationData = conditionList.FindPropertyRelative("animationData");
        EditorGUI.PropertyField(new Rect(10, 55, 175, 22), animationData, new GUIContent("切换动画："));
        if (animationData.objectReferenceValue != null)
        {
            GUI.color = Color.green;
            GUI.Box(new Rect(0, 80, 200, 20), "切换条件");
            GUI.color = normalColor;

            if (GUI.Button(new Rect(140, condHeght, 30, 20), "+"))
            {
                //int idx = conditions.arraySize == 0 ? 0 : conditions.arraySize - 1;
                conditions.InsertArrayElementAtIndex(0);
            }

            if (GUI.Button(new Rect(170, condHeght, 30, 20), "-"))
            {
                if (conditions.arraySize > 0)
                {
                    conditions.DeleteArrayElementAtIndex(conditions.arraySize - 1);

                }
            }

            for (int i = 0; i < conditions.arraySize; i++)
            {
                DrawConditions(i, conditions);
            }

        }
        GUILayout.EndArea();

    }

    private void DrawConditions(int index, SerializedProperty conditions)
    {
        SerializedProperty condition = conditions.GetArrayElementAtIndex(index);

        SerializedProperty conditionType = condition.FindPropertyRelative("conditionType");
        float posY = 105 + index * 23;
        conditionType.enumValueIndex = EditorGUI.Popup(new Rect(5, posY, 60, 22), string.Empty, conditionType.enumValueIndex, Enum.GetNames(typeof(Condition.ConditionType)));

        Condition.ConditionType conditionEnum = (Condition.ConditionType)conditionType.enumValueIndex;

        //选择条件
        switch (conditionEnum)
        {
            case Condition.ConditionType.animation:
                SerializedProperty characterAnimation = condition.FindPropertyRelative("characterAnimation");
                EditorGUI.PropertyField(new Rect(75, posY, 120, 18), characterAnimation,GUIContent.none);
                break;
            case Condition.ConditionType.spritePostionY:
                SerializedProperty relationPostionY = condition.FindPropertyRelative("spritePostionY.relation");
                SerializedProperty targetValuePostionY = condition.FindPropertyRelative("spritePostionY.targetValue");
                relationPostionY.enumValueIndex = EditorGUI.Popup(new Rect(75, posY, 60, 22), string.Empty, relationPostionY.enumValueIndex, Enum.GetNames(typeof(Condition.Relation)));
                targetValuePostionY.floatValue = EditorGUI.FloatField(new Rect(145, posY, 50, 18), targetValuePostionY.floatValue);
                break;
            case Condition.ConditionType.spriteSpeedY:
                SerializedProperty relationSpeedY = condition.FindPropertyRelative("spriteSpeedY.relation");
                SerializedProperty targetValueSpeedY = condition.FindPropertyRelative("spriteSpeedY.targetValue");
                relationSpeedY.enumValueIndex = EditorGUI.Popup(new Rect(75, posY, 60, 22), string.Empty, relationSpeedY.enumValueIndex, Enum.GetNames(typeof(Condition.Relation)));
                targetValueSpeedY.floatValue = EditorGUI.FloatField(new Rect(145, posY, 50, 18), targetValueSpeedY.floatValue);
                break;
            case Condition.ConditionType.inputSpeedX:
                SerializedProperty relationinputSpeedX = condition.FindPropertyRelative("inputSpeedX.relation");
                SerializedProperty targetValueinputSpeedX = condition.FindPropertyRelative("inputSpeedX.targetValue");
                relationinputSpeedX.enumValueIndex = EditorGUI.Popup(new Rect(75, posY, 60, 22), string.Empty, relationinputSpeedX.enumValueIndex, Enum.GetNames(typeof(Condition.Relation)));
                targetValueinputSpeedX.floatValue = EditorGUI.FloatField(new Rect(145, posY, 50, 18), targetValueinputSpeedX.floatValue);
                break;
            case Condition.ConditionType.inputSpeedY:
                SerializedProperty relationinputSpeedY = condition.FindPropertyRelative("inputSpeedY.relation");
                SerializedProperty targetValueinputSpeedY = condition.FindPropertyRelative("inputSpeedY.targetValue");
                relationinputSpeedY.enumValueIndex = EditorGUI.Popup(new Rect(75, posY, 60, 22), string.Empty, relationinputSpeedY.enumValueIndex, Enum.GetNames(typeof(Condition.Relation)));
                targetValueinputSpeedY.floatValue = EditorGUI.FloatField(new Rect(145, posY, 50, 18), targetValueinputSpeedY.floatValue);
                break;
            case Condition.ConditionType.inputKey:
                SerializedProperty inputType = condition.FindPropertyRelative("inputComparison.inputType");
                SerializedProperty action = condition.FindPropertyRelative("inputComparison.action");
                inputType.enumValueIndex = EditorGUI.Popup(new Rect(75, posY, 60, 22), string.Empty, inputType.enumValueIndex, Enum.GetNames(typeof(Condition.InputType)));
                action.enumValueIndex = EditorGUI.Popup(new Rect(145, posY, 50, 22), string.Empty, action.enumValueIndex, Enum.GetNames(typeof(InputActionDefine)));
                break;
            case Condition.ConditionType.custom:
                SerializedProperty customCondition = condition.FindPropertyRelative("customCondition");
                customCondition.enumValueIndex = EditorGUI.Popup(new Rect(75, posY, 120, 22), string.Empty, customCondition.enumValueIndex, Enum.GetNames(typeof(CustomCondition)));
                break;
            default:
                break;
        }
    }

    private void ConditionBtn()
    {
        GUI.Label(new Rect(350, 90, 100, 22), "动画切换条件:");
        if (GUI.Button(new Rect(440, 90, 50, 22), "+"))
        {
            int index = m_switchingConditions.arraySize == 0 ? 0 : m_switchingConditions.arraySize - 1;
            m_switchingConditions.InsertArrayElementAtIndex(index);
        }

        if (GUI.Button(new Rect(500, 90, 50, 22), "-"))
        {
            if (m_switchingConditions.arraySize >= m_conditionIndex && m_switchingConditions.arraySize > 0)
            {
                m_switchingConditions.DeleteArrayElementAtIndex(m_conditionIndex - 1);

            }
        }
    }


    #endregion
}
