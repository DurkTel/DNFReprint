using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 轨道相机
/// </summary>
public class OrbitCamera : SingletonMono<OrbitCamera>
{
    /// <summary>
    /// 焦点
    /// </summary>
    public Transform focus = default;
    /// <summary>
    /// 与焦点的距离
    /// </summary>
    [SerializeField, Range(0.1f, 20f)]
    private float distance = 5f;
    /// <summary>
    /// 焦点的缓动半径
    /// </summary>
    [SerializeField, Min(0f)]
    private float focusRadius = 0f;
    /// <summary>
    /// 焦点居中系数
    /// </summary>
    [SerializeField, Range(0f, 1f)]
    private float focusCentering = 0.5f;
    /// <summary>
    /// 相机遮挡检测的层级
    /// </summary>
    [SerializeField]
    private LayerMask obstructionMask = -1;
    /// <summary>
    /// 焦点对象的现在/以前的位置
    /// </summary>
    private Vector3 focusPoint, previousFocusPoint;
    /// <summary>
    /// 移动的限制
    /// </summary>
    [SerializeField]
    private Vector2 heightLimit, widthLimit;
    /// <summary>
    /// 规则相机
    /// </summary>
    public static Camera regularCamera;

    private Vector3 CameraHalfExtends
    {
        get
        {
            Vector3 halfExtends;
            halfExtends.y = regularCamera.nearClipPlane * Mathf.Tan(0.5f * Mathf.Deg2Rad * regularCamera.fieldOfView);
            halfExtends.x = halfExtends.y * regularCamera.aspect;
            halfExtends.z = 0f;
            return halfExtends;
        }
    }


    public static void Initialize()
    {
        GameObject go = new GameObject("OrbitCamera");
        go.AddComponent<OrbitCamera>();
        //go.AddComponent<AudioListener>();
        regularCamera = go.AddComponent<Camera>();
        regularCamera.orthographic = true;
        regularCamera.orthographicSize = 2f;
        regularCamera.farClipPlane = 100f;
        regularCamera.clearFlags = CameraClearFlags.SolidColor;
        regularCamera.backgroundColor = Color.black;
        regularCamera.tag = "MainCamera";
        DontDestroyOnLoad(go);
    }

    public void SetFocus(Entity entity)
    {
        GameObject target = new GameObject("CameraTarget");
        target.transform.localPosition = new Vector3(0, 0.6f, 0);
        target.transform.SetParentIgnore(entity.transform);
        focus = target.transform;
    }

    private void LateUpdate()
    {
        if (focus == null) return;
        UpdateFocusPoint();
        Quaternion lookRotation;
        lookRotation = transform.localRotation;

        Vector3 lookDirection = lookRotation * Vector3.forward;
        Vector3 lookPosition = focusPoint - lookDirection * distance;

        Vector3 rectOffset = lookDirection * regularCamera.nearClipPlane;
        Vector3 rectPosition = lookPosition + rectOffset;
        Vector3 castFrom = focus.position;
        Vector3 castLine = rectPosition - castFrom;
        float castDistance = castLine.magnitude;
        Vector3 castDirection = castLine / castDistance;

        if (Physics.BoxCast(castFrom, CameraHalfExtends, castDirection,
            out RaycastHit hit, lookRotation, castDistance, obstructionMask))
        {
            rectPosition = castFrom + castDirection * hit.distance;
            lookPosition = rectPosition - rectOffset;
        }

        transform.SetPositionAndRotation(lookPosition, lookRotation);
    }

    /// <summary>
    /// 更新焦点对象的位置
    /// </summary>
    private void UpdateFocusPoint()
    {
        previousFocusPoint = focusPoint;
        Vector3 targetPoint = focus.position;
        if (focusRadius > 0f)
        {
            float distance = Vector3.Distance(targetPoint, focusPoint);
            float t = 1f;
            if (distance > 0.01f && focusCentering > 0f)
            {
                t = Mathf.Pow(1f - focusCentering, Time.unscaledDeltaTime);
            }
            //与上次相比 焦点的位移大于缓动半径才进行设值
            if (distance > focusRadius)
            {
                t = Mathf.Min(t, focusRadius / distance);
            }
            focusPoint = Vector3.Lerp(targetPoint, focusPoint, t);
        }
        else
            focusPoint = targetPoint;

        if (heightLimit == Vector2.zero || widthLimit == Vector2.zero)
            return;
        focusPoint = new Vector3(Mathf.Clamp(focusPoint[0], widthLimit[0], widthLimit[1]), Mathf.Clamp(focusPoint[1], heightLimit[0], heightLimit[1]), focusPoint[2]);
    }

    public void SetCameraLimit(float minHeight, float maxHeight, float minWidth, float maxWidth)
    {
        heightLimit = new Vector2(minHeight, maxHeight);
        widthLimit = new Vector2(minWidth, maxWidth);
    }

}
