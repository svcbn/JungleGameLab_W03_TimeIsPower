using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
	#region PublicVariables
	public static GameManager instance;
	public enum EGameState
	{
		idle = 0,
		battle = 1
	}
	#endregion

	#region PrivateVariables
	private EGameState state;
	[SerializeField] private Player player;
	private Boss boss;
	private GameObject stageEnterTrigger;

	[SerializeField] private GameObject GameClearUI;
	[SerializeField] private List<Boss> bossList = new List<Boss>();
	[SerializeField] private List<GameObject> stageEnterTriggerList = new List<GameObject>();
	[SerializeField] private FadeBlackController fadeBlackController;
	#endregion

	#region PublicMethod
	public Player GetPlayer() => player;
	public Boss GetBoss() => boss;
	public void SetBoss(Boss _boss) => boss = _boss;
	public void Initialize()
	{

	}
	public void GameStart()
	{
		player.CanAct();
	}
	public void BattleStart(Utils.EStage _stage)
	{
		
		state = EGameState.battle;
		boss = bossList[(int)_stage];
		stageEnterTrigger = stageEnterTriggerList[(int)_stage];
		boss.Initialize();
		boss.gameObject.SetActive(true);
		BossHpGUI.instance.ShowGUI();
		BossHpGUI.instance.SetBossNameText(boss.bossName);
		BossHpGUI.instance.SetMaxHp(boss.GetMaxHp());
		BodyGenerator.instance.ClearBody();
		GhostManager.instance.RecordAndReplay();
		boss.PatternStart();
	}
	public void BattleEnd()
	{
		if (state == EGameState.idle)
			return;

		player.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
		state = EGameState.idle;
		DeathCounterManager.instance.PlayerDead();
		CameraController.instance.HitShake();
		fadeBlackController.StartFade();
		stageEnterTrigger.SetActive(true);
		GhostManager.instance.StopRecordAndReplay();

		Invoke(nameof(WaitBattleEnd), fadeBlackController.waitFadeTime+fadeBlackController.fadeTime);
	}

	private void WaitBattleEnd()
    {
		BossHpGUI.instance.HideGUI();
		DynamicObjectManager.instance.Clear();
		boss.gameObject.SetActive(false);
		boss = null;

	}
	public void GameClear()
	{
		GameClearUI.SetActive(true);
		Time.timeScale = 0;
		Physics2D.SyncTransforms();
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
