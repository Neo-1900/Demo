using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXController : MonoBehaviour
{
    public static SFXController instance;

    private void Awake()
    {
        instance = this;
    }

    //������Ч����
    public AudioSource[] soundEffect;

    public void PlaySFX(int sfxToPlay)
    {
        //��ֹͣ����, �ظ�����ʱ������Ч��
        soundEffect[sfxToPlay].Stop();
        soundEffect[sfxToPlay].Play();
    }

    //�ı���Ч���������Ե�����������
    public void PlaySFXPitched(int sfxToPlay)
    {
        soundEffect[sfxToPlay].pitch = Random.Range(.8f, 1.2f);

        PlaySFX(sfxToPlay);
    }
}