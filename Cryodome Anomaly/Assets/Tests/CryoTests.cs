﻿using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;

namespace Tests
{
    public class CryoTests
    {
        /** Damien's Tests
         * */
        [UnityTest]
        public IEnumerator PlayerFallCheck()
        {
            SetupScene();
            GameObject playerLoc = MonoBehaviour.Instantiate(Resources.Load<GameObject>("PlayerTesting"));
            float initPosY = playerLoc.transform.position.y;
            yield return new WaitForSecondsRealtime(8);
            if(playerLoc.transform.position.y != initPosY)
            {
                Debug.Log("The test failed");
                Assert.Fail();
            }
            else
            {
                Debug.Log("The test didn't fail");
            }
            yield return new WaitForSecondsRealtime(4);

            UnloadPrevScene();
            yield return null;
        }

        [UnityTest]
        public IEnumerator PlayerMapFallCheck()
        {
            SetupScene();
            float xSpeed = 2f;
            yield return new WaitForSecondsRealtime(12);
            GameObject playerLoc = MonoBehaviour.Instantiate(Resources.Load<GameObject>("PlayerTesting"));
            playerLoc.GetComponent<Rigidbody>().velocity = new Vector3(xSpeed, 0, 0);
            for (int i = 1; i < 10; i++)
            {
                yield return new WaitForSecondsRealtime(4);
                if(playerLoc.transform.position.y < 0)
                {
                    Debug.Log("The test failed");
                    Assert.Fail();
                    yield break;
                }
                else
                {
                    Debug.Log("Test hasn't failed yet");
                }

            }
         

            UnloadPrevScene();
            yield return null;
        }

        [UnityTest]
        public IEnumerator PlayerStressSpawn()
        {
            SetupScene();
            yield return new WaitForSeconds(5);
            var fps = 1 / Time.deltaTime;
            for (int i = 0; i < 5; i++)
            {
                fps = 1 / Time.deltaTime;
                for (int j = 0; j < 10; j++)
                {
                    MonoBehaviour.Instantiate(Resources.Load<GameObject>("PlayerTesting"));
                }
                Debug.Log(fps);
                yield return new WaitForSeconds(3);
                if (fps < 15)
                {
                    // if able to instantiate 50 object success
                    Debug.Log((i + 1) * 10);
                    if (i < 10)
                    {
                        Assert.Fail();
                        Debug.Log("Test Failed");
                    }
                    else
                    {
                        Debug.Log("Test has not yet failed.");
                    }
                    yield break;
                }
            }
            UnloadPrevScene();
            yield return null;
        }

        // **/

        /** Jon's Tests
          **/
        [UnityTest]
        public IEnumerator MonsterBoundsCheck()
        {
            // UnloadPrevScene();
            SetupScene();
            //MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestingCamera"));
            yield return new WaitForSeconds(5);

            GameObject tempMarker = MonoBehaviour.Instantiate(Resources.Load<GameObject>("MarkerTesting"));
            GameObject invalidMarker = MonoBehaviour.Instantiate(Resources.Load<GameObject>("MarkerTesting"));
            GameObject monster = MonoBehaviour.Instantiate(Resources.Load<GameObject>("MonsterSpeedTesting"));

            tempMarker.transform.position = new Vector3(-26, 1, -16);
            invalidMarker.transform.position = new Vector3(-26, -10, -16);
            invalidMarker.transform.localScale = new Vector3(100, 1, 100);

            yield return new WaitForSeconds(25);
            if (tempMarker != null || invalidMarker == null)
                Assert.Fail();

            yield return null;
        }

