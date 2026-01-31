using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledDeactivator : MonoBehaviour
{
    [SerializeField] private bool KILLEVERYTHING;
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    GameObject collidingObject = collision.gameObject;
    //    if(collidingObject.GetComponent<IPooledObject>() != null && KillTest(collidingObject))
    //    {
    //        //print(collision.gameObject.GetComponent<IPooledObject>().ToString());
    //        collision.gameObject.SetActive(false);
    //    }
    //}

    //private bool KillTest(GameObject collidingObject)
    //{
    //    //true if kill everything
    //    //true if not kill everything and no hpObject
    //    return KILLEVERYTHING || collidingObject.GetComponent<HPObject>() == null;
    //}
}
