using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using GDIDrawer;

namespace Lab01
{
    /// <summary>
    /// Class for processing byte arrays and counting the unique bytes in the array
    /// </summary>
    class BitCounter
    {
        //main buffer to be processed
        private Queue<byte[]> buffer = new Queue<byte[]>();
        //stopwatch to time the thread
        private Stopwatch s = new Stopwatch();

        //bool to stop a thread in process
        private bool running;


        /// <summary>
        /// property for adding an array to the queue
        /// </summary>
        public byte[] addToBuff
        {
            set
            {
                lock (buffer)
                {
                    buffer.Enqueue(value);
                }
            }
        }
        /// <summary>
        /// property for getting the name of the thread
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// dictionary that will be passed back to main
        /// </summary>
        public Dictionary<byte,int> myDic { get; private set; }
        /// <summary>
        /// delegate to tell main when to draw to the drawer
        /// </summary>
        public Delegate bitRender { private get;  set; }
        /// <summary>
        /// bool for stopping the class thread
        /// </summary>
        public bool stop 
        {
            set 
            {
                running = value;
            } 
        }

        /// <summary>
        /// initializing the class and thread that will process the queues
        /// </summary>
        /// <param name="callback"></param>
        public BitCounter(Delegate callback)
        {
            running =  true;
            myDic = new Dictionary<byte, int>();
            if (callback != null)
            {
                Thread bitCounting = new Thread(new ParameterizedThreadStart(BitCountThread));
                bitCounting.Start(callback);
            }
        }
        /// <summary>
        /// Method for processing the queue of bytes
        /// </summary>
        /// <param name="callback"></param>
        public void BitCountThread(object callback)
        {
            
            bool valid = true;//bool to see if the thread is still running
            do
            {
                //if the stop has been called breaking out of the thread
                if (!running)
                    break;
                //staring the time
                s.Start();
                byte[] working = null;
                //waiting for there to be a queue to work on
                while (working == null)
                {
                    lock (buffer)
                    {
                        if (buffer.Count > 0)
                            working = buffer.Dequeue();
                        else
                        {
                            Thread.Sleep(1);
                        }
                    }
                }
                //if the queue is not the ending array of length zero processing
                if (working.Length > 0 && running)
                {
                    //counting each byte in the array
                    foreach (byte b in working)
                    {
                        //if stop has been call breaking out of the process
                        if (!running)
                            break;

                        //if its not in the dictionary adding it
                        if (!myDic.ContainsKey(b))
                            myDic.Add(b, 1);
                        //else incrementing the keys value
                        else
                            ++myDic[b];
                        
                    }
                }
                else
                {
                    //nothing the process ending the thread
                    Console.WriteLine("bitcounter thread ending");
                    valid = false;
                }
                    
            }
            while (valid /*&& running*/);
            //stoping the timer
            s.Stop();
            //if the callback is a delegate and stop has not been called
            if(callback is Delegate && running)
            {
                //backing the information back to main
                Delegate del = (Delegate)callback;
                //invoking and sending the time ellapsed
                del.DynamicInvoke(myDic,s.ElapsedMilliseconds);
                //telling the drawer to update in main
                bitRender.DynamicInvoke();
                
            }
            
        }
    }
}
