using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Use : MonoBehaviour
{
    public GameObject prompt;

    public GameObject Player;
    public Transform otherDoor;
    bool playerIn;
    
    public void Show(GameObject prompt)
    {
        prompt.SetActive(true);
    }
    public void Hide(GameObject prompt)
    {
        prompt.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(Time.time + ":�i�J��Ĳ�o������H�O:" + other.gameObject.name);
        Show(prompt);
    }
    private void OnTriggerStay(Collider other)
    {
        Debug.Log(Time.time + "�d�bĲ�o������H�O:" + other.gameObject.name);
        if (other.gameObject.tag == "Player")
        {
            print("Press E to teleport");
            if (Input.GetKeyDown(KeyCode.E))
            {
                other.gameObject.transform.position = otherDoor.gameObject.transform.position;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log(Time.time + "���}Ĳ�o������H�O:" + other.gameObject.name);
        Hide(prompt);
    }
    void Start()
    {
        Hide(prompt);
    }
   

}
