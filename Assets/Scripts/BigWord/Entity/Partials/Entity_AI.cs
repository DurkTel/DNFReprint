using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;

public partial class Entity
{
    //private PathNode m_curPathNode;

    //private List<PathNode> m_curPath;

    ///// <summary>
    ///// 请求开始路径移动
    ///// </summary>
    //public void Request_PathMove(int gridX, int gridY, Navigation2D navigation = null)
    //{
    //    //计算路径是否可行
    //    navigation = navigation == null ? GMScenesManager.Instance.navigation2D : navigation;
    //    if(navigation == null)
    //    {
    //        Debug.LogError("当前地图没有导航信息无法寻路");
    //        return;
    //    }

    //    Vector2Int curGrid = Pathfinding.GetGridByPosition(navigation, transform.position);

    //    List<PathNode> pathList = Pathfinding.AStar_Finding(navigation.navigationData.map, curGrid, new Vector2Int(gridX, gridY));

    //    if (pathList == null && pathList.Count == 0)
    //    {
    //        return;
    //    }

    //    m_curPath = pathList;
    //    m_curPathNode = m_curPath[0];

    //    //停止当前移动
    //    Move_Stop();

    //}
}
