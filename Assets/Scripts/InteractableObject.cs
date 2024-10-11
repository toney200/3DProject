using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public string itemName;
    //Return name of object this is attached to 
    public string GetItemName()
    {
        return itemName;
    }
}
