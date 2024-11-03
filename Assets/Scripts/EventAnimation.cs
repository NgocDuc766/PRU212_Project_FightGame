using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventAnimation : MonoBehaviour
{


	public void ScaleSkill (GameObject obj)
	{
		iTween.PunchScale (obj, obj.transform.localScale + new Vector3 (7, 0, 0), 2f);
	}


	public void AddJumpForce (float speed)
	{
		Player1Controll player = GetComponent<Player1Controll> ();
		if (player != null)
			player.gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, speed);
	}

	public void CallBullet ()
	{
		Player1Controll player1 = GetComponent<Player1Controll> ();
		Player2Controll player2 = GetComponent<Player2Controll> ();
		if (player1 != null) {
			CallBulletPlayer1 (player1);
		}
		if (player2 != null) {
			CallBulletPlayer2 (player2);
		}
	}

	//public void CanUsingSkillSpecial()
	//{
	//	Player2Controll AI = GetComponent<Player2Controll>();
	//	if (AI != null)
	//		AI.isUsingSkillS = false;
	//}

	public void CallBulletPlayer1(Player1Controll player1)
	{
		
		Transform pos = player1.transform.Find ("posCallBullet");
		string nameBullet = player1.gameObject.name;
		GameObject temp = SelectBullet (nameBullet);
		GameObject bullet = Instantiate (temp, pos.position, Quaternion.identity) as GameObject;
		bullet.tag = "bulletPlayer";
		Vector3 direc = player1.transform.localScale.x > 0 ? new Vector3 (20, 0, 0) : new Vector3 (-20, 0, 0);
		iTween.MoveBy (bullet, direc, 2f);
		Destroy (bullet, 1);

	}

	public void CallBulletPlayer2(Player2Controll player2)
	{
		Transform pos = player2.transform.Find("posCallBullet");
		string nameBullet = player2.gameObject.name;
		GameObject temp = SelectBullet(nameBullet);
		GameObject bullet = Instantiate(temp, pos.position, Quaternion.identity) as GameObject;
		bullet.tag = "bulletEnemy";
		Vector3 direc = player2.transform.localScale.x > 0 ? new Vector3(20, 0, 0) : new Vector3(-20, 0, 0);
		iTween.MoveBy(bullet, direc, 2f);
		Destroy(bullet, 1);
	}

	private GameObject SelectBullet (string nameBullet)
	{
		PrefabGO prefabs = PrefabGO.GetInstance ();
		switch (nameBullet) {
		case "Songoku":
			return prefabs.bulletGoku;
			break;
		case "Buu":
			return prefabs.bulletBuu;
			break;
		case "FatBuu":
			return prefabs.bulletFatBuu;
			break;
		case "Gotenk":
			return prefabs.bulletGotenk;
			break;
		case "SuperSongoku":
			return prefabs.bulletSuperGoku;
			break;
		}
		return prefabs.bulletGoku;
	}

}
