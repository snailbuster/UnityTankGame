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

    //̹�˱�ը
    public ParticleSystem tankExplosion;
    public AudioSource tankFire;

    //��Ч
    public AudioSource m_TankAudio;         // Reference to the audio source used to play engine sounds. NB: different to the shooting audio source.
    public AudioClip m_EngineIdling;            // Audio to play when the tank isn't moving.
    public AudioClip m_EngineDriving;           // Audio to play when the tank is moving.
    public AudioSource m_TankShootAudio;
    public AudioClip m_TankFire;
    

    // Start is called before the first frame update
    void Start()
    {
        TankInitilization();
        AudioStart();
        
    }

    void TankInitilization()
    {
        print("̹�˳�ʼ��type"+tankType);
        _rigidbody = this.gameObject.GetComponent<Rigidbody>();
        inputHorizontalStr = inputHorizontalStr + (int)tankType;
        inputVerticalStr = inputVerticalStr + (int)tankType;
        inputFireStr = inputFireStr + (int)tankType;

        //��ʼ��slider
        hpSlider.maxValue = HP;
        hpSlider.value = HP;

    }

    // Update is called once per frame
    //Horizontal1 ad
    //Vertical1  sw
    void Update()
    {
        TankMove(); //̹���ƶ�


    }

    //���ܣ�̹���ƶ�
    private void TankMove()
    {
        h_Value = Input.GetAxis(inputHorizontalStr);
        v_Value = Input.GetAxis(inputVerticalStr);
        EngineAudio(h_Value, v_Value); //�ƶ���Ч

        //move
        if (v_Value != 0)
        {
            _rigidbody.MovePosition(this.transform.position + v_Value * this.transform.forward * speed * Time.deltaTime);
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

    //���ܣ�̹�˿���
    void OpenFire(float shellSpeed)
    {

        
        GameObject shellObj = Instantiate(shell, shellPos.position, shellPos.transform.rotation);
        Rigidbody shellRigidbody = shellObj.GetComponent<Rigidbody>();
        if (shellRigidbody != null)
        {
            TankFireAudio();//������Ч
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

    //������ʼ��
    public void AudioStart()
    {
        m_TankAudio.clip = m_EngineDriving;
        m_TankAudio.spatialBlend = 0;
        m_TankAudio.pitch = 0.9f;
        m_TankAudio.loop = true;
        m_TankAudio.volume = 0;
        m_TankAudio.Play();

   
    }

    //�ƶ���Ч
    private void EngineAudio(float h_value, float v_value)
    {
        if (Mathf.Abs(h_value) > 0.1f || Mathf.Abs(v_value) > 0.1f)
        {
            m_TankAudio.volume = 0.1f;
        }
        else
        {
            m_TankAudio.volume -= 0.1f;
            m_TankAudio.volume = Mathf.Max(0, m_TankAudio.volume);
        }
        
    }

    //������Ч
    private void TankFireAudio()
    {
        AudioSource.PlayClipAtPoint(m_TankFire, Camera.main.transform.position);
    }

}
