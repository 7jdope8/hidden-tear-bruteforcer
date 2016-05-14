using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.IO;

namespace hidden_tear_bruteforcer
{

    public partial class MainForm : Form
    {

        // Custom settings structure
        public struct CustomSettings
        {
            public enum PasswordGenerator
            {
                CreateSecurePassword,
                CreatePseudoPassword
            };
            public int passwordLength;
            public PasswordGenerator passwordGenerator;
            public string possibleCharacters;
            public byte[] salt;
        }

        // Custom settings loaded
        static CustomSettings customSettings = new CustomSettings();

        // Filenames
        String sampleFileName;
        static String keyListFileName;
        static String decryptedFileName;

        // Sample timestamp
        static int sampleTimestampTick;

        // Modes
        public enum Mode
        {
            HiddenTear,
            EDA2,
            BankAccountSummary,
            MireWare,
            EightLockEight,
            Custom
        };

        // Current mode
        static Mode currentMode = Mode.HiddenTear;

        // Attempts run
        static int attempts = 0;

        // Modified date dialog (form)
        ModifiedDateForm modifiedDialog = new ModifiedDateForm();

        // Custom mode dialog (form)
        CustomModeForm customDialog = new CustomModeForm();

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
        static byte[] saltBytesHiddenTear = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
        static byte[] saltBytesEDA2 = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
        static byte[] saltBytesMireWare = new byte[] { 3, 1, 3, 3, 7, 1, 5, 7 };
        static byte[] saltEightLock = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

        // Random strings from samples
        static string randomStringEDA2 = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890*/&%!=";
        static string randomStringHiddenTear = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890*!=&?&/";
        static string randomStringBankAccountSummary = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890*!=/";
        static string randomStringEightLock = "1234567890qwertyuiopasdfghjklzxc";

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

        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            // Display version in title
            var version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            this.Text = String.Format("HT BruteForcer v{0}", version);

            // Load the modes into list
            var modes = Enum.GetValues(typeof(Mode));

            foreach (var mode in modes)
            {
                ModeLabel.DropDownItems.Add(mode.ToString());
            }
            ModeLabel.Text = "Mode: " + ModeLabel.DropDownItems[0].Text;

            // Load password generators into custom dialog
            var generators = Enum.GetValues(typeof(CustomSettings.PasswordGenerator));

            foreach (var generator in generators)
            {
                customDialog.CustomPasswordGenerator.Items.Add(generator.ToString());
            }
            customDialog.CustomPasswordGenerator.SelectedIndex = 0;

            // Add close handler to modified dialog
            modifiedDialog.FormClosed += ModifiedDialog_FormClosed;

            // Add close handler to custom dialog
            customDialog.FormClosed += CustomDialog_FormClosed;

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

        public static string CreateSecurePassword(int length, String randomString)
        {
            StringBuilder stringBuilder = new StringBuilder();
            
            while (length-- > 0)
            {
                stringBuilder.Append(randomString[MainForm.GetInt(rNGCryptoServiceProvider, randomString.Length)]);
            }
            return stringBuilder.ToString();
        }

        public static string CreatePseudoPassword(int length, int seed, String randomString)
        {
            StringBuilder res = new StringBuilder();
            Random rnd = new Random(seed);
            while (0 < length--)
            {
                res.Append(randomString[rnd.Next(randomString.Length)]);
            }
            return res.ToString();
        }

        static public byte[] AES_Decrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes, byte[] saltBytes)
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

            // Different buffer
            int diff = 0;

            // Cache integer cast of timestamp
            int sampleTimestampTickInt = (int)sampleTimestampTick;

            // Salt
            byte[] saltBytes;

