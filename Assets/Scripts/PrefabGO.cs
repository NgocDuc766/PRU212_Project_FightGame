using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabGO : MonoBehaviour
{
	
	public GameObject particalDamaged;
	[Header ("char player1")]
	public GameObject[] player1s;
	[Header ("char player2")]
	public GameObject[] player2s;
	[Header ("bullet")]
	public GameObject bulletGoku, bulletBuu, bulletFatBuu, bulletSuperGoku, bulletGotenk;

	private static PrefabGO instance;

	public static PrefabGO GetInstance ()
	{
		return instance;
	}

	private PrefabGO ()
	{
		if (instance == null) {
			instance = this;
		}
	}
}
