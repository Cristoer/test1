using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    float _verticalSpeed;
    float _horizontallSpeed;
    [SerializeField] GameConfig gameConfig;
    [SerializeField] GameController gameController;
    [SerializeField] float[] borders;

    private void Start()
    {
        //Vector3 screen = Camera.main.WorldToScreenPoint(point.position);
        //Vector3 newPos = Camera.main.ScreenToWorldPoint(new Vector3(screen.x - 100, screen.y, screen.z));
        //horizontallSpeed = (point.position - newPos).x / 100;

        //horizontallSpeed = 10f / Screen.width;
        _horizontallSpeed = gameConfig.playerConfig.horizontalSpeed;
        _verticalSpeed = gameConfig.playerConfig.verticalSpeed;
    }
    float lastX;
    void Update()
    {
        if (gameController.gameState != GameState.Gaming) return;
        if (Input.GetMouseButtonDown(0)) {
            lastX = Input.mousePosition.x;
        }
        float moveX = 0;
        if (Input.GetMouseButton(0)) {
            float nowX = Input.mousePosition.x;
            moveX = nowX - lastX;
            lastX = nowX;
        }
        Vector3 position = transform.position;
        position.x += moveX* _horizontallSpeed;
        if (position.x < borders[0]) position.x = borders[0];
        if (position.x > borders[1]) position.x = borders[1];
        position.z += _verticalSpeed * Time.deltaTime;
        transform.localPosition = position;
    } 
}
