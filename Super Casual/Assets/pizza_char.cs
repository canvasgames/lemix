using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class pizza_char : MonoBehaviour
{
	#region ===== Variables Declaration =====
    [HideInInspector]
    public int number_of_catastrophes;

    public GameObject part1_obj, part2_obj, part3_obj, part4_obj, part5_obj, part6_obj, part7_obj, part8_obj;
    public GameObject arrow, button_spin;
    float part1_pct, part2_pct, part3_pct, part4_pct, part5_pct, part6_pct, part7_pct, part8_pct;
    float part1_reward, part2_reward, part3_reward, part4_reward, part5_reward, part6_reward, part7_reward, part8_reward;
    float previousYInput, initialTime;

    [HideInInspector]public  bool openingTampaDoTeuCu = true;
	#endregion
    // Use this for initialization
    void Start()
    {
        define_percentages(12.5f, 12.5f, 12.5f, 12.5f, 12.5f, 12.5f, 12.5f, 12.5f);
        define_rewards(20, 20, 20, 20, 20, 20, 20, 20);
    }

    // Update is called once per frame
    void Update()
    {
        
    }







    public void define_percentages(float part1pct, float part2pct, float part3pct, float part4pct, float part5pct, float part6pct, float part7pct, float part8pct)
    {
        part1_pct = part1pct;
        part2_pct = part2pct;
        part3_pct = part3pct;
        part4_pct = part4pct;
        part5_pct = part5pct;
        part6_pct = part6pct;
        part7_pct = part7pct;
        part8_pct = part8pct;

        if (part3_pct <= 0)
        {
            part3_obj.SetActive(false);
        }
        if (part4_pct <= 0)
        {
            part4_obj.SetActive(false);
        }
        if (part5_pct <= 0)
        {
            part5_obj.SetActive(false);
        }
        if (part6_pct <= 0)
        {
            part6_obj.SetActive(false);
        }
        if (part7_pct <= 0)
        {
            part7_obj.SetActive(false);
        }
        if (part8_pct <= 0)
        {
            part8_obj.SetActive(false);
        }

        part1_obj.GetComponent<Image>().fillAmount = part1_pct * 0.01f;

        part2_obj.GetComponent<Image>().fillAmount = part2_pct * 0.01f;
        part2_obj.transform.DORotate(new Vector3(part2_obj.GetComponent<RectTransform>().rotation.x,
        part2_obj.GetComponent<RectTransform>().rotation.y, (-360 * 0.01f * part1_pct)), 0);


        part3_obj.GetComponent<Image>().fillAmount = part3_pct * 0.01f;
        part3_obj.transform.DORotate(new Vector3(part3_obj.GetComponent<RectTransform>().rotation.x,
        part3_obj.GetComponent<RectTransform>().rotation.y, (-360 * 0.01f * (part1_pct + part2_pct))), 0);


        part4_obj.GetComponent<Image>().fillAmount = part4_pct * 0.01f;
        part4_obj.transform.DORotate(new Vector3(part4_obj.GetComponent<RectTransform>().rotation.x,
        part4_obj.GetComponent<RectTransform>().rotation.y, (-360 * 0.01f * (part1_pct + part2_pct + part3_pct))), 0);



        part5_obj.GetComponent<Image>().fillAmount = part5_pct * 0.01f;
        part5_obj.transform.DORotate(new Vector3(part5_obj.GetComponent<RectTransform>().rotation.x,
        part5_obj.GetComponent<RectTransform>().rotation.y, (-360 * 0.01f * (part1_pct + part2_pct + part3_pct + part4_pct))), 0);

        part6_obj.GetComponent<Image>().fillAmount = part6_pct * 0.01f;
        part6_obj.transform.DORotate(new Vector3(part6_obj.GetComponent<RectTransform>().rotation.x,
        part6_obj.GetComponent<RectTransform>().rotation.y, (-360 * 0.01f * (part1_pct + part2_pct + part3_pct + part4_pct + part5_pct))), 0);

        part7_obj.GetComponent<Image>().fillAmount = part7_pct * 0.01f;
        part7_obj.transform.DORotate(new Vector3(part7_obj.GetComponent<RectTransform>().rotation.x,
        part7_obj.GetComponent<RectTransform>().rotation.y, (-360 * 0.01f * (part1_pct + part2_pct + part3_pct + part4_pct + part5_pct + part6_pct))), 0);

        part8_obj.GetComponent<Image>().fillAmount = part8_pct * 0.01f;
        part8_obj.transform.DORotate(new Vector3(part8_obj.GetComponent<RectTransform>().rotation.x,
        part8_obj.GetComponent<RectTransform>().rotation.y, (-360 * 0.01f * (part1_pct + part2_pct + part3_pct + part4_pct + part5_pct + part6_pct + part7_pct))), 0);
    }

    void define_rewards(float part1, float part2, float part3, float part4, float part5, float part6, float part7, float part8)
    {
        part1_reward = part1; part2_reward = part2; part3_reward = part3; part4_reward = part4;
        part5_reward = part5; part6_reward = part6; part7_reward = part7; part8_reward = part8;
    }

    public void initClick()
    {
        initialTime = Time.time;
        previousYInput = Input.mousePosition.y;
    }
    public void endClick()
    {
        if (openingTampaDoTeuCu == false)
        {
            rotate(Time.time - initialTime, Vector2.Distance(new Vector2(Input.mousePosition.y, 0), new Vector2(previousYInput, 0)), Input.mousePosition.x, Input.mousePosition.y, previousYInput);

        }
    }

    public void rotate(float time, float distance, float inputX, float inputY, float lastY)
    {
        float angle;
        Debug.Log(inputX);
        //Debug.Log(time + " Tempooooo");
        //Debug.Log(distance + " Distancia");
        if(distance > 35 && time < 0.7f && time > 0.1f)
        {
            if(inputX < 110 && inputY > lastY || inputX >= 110 && inputY < lastY)
            {
                //Debug.Log("gira horario");
                angle = Random.Range(-1, -360);

            }
            else
            {
                angle = Random.Range(1, 360);
                //Debug.Log("gira anti-horario");
            }
            float clampdistance = Mathf.Clamp(distance, 35, 300);
            float clampTime = Mathf.Clamp(distance, 0.1f, 0.6f);
            float force = clampdistance / clampTime;
            angle = angle * (force);

            Debug.Log(force + " " + angle);
            transform.DORotate(new Vector3(0, 0, angle), Random.Range(2,3.5f),RotateMode.WorldAxisAdd).SetEase(Ease.OutQuart).OnComplete(give_reward);
            hud_controller.si.addRoulleteTime();
        }

    }

    void give_reward()
    {
        //Debug.Log("angle " + transform.rotation.eulerAngles.z);

        float angle_temp = transform.GetComponent<RectTransform>().eulerAngles.z;
        if (angle_temp >= 0 && angle_temp <= (360 * part1_pct * 0.01f))
        {
             Debug.Log("Caiu no 1");
            hud_controller.si.add_pw_time(part1_reward);

        }
        else if (angle_temp <= ((360 * (part1_pct + part2_pct) * 0.01f)))
        {
            hud_controller.si.add_pw_time(part2_reward);
            Debug.Log("Caiu no 2");
        }
        else if (angle_temp <= ((360 * (part1_pct + part2_pct + part3_pct) * 0.01f)))
        {
            hud_controller.si.add_pw_time(part3_reward);
            Debug.Log("Caiu no 3");
        }
        else if (angle_temp <= ((360 * (part1_pct + part2_pct + part3_pct + part4_pct) * 0.01f)))
        {
            hud_controller.si.add_pw_time(part4_reward);
             Debug.Log("Caiu no 4");
        }
        else if (angle_temp <= ((360 * (part1_pct + part2_pct + part3_pct + part4_pct + part5_pct) * 0.01f)))
        {
            hud_controller.si.add_pw_time(part5_reward);
            Debug.Log("Caiu no 5");
        }
        else if (angle_temp <= ((360 * (part1_pct + part2_pct + part3_pct + part4_pct + part5_pct + part6_pct) * 0.01f)))
        {
            hud_controller.si.add_pw_time(part6_reward);
            Debug.Log("Caiu no 6");
        }
        else if (angle_temp <= ((360 * (part1_pct + part2_pct + part3_pct + part4_pct + part5_pct + part6_pct + part7_pct) * 0.01f)))
        {
            hud_controller.si.add_pw_time(part7_reward);
            Debug.Log("Caiu no 7");
        }
        else if (angle_temp <= ((360 * (part1_pct + part2_pct + part3_pct + part4_pct + part5_pct + part6_pct + part7_pct + part8_pct) * 0.01f)))
        {
            hud_controller.si.add_pw_time(part8_reward);
            Debug.Log("Caiu no 8");
        }
    }


}
