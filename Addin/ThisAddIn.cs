using System;
using Safeguard.Common;
using Safeguard.Common.Ui;
using Safeguard.Test.Ui;
using SafeguardAddin;

namespace Safeguard.ExcelAddin
{
    public partial class ThisAddIn
    {
        private readonly WpfHost _contentHost = new WpfHost();

        private void ThisAddIn_Startup(object sender, EventArgs e)
        {
            _contentHost.WpfElementHost.Child = new SafeguardForm
            {
                // TODO: Create interface instances
                DataContext = new AddinViewModel(new SafeguardHandle(), new ExcelTestImpl())
            };

            var taskPane = CustomTaskPanes.Add(_contentHost, "Safeguard");
            taskPane.Visible = true;

        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
        }

        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }
        
        #endregion
    }
}
