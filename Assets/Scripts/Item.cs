using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public InventoryItem data {get; private set;}
    public int stackSize {get; private set;}

    // Start is called before the first frame update
    public Item(InventoryItem sauce)
    {
        data = sauce;
        AddToStack();
        
    }

    public void AddToStack(){
        stackSize++;
    }
    public void RemoveFromStack(){
        stackSize--;
    }

}
