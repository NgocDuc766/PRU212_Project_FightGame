using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using XXX.UI.Popup;

public enum GameState
{
	Prepare,
	Pause,
	Playing,
	Stop
}

public class GameManager : MonoBehaviour
{
	private static GameManager instance;
	public Image _hp1;
	public Image _anger1;
	public Image _hp2;
	public Image _anger2;

	public static GameManager GetIntance()
	{
		return instance;
	}

	private GameManager()
	{
		if (instance == null) {
			instance = this;
		}
	}

	public GameState state;

	[Header("vi tri goi player")]
	public Transform posPlayer1;
	[Header("vi tri goi AI")]
	public Transform posPlayer2;
	[Header("text start")]
	public Text txtTimerStart;
	[Header("slider")]
	public GameObject sliderHealthy, sliderPower;

	[Header("slider")]
	public GameObject sliderHealthyEnemy, sliderPowerEnemy;

	private PrefabGO prefabScript;


	// Use this for initialization
	void Start()
	{
		//Time.timeScale = 1;
		state = GameState.Prepare;
		prefabScript = PrefabGO.GetInstance();
		Debug.Log("1");
		if (MenuControll.GetInstance() == null) {
			Debug.Log("nulll");

		}
		InitPlayer1();
		InitPlayer2();
		//InitAI ();
		//StartCoroutine(Loading());
	}


	//private IEnumerator Loading()
	//{
	//	if (MenuControll.GetInstance() != null) {
	//		MenuControll.GetInstance().bgLoading.gameObject.SetActive(true);
	//		for (float i = 1; i > 0.2f; i -= 0.1f) {
	//			var colorTemp = MenuControll.GetInstance().bgLoading.GetComponent<SpriteRenderer>().color;
	//			colorTemp.a = i;
	//			MenuControll.GetInstance().bgLoading.GetComponent<SpriteRenderer>().color = colorTemp;
	//			yield return new WaitForSeconds(0.1f);
	//		}
	//		MenuControll.GetInstance().bgLoading.gameObject.SetActive(false);
	//		StartCoroutine(CountTimerStart());
	//	}
	//}

	private void InitPlayer1()
	{
		//if (MenuControll.GetInstance() != null)
		//{
		Debug.Log(MenuControll.player1IndexChara);
		Instantiate(prefabScript.player1s[MenuControll.player1IndexChara], posPlayer1.position, Quaternion.identity);
		//}
		//else
		//{
		//	Debug.Log("null ma");
		//}
	}

	private void InitPlayer2()
	{
		//if (MenuControll.GetInstance() != null)
		//{
		//Debug.Log(MenuControll.indexChara);
		Instantiate(prefabScript.player2s[MenuControll.player2IndexChara], posPlayer2.position, Quaternion.identity);
		//    }
		//}
	}
		//private void InitPlayer2()
		//{
		//	var index = SceneManager.GetActiveScene().name.Substring(5);
		//	//Debug.Log(index);
		//	var AI = int.Parse(index) - 1;
		//	GameObject aiPrefab = prefabScript.AIs[AI];
		//	Instantiate(aiPrefab, posAI.position, Quaternion.identity);
		//}

		private IEnumerator CountTimerStart()
		{
			txtTimerStart.text = "3";
			yield return new WaitForSeconds(1);
			txtTimerStart.text = "2";
			yield return new WaitForSeconds(1);
			txtTimerStart.text = "1";
			yield return new WaitForSeconds(1);
			txtTimerStart.text = "Fight!";
			yield return new WaitForSeconds(1);
			txtTimerStart.text = "";
			state = GameState.Playing;
		}

		//public void UpdateHealthy (ref int curHealthy, ref int damagedHealthy,ref int curPowerEnemy,int maxPower, int action)
		//{
		//	if(action == 1 && (curPowerEnemy + 10) <= maxPower){
		//		curPowerEnemy += 10;
		//		UpdatePowerEnemy(curPowerEnemy);
		//	}
		//	if (curHealthy >= damagedHealthy) {
		//		curHealthy = curHealthy - damagedHealthy;
		//		iTween.ScaleTo (sliderHealthy, new Vector3 (curHealthy * 2.6f / 100, sliderHealthy.transform.localScale.y, 0), 0f);
		//	} else {
		//		curHealthy = 0;
		//		iTween.ScaleTo (sliderHealthy, sliderHealthy.transform.localScale + new Vector3 (0, 0, 0), 0f);
		//	}
		//}
		public void UpdateHealthyPlayer1(ref int curHealthy, ref int healthy, ref int curpower, ref int power, int action)
		{
			if (curHealthy < 100)
			{
				_hp1.fillAmount = (float)curHealthy / healthy;
			}

			if (curpower < 100)
			{
				_anger1.fillAmount = (float)curpower / power;
			}
		}
		public void UpdateHealthyPlayer2(ref int curHealthy, ref int healthy, ref int curpower, ref int power, int action)
		{
			if (curHealthy < 100)
			{
				_hp2.fillAmount = (float)curHealthy / healthy;
			}

			if (curpower < 100)
			{
				_anger2.fillAmount = (float)curpower / power;
			}

		}

		//   public void UpdateHealthyEnemy (ref int curHealthyEnemy, ref int damagedHealthyEnemy,ref int curPower,int maxPower, int action)
		//{
		//	if(action == 1 && (curPower + 10) <= maxPower){
		//		curPower += 10;
		//		UpdatePower(curPower);
		//	}

		//	if (curHealthyEnemy >= damagedHealthyEnemy) {
		//		curHealthyEnemy = curHealthyEnemy - damagedHealthyEnemy;
		//		print ("currhealthy " + curHealthyEnemy);
		//		iTween.ScaleTo (sliderHealthyEnemy, new Vector3 (curHealthyEnemy * 2.6f / 100, sliderHealthyEnemy.transform.localScale.y, 0), 0f);
		//	} else {
		//		curHealthyEnemy = 0; // quái vật chết
		//		OnWinGame();
		//		iTween.ScaleTo (sliderHealthyEnemy, sliderHealthyEnemy.transform.localScale + new Vector3 (0, 0, 0), 0f);
		//	}
		//}

		//private void OnWinGame()
		//{
		//	BasePopupManager.Instance.ShowPopupWin();
		//	//Time.timeScale = 0;
		//}

		//public void UpdatePower (int curPower)
		//{
		//	iTween.ScaleTo (sliderPower, new Vector3 (curPower * 2f / 100, sliderPower.transform.localScale.y, 0), 0f);
		//}

		//public void UpdatePowerEnemy (int curPowerEnemy)
		//{
		//	iTween.ScaleTo (sliderPowerEnemy, new Vector3 (curPowerEnemy * 2f / 100, sliderPowerEnemy.transform.localScale.y, 0), 0f);
		//}
	}
