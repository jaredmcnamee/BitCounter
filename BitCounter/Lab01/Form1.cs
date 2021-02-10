using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GDIDrawer;
using System.Threading;
using System.Diagnostics;

namespace Lab01
{
    //delegate for updateing the ellapsed time
    public delegate void delvoidlong(long time);
    //delegate for taking the returned dictionary and invoking a secondary callback method
    public delegate void deldic(Dictionary<byte,int> retDic ,long retTime);
    //delegate to update and call back to the canvas
    public delegate void delvoidvoid();
    //delegate to update the sum of the bits
    public delegate void delvoiddouble(double sum);
    public partial class Form1 : Form
    {
        //the main queue that all threads will access
        Queue<Queue<byte[]>> _mainBus = new Queue<Queue<byte[]>>();
        //dictionary of named bit counters for updating the main dictionary 
        Dictionary<string, BitCounter> _bits = new Dictionary<string, BitCounter>();
        //main dictionary that contains the total count 
        Dictionary<byte, int> drawDic = new Dictionary<byte, int>();
        public deldic retClass;
        public CDrawer _canvas;
        BitCounter _bitCounter;

        //thread safe bool for stopping threads
        public volatile bool stopThread = true;

        long totalTime = 0;
        double totalSum = 0.0;
        long totalSize = 0;
        
        public Form1()
        {
            InitializeComponent();
            _canvas = new CDrawer(800, 800) { ContinuousUpdate = false };
            UI_Btn_SelectFile.Click += UI_Btn_SelectFile_Click;
            UI_btn_reset.Click += UI_btn_reset_Click;
        }

        /// <summary>
        /// when the reset button is clicked returning the program to its inital state
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UI_btn_reset_Click(object sender, EventArgs e)
        {

            //ending any bitcounter thread that is still running
            foreach (KeyValuePair<string, BitCounter> b in _bits)
            {
                b.Value.stop = false;
            }
            //clearing every collection
            UI_lv_Btyes.Items.Clear();
            drawDic.Clear();
            _mainBus.Clear();
            _bits.Clear();

            //calling the stop thread thread safe bool
            stopThread = false;
            //reseting the progress bar
            UI_pbar_1sPercent.Value = 0;
            UI_tbx_PecentStatus.Text = $"Percentage of the file that is 1s: Aborted";

        }

        /// <summary>
        /// When the select file button is pressed attempting to open a file update the UI and start
        /// a loading thread and a processing thread to count the
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UI_Btn_SelectFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = true;
            //setting the intial directory to be in the project file
            ofd.InitialDirectory = Path.GetFullPath(Environment.CurrentDirectory + @"\..\..");

            if(ofd.ShowDialog() == DialogResult.OK)
            {
                foreach (string fn in ofd.FileNames)
                {
                    //when the dialogresult returns ok then attempting to get the file info for the file
                    FileInfo fInfo = null;
                    try
                    {
                        fInfo = new FileInfo(fn);

                        //incrementing the total size of the files that have been worked on
                        totalSize += fInfo.Length;
                        //updating the UI
                        UI_tbx_FileName.Text = fInfo.Name.ToString();
                        UI_tBx_FileSize.Text = fInfo.Length.ToString();
                        //making the progress bar larger
                        UI_pbar_1sPercent.Maximum += (int)(fInfo.Length / 2048) + 1;

                    }
                    //catching errors in loading the file and displaying the message to the user
                    catch (Exception ew)
                    {
                        MessageBox.Show(ew.Message);
                    }

                    //starting a loading and processing thread and passing the file info for later use in the threads
                    Thread loading = new Thread(new ParameterizedThreadStart(loadThread));
                    Thread processing = new Thread(new ParameterizedThreadStart(processThread));

                    //setting up the bitcounter class and other delegate for call back
                    retClass = new deldic(ClassCallback);
                    _bitCounter = new BitCounter(retClass);
                    _bitCounter.bitRender = new delvoidvoid(UpdateCanvas);

                    //naming the class thread
                    _bitCounter.name = _bits.Count.ToString();
                    //adding the thread to the list
                    _bits.Add(_bitCounter.name, _bitCounter);

                    loading.Start(new Tuple<FileInfo, string>(fInfo, _bitCounter.name));
                    processing.Start(fInfo);
                }
            }
        }

        /// <summary>
        /// The thread method that will take a file and load 2k byte arrays of the information into a queue
        /// </summary>
        /// <param name="info"></param>
        public void loadThread(object info)
        {
            //protecting against garbage data
            if (!(info is Tuple<FileInfo, string>))
                throw new ArgumentException("Not a Tuple");

            //splitting the input into the bitcounter key and its load info
            FileInfo load = ((Tuple<FileInfo, string>)info).Item1;
            string key = ((Tuple<FileInfo, string>)info).Item2;
            //output queue to be fed into the main bus
            Queue<byte[]> output = new Queue<byte[]>();
            
            //opening a file stream to read the file
            using (FileStream fs = File.OpenRead(load.FullName))
            {
                int arraySize = (int)load.Length;
                byte[] b = new byte[2048];

                //until 0 bytes are returned
                while(fs.Read(b,0,b.Length)>0)
                {
                    //if data is found queueing the information
                    lock(output)
                    {
                        output.Enqueue(b);
                    }
                    //adding the byte array to the queue for the class thread
                    lock(_bits[key])
                    {
                        _bits[key].addToBuff = b;
                    }
                    arraySize -= 2048;

                    //if there is less than 2k bytes left to be read 
                    //adjust the byte array size to be whats left
                    if (arraySize < 2048 && arraySize > 0)
                        b = new byte[arraySize];
                    else
                        b = new byte[2048];
                }
                //queueing up a zero sized byte array as the last item to signify the end
                output.Enqueue(new byte[0]);

                //adding the end byte array to the class thread
                lock (_bits[key])
                {
                    _bits[key].addToBuff = new byte[0];
                }
            }
            //locking the main bus and queueing the queue of byte arrays
            lock(_mainBus)
            {
                _mainBus.Enqueue(output);
            }
        }

