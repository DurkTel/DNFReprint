using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cfg.db;

public class PortalEntity : Entity
{
    /// <summary>
    /// 半径
    /// </summary>
    private float m_radius = 150f;
    /// <summary>
    /// 传送到的地图信息
    /// </summary>
    private MapCfg m_mapCfg;

    private Portal m_portalCfg;

    protected override void Skin_CreateAvatar()
    {
        base.Skin_CreateAvatar();

        InitPortal();

    }

    public override void ContactHandle(GMUpdateCollider.ContactPair contact, ColliderInfos collInfo)
    {
        if (contact.victim.entity == GMEntityManager.localPlayer && m_mapCfg != null)
        {
            GMScenesManager.Instance.SwitchScene(m_mapCfg.Id, new Vector3(m_portalCfg.ToX, m_portalCfg.ToY, m_portalCfg.ToZ));
            Debug.Log("传送到：" + m_mapCfg.MapName);
        }
    }

    public override void FixedUpdate(float deltaTime)
    {
        base.FixedUpdate(deltaTime);

        colliderUpdate.ClearContactZ(entityId);
    }

    public void SetData(MapCfg mapCfg, Portal portal)
    {
        m_mapCfg = mapCfg;
        m_portalCfg = portal;
    }

    private void InitPortal()
    {
        gameObject.name = "portal";

        CreateCollider(m_collidersXY, m_triggerXY, GMUpdateCollider.Axial.AxialXY);
        CreateCollider(m_collidersZ, m_triggerZ, GMUpdateCollider.Axial.AxialZ);

        RefreshCollider();

        mainAvatar.loadCompleted = true;
    }

    private void RefreshCollider()
    { 
        int allCount = m_collidersXY.Count;
        for (int i = 0; i < allCount; i++)
        {
            m_collidersXY[i].offset = Vector2.zero;
            m_collidersXY[i].size = Vector2.one * m_radius;
            m_collidersXY[i].isTrigger = true;
            m_collidersXY[i].transform.localScale = Vector3.one;
            m_triggerXY[i].hashCode = 999;
            m_triggerXY[i].layer = ColliderLayer.Interact;

            m_collidersZ[i].offset = Vector2.zero;
            m_collidersZ[i].size = new Vector2(m_radius, 20);
            m_collidersZ[i].isTrigger = true;
            m_collidersZ[i].transform.localScale = Vector3.one;
            m_triggerZ[i].hashCode = 999;
            m_triggerZ[i].layer = ColliderLayer.Interact;

            m_collidersXY[i].enabled = true;
            m_collidersZ[i].enabled = true;
            m_triggerXY[i].enabled = true;
            m_triggerZ[i].enabled = true;

        }
    }
}
