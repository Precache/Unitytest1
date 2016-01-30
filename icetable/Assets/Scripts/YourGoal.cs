using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class YourGoal : MonoBehaviour {

    public Text triggerText;

    void OnTriggerEnter(Collider other)
    {
        //LayerMask.NameToLayer("ScorePuck");
        if (other.gameObject.layer == LayerMask.NameToLayer("ScorePuck"))
        {
            GameManager.instance.ScoredYou();
        }
        else
        {
            //GameManager.instance.ScoredYou();
        }
        triggerText.text = "Col Layer:" + other.gameObject.layer + "\nMsk Layer:" + LayerMask.NameToLayer("ScorePuck");
    }

	// Use this for initialization
	void Start () {
        triggerText.text = "Msk Layer:" + LayerMask.NameToLayer("ScorePuck");

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
