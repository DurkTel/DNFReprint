using UnityEngine;
using UnityEditor;
using System.Text;

public enum TimeFormat
{
    Frames = 0,
    Seconds,
    SecondsFormatted
}
public struct FrameRange
{
    // start frame
    [SerializeField]
    private int _start;

    // end frame
    [SerializeField]
    private int _end;

    /// @brief Returns the start frame.
    public int Start
    {
        get { return _start; }
        set
        {
            _start = value;
        }
    }

    /// @brief Returns the end frame.
    public int End
    {
        get { return _end; }
        set
        {
            _end = value;
        }
    }

    /// @brief Sets / Gets the length.
    /// @note It doesn't cache the value.
    public int Length { set { End = _start + value; } get { return _end - _start; } }

    /**
     * @brief Create a frame range
     * @param start Start frame
     * @param end End frame
     * @note It is up to you to make sure start is smaller than end.
     */
    public FrameRange(int start, int end)
    {
        this._start = start;
        this._end = end;
    }

    /// @brief Returns \e i clamped to the range.
    public int Cull(int i)
    {
        return Mathf.Clamp(i, _start, _end);
    }

    /// @brief Returns if \e i is inside [start, end], i.e. including borders
    public bool Contains(int i)
    {
        return i >= _start && i <= _end;
    }

    /// @brief Returns if \e i is inside ]start, end[, i.e. excluding borders
    public bool ContainsExclusive(int i)
    {
        return i > _start && i < _end;
    }

    /// @brief Returns if the ranges intersect, i.e. touching returns false
    /// @note Assumes They are both valid
    public bool Collides(FrameRange range)
    {
        return _start < range._end && _end > range._start;
        //			return (range.start > start && range.start < end) || (range.end > start && range.end < end );
    }

    /// @brief Returns if the ranges overlap, i.e. touching return true
    /// @note Assumes They are both valid
    public bool Overlaps(FrameRange range)
    {
        return range.End >= _start && range.Start <= _end;
    }

    /// @brief Returns what kind of overlap it has with \e range.
    /// @note Assumes They are both valid
    //public FrameRangeOverlap GetOverlap(FrameRange range)
    //{
    //    if (range._start >= _start)
    //    {
    //        // contains, left or none
    //        if (range._end <= _end)
    //        {
    //            return FrameRangeOverlap.ContainsFull;
    //        }
    //        else
    //        {
    //            if (range._start > _end)
    //            {
    //                return FrameRangeOverlap.MissOnRight;
    //            }
    //            return FrameRangeOverlap.ContainsStart;
    //        }
    //    }
    //    else
    //    {
    //        // contained, right or none
    //        if (range._end < _start)
    //        {
    //            return FrameRangeOverlap.MissOnLeft;
    //        }
    //        else
    //        {
    //            if (range._end > _end)
    //            {
    //                return FrameRangeOverlap.IsContained;
    //            }

    //            return FrameRangeOverlap.ContainsEnd;
    //        }
    //    }
    //}

    public static bool operator ==(FrameRange a, FrameRange b)
    {
        return a._start == b._start && a._end == b._end;
    }

    public static bool operator !=(FrameRange a, FrameRange b)
    {
        return !(a == b);
    }

    public override bool Equals(object obj)
    {
        if (obj.GetType() != GetType())
            return false;

        return (FrameRange)obj == this;
    }

    public override int GetHashCode()
    {
        return _start + _end;
    }

    public override string ToString()
    {
        return string.Format("[{0}; {1}]", _start, _end);
    }
}
public class TimeScrubberWindow : EditorWindow
{
    static TimeFormat _timeFormat;
    static float TIMELINE_SCRUBBER_HEIGHT = 20;
    static int MIN_PIXELS_BETWEEN_FRAMES = 100;
    static int TIMELINE_SCRUBBER_TEXT_HEIGHT = 15;

    static string currentAnimation;
    static string mCurrentAnimation;