        /// <summary>
        /// A thread for processing the queues of byte arrays and counting the number of 1s in each array
        /// </summary>
        /// <param name="info"></param>
        public void processThread(object info)
        {
            FileInfo process = (FileInfo)info;
            Queue<byte[]> working = null;
            //starting a stop watch to count the processing time 
            Stopwatch s = new Stopwatch();
            double sum = 0.0;
            bool valid = true;
            
            s.Start();
            do
            {
                while (working == null)
                {
                    //if the main bus has a queue to process
                    lock (_mainBus)
                    {
                        if (_mainBus.Count > 0)
                        {
                            //dequeuing and setting as the working queue
                            working = _mainBus.Dequeue();
                        }
                        else
                        {
                            //otherwise sleeping the thread
                            Thread.Sleep(1);
                        }
                    }
                }
                //iterateing through each byte and counting the 1s
                foreach(byte[] b in working)
                {
                    //if the reset button is pressed break out of this funcion
                    if (!stopThread)
                        break;
                    sum = 0;
                    if (b.Length != 0)
                    {
                        foreach (byte nom in b)
                        {
                            byte temp = nom;
                            while (temp > 0)
                            {
                                sum += temp & 1;
                                temp >>= 1;
                            }
                            
                        }
                    }
                    //ending the thread if there is nothing else to work on
                    else
                        valid = false;
                    

                    //sending the sum of the 1s to a callback method for processing
                    Invoke(new delvoiddouble(ProcessCallbackSum), sum);
                }
            } while (valid && stopThread);

            //stopping the stopwatch
            s.Stop();
            
            if(stopThread)
                //sending the elapsed time to a callback method for processing
                Invoke(new delvoidlong(ProcessCallbackTimer), s.ElapsedMilliseconds);
        }
        /// <summary>
        /// A method for updating the UI to show the progress of the files being processed
        /// </summary>
        /// <param name="time"></param>
        /// <param name="sum"></param>
        private void ProcessCallbackSum(double sum)
        {
            
            totalSum += sum;

            //percentage calc total sum is in bits so totalsize in bytes times 8
            double percent = (totalSum/(totalSize*8));

            
            UI_tbx_PecentStatus.Text = $"Percentage of the file that is 1s: {percent:P4}";
            UI_pbar_1sPercent.PerformStep();
        }

        /// <summary>
        /// the call back for updating the time to the ui
        /// </summary>
        /// <param name="time"></param>
        private void ProcessCallbackTimer(long time)
        {
            totalTime += time;

            UI_gb_1sPercent.Text = ($"1s Percentage (in {totalTime} ms)");
        }

        /// <summary>
        /// main callback for returning the dicitionary count from the class thread
        /// </summary>
        /// <param name="myDic"></param>
        /// <param name="retTime"></param>
        public void ClassCallback(Dictionary<Byte, int> myDic, long retTime)
        {
            //invokes a secondary call back for processing
            Invoke(new deldic(SecondaryCallBack),myDic,retTime);
        }

        /// <summary>
        /// Secondary call back for processing the dictionary information
        /// </summary>
        /// <param name="myDic"></param>
        /// <param name="reTime"></param>
        public void SecondaryCallBack(Dictionary<byte,int> myDic, long reTime)
        {
            //clearing the items so the new info doesnt get appended
            UI_lv_Btyes.Items.Clear();
            //adding the to total time
            totalTime += reTime;
            //adding all keys that are not in the dictionary with their current value or 
            //adding the values of none unique bits to their value in the diction
            foreach(KeyValuePair<byte,int> kvp in myDic)
            {
                if (!drawDic.ContainsKey(kvp.Key))
                    drawDic.Add(kvp.Key, kvp.Value);
                else
                    drawDic[kvp.Key] += kvp.Value;   
            }
            //locking the main dicitonary sorting it and adding all of it to the listview
            lock (drawDic)
            {
                drawDic = drawDic.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
                foreach (KeyValuePair<byte, int> kvp in drawDic)
                {
                    ListViewItem lv = new ListViewItem($"{kvp.Key:X2}");
                    lv.SubItems.Add(kvp.Value.ToString());
                    UI_lv_Btyes.Items.Add(lv);
                }
            }
        }
        /// <summary>
        /// Method for updating the canvas
        /// </summary>
        public void UpdateCanvas()
        {
            //clearing the canvas
            _canvas.Clear();
            
            //getting the max value of the dicitonary
            int max = drawDic.Max(x => x.Value);
            lock (drawDic)
            {
                //for each key value pair creating a rectangel and coloring it based its value/max value ratio
                foreach (KeyValuePair<byte, int> kvp in drawDic)
                {
                    int x = (int)kvp.Key % 16;
                    int y = (int)kvp.Key / 16;
                    int col = (int)(((double)kvp.Value / max) * 255);
                    _canvas.AddRectangle(x * 50, y * 50, 50, 50, Color.FromArgb(col, col, col)); ;

                    //if the color isn't black drawing it to the screen
                    if (col > 0)
                        _canvas.AddText($"{kvp.Key:X}", 10, x * 50, y * 50, 50, 50, Color.Yellow);
                }
            }
            //rending the canvas
            _canvas.Render();
            
        }
    }
}
