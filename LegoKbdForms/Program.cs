using LegoKbdForms;

using System;
using System.Windows.Forms;

namespace YourNamespace
{
    class Program
    {
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Create a hidden form
            var hiddenForm = new HiddenForm();
            Application.Run(hiddenForm);
        }
    }

    // HiddenForm class to create a hidden form with a message
    class HiddenForm : Form
    {
        public HiddenForm()
        {
            // Hide the form
            this.ShowInTaskbar = false;
            this.Visible = false;

            // Hook the keyboard
            KeyboardHook.HookKeyboard();
        }

        protected override void Dispose(bool disposing)
        {
            // Unhook the keyboard when disposing the form
            KeyboardHook.UnhookKeyboard();
            base.Dispose(disposing);
        }

        protected override void SetVisibleCore(bool value)
        {
            // Prevent the form from becoming visible
            if (!IsHandleCreated)
            {
                CreateHandle();
            }
            base.SetVisibleCore(false);
        }
    }
}
