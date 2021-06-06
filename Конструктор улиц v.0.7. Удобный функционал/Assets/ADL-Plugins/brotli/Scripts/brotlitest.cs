﻿using System;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if !UNITY_WEBGL || UNITY_EDITOR
using System.Threading;
using System.IO;
#endif


public class brotlitest : MonoBehaviour {
#if (!UNITY_WEBPLAYER && !UNITY_WEBGL) || UNITY_EDITOR

    //some variables to get status returns from the functions
    private int  lz1, lz2, lz3, lz4, fbuftest;

	//a single item ulong array to get the progress of the compression
    private ulong[] progress = new ulong[1];
	//a single item int array to get the progress of the decompression
	private ulong[] progress2 = new ulong[1];
	//a single item int array to get the progress of the decompression
	private ulong[] progress3 = new ulong[1];
		
    //a test file that will be downloaded to run the tests
    private string myFile = "testLZ4.tif";

    //the adress from where we download our test file
    private string uri = "https://dl.dropbox.com/s/r1ccmnreyd460vr/";

    //our path where we do the tests
    private string ppath;

    private bool compressionStarted;
    private bool downloadDone, downloadError;

    //a reusable buffer
    private byte[] buff;

    //buffer operations buffers
    byte[] bt = null, bt2 = null;

    //fixed size buffer, that don't gets resized, to perform decompression of buffers in them and avoid memory allocations.
    private byte[] fixedOutBuffer = new byte[1024*768*3];

