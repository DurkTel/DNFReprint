using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GMScene
{
    private List<GMScenePart> sceneParts = new List<GMScenePart>();

    public MapType mapType;

    public int mapId;

    public bool isLoading;

    public enum MapType
    {
        /// <summary>
        /// 主城
        /// </summary>
        Unique,
    }

}
