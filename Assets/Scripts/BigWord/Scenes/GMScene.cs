﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cfg.db;

public class GMScene
{
    private List<GMScenePart> sceneParts = new List<GMScenePart>();

    private MapType m_mapType;
    public MapType mapType { get { return m_mapType; } }

    private int m_mapId;
    public int mapId { get { return m_mapId; } }

    private GameObject m_gameObject;
    public GameObject gameObject { get { return m_gameObject; } }

    private Transform m_transform;
    public Transform transform { get { return m_transform; } }

    private MapCfg m_mapData;
    public MapCfg mapData { get { return m_mapData; } }

    private float m_releaseTime;
    public float releaseTime { get { return m_releaseTime; } }

    private List<PortalEntity> m_portalEntities = new List<PortalEntity>();

    public void SetData(GameObject game, MapCfg mapData)
    {
        m_gameObject = game;
        m_transform = game.transform;
        m_mapData = mapData;
    }

    public void Release()
    {
        m_mapData = null;
        m_transform = null;
        m_gameObject = null;
    }

    public void Activate(Vector3 pos)
    {
        m_transform.position = Vector3.zero;
        if (GMEntityManager.localPlayer != null)
        {
            //切换和平或战斗状态
            GMEntityManager.localPlayer.ChangeStatus(m_mapData.MapType == cfg.MapType.MAIN_CITY ? CharacterEntity.CharacterStatus.PEACE : CharacterEntity.CharacterStatus.FIGHT);
            //重置位置
            GMEntityManager.localPlayer.transform.position = pos != Vector3.zero? pos : new Vector3(m_mapData.CharacterPosX, m_mapData.CharacterPosY, 0);
            //限制相机
            OrbitCamera.Instance.SetCameraLimit(m_mapData.CameraMinHeight, m_mapData.CameraMaxHeight, m_mapData.CameraMinWidth, m_mapData.CameraMaxWidth);
            //背景音乐
            MusicManager.Instance.PlayBkMusic(m_mapData.BgMusic);
            CreateEntityByMapCfg();
        }
    }

    public void Inactivation()
    {
        m_transform.position = new Vector3(9999, 9999, 9999);
        m_releaseTime = Time.realtimeSinceStartup;
        foreach (var portEntity in m_portalEntities)
        {
            GMEntityManager.Instance.ReleaseEntity(portEntity.entityId);
        }
        m_portalEntities.Clear();
    }

    private void CreateEntityByMapCfg()
    {
        foreach (var portal in m_mapData.Portals)
        {
            PortalEntity portEntity = (PortalEntity)GMEntityManager.CreateEntity(5);
            portEntity.Skin_SetAvatarPosition(new Vector3(portal.X, portal.Y, portal.Z));
            MapCfg portMapData = MDefine.tables.TbMap.Get(portal.MapId);
            portEntity.SetData(portMapData, portal);
            m_portalEntities.Add(portEntity);
        }
    }

}
public enum MapType
{
    /// <summary>
    /// 主城
    /// </summary>
    Unique,
}
