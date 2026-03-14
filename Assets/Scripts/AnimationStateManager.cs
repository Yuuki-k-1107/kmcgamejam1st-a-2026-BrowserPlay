using R3;
using UnityEngine;

public class AnimationStateManager : MonoBehaviour
{

	private readonly ReactiveProperty<GameState> _State;
	public ReadOnlyReactiveProperty<GameState> State => _State;


}
