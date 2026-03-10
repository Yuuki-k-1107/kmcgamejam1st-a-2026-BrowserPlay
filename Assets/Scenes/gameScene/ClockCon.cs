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
using UnityEngine.InputSystem;

public class ClockCon : MonoBehaviour
{
	[SerializeField] Transform Transform;
    readonly Vector3 Jump = new(0, 0.5f);
	readonly Vector3 vibration = new(0.1f, 0);

	[SerializeField] float MaxAlarmTime;

	bool alarming = false;
	CancellationTokenSource CTSAlarmStop = new CancellationTokenSource();
	CancellationToken CTAlarmStop;

	private async Task Start()
	{
		float alarmTime = Random.Range(1, 1 + MaxAlarmTime);
		Debug.Log(alarmTime);
		await UniTask.Delay(TimeSpan.FromSeconds(alarmTime));
		UniTask AlarmTask = Alarm();

		await UniTask.Delay(3000);//外部から何かのイベントとかを受け取る
		CTSAlarmStop.Cancel();
	}

	/// <summary>
	/// アラームを鳴らす
	/// </summary>
	/// <returns></returns>
	async UniTask Alarm()
	{
		Debug.Log("Alarm Start");
		Transform.position += Jump;
		CTAlarmStop = CTSAlarmStop.Token;
		await Alarming(CTAlarmStop);
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
				Transform.position += vibration;
				vibe = true;
				await UniTask.Delay(1);
				Transform.position -= vibration;
				vibe = true;
				await UniTask.Delay(1);
			}
		}
		catch(OperationCanceledException) { /*何もせずにfinally*/}
		finally
		{
			if(vibe)
			{
				Transform.position -= vibration;
			}
			Debug.Log("Alarm Stop");
		}
	}

	
}
