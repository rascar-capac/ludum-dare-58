using System.Collections.Generic;
using UnityEngine;

public class SpawnerOnRestart : MonoBehaviour
{
    public Transform Prefab;

    public List<SceneObjectInfo> ObjectsFromScene = new();

    private void Awake()
    {
        ObjectsFromScene.Clear();

        foreach (Rigidbody2D sceneObject in transform.GetComponentsInChildren<Rigidbody2D>())
        {
            ObjectsFromScene.Add(new(sceneObject.transform));
        }

        GameManager.Instance.OnGameStarted += GameManager_OnGameStarted;
    }

    private void GameManager_OnGameStarted(bool isFirstGame)
    {
        if (!isFirstGame)
        {
            RespawnObjects();
        }
    }

    public void RespawnObjects()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        foreach (SceneObjectInfo sceneObjectInfo in ObjectsFromScene)
        {
            Transform respawnedObject = Instantiate(Prefab, sceneObjectInfo.Position, sceneObjectInfo.Rotation, transform);
            respawnedObject.localScale = sceneObjectInfo.Scale;
        }
    }

    public struct SceneObjectInfo
    {
        public Vector3 Position;
        public Quaternion Rotation;
        public Vector3 Scale;

        public SceneObjectInfo(Transform sceneObject)
        {
            Position = sceneObject.position;
            Rotation = sceneObject.rotation;
            Scale = sceneObject.localScale;
        }
    }
}
