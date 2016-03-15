/*
 _     _     _     _              _                  
| |   (_)   | |   | |            | |                 
| |__  _  __| | __| | ___ _ __   | |_ ___  __ _ _ __ 
| '_ \| |/ _` |/ _` |/ _ \ '_ \  | __/ _ \/ _` | '__|
| | | | | (_| | (_| |  __/ | | | | ||  __/ (_| | |   
|_| |_|_|\__,_|\__,_|\___|_| |_|  \__\___|\__,_|_| 
 * Coded by Utku Sen(Jani) / August 2015 Istanbul / utkusen.com 
 * hidden tear may be used only for Educational Purposes. Do not use it as a ransomware!
 * You could go to jail on obstruction of justice charges just for running hidden tear, even though you are innocent.
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security;
using System.Security.Cryptography;
using System.IO;
using System.Net;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace hidden_tear_bruteforcer
{

    public partial class Form1 : Form
    {
        
        String sampleFileName;
        static String keyListFileName;
        static String decryptedFileName;

        // Attempts run
        static int attempts = 0;

        // Keys loaded from file
        static Queue<String> keyList = new Queue<String>();

        // PNG magic byte header
        static byte[] PNG_MAGIC_BYTES = new byte[8] { 137, 80, 78, 71, 13, 10, 26, 10 };

        // Background worker
        static BackgroundWorker worker = new BackgroundWorker();

        // Hash instance
        static SHA256 sha256 = SHA256.Create();

        // Set your salt here, change it to meet your flavor:
        // The salt bytes must be at least 8 bytes.
        static byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

        static string seed = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890*/&%!=";

        // AES instance
        static RijndaelManaged AES = new RijndaelManaged
        {
            KeySize = 256,
            BlockSize = 128,
            Mode = CipherMode.CBC
        };

        static int keyBytes = AES.KeySize / 8;
        static int ivBytes = AES.BlockSize / 8;

        // RNG instance
        static RNGCryptoServiceProvider rNGCryptoServiceProvider = new RNGCryptoServiceProvider();

        public Form1()
        {
            InitializeComponent();
        }

        public static int GetInt(RNGCryptoServiceProvider rnd, int max)
        {
            byte[] array = new byte[4];
            int num;
            do
            {
                rnd.GetBytes(array);
                num = (BitConverter.ToInt32(array, 0) & 2147483647);
            }
            while (num >= max * (2147483647 / max));
            return num % max;
        }

        public static string CreatePassword(int length)
        {
            StringBuilder stringBuilder = new StringBuilder();
            
            while (length-- > 0)
            {
                stringBuilder.Append(seed[Form1.GetInt(rNGCryptoServiceProvider, seed.Length)]);
            }
            return stringBuilder.ToString();
        }

        static public byte[] AES_Decrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes)
        {
            byte[] decryptedBytes = null;

            try {
                using (MemoryStream ms = new MemoryStream())
                {
                    
                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    AES.Key = key.GetBytes(keyBytes);
                    AES.IV = key.GetBytes(ivBytes);
                    
                    using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                        cs.Close();
                    }
                    decryptedBytes = ms.ToArray();
                    
                }
            }
            catch(Exception e)
            {
                
            }

            return decryptedBytes;
        }

        private void StartBruteForce()
        {
            // Display attempts label
            DisplayText.Text = "Setting up...";
            DisplayText.Visible = true;

            Log("Starting brute force on " + sampleFileName);

            // Setup background worker thread
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;

            worker.DoWork += BruteForce;
            worker.RunWorkerCompleted += RunWorkerCompleted;
            worker.ProgressChanged += ProgressChanged;
            worker.RunWorkerAsync(sampleFileName);

        }

        static private void BruteForce(object sender, DoWorkEventArgs e)
        {

            String testFile = e.Argument.ToString();

            // Load the sample file into memory
            byte[] bytesToBeDecrypted = File.ReadAllBytes(testFile);

            // Password buffer
            string passwordAttempt = "";

            // Password found flag
            bool passwordFound = false;

            // Byte buffer
            byte[] bytesFileMagic = new byte[8];

            while (true)
            {
                attempts++;
                // Check for cancel
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }

                // Check for a loaded key list
                if (keyListFileName == null)
                {
                    // Generate a random password
                    passwordAttempt = CreatePassword(32);
                    //passwordAttempt = "VrtiGxUbI8afaJwGbkePtpJKINyGIkZC";
                }
                else
                {
                    // Get next key from key list
                    passwordAttempt = GetNextKey();
                }

                // Check to break on end of key file
                if (passwordAttempt == null)
                {
                    throw new Exception("Reached end of keylist");
                }

                // Report progress
                try {
                    worker.ReportProgress(0, passwordAttempt);
                }
                catch(Exception)
                {
                    break;
                }
                
                // Hash password bytes
                byte[] passwordBytes = Encoding.UTF8.GetBytes(passwordAttempt);
                passwordBytes = sha256.ComputeHash(passwordBytes);
                
                // Decrypt with password
                byte[] bytesDecrypted = AES_Decrypt(bytesToBeDecrypted, passwordBytes);
                
                // Check for failed decryption
                if (bytesDecrypted == null)
                {
                   continue;
                }
                
                // Get first 8 bytes of "decrypted" file
                Array.Copy(bytesDecrypted, bytesFileMagic, 8);
                
                // Check for PNG
                if(bytesFileMagic.SequenceEqual(PNG_MAGIC_BYTES))
                {
                    passwordFound = true;
                    decryptedFileName = Path.GetDirectoryName(testFile) + "\\decrypted-" + Path.GetFileNameWithoutExtension(testFile);
                    FileStream fileStream = new FileStream(decryptedFileName, FileMode.Create, FileAccess.Write);
                    fileStream.Write(bytesDecrypted, 0, bytesDecrypted.Length);
                    fileStream.Close();
                    break;
                }
                else
                {
//                    Log("Possible password for " + testFile + ": " + passwordAttempt);
                }
                
            }

            if (passwordFound)
            {
                e.Result = passwordAttempt;
            }

        }

        void ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            DisplayText.Text = "Attempting: " + e.UserState.ToString();
            AttemptsLabel.Text = "Attempts: " + attempts;
        }

        void RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                DisplayText.Text = "Error: " + e.Error.Message;
                Log("Error: " + e.Error.ToString());
            }
            else if(!e.Cancelled)
            {
                label3.Visible = true;
                DisplayText.Text = "Password: " + e.Result.ToString();
                InfoLabel.Visible = true;
                InfoLabel.Text = "Click here to check file for success:\r\n" + Path.GetFileName(decryptedFileName);

                bruteforceButton.Enabled = false;
                Log("Password found for " + sampleFileName + " (" + attempts + " attempts): " + e.Result.ToString());
            }
            else
            {
                Log("Bruteforce stopped");
            }
        }

        static String GetNextKey()
        {
            if (keyList.Count == 0)
                return null;

            return keyList.Dequeue();

        }

        static void Log(String text)
        {
            using (StreamWriter log = new StreamWriter("log.txt", true))
            {
                log.WriteLine(text);
            }
        }
        private void InfoLabel_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(decryptedFileName);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(this.sampleFileName == null)
            {
                DisplayText.Text = "Please select an encrypted file sample";
                return;
            }

            if (!worker.IsBusy)
            {
                bruteforceButton.Text = "Pause Bruteforce";
                FileButton.Enabled = false;
                StartBruteForce();                
            }
            else
            {
                bruteforceButton.Text = "Start Bruteforce";
                FileButton.Enabled = true;
                worker.CancelAsync();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            
            this.Text = String.Format("HT BruteForcer v{0}", version);
        }

        private int LoadKeyList(String keyListFile)
        {
            // Clear key list
            keyList.Clear();

            int rows = 0;
            using(StreamReader sr = new StreamReader(keyListFile))
            {
                while (!sr.EndOfStream)
                {

                    // Store last value as the key
                    String[] line = sr.ReadLine().Split(',');
                    keyList.Enqueue(line.Last().Trim('"'));
                    rows++;

                }

            }

            return rows;
        }

        private void FileButton_Click(object sender, EventArgs e)
        {
            if (openSampleFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.sampleFileName = openSampleFileDialog.FileName;

                FileSelectedLabel.Text = Path.GetExtension(sampleFileName) + " file loaded";
            }
        }

        private void loadKeyListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(openKeyListFileDialog.ShowDialog() == DialogResult.OK)
            {
                keyListFileName = openKeyListFileDialog.FileName;

                // Load CSV into memory
               int keys = LoadKeyList(keyListFileName);
               DisplayText.Text = keys + " keys loaded";
               Log("Loaded list \"" + Path.GetFileName(keyListFileName) + "\" with " + keys + " keys");

            }
        }
    }
}
