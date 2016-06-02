using UnityEngine;
using System.Collections;


/*this event manager consists of all delegates and is static so you can acces it from any class.
don't put this class into a object as it doesn't inherit monobehaviour.*/
public class EventManager {

    public delegate void TimesUpHandeler();
    public static event TimesUpHandeler OnTimesUp;

    //nullifies all delegates making them empty. Ideal when you want to reassign delegates.
    public static void Nullify()
    {
        OnTimesUp = null;
    }

    //when the time is up of a match.
    public static void TimeIsUp()
    {
        if (OnTimesUp != null)
        {
            OnTimesUp();
        }
    }
}