    // Use this for initialization
    void Start () {
		ppath = Application.persistentDataPath;

        #if UNITY_STANDALONE_OSX && !UNITY_EDITOR
			ppath=".";
        #endif

        buff = new byte[0];

        Debug.Log(ppath);

        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        #if !UNITY_TVOS || UNITY_EDITOR
        if (!File.Exists(ppath + "/" + myFile))
        #endif
        StartCoroutine(DownloadTestFile());
        #if !UNITY_TVOS || UNITY_EDITOR
        else downloadDone = true;
        #endif
    }


	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape)) { Application.Quit(); }
    }


    void OnGUI() {

        if (downloadDone) {
            GUI.Label(new Rect(50, 0, 350, 30), "package downloaded, ready to extract");
            GUI.Label(new Rect(50, 30, 450, 90), ppath);

            if (GUI.Button(new Rect(50, 150, 250, 50), "start brotli test")) {
                compressionStarted = true;
				lz1 = 0; lz2 = 0; progress[0] = 0; progress2[0] = 0; progress3[0] = 0;
                //call the decompresion demo functions.
                // DoTests();
                //we call the test function on a thread to able to see progress. WebGL does not support threads.

				Thread th = new Thread(DoTests); th.Start();

            }
        } else {
            if(downloadError) GUI.Label(new Rect(50, 150, 250, 50), "Download Error!");
        }

        if (compressionStarted) {
            #if !UNITY_TVOS || UNITY_EDITOR
                //if the return code is 1 then the decompression was succesful.
                GUI.Label(new Rect(50, 220, 250, 40), "brotli Compress:    " + lz1.ToString() + "  : " + progress[0].ToString());

                GUI.Label(new Rect(50, 260, 250, 40), "brotli Decompress: " + lz2.ToString() + "  : " + progress2[0].ToString());
            #endif
            GUI.Label(new Rect(50, 300, 250, 40), "Buffer Compress:    " + lz3.ToString() + "  : " + progress3[0].ToString());

            GUI.Label(new Rect(50, 340, 250, 40), "Buffer Decompress: " + lz4.ToString());

			#if (UNITY_EDITOR || UNITY_IPHONE || UNITY_IOS || UNITY_STANDALONE_OSX || UNITY_ANDROID || UNITY_STANDALONE_LINUX) && !UNITY_EDITOR_WIN
				GUI.Label(new Rect(50, 380, 250, 40), "FileBuffer test: " + fbuftest.ToString() + "  : " + progress3[0].ToString() );
			#endif
        }

     }


    void DoTests() {
            #if !UNITY_TVOS || UNITY_EDITOR
                //File tests
                //compress a file to brotli format.
                lz1 = brotli.compressFile(ppath+ "/" + myFile, ppath + "/" + myFile + ".br", progress);

                //decompress the previously compressed archive
                lz2 = brotli.decompressFile(ppath + "/" + myFile + ".br", ppath + "/" + myFile + ".Br.tif",  progress2);
        
                //Buffer tests
                if (File.Exists(ppath + "/" + myFile)) {
                    bt = File.ReadAllBytes(ppath + "/" + myFile);

                    //compress a byte buffer (we write the output buffer to a file for debug purposes.)
                    if (brotli.compressBuffer(bt, ref buff,  progress3, true)){
                        lz3 = 1;
                        File.WriteAllBytes(ppath + "/buffer1.brbuf", buff);
                    }

                    bt2 = File.ReadAllBytes(ppath + "/buffer1.brbuf");
            
                    //decompress a byte buffer (we write the output buffer to a file for debug purposes.)
                    if (brotli.decompressBuffer(bt2, ref buff, true)){
                        lz4 = 1;
                        File.WriteAllBytes(ppath + "/buffer1.tif", buff);
                    }

                    //FIXED BUFFER FUNCTION:
                    int decompressedSize = brotli.decompressBuffer(bt2, fixedOutBuffer, true);
                    if (decompressedSize > 0) Debug.Log(" # Decompress Fixed size Buffer: " + decompressedSize);

                    //NEW BUFFER FUNCTION
                    var newBuffer = brotli.decompressBuffer(bt2, true);
                    if (newBuffer != null) { File.WriteAllBytes(ppath + "/buffer1NEW.tif", newBuffer); Debug.Log(" # new Buffer: " + newBuffer.Length); }
                    newBuffer = null;

                }
            #else
                if (brotli.compressBuffer(bt, ref buff,  progress3, true)) lz3 = 1;

                bt2 = new byte[1];

                if (brotli.decompressBuffer(buff, ref bt2, true)) lz4 =1;

                int decompressedSize = brotli.decompressBuffer(buff, fixedOutBuffer, true);
                if (decompressedSize > 0) Debug.Log(" # Decompress Fixed size Buffer: " + decompressedSize);

                var newBuffer = brotli.decompressBuffer(buff, true);
                if(newBuffer != null) Debug.Log(" # Decompressed buffer size: " + newBuffer.Length);
                newBuffer = null;
            #endif





        //make FileBuffer test on supported platfoms.
#if (UNITY_IPHONE || UNITY_IOS || UNITY_STANDALONE_OSX || UNITY_ANDROID || UNITY_STANDALONE_LINUX || UNITY_EDITOR) && !UNITY_EDITOR_WIN
        //make a temp buffer to read a br file in.
        if (File.Exists(ppath + "/" + myFile + ".br")){
				progress2[0] = 0;
				byte[] FileBuffer = File.ReadAllBytes(ppath + "/" + myFile + ".br");
				fbuftest = brotli.decompressFile(null, ppath + "/" + myFile + "FB.tif",  progress2, FileBuffer);
			}
		#endif
    }

   IEnumerator DownloadTestFile() {

        #if !UNITY_TVOS || UNITY_EDITOR
        //make sure a previous brotli file having the same name with the one we want to download does not exist in the ppath folder
        if (File.Exists(ppath + "/" + myFile)) File.Delete(ppath + "/" + myFile);
        #endif

        Debug.Log("starting download");

        //replace the link to the brotli file with your own (although this will work also)
        using (UnityEngine.Networking.UnityWebRequest www = UnityEngine.Networking.UnityWebRequest.Get(uri + myFile)) {
            #if UNITY_5 || UNITY_4
            yield return www.Send();
            #else
            yield return www.SendWebRequest();
            #endif

            if (www.error != null) {
                Debug.Log(www.error);
                downloadError = true;
            } else  {
                downloadDone = true;

                #if !UNITY_TVOS || UNITY_EDITOR
                    //write the downloaded brotli file to the ppath directory so we can have access to it
                    //depending on the Install Location you have set for your app, set the Write Access accordingly!
                    File.WriteAllBytes(ppath + "/" + myFile, www.downloadHandler.data);
                #else
                    bt = new byte[www.downloadHandler.data.Length];
                    Array.Copy(www.downloadHandler.data, bt, www.downloadHandler.data.Length);
                    yield return true;
                     compressionStarted = true;
                    DoTests();
                #endif

                Debug.Log("download done");
            }
        }
    }

#else
        void OnGUI(){
            GUI.Label(new Rect(10, 10, 500, 40), "Does not work on WebGL.");
        }
#endif
}


