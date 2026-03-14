using R3;
using UnityEngine;

public class AnimationStateManager : MonoBehaviour
{
	//各アニメーションオブジェクト
	[SerializeField] GameObject outFromBedAnimObj;
	[SerializeField] GameObject intoBedAnimObj;
	[SerializeField] GameObject BattlingAnimObj;
	[SerializeField] GameObject BattlingBedAnimObj;

	private readonly ReactiveProperty<GameState> _State;
	public ReadOnlyReactiveProperty<GameState> State => _State;

	AnimationStateManager()
	{
		//アニメーションオブジェクトのアクティベーション
		State.Subscribe(_ =>
		{
			outFromBedAnimObj.SetActive(State.CurrentValue == GameState.AlarmStoped);
			intoBedAnimObj.SetActive(State.CurrentValue == GameState.Final);
			BattlingAnimObj.SetActive(State.CurrentValue == GameState.Playing);
			BattlingBedAnimObj.SetActive(State.CurrentValue == GameState.Playing);
		});
	}

	public void Reset()
	{
		_State.Value = GameState.InBed;
	}
}
