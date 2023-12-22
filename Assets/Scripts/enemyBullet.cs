using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBullet : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject pro;

    public Transform pPosition;

    public Vector3 intial;

    public float maxDis;
    public float currentDist;
   void Start()
    {
        
        maxDis = 150;
    }
    void Awake(){
         intial = pPosition.position;
    }
    void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Tree") ){
            Destroy(pro);
        }
        
    }
    void setInactive(){
        float currentDist = Vector3.Distance(intial,pPosition.position);
        if(currentDist > maxDis){
            Destroy(pro);
        }
    }
    // Update is called once per frame
    void Update()
    {
        setInactive();
    }
}
