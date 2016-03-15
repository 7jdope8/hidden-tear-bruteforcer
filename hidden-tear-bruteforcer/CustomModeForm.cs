using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hidden_tear_bruteforcer
{
    public partial class CustomModeForm : Form
    {
        // Flag for close by button
        public bool closedByButton = false;

        public CustomModeForm()
        {
            InitializeComponent();
        }

        private void ApplySettingsButton_Click(object sender, EventArgs e)
        {

            // Set close flag
            closedByButton = true;

            // Close the form
            Close();

        }

    }
}
