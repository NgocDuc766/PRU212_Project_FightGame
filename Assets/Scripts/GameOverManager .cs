﻿using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public GameObject KOObject;        // Gán đối tượng K.O. trong Inspector
    [Header("Winner Left")]
    public GameObject winnerLeft;      // Gán đối tượng Winner bên trái trong Inspector
    [Header("Winner Right")]
    public GameObject winnerRight;     // Gán đối tượng Winner bên phải trong Inspector

    private bool gameIsOver = false;

    private static GameOverManager instance;
    private static readonly object instanceLock = new object();

    public static GameOverManager GetInstance()
    {
        return instance;
    }

    void Awake()
    {
        // Đảm bảo cả ba đối tượng đều bị tắt khi game bắt đầu
        KOObject.SetActive(false);
        winnerLeft.SetActive(false);
        winnerRight.SetActive(false);
        //
        lock (instanceLock)
        {
            if (instance == null)
            {
                instance = this;
            }
        }
    }
    // Phương thức xử lý logic game over khi một người chơi chết
    public void HandleGameOver(bool isPlayer1Dead, bool isPlayer2Dead)
    {
        if (gameIsOver) return;  // Ngăn chặn việc kích hoạt game-over nhiều lần

        gameIsOver = true;

        // Hiển thị đối tượng K.O.
        KOObject.SetActive(true);

        // Hiển thị Winner ở vị trí phù hợp
        if (!isPlayer1Dead && isPlayer2Dead)
        {
            winnerLeft.SetActive(true); // Player 2 thắng, hiển thị bên phải
        }
        if(isPlayer1Dead && !isPlayer2Dead)
        {
            winnerRight.SetActive(true);  // Player 1 thắng, hiển thị bên trái
        }
    }
}