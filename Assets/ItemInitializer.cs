using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInitializer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        BoxCollider2D bc2d = gameObject.AddComponent(typeof(BoxCollider2D)) as BoxCollider2D;
        gameObject.tag = "CollectibleItem";
        bc2d.isTrigger = true;
    }

}