            // Determine mode
            switch (currentMode)
            {

                // HiddenTear mode
                case Mode.HiddenTear:

                    // Set salt
                    saltBytes = saltBytesHiddenTear;

                    break;

                // EDA2 mode
                case Mode.EDA2:

                    // Set salt
                    saltBytes = saltBytesEDA2;

                    break;

                // BankAccountSummaryMode
                case Mode.BankAccountSummary:

                    saltBytes = saltBytesHiddenTear;

                    break;

                // MireWare mode
                case Mode.MireWare:

                    // Set salt
                    saltBytes = saltBytesMireWare;

                    break;

                // 8lock8 mode
                case Mode.EightLockEight:

                    // Set salt
                    saltBytes = saltEightLock;

                    break;

                // Custom mode
                case Mode.Custom:

                    // Set salt
                    saltBytes = customSettings.salt;

                    break;

                default:

                    throw new InvalidEnumArgumentException("Invalid mode");

            }

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
                if (keyListFileName != null)
                {

                    // Get next key from key list
                    passwordAttempt = GetNextKey();

                }
                else
                {
                    // Determine mode
                    switch (currentMode) {

                        // HiddenTear mode
                        case Mode.HiddenTear:

                            // Generate psuedo-random password
                            passwordAttempt = CreatePseudoPassword(15, sampleTimestampTickInt - diff, randomStringHiddenTear);
                            diff++;

                            break;

                        // EDA2 mode
                        case Mode.EDA2:

                            // Generate a random password
                            passwordAttempt = CreateSecurePassword(32, randomStringEDA2);

                            // Test password for known file
                            //passwordAttempt = "VrtiGxUbI8afaJwGbkePtpJKINyGIkZC";

                            break;

                        // BankAccountSummary mode
                        case Mode.BankAccountSummary:

                            // Generate psuedo-random password
                            passwordAttempt = CreatePseudoPassword(10, sampleTimestampTickInt - diff, randomStringBankAccountSummary);
                            diff++;

                            break;

                        // MireWare mode
                        case Mode.MireWare:

                            // Generate psuedo-random password
                            passwordAttempt = CreatePseudoPassword(15, sampleTimestampTickInt - diff, randomStringHiddenTear);
                            diff++;

                            break;

                        // 8lock8 mode
                        case Mode.EightLockEight:

                            // Generate pseudo-random password
                            passwordAttempt = CreatePseudoPassword(32, sampleTimestampTickInt - diff, randomStringEightLock);
                            diff++;

                            break;

                        // Custom mode
                        case Mode.Custom:

                            // Determine generator to use
                            // Super inefficient for each loop, but the Rfc2898DeriveBytes already takes 98% of execution time anyways...
                            switch (customSettings.passwordGenerator)
                            {

                                // Secure generator
                                case CustomSettings.PasswordGenerator.CreateSecurePassword:

                                    passwordAttempt = CreateSecurePassword(customSettings.passwordLength, customSettings.possibleCharacters);

                                    break;

                                // Pseudo generator
                                case CustomSettings.PasswordGenerator.CreatePseudoPassword:

                                    passwordAttempt = CreatePseudoPassword(customSettings.passwordLength, sampleTimestampTickInt - diff, customSettings.possibleCharacters);
                                    diff++;

                                    break;

                                default:

                                    throw new InvalidEnumArgumentException("Invalid password generator");

                            }

                            break;

                        default:

                            throw new InvalidEnumArgumentException("Invalid mode");

                    }

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
                byte[] bytesDecrypted = AES_Decrypt(bytesToBeDecrypted, passwordBytes, saltBytes);
                
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

                // Get sample filename
                this.sampleFileName = openSampleFileDialog.FileName;

                // Get modified date in milliseconds
                DateTime writeTime = File.GetLastWriteTime(sampleFileName);
                var timestamp = writeTime - DateTime.Now.AddMilliseconds(-Environment.TickCount);
                sampleTimestampTick = (int) timestamp.TotalMilliseconds;

                // Set form controls
                modifiedDialog.ModifiedDatePicker.Value = modifiedDialog.ModifiedTimePicker.Value = writeTime;

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

        private void ModeLabel_Click(object sender, EventArgs e)
        {
            // Show the dropdown
            ModeLabel.ShowDropDown();
        }

        private void ModeLabel_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            // Set the current mode
            currentMode = (Mode) Enum.Parse(typeof(Mode), e.ClickedItem.ToString());

            // Update the label
            ModeLabel.Text = "Mode: " + e.ClickedItem.ToString();

            // Special case for custom
            if(currentMode == Mode.Custom)
            {

                // Open custom properties dialog
                customDialog.ShowDialog();

            }

        }

        private void setModifiedDateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Open modified dialog
            modifiedDialog.ShowDialog();

        }

        private void ModifiedDialog_FormClosed(object sender, FormClosedEventArgs e)
        {

            // Check for proper closing and by button
            if(e.CloseReason == CloseReason.UserClosing && modifiedDialog.closedByButton)
            {

                // Get the modified date
                DateTime modifiedDate = modifiedDialog.ModifiedDatePicker.Value.Date.Add(modifiedDialog.ModifiedTimePicker.Value.TimeOfDay);
                var timestamp = modifiedDate - DateTime.Now.AddMilliseconds(-Environment.TickCount);
                sampleTimestampTick = (int) timestamp.TotalMilliseconds;

                // Reset the flag
                modifiedDialog.closedByButton = false;

            }

        }

        private void CustomDialog_FormClosed(object sender, FormClosedEventArgs e)
        {

            // check for proper closing and by button
            if(e.CloseReason == CloseReason.UserClosing && customDialog.closedByButton)
            {

                // Get settings
                customSettings.passwordLength = int.Parse(customDialog.CustomPasswordLength.Text);
                customSettings.passwordGenerator = (CustomSettings.PasswordGenerator) Enum.Parse(typeof(CustomSettings.PasswordGenerator), customDialog.CustomPasswordGenerator.Text);
                customSettings.possibleCharacters = customDialog.CustomPossibleCharacters.Text;

            }

        }

    }
}
