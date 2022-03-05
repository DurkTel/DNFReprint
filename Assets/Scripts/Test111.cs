using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test111 : MonoBehaviour
{
    // Start is called before the first frame update

    Entity.ModelInfo[] modelInfos;
    void Start()
    {
        EntityManager.Initialize();
        Entity entity = EntityManager.Instance.CreateEntity(Entity.EntityType.LocalPlayer);
        modelInfos = new Entity.ModelInfo[]
        {
            new Entity.ModelInfo()
            {
                fashionCode = 10000,
                position = Vector3.zero,
                scale = Vector3.one * 0.01f
            },
            new Entity.ModelInfo()
            {
                fashionCode = 10000,
                position = new Vector3(0, 0, -0.001f),
                scale = Vector3.one * 0.01f
            },
            new Entity.ModelInfo()
            {
                fashionCode = 10000,
                position = new Vector3(0, 0, -0.011f),
                scale = Vector3.one * 0.01f
            },
            new Entity.ModelInfo()
            {
                fashionCode = 10000,
                position = new Vector3(0, 0, -0.011f),
                scale = Vector3.one * 0.01f
            },
            new Entity.ModelInfo()
            {
                fashionCode = 10000,
                position = new Vector3(0, 0, -0.004f),
                scale = Vector3.one * 0.01f
            },
            new Entity.ModelInfo()
            {
                fashionCode = 10000,
                position = new Vector3(0, 0, -0.005f),
                scale = Vector3.one * 0.01f
            },
            new Entity.ModelInfo()
            {
                fashionCode = 10000,
                position = new Vector3(0, 0, -0.005f),
                scale = Vector3.one * 0.01f
            },
            new Entity.ModelInfo()
            {
                fashionCode = 10000,
                position = new Vector3(0, 0, -0.01f),
                scale = Vector3.one * 0.01f
            },
            new Entity.ModelInfo()
            {
                fashionCode = 10000,
                position = new Vector3(0, 0, -0.01f),
                scale = Vector3.one * 0.01f
            },
        };
        entity.models = modelInfos;
        entity.Skin_SetVisible(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Entity entity = EntityManager.Instance.CreateEntity(Entity.EntityType.LocalPlayer);
            entity.models = modelInfos;

            entity.Skin_SetVisible(true);

        }
    }
}
