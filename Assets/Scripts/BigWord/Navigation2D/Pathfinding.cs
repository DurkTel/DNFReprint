using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class Pathfinding
    {

        #region A星

        private static List<AstarPathNode> m_openList = new List<AstarPathNode>();

        private static List<AstarPathNode> m_closeList = new List<AstarPathNode>();
        public static List<PathNode> AStar_Finding(PathNode[,] map, Vector2Int startNode, Vector2Int targetNode)
        {
            if (map.GetLength(0) > startNode[0] || map.GetLength(1) > startNode[1] || map.GetLength(0) > targetNode[0] || map.GetLength(1) > targetNode[1])
            {
                Error();
                return null;
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

        #endregion

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
            if (x + 1 < lengh && x + 1 >= 0)
                nodes.Add(map[x + 1, y]);
            //下
            if (y - 1 >= 0)
                nodes.Add(map[x, y - 1]);
            //上
            if (y + 1 < width && y + 1 >= 0)
                nodes.Add(map[x, y + 1]);
            //左下
            if (x - 1 >= 0 && y - 1 >= 0)
                nodes.Add(map[x - 1, y - 1]);
            //左上
            if (x - 1 >= 0 && y + 1 < width && y + 1 >= 0)
                nodes.Add(map[x - 1, y + 1]);
            //右下
            if (x + 1 < lengh && x + 1 >= 0 && y - 1 >= 0)
                nodes.Add(map[x + 1, y - 1]);
            //右上
            if (x + 1 < lengh && x + 1 >= 0 && y + 1 < width && y + 1 >= 0)
                nodes.Add(map[x + 1, y + 1]);

            return nodes;
        }

        private static void Error()
        {
            Debug.LogError("当前寻路不可达");
        }


        public static Vector2Int GetGridByPosition(Navigation2D navigation, Vector3 position)
        {
            float nodeSize = navigation.navigationData.nodeSize;

            float posX = position[0];
            float posY = position[1];

            int gridX = (int)Mathf.Floor(posX / nodeSize);
            int gridY = (int)Mathf.Floor(posY / nodeSize);

            Vector2Int grid = new Vector2Int(gridX, gridY);
            return grid;
        }
    }
}