using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum TankType
{
    Tank_One = 1,
    Tank_Two = 2,
    Tank_Enemy = 3,
}

public class TankControl : MonoBehaviour
{

    public Rigidbody _rigidbody;
    public float h_Value;
    public float v_Value;
    public float speed = 30;
    public float rotateSpeed = 60;

    public TankType tankType = TankType.Tank_One;
    

    public string inputHorizontalStr;
    public string inputVerticalStr;
    public string inputFireStr;


    //Fire
    public GameObject shell;
    public Transform shellPos;
    public float MaxSpeed = 30;
    public float currentSpeed = 10;
    public float speedChange = 5;

    //HP
    public float HP = 15;
    public Slider hpSlider;

    //坦克爆炸
    public ParticleSystem tankExplosion;
    public AudioSource tankFire;

    // Start is called before the first frame update
    void Start()
    {
        TankInitilization(); 
    }

    void TankInitilization()
    {
        print("坦克初始化type"+tankType);
        _rigidbody = this.gameObject.GetComponent<Rigidbody>();
        inputHorizontalStr = inputHorizontalStr + (int)tankType;
        inputVerticalStr = inputVerticalStr + (int)tankType;
        inputFireStr = inputFireStr + (int)tankType;

        //初始化slider
        hpSlider.maxValue = HP;
        hpSlider.value = HP;
    }

    // Update is called once per frame
    //Horizontal1 ad
    //Vertical1  sw
    void Update()
    {
        h_Value = Input.GetAxis(inputHorizontalStr);
        v_Value = Input.GetAxis(inputVerticalStr);

        //move
        if (v_Value != 0)
        {
            _rigidbody.MovePosition(this.transform.position + v_Value*this.transform.forward * speed * Time.deltaTime);
        }
        if (h_Value != 0)
        {
            if (v_Value < 0)
            {
                h_Value = -h_Value;
            }
            this.gameObject.transform.Rotate(Vector3.up * h_Value * rotateSpeed * Time.deltaTime);
        }
        

        if (Input.GetButton(inputFireStr))
        {
            currentSpeed += speedChange * Time.deltaTime;
            if (currentSpeed >= MaxSpeed)
            {
                currentSpeed = MaxSpeed;
            }
        }

        if (Input.GetButtonUp(inputFireStr))
        {
            OpenFire(currentSpeed);
            currentSpeed = 10;
        }


    }

    void OpenFire(float shellSpeed)
    {

        
        GameObject shellObj = Instantiate(shell, shellPos.position, shellPos.transform.rotation);
        Rigidbody shellRigidbody = shellObj.GetComponent<Rigidbody>();
        if (shellRigidbody != null)
        {
            shellRigidbody.velocity = shellPos.forward * shellSpeed;
        }

    }

    public void ShellDamage(float damage)
    {
        if (HP > 0)
        {
            HP -= damage;
            hpSlider.value = HP;
        }
        if (HP <= 0)
        {
            //AudioManager._audioManagerInstance.tankExplosionAudioPlay();
            if (tankExplosion != null)
            {
                tankExplosion.transform.parent = null;
                tankExplosion.Play();
                Destroy(tankExplosion.gameObject, tankExplosion.main.duration);
            }
            this.gameObject.SetActive(false);
        }

    }

}
