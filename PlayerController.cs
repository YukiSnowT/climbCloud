using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigid2D;
    Animator animator;
    float jumpForce = 680.0f;
    float walkForce = 30.0f;
    float maxWalkSpeed = 2.0f;
    float threshold = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        this.rigid2D = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //ジャンプ
        // if(Input.GetKeyDown(KeyCode.Space) && this.rigid2D.velocity.y == 0){
        if(Input.GetMouseButtonDown(0) && this.rigid2D.velocity.y == 0){
            this.animator.SetTrigger("JumpTrigger");
            this.rigid2D.AddForce(transform.up * this.jumpForce);
        }

        //左右移動
        int key = 0;
        // if(Input.GetKey(KeyCode.RightArrow)) key = 1;
        // if(Input.GetKey(KeyCode.LeftArrow)) key = -1;
        if(Input.acceleration.x > this.threshold) key = 1;
        if(Input.acceleration.x < -this.threshold) key = -1;

        //速度
        float speedx = Mathf.Abs(this.rigid2D.velocity.x);

        //スピード制限
        if(speedx < this.maxWalkSpeed){
            this.rigid2D.AddForce(transform.right * key * this.walkForce);
        }

        //反転
        if(key != 0){
            transform.localScale = new Vector3(key,1,1);
        }

        //アニメ速度変更
        if(this.rigid2D.velocity.y == 0){
            this.animator.speed = speedx / 2.0f;
        }else{
            this.animator.speed = 1.0f;
        }

        //画面外でリスタート
        if(transform.position.y < -6){
            SceneManager.LoadScene("GameScene");
        }
    }

    //ゴール
    void OnTriggerEnter2D(Collider2D other){
        SceneManager.LoadScene("ClearScene");
        // Debug.Log("ゴール");
    }
}
