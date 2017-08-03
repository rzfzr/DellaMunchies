using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Main : MonoBehaviour {


    public Text order,fries,juice;



    List<GameObject> currentCollisions = new List<GameObject>();

    private string[] menu1 = { "lettuce","tomato" };//vegetariano
    private string[] menu2 = { "bread","meat","meat","meat","meat" };//DellaSpecial
    private string[] menu3 = { "lettuce","meat" };//kids
    private string[] menu4 = { "lettuce","tomato","meat","cheese" };//xtudo
    private string[] menu5 = { "meat","cheese","cheese","cheese" };//xqueijo

    private string[] menuOrdered;
    public GameObject goal;
    void Start () {

        menuOrdered = NewOrder();


	}

    void NewFries() {

        int seed = Random.Range(0,3);

        if (seed == 0) {
            fries.text = " ";
        } else {
            fries.text = seed.ToString() + " batata";
        }
        
    }
    void NewJuices() {

        int seedChoice = Random.Range(0,3);

        int seedNumber = Random.Range(0,3);

        if (seedNumber == 0) {
            juice.text = " ";
        } else {
            if (seedChoice == 0) {
                juice.text = seedNumber.ToString() + " suco de abacaxi";

            }else if (seedChoice == 1) {
                juice.text = seedNumber.ToString() + " sprite";
            }else{
                juice.text = seedNumber.ToString() + " limonada";
            }
        }


    }

    string[] NewOrder() {

        NewFries();
        NewJuices();


        int seed = Random.Range( 1,5);

        if (seed == 1) {
            order.text = "Vegetariano";
            return menu1;
        } else if (seed == 2) {
            order.text = "DellaSpecial";
            return menu2;
        } else if (seed == 3) {
            order.text = "Kids";
            return menu3;
        } else if (seed == 4) {
            order.text = "X-tudo";
            return menu4;
        } else {
            order.text = "X-queijo";
            return menu5;
        }


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

    public void OnResetButton() {

        SceneManager.LoadScene("play");
        
    }

    IEnumerator Test() {

        if (CompareMenu(menuOrdered)) {
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