        [UnityTest]
        public IEnumerator MonsterSpawningStress() {
            // UnloadPrevScene();
            SetupScene();
            //MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestingCamera"));
            yield return new WaitForSeconds(5);
            var fps = 1 / Time.deltaTime;
            for (int i = 0; i < 5; i++) {
                fps = 1 / Time.deltaTime;
                for (int j = 0; j < 1; j++) {
                    MonoBehaviour.Instantiate(Resources.Load<GameObject>("MonsterTesting"));
                }
                Debug.Log(fps);
                yield return new WaitForSeconds(3);
                if (fps < 15) {
                    //if able to instantiate 1000 object success
                    Debug.Log((i + 1) * 100);
                    if (i < 10) {
                        Assert.Fail();
                    }
                    yield break;
                }
            }
            yield return null;
        }

        [UnityTest]
        public IEnumerator MonsterMarkerSpawningStress() {
            // UnloadPrevScene();
            SetupScene();
            //MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestingCamera"));
            var fps = 1 / Time.deltaTime;
            for (int i = 0; i < 10; i++) {
                fps = 1 / Time.deltaTime;
                for (int j = 0; j < 100; j++) {
                    MonoBehaviour.Instantiate(Resources.Load<GameObject>("MarkerTesting"));
                }
                Debug.Log(fps);
                yield return new WaitForSeconds(1);
                if (fps < 15) {
                    //if able to instantiate 1000 object success
                    Debug.Log((i + 1) * 100);
                    if (i < 10) {
                        Assert.Fail();
                    }
                    yield break;
                }
            }
            yield return null;
        }
        /*
        [UnityTest]
        public IEnumerator SpawnMassMarkers() {
            MonsterController monster = GameObject.Find("Monster").GetComponent<MonsterController>();
            for (int i = 0; i < 999; i++) {
                monster.AddTarget(new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10)));
            }
        }
        [UnityTest]
        void PlayNoises() {
            MonsterNoiseController monster = GameObject.Find("Monster").GetComponent<MonsterNoiseController>();
            for(int i = 0; i < 999; i++) {
                monster.commitDie();
                monster.locatedPlayer();
            }
        }

        // Goal: Spawn tons of markers directly on top of the monster. They should all immediately be deleted.
        // Pass: No markers remain after a set amount of time.
        // Fail: At least one marker is still in the scene.
        [UnityTest]
        void MassMarkerPlaceAtMe() {
            MonsterController monster = GameObject.Find("Monster").GetComponent<MonsterController>();
            int temp = monster.GetTargetCount();
            for (int i = 0; i < 999; i++) {
                monster.AddTarget(monster.gameObject.transform.position);
            }

            // Todo: incorporate Assert.Fail()
            if (temp != monster.GetTargetCount())
                return;
                // Assert.Fail();
        }
        */


        // **/

        /** Hunter's Tests
         **/
         //Test to determine how many batteries we can spawn before frame rate drops below 15FPS
        [UnityTest]
        public IEnumerator StressSpawn()
        {
           // UnloadPrevScene();
            SetupScene();
            //MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestingCamera"));
            var fps = 1 / Time.deltaTime;
            for (int i = 0; i < 5; i++)
            {
                fps = 1 / Time.deltaTime;
                for (int j = 0; j < 20; j++)
                {
                    MonoBehaviour.Instantiate(Resources.Load<GameObject>("FlashlightTesting"));
                }
                Debug.Log(fps);
                yield return new WaitForSeconds(1);
                if (fps < 15)
                {
                    //if able to instantiate 1000 object success
                    Debug.Log((i + 1) * 100);
                    if (i < 10)
                    {
                        Assert.Fail();
                    }
                    yield break;
                }
            }
            yield return null;
        }
        //This test has the same functionality as Damiens. Spawn a battery, add velocity to the battery and see if it will end up falling out of the map
        [UnityTest]
        public IEnumerator BatteryFallCheck()
        {
            SetupScene();
            float Vel = 2f;
            yield return new WaitForSecondsRealtime(12);
            GameObject BatteryLoc = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestBattery"));
            BatteryLoc.GetComponent<Rigidbody>().velocity = new Vector3(Vel, 0, 0);
            for (int i = 1; i < 10; i++)
            {
                yield return new WaitForSecondsRealtime(4);
                if (BatteryLoc.transform.position.y < 0)
                {
                    Debug.Log("Test Failed, battery fell");
                    Assert.Fail();
                    yield break;
                }
                else
                {
                    Debug.Log("Test Not Failing");
                }
            }
            yield return null;
        }
        //Spawn flashlight, make sure it does not fly out of map
        [UnityTest]
        public IEnumerator FlashlightFallCheck()
        {
            SetupScene();
            float Vel = 2f;
            yield return new WaitForSecondsRealtime(12);
            GameObject BatteryLoc = MonoBehaviour.Instantiate(Resources.Load<GameObject>("FlashlightTesting"));
            BatteryLoc.GetComponent<Rigidbody>().velocity = new Vector3(Vel, 0, 0);
            for (int i = 1; i < 10; i++)
            {
                yield return new WaitForSecondsRealtime(4);
                if (BatteryLoc.transform.position.y < 0)
                {
                    Debug.Log("Test Failed, Flashlight fell");
                    Assert.Fail();
                    yield break;
                }
                else
                {
                    Debug.Log("Test Not Failing");
                }
            }
            yield return null;
        }






