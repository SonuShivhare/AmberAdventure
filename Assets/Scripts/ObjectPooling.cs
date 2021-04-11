using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    [SerializeField] private GameObject pooledObject;
    [SerializeField] private int count;
    [SerializeField] private bool willGrow = true;

    public List<GameObject> poolList;
    void Awake()
    {
        for (int i = 0; i < count; i++)
        {
            GameObject obj = (GameObject)Instantiate(pooledObject);
            obj.SetActive(false);
            poolList.Add(obj);

        }
    }

    public void PlaceIntoScene(Transform firePoint)
    {
        if (poolList.Count != 0)
        {
            Quaternion quaternion = firePoint.rotation;
            poolList[0].transform.position = firePoint.position;
            poolList[0].transform.rotation = quaternion;
            poolList[0].SetActive(true);
            poolList.RemoveAt(0);
            return;
        }

        if (willGrow)
        {
            Quaternion quaternion = firePoint.rotation;
            GameObject obj = (GameObject)Instantiate(pooledObject);
            obj.transform.position = firePoint.position;
            obj.transform.rotation = quaternion;
            return;
        }
        return;
    }

    public void PlaceBackToList(GameObject obj)
    {
        obj.SetActive(false);
        //obj.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        poolList.Add(obj);
    }
}
