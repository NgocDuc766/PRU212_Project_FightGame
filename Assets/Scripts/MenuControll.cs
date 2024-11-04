using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuControll : MonoBehaviour
{
    public static int player1IndexChara { get; set; }
    public static int player2IndexChara { get; set; }
    private static MenuControll instance;

    private bool isPlayer1Selecting = true; // Trạng thái xem người chơi nào đang chọn
    private bool player1Confirmed = false;  // Kiểm tra nếu người chơi 1 đã xác nhận

    public static MenuControll GetInstance()
    {
        
        return instance;
    }

    private MenuControll()
    {
        if (instance == null)
        {
            instance = this;
        }
    } 


    [Header("Mũi tên lựa chọn")]
    public GameObject selectObj;

    [Header("Biểu tượng nhân vật cho Player 1")]
    public GameObject[] iconChar;

    [Header("Biểu tượng nhân vật cho Player 2")]
    public GameObject[] iconCharPlayer2;

    public string player1CharacterName { get; private set; }
    public string player2CharacterName { get; private set; }

    void Start()
    {
        Debug.Log("Menu co start");
        player1IndexChara = 0;
        player2IndexChara = 0;

        if(GetInstance() != null)
        {
            Debug.Log("a");
        }
        else
        {
            Debug.Log("b");

        }

        // Ẩn tất cả biểu tượng của cả hai người chơi
        ResetIcons(iconChar);
        ResetIcons(iconCharPlayer2);

        // Chỉ hiển thị biểu tượng ban đầu của Player 1
        iconChar[0].SetActive(true);
        DontDestroyOnLoad(this);
    }

    void Update()
    {
        // Chuyển sang chọn nhân vật cho Player 2 nếu nhấn "J" và Player 1 đã xác nhận
        if (Input.GetKeyDown(KeyCode.J) && !player1Confirmed)
        {
            player1Confirmed = true;
            isPlayer1Selecting = false;

            // Hiển thị icon đã chọn của Player 1
            iconChar[player1IndexChara].SetActive(true);

            // Chuyển sang hiển thị biểu tượng của Player 2
            ResetIcons(iconCharPlayer2); // Đảm bảo tất cả icon của Player 2 bị tắt trước khi bật
            iconCharPlayer2[player2IndexChara].SetActive(true); // Bật icon cho Player 2
        }
    }

    public void SelectCharacter()
    {
        var btnChar = EventSystem.current.currentSelectedGameObject;
        if (btnChar != null)
        {
            selectObj.transform.position = btnChar.transform.position + new Vector3(0, 1, 0);

            if (isPlayer1Selecting)
            {
                player1CharacterName = btnChar.name;
                SetActiveIcon(player1IndexChara, btnChar.name, iconChar); // Lưu icon của Player 1
            }
            else
            {
                player2CharacterName = btnChar.name;
                SetActiveIcon(player2IndexChara, btnChar.name, iconCharPlayer2); // Lưu icon của Player 2
            }
        }
    }

    private void SetActiveIcon(int playerIndex, string characterName, GameObject[] iconArray)
    {
        // Tắt tất cả các icon trong mảng trước khi bật icon đã chọn
        ResetIcons(iconArray);

        switch (characterName)
        {
            case "Fire":
                playerIndex = 0;
                iconArray[0].SetActive(true);
                break;
            case "Water":
                playerIndex = 1;
                iconArray[1].SetActive(true);
                break;
            case "Metal":
                playerIndex = 2;
                iconArray[2].SetActive(true);
                break;
            case "Goku":
                playerIndex = 3;
                iconArray[3].SetActive(true);
                break;
            case "Gotenks":
                playerIndex = 4;
                iconArray[4].SetActive(true);
                break;
            case "FatBuu":
                playerIndex = 5;
                iconArray[5].SetActive(true);
                break;
        }

        // Lưu chỉ số nhân vật đã chọn cho từng người chơi
        if (isPlayer1Selecting)
        {
            player1IndexChara = playerIndex;
            Debug.Log(player1IndexChara);

        }
        else
        {
            player2IndexChara = playerIndex;
            Debug.Log(player2IndexChara);
        }

    }

    private void ResetIcons(GameObject[] iconArray)
    {
        // Tắt tất cả các biểu tượng nhân vật trong mảng đã chọn
        for (int i = 0; i < iconArray.Length; i++)
        {
            iconArray[i].SetActive(false);
        }
    }

    //public GameObject bgLoading;
    //public GameObject canvas;

    //private IEnumerator Loading()
    //{
    //    canvas.SetActive(false);
    //    bgLoading.gameObject.SetActive(true);
    //    for (float i = 0; i < 1f; i += 0.3f)
    //    {
    //        var colorTemp = bgLoading.GetComponent<SpriteRenderer>().color;
    //        colorTemp.a = i;
    //        bgLoading.GetComponent<SpriteRenderer>().color = colorTemp;
    //        yield return new WaitForSeconds(0.1f);
    //    }
    //}

    public void ClickBtnStart()
    {
        if (player1Confirmed && !isPlayer1Selecting) // Kiểm tra nếu cả hai người chơi đã chọn xong
        {
            // Lưu thông tin nhân vật đã chọn cho từng người chơi
            PlayerPrefs.SetInt("Player1Character", player1IndexChara);
            PlayerPrefs.SetInt("Player2Character", player2IndexChara);

            //canvas.SetActive(false);
            //bgLoading.SetActive(false);
            SceneManager.LoadSceneAsync(1); // Chuyển đến scene mới
        }
    }
}
