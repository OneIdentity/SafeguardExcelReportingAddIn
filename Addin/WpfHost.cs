using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Integration;

namespace SafeguardAddin
{
    public partial class WpfHost : UserControl
    {
        public WpfHost()
        {
            InitializeComponent();
        }

        public ElementHost WpfElementHost => wpfElementHost;
    }
}
