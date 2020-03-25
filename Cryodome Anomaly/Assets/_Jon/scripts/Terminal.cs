﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Terminal : MonoBehaviour
{
    public Keypad keypad;
    Text title;
    Text txt;

    int keypadCode;

    void Start(){
        title = transform.GetChild(0).GetChild(1).GetComponent<Text>();
        txt = transform.GetChild(0).GetChild(0).GetComponent<Text>();
        StartCoroutine(LateStart(2));
    }

    IEnumerator LateStart(float wait) {
        yield return new WaitForSeconds(wait);
        if (keypad != null) {
            keypadCode = keypad.GetCode();
            title.text = keypad.GetTitle();
            GenerateMathPuzzle(keypad.accessLevel);
        }
    }

    // Generates a moderately difficult math problem associated with a door's passcode.
    // Difficulty ranges from 0-4
    void GenerateMathPuzzle(int difficulty) {
        string final = " = ";
        string partialVal = "????";
        int[] codeArr = keypad.GetCodeArr();

        int divisor = 11;
        int multiple = 1;

        int adder1 = 0;
        int adder2 = 0;

        if (difficulty <= 1) {
            partialVal = "?" + codeArr[1] + codeArr[2] + codeArr[3];
        } else if (difficulty == 2) {
            partialVal = "??" + codeArr[2] + codeArr[3];
        } else if (difficulty == 3) {
            partialVal = "???" + codeArr[3];
        }

        // BC's favorite number
        if (keypadCode == 513) {
            txt.text = "Dr BC's favorite number: 0???";
            return;
        }

        int problemDecider;

        switch (difficulty)
        {
            // For easiest difficulty, always give an addition problem.
            case 0:
                adder1 = Random.Range(0, (int)keypadCode + 1);
                adder2 = keypadCode - adder1;
                // Example: looks like: 42 + 72 = ?114
                txt.text = adder1 + " + " + adder2 + final + partialVal;
                break;
            case 1:
                problemDecider = Random.Range(0, 2);
                if(keypadCode >= (1000) && problemDecider == 0)
                {
                    int modulator = 1;
                    divisor = modulator;
                    while (modulator < keypadCode / 6)
                    {
                        if (keypadCode % modulator == 0)
                            divisor = modulator;
                        modulator++;
                    }

                    multiple = keypadCode / divisor;
                    txt.text = divisor + " * " + multiple + final + partialVal;
                }
                else
                {
                    adder1 = Random.Range(0, (int)keypadCode + 1);
                    adder2 = keypadCode - adder1;
                    txt.text = adder1 + " + " + adder2 + final + partialVal;
                }
                break;
            case 2:
                problemDecider = Random.Range(0, 3);
                if (keypadCode >= (1000) && problemDecider == 0)
                {
                    int modulator = 1;
                    divisor = modulator;
                    while (modulator < keypadCode / 6)
                    {
                        if (keypadCode % modulator == 0)
                            divisor = modulator;
                        modulator++;
                    }

                    multiple = keypadCode / divisor;
                    txt.text = divisor + " * " + multiple + final + partialVal;
                } 
                else
                {
                    adder1 = Random.Range(0, (int)keypadCode + 1);
                    adder2 = keypadCode - adder1;
                    txt.text = adder1 + " + " + adder2 + final + partialVal;
                }
                break;
            case 3:     //Temporary place holder is addition problem
                adder1 = Random.Range(0, (int)keypadCode + 1);
                adder2 = keypadCode - adder1;
                txt.text = adder1 + " + " + adder2 + final + partialVal;
                break;
            case 4:     //Also placeholder of addition
                adder1 = Random.Range(0, (int)keypadCode + 1);
                adder2 = keypadCode - adder1;
                txt.text = adder1 + " + " + adder2 + final + partialVal;
                break;
            default:
                Debug.Log("Error in terminal math generation");
                break;
        }

        // Division problem, gives for example "14 * 621 = ?"
        // Tends to be kinda hard, easy doors should not have this problem.
    }
}
