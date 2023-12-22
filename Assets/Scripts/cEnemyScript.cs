using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class cEnemyScript : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;
    public int hp;
    public int damage;
    //public float speed;
    public GameObject close;
    public AudioSource hit;
    public AudioSource die;

   // public float stun;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        hp = 20;
        damage = 5;
    }
 
    void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("PlayerBullet")){
            hp -= damage;
            hit.Play();
        }else if(other.gameObject.CompareTag("lvl2")){
            hp -= damage*4;
            hit.Play();
        }
        if(hp<=0){
            die.Play();
            close.SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        agent.destination = player.position;
    }
}
