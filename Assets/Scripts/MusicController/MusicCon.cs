using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.TextCore.Text;
using System;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MusicCon : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] AudioClip Audio;
    [SerializeField] bool IsBGM;
    [Header("Tori-Aezu")]
    [SerializeField, Range(0, 1)] float Volume;

    AudioSource AudioSource;

    public MusicCon(AudioSource AudioSouce)
    {
        this.AudioSource = AudioSouce;
    }

	private void Awake()
	{
        AudioSource.resource = Audio;
        AudioSource.volume = Volume;
	}

	//音量調整
	public void Play()
    {
        if(IsBGM)
        {
            AudioSource.Play();
        }
        else
        {
            AudioSource.PlayOneShot(Audio);
        }
    }
    public void Stop()
        => AudioSource.Stop();

#if UNITY_EDITOR
    /// <summary>
    /// カスタムインスペクター
    /// </summary>
    [CustomEditor(typeof(MusicCon))]
    public class MusicConEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            // target は処理コードのインスタンスだよ！ 処理コードの型でキャストして使ってね！
            MusicCon Con = target as MusicCon;

            AudioClip audio = EditorGUILayout.ObjectField("Audio", null, typeof(AudioClip), true) as AudioClip;
            EditorGUILayout.BeginHorizontal();
            bool BGM = EditorGUILayout.Toggle("IsBGM", false);
            float volume = (float)EditorGUILayout.IntSlider("volume", 100, 0, 100) / 100;
            EditorGUILayout.EndHorizontal();
            Con.Audio = audio;
            Con.IsBGM = BGM;
            Con.Volume = volume;
        }
    }
#endif
}
