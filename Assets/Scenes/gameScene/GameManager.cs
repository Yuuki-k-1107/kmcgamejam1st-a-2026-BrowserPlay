using Cysharp.Threading.Tasks;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	ClockCon ClockCon;

	#region アラーム
	void GameStart()
	{
		ClockCon.AlarmStart();
		//QTEも開始
	}
	#endregion

	#region QTE関連
	/// <summary>
	/// アラームを止めるための仮
	/// </summary>
	public void AlarmStop()
	{
		ClockCon.AlarmStop();
	}

	public void QTEEnded(int combo)
	{
		//リザルト表示
		Debug.Log("リザルト表示");
	}
	#endregion
}