    private void OnEnable()
    {
        mCurrentAnimation = null;
    }
    
    public static void TimeScrubber(Rect rect) 
    {
        //if (SpriteAnimationEditorWindow.currentSelectFrame == null) return;
        //currentAnimation = SpriteAnimationEditorWindow.currentSelectFrame.name;
       // CharacterAnimation characterAnimation=new CharacterAnimation();

        //		Rect actualRect = rect;
        //		actualRect.xMax -= 20; // buffer on the right

        Rect clickRect = rect;
        clickRect.yMin = clickRect.yMax - TIMELINE_SCRUBBER_HEIGHT;

        //int length = range.Length;
        //int length = characterAnimation.GetAnimationLength();


        int frames = 0;

        float width = rect.width;

        int maxFramesBetweenSteps = Mathf.Max(1, Mathf.FloorToInt(width / MIN_PIXELS_BETWEEN_FRAMES));

        int numFramesPerStep = Mathf.Max(1, 7 / maxFramesBetweenSteps);


        int numFramesIter = numFramesPerStep < 30 ? 1 : numFramesPerStep / 10;

        Vector3 pt = new Vector3(rect.x, rect.yMax - TIMELINE_SCRUBBER_TEXT_HEIGHT, 0);
        Vector3 pt2= new Vector3(rect.x, rect.yMax - TIMELINE_SCRUBBER_TEXT_HEIGHT, 0); ;

        Rect backgroundRect = clickRect;
        backgroundRect.xMin = 0;
        backgroundRect.xMax += 20;



        Handles.color = new Color(1, 1, 1, 1);



        GUIStyle labelStyle = new GUIStyle(EditorStyles.boldLabel);
        labelStyle.normal.textColor = Color.white;
        labelStyle.alignment = TextAnchor.UpperCenter;

        GUI.contentColor = new Color(1, 1, 1, 1);

        frames = (frames / numFramesIter) * numFramesIter;

        //if (characterAnimation.animationData.frameDatas.Count > 0&&mCurrentAnimation!=currentAnimation)
        //{
        //    for (int j = 0; j < characterAnimation.animationData.frameDatas.Count; j++)
        //    {
        //        characterAnimation.animationData.frameDatas[j].select = false;
        //    }

        //    ColliderInfosEditor.currentFrame = 0;

        //    characterAnimation.selectFrame = 0;

        //    updateAnimation.ShowCurrentFrame(int.Parse(characterAnimation.animationData.frameDatas[0].sprite.name));


        //    // CharacterActionEditorWindow.currentSelectFrame = characterAnimation.colliderInfos;



        //    characterAnimation.animationData.frameDatas[0].select = true;

        //    mCurrentAnimation = currentAnimation;
        //}

        if (Event.current.type == EventType.MouseDown)
        {

            Debug.Log("点击");
            bool go = false;
            //for (int j = 0; j < characterAnimation.animationData.frameDatas.Count; j++)
            //{            
            //    if (characterAnimation.animationData.frameDatas[j].FrameLine.Contains(Event.current.mousePosition))
            //    {
            //        go = true;

            //    }
            //}
            if (go == false) return;

            //for (int j = 0; j < characterAnimation.animationData.frameDatas.Count; j++)
            //{
            //    characterAnimation.animationData.frameDatas[j].select = false;
            //    if (characterAnimation.animationData.frameDatas[j].FrameLine.Contains(Event.current.mousePosition))
            //    {

            //        inputButton();

            //        updateAnimation.ShowCurrentFrame(int.Parse(characterAnimation.animationData.frameDatas[j].sprite.name));


            //        characterAnimation.selectFrame = j;

            //       // CharacterActionEditorWindow.currentSelectFrame = characterAnimation.colliderInfos;

            //        Debug.Log("当前选中第几帧  " + j);
            //        ColliderInfosEditor.currentFrame = j;

            //        characterAnimation.animationData.frameDatas[j].select = true;

            //    }
            //}


        }
        //while (frames <= range.End)
        pt2.x = rect.x + (width * ((float)(frames - 0) / 7));
        if (pt.x >= rect.x)
        {

            //int a = -characterAnimation.animationData.frameDatas[0].interval;
            //for (int i = 0; i < characterAnimation.animationData.frameDatas.Count; i++)
            //{
                //a += characterAnimation.animationData.frameDatas[i].interval;
                //Vector3 temp = new Vector3(280 + a, 408, 0);
                Vector3 temp = pt + new Vector3(80, 0, 0);

                if (temp.x < 250 + rect.width)
                {
                    Vector3 aaa = temp - new Vector3(0, rect.height - 20, 0);
                    //Handles.DrawLine(temp,aaa );  //画长线
                    //if (characterAnimation.animationData.frameDatas[i].select==false)
                    //{
                    //    GUI.DrawTextureWithTexCoords(new Rect(aaa.x, aaa.y, 3, rect.height - 30), Resources.Load<Texture>("Lines/white"), new Rect(0, 0, 1, 1));
                    //}
                    //else
                    //{
                        GUI.DrawTextureWithTexCoords(new Rect(aaa.x, aaa.y, 3, rect.height - 30), Resources.Load<Texture>("Lines/yellow"), new Rect(0, 0, 1, 1));
                    //}

                    //characterAnimation.animationData.frameDatas[i].FrameLine = new Rect(aaa.x, aaa.y, 6, rect.height - 30);

                    GUI.Label(new Rect(temp.x - 30, temp.y, 60, TIMELINE_SCRUBBER_TEXT_HEIGHT), 80.ToString(), labelStyle);
                     
                }

            //}

            //if (Event.current.type == EventType.Repaint && rect.width > 0)
            //{
            //    Vector3 smallTickPt = pt2;
            //    smallTickPt.y -= 20;
            //    Handles.DrawLine(smallTickPt, smallTickPt - new Vector3(0, 20, 0));  //画小线条

            //}


        }


        


        GUI.color = Color.white;
        GUI.contentColor = Color.white;

        Handles.color = new Color(1, 1, 1, 1);

            // Handles.DrawLine(new Vector3(rect.x, rect.yMin, 0), new Vector3(rect.x, rect.yMax - TIMELINE_SCRUBBER_HEIGHT, 0));





        rect.height = TIMELINE_SCRUBBER_HEIGHT;

        

    }
    private void Update()
    {
        
    }
    public void DrawLines()
    {

    }

