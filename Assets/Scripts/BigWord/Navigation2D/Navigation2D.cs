using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class Navigation2D : MonoBehaviour
    {
        public PathNode[,] mapData;

        public NavigationData navigationData;

        public void InitMap()
        {
            if (mapData != null)
                return;

            if (navigationData == null)
                Debug.LogError("当前寻路网格为空");

            mapData = new PathNode[navigationData.width, navigationData.length];
            for (int x = 0; x < navigationData.width; x++)
            {
                for (int y = 0; y < navigationData.length; y++)
                {
                    PathNode node = new AstarPathNode();
                    int status = navigationData.lockNodes.Contains(new Vector2Int(x, y)) ? PathNode.NODE_BLOCK : PathNode.NODE_NONE;
                    node.SetData(x, y, status);
                    mapData[x, y] = node;
                }
            }
        }

        public bool GridInMap(int x, int y)
        {
            return x > 0 && y > 0 && x < navigationData.width && y < navigationData.length;
        }

        public Vector2 GetPositionByGrid(float x, float y)
        {
            if (navigationData == null)
            {
                Debug.LogError("当前寻路网格为空");
                return Vector2Int.zero;
            }

            float nodeSize = navigationData.nodeSize;

            int gridX = (int)Mathf.Max(Mathf.Floor(x / nodeSize), 0);
            int gridY = (int)Mathf.Max(Mathf.Floor(y / nodeSize), 0);

            Vector2Int grid = new Vector2Int(gridX, gridY);
            return grid;
        }

        public Vector2Int GetGridByPosition(float x, float y)
        {
            if (navigationData == null)
            {
                Debug.LogError("当前寻路网格为空");
                return Vector2Int.zero;
            }

            float nodeSize = navigationData.nodeSize;

            int gridX = (int)Mathf.Max(Mathf.Floor(x / nodeSize), 0);
            int gridY = (int)Mathf.Max(Mathf.Floor(y / nodeSize), 0);

            Vector2Int grid = new Vector2Int(gridX, gridY);
            return grid;
        }

        /// <summary>
        /// 计算路径
        /// </summary>
        /// <param name="sx">开始X坐标</param>
        /// <param name="sy">开始Y坐标</param>
        /// <param name="ex">目标点X坐标</param>
        /// <param name="ey">目标点Y坐标</param>
        /// <param name="path">返回路径</param>
        /// <returns>是否可达</returns>
        public bool CalculatePath(float sx, float sy, float ex, float ey, out List<PathNode> path)
        {
            InitMap();
            Vector2Int startNode = GetGridByPosition(sx, sy);
            Vector2Int endNode = GetGridByPosition(ex, ey);
            path = AStar_Finding(mapData, startNode, endNode);

            return path.Count > 0;
        }

        public bool CalculatePath(float sx, float sy, PathNode enode, out List<PathNode> path)
        {
            InitMap();
            Vector2Int startNode = GetGridByPosition(sx, sy);
            Vector2Int endNode = new Vector2Int(enode.X, enode.Y);
            path = AStar_Finding(mapData, startNode, endNode);

            return path.Count > 0;
        }

        /// <summary>
        /// 计算可行走区域
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        public List<PathNode> CalculateArea(float x, float y, float radius)
        {
            InitMap();
            List<PathNode> area = new List<PathNode>(0);
            Vector2Int node = GetGridByPosition(x, y);
            for (int i = 1; i <= radius; i++)
            {
                for (int j = 1; j <= radius; j++)
                {
                    int right = node.x + i;
                    int left = node.x - i;
                    int up = node.y + j;
                    int down = node.y - j;

                    if (GridInMap(right, up) && mapData[right, up].status != PathNode.NODE_BLOCK)
                        area.Add(mapData[right, up]);

                    if (GridInMap(right, down) && mapData[right, down].status != PathNode.NODE_BLOCK)
                        area.Add(mapData[right, down]);

                    if (GridInMap(left, up) && mapData[left, up].status != PathNode.NODE_BLOCK)
                        area.Add(mapData[left, up]);

                    if (GridInMap(left, down) && mapData[left, down].status != PathNode.NODE_BLOCK)
                        area.Add(mapData[left, down]);
                }
            }
            return area;
        }


        #region A星寻路算法

        private static List<AstarPathNode> m_openList = new List<AstarPathNode>();

        private static List<AstarPathNode> m_closeList = new List<AstarPathNode>();
        public static List<PathNode> AStar_Finding(PathNode[,] map, Vector2Int startNode, Vector2Int targetNode)
        {
            if (startNode[0] >= map.GetLength(0) || startNode[1] >= map.GetLength(1) || targetNode[0] >= map.GetLength(0) || targetNode[1] >= map.GetLength(1))
            {
                Error();
                return new List<PathNode>(0);
            }

            //重置地图数据
            m_openList.Clear();
            m_closeList.Clear();
            //标记起点和终点
            AstarPathNode sNode = map[startNode[0], startNode[1]] as AstarPathNode;
            sNode.status = AstarPathNode.NODE_START;
            AstarPathNode eNode = map[targetNode[0], targetNode[1]] as AstarPathNode;
            eNode.status = AstarPathNode.NODE_End;
            //重置起点 将父节点置空
            sNode.G = 0;
            sNode.H = sNode.GetH(eNode);
            sNode.Parent = null;
            m_openList.Add(sNode);

            while (m_openList.Count > 0)
            {
                //取到开启列表中最小F值的点
                AstarPathNode minNode = GetMinFNode();
                //移到关闭列表
                m_closeList.Add(minNode);
                m_openList.Remove(minNode);

                if (minNode.status == AstarPathNode.NODE_End)
                    break;

                List<PathNode> neighor = GetNeighor(map, minNode);
                foreach (AstarPathNode PathNode in neighor)
                {
                    if (PathNode.status != AstarPathNode.NODE_BLOCK &&
                        !m_openList.Contains(PathNode) &&
                        !m_closeList.Contains(PathNode))
                    {
                        //与终点的距离
                        PathNode.H = PathNode.GetH(eNode);
                        //与起点的距离 要加上父节点
                        PathNode.G = PathNode.GetG(minNode) + minNode.G;
                        PathNode.Parent = minNode;

                        m_openList.Add(PathNode);
                    }
                }
            }
            List<PathNode> path = GetPath(eNode);
            path.Reverse();

            return path;
        }

        /// <summary>
        /// 从开启列表中获得F值最小的
        /// </summary>
        private static AstarPathNode GetMinFNode()
        {
            AstarPathNode minNode = null;
            int minFCount = int.MaxValue;

            foreach (AstarPathNode PathNode in m_openList)
            {
                if (PathNode.F < minFCount)
                {
                    minFCount = PathNode.F;
                    minNode = PathNode;
                }
            }

            return minNode;
        }

        private static List<PathNode> GetPath(PathNode eNode)
        {
            //向上输出父节点得出路径
            List<PathNode> path = new List<PathNode>();
            PathNode target = eNode;
            while (target.status != PathNode.NODE_START)
            {
                if (target.Parent == null)
                    break;
                path.Add(target);
                target = target.Parent;
            }

            if (path.Count <= 0)
                Error();

            return path;
        }

        /// <summary>
        /// 获得相邻的节点
        /// </summary>
        /// <param name="currentNode"></param>
        /// <returns></returns>
        private static List<PathNode> GetNeighor(PathNode[,] map, PathNode currentNode)
        {
            List<PathNode> nodes = new List<PathNode>();
            int x = currentNode.X;
            int y = currentNode.Y;

            int width = map.GetLength(0);
            int lengh = map.GetLength(1);

            //左
            if (x - 1 >= 0)
                nodes.Add(map[x - 1, y]);
            //右
            if (x + 1 < width && x + 1 >= 0)
                nodes.Add(map[x + 1, y]);
            //下
            if (y - 1 >= 0)
                nodes.Add(map[x, y - 1]);
            //上
            if (y + 1 < lengh && y + 1 >= 0)
                nodes.Add(map[x, y + 1]);
            //左下
            if (x - 1 >= 0 && y - 1 >= 0)
                nodes.Add(map[x - 1, y - 1]);
            //左上
            if (x - 1 >= 0 && y + 1 < lengh && y + 1 >= 0)
                nodes.Add(map[x - 1, y + 1]);
            //右下
            if (x + 1 < width && x + 1 >= 0 && y - 1 >= 0)
                nodes.Add(map[x + 1, y - 1]);
            //右上
            if (x + 1 < width && x + 1 >= 0 && y + 1 < lengh && y + 1 >= 0)
                nodes.Add(map[x + 1, y + 1]);

            return nodes;
        }

        private static void Error()
        {
            //Debug.LogError("当前寻路不可达");
        }

        #endregion
    }
}
