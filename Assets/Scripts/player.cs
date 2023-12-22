using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class player : MonoBehaviour
{
    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI introScreen;
    public TextMeshProUGUI ammoSecText;
    public GameObject bullet;
    public GameObject bulletSec;
    public Transform cam;
    public float bulletSpeed;
    public int ammoTotal;
    public int ammoTotalSec;
    public float reloadTime;
    public float reloadTimeSec;
    public AudioSource pew;
    public AudioSource hit;
    public AudioSource item;
    public int health;
    public int defense;
    public int damage;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
        introScreen.text = "Defeat All Enemies To Win. Mushroom PickUps give health, Hats Give Defense. Press Left Shift To Start";
        bulletSpeed = 1600f;
        ammoTotal = 12;
        ammoTotalSec = 2;
        reloadTime = 5;
        reloadTimeSec = 2;
        health = 100;
        ammoText.text = "Main Ammo: " + ammoTotal.ToString();
        ammoSecText.text = "Secondary: " + ammoTotalSec.ToString();
        healthText.text = "HP " + health.ToString();
        defense = 0;
        damage = 5;
    }
    void OnMouseClick(){
        if(Input.GetMouseButtonDown(0)){
            if(ammoTotal > 0){
                pew.Play();
                GameObject projectile = Instantiate(bullet, cam.position,cam.rotation);
                projectile.GetComponent<Rigidbody>().AddForce(cam.forward*bulletSpeed);
                ammoTotal-=1;
                ammoText.text = "Main Ammo: " + ammoTotal.ToString();
            }
        }
        if(Input.GetMouseButtonDown(1)){
            if(ammoTotalSec > 0){
                pew.Play();
                GameObject projectileSec = Instantiate(bulletSec, cam.position,cam.rotation);
                projectileSec.GetComponent<Rigidbody>().AddForce(cam.forward*(bulletSpeed*3));
                ammoTotalSec-=1;
                ammoSecText.text = "Secondary: " + ammoTotalSec.ToString();
            }
        }
    }
    void Reload(){
        if(ammoTotal == 0){
            bullet.SetActive(false);
            if(reloadTime > 0){
                reloadTime -= Time.deltaTime;
                ammoText.text = "Reloading in " + ((int)reloadTime + 1).ToString();;
            }else if((int)reloadTime < 1){
                reloadTime = 5;
                ammoTotal = 12;
                ammoText.text = "Main Ammo: " + ammoTotal.ToString();
                bullet.SetActive(true);
            }
        }
        if(ammoTotalSec == 0){
            bulletSec.SetActive(false);
            if(reloadTimeSec > 0){
                reloadTimeSec -= Time.deltaTime;
                ammoSecText.text = "Reloading in " + ((int)reloadTimeSec + 1).ToString();;
            }else if((int)reloadTimeSec < 1){
                reloadTimeSec = 2;
                ammoTotalSec = 2;
                ammoSecText.text = "Secondary: " + ammoTotalSec.ToString();
                bulletSec.SetActive(true);
            }
        }
    }
    void OnTriggerEnter(Collider other){
      float df = 0;
      if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("EnemyBullet")){
            hit.Play();
            health -= 15+defense;
            healthText.text = "HP: " + health.ToString();
      }
      if (other.gameObject.CompareTag("HPPickUp")){
            item.Play();
            health += 50;
            healthText.text = "HP: " + health.ToString();
            other.gameObject.SetActive(false);
      }
      if (other.gameObject.CompareTag("DefenseUp")){
            item.Play();
            defense +=  5;
            other.gameObject.SetActive(false);
            df = 2;
      }
     if(df > 0){
        df -= Time.deltaTime;
        healthText.text = "DEFENSE UP!";
     }else if((int)df < 1){
        healthText.text = "HP: " + health.ToString();
     }
        
    }
    void Reset(){
        if(Input.GetKeyDown(KeyCode.R)){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Time.timeScale = 1;
        }
    }
    void win(){
        GameObject a = GameObject.FindWithTag("Enemy");
        if(a == null){
            introScreen.enabled = true;
            introScreen.text = "YOU WIN Press R to Retry";
            Time.timeScale = 0;
        }
    }
    // Update is called once per frame
    void FixedUpdate(){
        Reload();
    }
    void Update()
    {
        win();
        Reset();
        if(Input.GetKeyDown(KeyCode.LeftShift)){
            introScreen.enabled = false;
            Time.timeScale = 1;
        }
        OnMouseClick();
        if(health<= 0){
            introScreen.enabled = true;
            introScreen.text = "You Died :( Press R to Try Again";
            Time.timeScale = 0;
        }
    }
}
