using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using System.Threading;
using System.Threading.Tasks;
using Unity.VisualScripting;
using NUnit.Framework;

using Random = UnityEngine.Random;

public class ClockCon : MonoBehaviour
{
	Vector3 startPos;
    readonly Vector3 Jump = new(0, 0.5f);
	readonly Vector3 vibration = new(0.1f, 0);

	[SerializeField] float MaxAlarmTime;

	bool alarming = false;
	double alarmStartTime;
	CancellationTokenSource CTSAlarmStop;
	CancellationToken CTAlarmStop;

	/// <summary>
	/// リセット用のデータを設定
	/// </summary>
	private void Start()
	{
		startPos = base.transform.position;
	}

	/// <summary>
	/// アラームのタイマーを動かす
	/// </summary>
	/// <returns></returns>
	public async UniTask AlarmTimerStart()
	{
		float alarmTime = Random.Range(1, 1 + MaxAlarmTime);
		Debug.Log(alarmTime);
		await UniTask.Delay(TimeSpan.FromSeconds(alarmTime));
	}

	/// <summary>
	/// アラームを鳴らす
	/// </summary>
	/// <returns></returns>
	public async UniTask AlarmStart()
	{
		//アラームが鳴る
		Debug.Log("Alarm Start");
		transform.position += Jump;
		alarming = true;
		alarmStartTime = Time.time;
		CTSAlarmStop = new CancellationTokenSource();
		CTAlarmStop = CTSAlarmStop.Token;
		await Alarming(CTAlarmStop);
	}

	/// <summary>
	/// アラームを止める
	/// </summary>
	/// <returns>止めるまでの時間</returns>
	public double AlarmStop()
	{
		alarming = false;
		CTSAlarmStop.Cancel();
		CTSAlarmStop = null;
		double alarmTime = Time.timeAsDouble - alarmStartTime;
		Debug.Log($"Alarm Stop  Time : {alarmTime}");
		return alarmTime;
	}

	/// <summary>
	/// 振動する
	/// </summary>
	/// <param name="CTAlarmStop"></param>
	/// <returns></returns>
	async UniTask Alarming(CancellationToken CTAlarmStop)
	{
		bool vibe = false;
		try
		{
			while(!CTAlarmStop.IsCancellationRequested)
			{
				transform.position += vibration;
				vibe = true;
				await UniTask.Delay(1);
				transform.position -= vibration;
				vibe = true;
				await UniTask.Delay(1);
			}
		}
		catch(OperationCanceledException) { /*何もせずにfinally*/}
		finally
		{
			if(vibe)
			{
				transform.position -= vibration;
			}
			Debug.Log("Alarm Stop");
		}
	}

	public void Reset()
	{
		base.transform.position = startPos;
	}
}