        // **/

        /** Tobias's Tests
         **/
        [UnityTest]
        public IEnumerator PlayerPrefTest()
        {
            SceneManager.LoadScene("MainMenu");  //Load MainMenu Scene for Test

            // Use the Assert class to test conditions.
            // Use yield to skip a frame.

            Canvas Canvas = GameObject.FindObjectOfType<Canvas>();   //
            
            
            yield return null;
            

        }

        [UnityTest]
        public IEnumerator SceneTransitionTest()
        {
            SceneManager.LoadScene("MainMenu");  //Load MainMenu Scene for Test

            // Use the Assert class to test conditions.
            // Use yield to skip a frame.

            //FIND THE BUTTON TO GET TO NEXT SCENE
            Canvas canvas = GameObject.FindObjectOfType<Canvas>();   //Reference the Canvas
            Transform Main_Menu_Position = canvas.transform.Find("Main_Menu");   //Find Main Menu
            Transform Normal_Mode_Position = Main_Menu_Position.Find("Normal_Mode");   //Find Normal Mode Button
            Transform Choose_Input_Position = canvas.transform.Find("Choose Input");   //Find Choose Input Menu
            Transform Keyboard_Position = Choose_Input_Position.Find("Keyboard/Mouse");   //Find Keyboard/Mouse Button

            //TRANSITION TO NORMAL MODE --> KEYBOARD/MOUSE
            Normal_Mode_Position.GetComponent<Button>().onClick.Invoke();
            Keyboard_Position.GetComponent<Button>().onClick.Invoke();

            yield return new WaitForSeconds(30);   //Wait 30 seconds for new scene to load

            if(SceneManager.GetActiveScene().name != "main")
            {
                Debug.Log("Test Failed.  Proper Scene Was Not Loaded.");
                Assert.Fail();
            }
            else
            {
                Debug.Log("Test Passed.  Scene Loaded.");
            }

            yield return null;

        }

        // **/

        /** Christian's Tests
         **/
        [UnityTest]
        public IEnumerator RandomNumGeneratorTest()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            int[] passcode = new int[4];
            for (int i = 0; i < 4; i ++) {
                passcode[i] = -1;
            }
            for (int i = 0; i < 4; i ++) {
                passcode[i] = UnityEngine.Random.Range(0, 9);
            }
            for (int i = 0; i < 4; i ++) {
                if (passcode[i] < 0 || passcode[i] > 9) {
                    Assert.Fail();
                    yield break;
                }
            }
            yield return null;
        }

        // **/

        // External Functions(Comment your portions if you try to use this like the above Tests
        public void SetupScene()
        {
            SceneManager.LoadScene("main");
        }

        public void UnloadPrevScene()
        {
            SceneManager.LoadScene("blank");
        }

    }
}
