﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEntity : Entity
{
    protected override void Skin_CreateAvatar()
    {
        base.Skin_CreateAvatar();


        if (boxCollider == null)
        {
            boxCollider = gameObject.AddComponent<BoxCollider2D>();
            boxCollider.offset = new Vector2(0, 0.08f);
            boxCollider.size = new Vector2(0.5f, 0.1f);
        }


        if (rigidbody == null)
        {
            rigidbody = gameObject.AddComponent<Rigidbody2D>();
            rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
            rigidbody.sleepMode = RigidbodySleepMode2D.NeverSleep;
            rigidbody.gravityScale = 0;
            rigidbody.drag = 10f;
            rigidbody.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        }


        //初始化完载体加载各个皮肤部件
        ResourceRequest re = AssetLoader.LoadAsync<GameObject>(AvatarUtility.commonCharacterBone);
        re.completed += (p) =>
        {
            rootBone = Object.Instantiate(re.asset as GameObject).transform;
            rootBone.SetParent(mainAvatar.gameObject.transform);
            rootBone.localPosition = Vector3.zero;
            Init_Skin();
        };
    }
}
