using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Main : MonoBehaviour {
    List<GameObject> currentCollisions = new List<GameObject>();

    private string[] menu1 = { "lettuce","tomato" };//vegetariano
    private string[] menu2 = { "bread","meat","meat","meat","meat" };//DellaSpecial
    private string[] menu3 = { "lettuce","tomato","meat" };//kids
    private string[] menu4 = { "lettuce","tomato","meat","cheese" };//xtudo
    private string[] menu5 = { "meat","cheese","cheese","cheese" };//xqueijo

    public GameObject goal;
    void Start () {
        
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    void OnTriggerEnter(Collider other)
    {
        //print("Colisao com "+other.tag);

        currentCollisions.Add(other.gameObject);


    }

    void OnTriggerExit(Collider other) {

        // Remove the GameObject collided with from the list.
        currentCollisions.Remove(other.gameObject);

    }

    public void OnDoneButton() {
        PrintList(currentCollisions);
        currentCollisions.Sort(SortByY);
        print("Ordered: ");
        PrintList(currentCollisions);

        StartCoroutine(Test());

    }

    IEnumerator Test() {





        if (CompareMenu(menu2)) {
            goal.GetComponent<Renderer>().material.color = Color.green;

        } else {
            goal.GetComponent<Renderer>().material.color = Color.red;
        }


        yield return new WaitForSeconds(.2f);
        goal.GetComponent<Renderer>().material.color = Color.grey;

    }


    public bool CompareMenu(string[] menu) {

        if (currentCollisions.Count-2 != menu.Length) {//check number of ingredients
            return false;
        }

        if (currentCollisions[0].tag!="bread" || currentCollisions[currentCollisions.Count-1].tag != "bread") {//check if is covered by bread
            return false;
        }

        bool hasIt = false;
        for (int j = 0; j < currentCollisions.Count - 2; j++) {
            for (int i = 0; i < menu.Length; i++){
                if (currentCollisions[j+1].tag == menu[i]) {
                    hasIt = true;
                }
            }
                if (!hasIt) {
                    return false;
                }
        }



        return true;
    }

    static int SortByY(GameObject x1,GameObject x2) {
        return x1.transform.position.y.CompareTo(x2.transform.position.y);
    }


    public void PrintList(List<GameObject> list) {

        foreach (GameObject gObject in list) {
            print(gObject.name);
        }
    }
}
