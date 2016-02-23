using UnityEngine;
using System.Collections;

public enum PW_Types
{
    Invencible = 1, Super = 2, Sight = 3
}

public class PW_Collect : MonoBehaviour {

    public int pw_type;
    int rand;
    // Use this for initialization
    void Start () {

        if (globals.s.PW_INVENCIBLE == false && globals.s.PW_SIGHT_BEYOND_SIGHT == false && globals.s.PW_SUPER_JUMP == false)
        {
            rand = Random.Range((int)PW_Types.Invencible, (int)PW_Types.Sight + 1);
        }
        else
        {
            if(globals.s.PW_INVENCIBLE == false)
            {
                rand = (int)PW_Types.Invencible;
            }
            else if (globals.s.PW_SIGHT_BEYOND_SIGHT == false)
            {
                rand = (int)PW_Types.Sight;
            }
            else if (globals.s.PW_SUPER_JUMP == false)
            {
                rand = (int)PW_Types.Super;
            }
        }
            
      // rand = (int)PW_Types.Invencible;
      pw_type = rand;
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    public void collect()
    {
        Destroy(transform.gameObject);
    }

}
