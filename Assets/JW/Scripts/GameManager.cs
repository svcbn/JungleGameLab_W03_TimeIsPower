using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
	#region PublicVariables
	public static GameManager instance;
	#endregion

	#region PrivateVariables
	[SerializeField] private Player player;
	[SerializeField] private Boss boss;

	[SerializeField] private UnityEvent onGameStart;
	[SerializeField] private UnityEvent onBattleStart;
	[SerializeField] private UnityEvent onBattleEnd;
	#endregion

	#region PublicMethod
	public Player GetPlayer() => player;
	public Boss GetBoss() => boss;
	public void Initialize()
	{

	}

	public void GameStart()
	{
		onGameStart.Invoke();
	}
	public void BattleStart()
	{
		onBattleStart.Invoke();
		StartCoroutine(boss.ChangePattern());
	}
	public void BattleEnd()
	{
		onBattleEnd.Invoke();
	}
	#endregion

	#region PrivateMethod
	private void Awake()
	{
		if(instance == null)
		{
			instance = this;
		}
	}
	#endregion
}
