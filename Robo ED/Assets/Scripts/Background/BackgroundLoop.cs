using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundLoop : MonoBehaviour
{
    [SerializeField] private GameObject[] backgrounds;

    private Camera mainCamera;

    private Vector2 screenBounds;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = gameObject.GetComponent<Camera>();
        screenBounds = 
            mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
        foreach (GameObject obj in backgrounds)
        {
            LoadChildObjects(obj);
        }
    }

    void LoadChildObjects(GameObject obj)
    {
        float objectWidth = obj.GetComponent<SpriteRenderer>().bounds.size.x;
        int childrenNeeded = (int) Mathf.Ceil(screenBounds.x * 2 / objectWidth);
        GameObject clone = Instantiate(obj) as GameObject;
        for (int i = 0; i <= childrenNeeded; i++)
        {
            GameObject c = Instantiate(clone, obj.transform, true) as GameObject;
            var position = obj.transform.position;
            c.transform.position = new Vector3(objectWidth * i, position.y, position.z);
            c.name = obj.name + i;
        }
        Destroy(clone);
        Destroy(obj.GetComponent<SpriteRenderer>());
    }

    void RepositionChildObjects(GameObject obj)
    {
        Transform[] children = obj.GetComponentsInChildren<Transform>();
        if (children.Length > 1)
        {
            GameObject firstChild = children[1].gameObject;
            GameObject lastChild = children[^1].gameObject;
            float halfObjectWidth = lastChild.GetComponent<SpriteRenderer>().bounds.extents.x;
            if (transform.position.x + screenBounds.x > lastChild.transform.position.x + halfObjectWidth)
            {
                firstChild.transform.SetAsLastSibling();
                var position = lastChild.transform.position;
                firstChild.transform.position = new Vector3(position.x + halfObjectWidth * 2,
                    position.y, position.z);
            }
            else if (transform.position.x - screenBounds.x < firstChild.transform.position.x - halfObjectWidth)
            {
                lastChild.transform.SetAsFirstSibling();
                var position = firstChild.transform.position;
                lastChild.transform.position = new Vector3(position.x - halfObjectWidth * 2,
                    position.y, position.z);
            }
        }
    }
    
    private void LateUpdate()
    {
        foreach (GameObject obj in backgrounds)
        {
            RepositionChildObjects(obj);
        }
    }
}
