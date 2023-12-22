using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class bEnemyScript : MonoBehaviour
{
    public Transform player;
    public Transform cam;
    public float range;
    private NavMeshAgent agent;
    public GameObject bullet;
    public float bulletSpeed;
    public float wait;
    public int hp;
    public int damage;
    //public float speed;
    public GameObject boss;
    public AudioSource pew;
    public AudioSource hit;
    public AudioSource die;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        hp = 150;
        damage = 5;
        wait = 0;
        bulletSpeed = 500;
        range = 50;
    }
  void autoFire(){
        float rangeCurrent = Vector3.Distance(player.position,boss.GetComponent<Transform>().position);
        if(rangeCurrent<range){
            if(wait > 0){
                bullet.SetActive(false);
                wait -= Time.deltaTime;
            }else{
                bullet.SetActive(true);
                wait = 3;
                pew.Play();
                GameObject projectile = Instantiate(bullet, cam.position,cam.rotation);
                projectile.GetComponent<Rigidbody>().AddForce(cam.forward*bulletSpeed);
            }
        }
    }
    void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("PlayerBullet")){
            hp -= damage;
            hit.Play();
        }else if(other.gameObject.CompareTag("lvl2")){
            hp -= damage*5;
            hit.Play();
        }
        if(hp<=0){
            die.Play();
            boss.SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        autoFire();
        agent.destination = player.position;
    }
}
