using UnityEngine;
using System.Collections;
using DG.Tweening;

public class main_camera : MonoBehaviour {

    private Rigidbody2D rb;
    bool initiated= false;
    public bool moving = false;
    public bool falling = false;
    public bool pw_super_jump = false;
    public static main_camera s;
    public bool hitted_on_wall = false;

    void Awake (){ s = this; }
  
     // Use this for initialization
    void Start()
    {
        transform.position = new Vector3(0, 0, -10);
        rb = transform.GetComponent<Rigidbody2D>();

        // set the desired aspect ratio (the values in this example are
        // hard-coded for 16:9, but you could make them into public
        // variables instead so you can set them at design time)
        float targetaspect = 9.6f / 16.0f;

        // determine the game window's current aspect ratio
        float windowaspect = (float)Screen.width / (float)Screen.height;

        // current viewport height should be scaled by this amount
        float scaleheight = windowaspect / targetaspect;

        // obtain camera component so we can modify its viewport
        Camera camera = GetComponent<Camera>();

        // if scaled height is less than current height, add letterbox
        if (scaleheight < 1.0f)
        {
            Rect rect = camera.rect;

            rect.width = 1.0f;
            rect.height = scaleheight;
            rect.x = 0;
            rect.y = (1.0f - scaleheight) / 2.0f;

            camera.rect = rect;
        }
        else // add pillarbox
        {
            float scalewidth = 1.0f / scaleheight;

            Rect rect = camera.rect;

            rect.width = scalewidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scalewidth) / 2.0f;
            rect.y = 0;

            camera.rect = rect;
        }
    }


    public void OnBallFalling() {
        if (!falling) {
            Debug.Log("[CAMERA] ON BALL FALLING !! ");
            falling = true;
            transform.DOMoveY(transform.position.y - globals.s.FLOOR_HEIGHT-0.5f, 0.4f).SetEase(Ease.InOutQuad).OnComplete(() => falling = false);
        }
    }

    public void OnBallTooHigh() {
        if (!falling) {
            Debug.Log("[CAMERA] ON BALL TOO HIGH !! BALL Y: " + globals.s.BALL_Y + " | LIMIT: " + (transform.position.y + globals.s.FLOOR_HEIGHT + 1.5f));
            falling = true;
            transform.DOMoveY(transform.position.y + globals.s.FLOOR_HEIGHT + 0.5f, 0.4f).SetEase(Ease.InOutQuad).OnComplete(() => falling = false);
        }
        else Debug.Log("[CAMERA] IT IS FALLING! NOT BALL TO HIGH =/");
    }

    public void init_PW_super_jump(float pos_y, float time)
    {
        transform.DOMoveY(pos_y, time).SetEase(Ease.InOutSine);
        pw_super_jump = true;
    }
    public void PW_super_jump(float pos_y)
    {
        transform.position = new Vector3(transform.position.x, pos_y, transform.position.z);
    }

    public void pw_super_jump_end()
    {
        Debug.Log("[CAMERA] SUPER JUMP END !! ");
        pw_super_jump = false;
    }

    public void on_ball_up() {
        if (!moving && globals.s.BALL_Y > transform.position.y - globals.s.FLOOR_HEIGHT / 4)//Debug.Log("MY Y POS: " + transform.position.y);  if (globals.s.BALL_Y > transform.position.y)
        {
            rb.velocity = new Vector2(0, globals.s.CAMERA_SPEED);
            moving = true;
        }
    }

    // Update is called once per frame
    void Update() {
        
        //transform.position = new Vector3 (0, 0,0);
        if (pw_super_jump == false && globals.s.REVIVING == false && globals.s.GAME_STARTED == true)
        {
            if (globals.s.GAME_OVER == 0 && !falling && !hitted_on_wall) {
                if (initiated == false)
                {
                    if (globals.s.BALL_Y > transform.position.y) //if ball is in a superior position than the camera
                    {
                        rb.velocity = new Vector2(0, globals.s.CAMERA_SPEED);
                        initiated = true;
                        moving = true;
                    }
                }
                else
                {
                    // ball is to high
                    if (globals.s.BALL_GROUNDED && globals.s.BALL_Y > transform.position.y + globals.s.FLOOR_HEIGHT + 1.5f) { 
                        OnBallTooHigh();
                    }

                    // stop camera
                    else if (moving && globals.s.BALL_Y < transform.position.y - globals.s.FLOOR_HEIGHT)
                    {
                        rb.velocity = new Vector2(0, 0);
                        moving = false;
                    }


                    else if (globals.s.BALL_Y > transform.position.y - globals.s.FLOOR_HEIGHT / 4 && globals.s.BALL_GROUNDED == true)//Debug.Log("MY Y POS: " + transform.position.y);  if (globals.s.BALL_Y > transform.position.y)
                    {
                        rb.velocity = new Vector2(0, globals.s.CAMERA_SPEED);
                        moving = true;
                    }
                }
            }
            else
            {
                rb.velocity = new Vector2(0, 0);
                moving = false;
            }
        }
    }
}
