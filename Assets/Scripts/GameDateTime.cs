using System;
using UnityEngine;
using TMPro;

public class GameDateTime : MonoBehaviour
{
    public static GameDateTime instance; // Singleton instance

    public int startingYear = 1320;
    public int startingMonth = 1;
    public int startingDay = 1;
    public int startingHour = 0;
    public int startingMinute = 0;

    private int currentYear;
    private int currentMonth;
    private int currentDay;
    private int currentHour;
    private int currentMinute;

    public Action<int, int, int, int, int> onDateTimeChanged; // Event for when the date and time change

    public TMP_Text dateTimeText; // Reference to the TextMeshPro component

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        InitializeDateTime();
    }

    private void InitializeDateTime()
    {
        currentYear = startingYear;
        currentMonth = startingMonth;
        currentDay = startingDay;
        currentHour = startingHour;
        currentMinute = startingMinute;
    }

    private void Update()
    {
        // Update the date and time in your game (e.g., based on real time or game time)
        // You can modify this logic according to your game's needs
        currentMinute++;

        if (currentMinute >= 60)
        {
            currentMinute = 0;
            currentHour++;

            if (currentHour >= 24)
            {
                currentHour = 0;
                currentDay++;

                if (currentDay > DateTime.DaysInMonth(currentYear, currentMonth))
                {
                    currentDay = 1;
                    currentMonth++;

                    if (currentMonth > 12)
                    {
                        currentMonth = 1;
                        currentYear++;
                    }
                }
            }
        }

        // Update the date and time text with the current date and time
        dateTimeText.text = $"{currentDay:D2}/{currentMonth:D2}/{currentYear:D4} {currentHour:D2}:{currentMinute:D2}";

        // Invoke the event when the date and time change
        onDateTimeChanged?.Invoke(currentYear, currentMonth, currentDay, currentHour, currentMinute);
    }
}