    public static GUIStyle GetTimeScrubberStyle()
    {
        if (EditorGUIUtility.isProSkin)
        {
            GUIStyle toolbarStyle = new GUIStyle(EditorStyles.toolbar);
            toolbarStyle.fixedHeight = 0;
            return toolbarStyle;
        }

        return GUIStyle.none;
    }
    public static string GetTime(int frame, int frameRate)
    {
        switch (_timeFormat)
        {
            case TimeFormat.Frames:
                return frame.ToString();
            case TimeFormat.Seconds:
                return ((float)frame / frameRate).ToString("0.##");
            case TimeFormat.SecondsFormatted:
                return string.Format("{0}:{1}", frame / frameRate, frame % frameRate);
        }

        return frame.ToString();
    }


    [System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint = "keybd_event")]
    static extern void keybd_event(
    byte bVk,            //虚拟键值 对应按键的ascll码十进制值  
    byte bScan,          //0
    int dwFlags,         //0 为按下，1按住，2为释放 
    int dwExtraInfo      //0
    );
    public static void inputButton()
    {
          keybd_event(27, 0, 0, 0);

        keybd_event(27, 0, 2, 0);
        //keybd_event(13, 0, 0, 0);
        //keybd_event(66, 0, 1, 0);
        //  keybd_event(66, 0, 2, 0);

        Debug.Log("模拟敲击ESC");

    }


}
