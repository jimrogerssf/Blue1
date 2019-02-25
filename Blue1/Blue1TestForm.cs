using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using InTheHand.Net;
using InTheHand.Net.Sockets;

namespace Blue1
{
   public partial class Blue1TestForm : Form
   {
      public Blue1TestForm()
      {
         InitializeComponent();
      }

      private void BtnConnect_Click(object sender, EventArgs e)
      {
         Connect();
      }

      /// <summary>
      /// Finds local bluetooth device, discover other devices in neighborhood
      /// and tries pairing with first device found
      /// </summary>
      private async void Connect()
      {
         BluetoothEx.DeviceManager dm = new BluetoothEx.DeviceManager();
         MessageManager mm = new MessageManager(this.TBMessages, this.progressBar1);

         var radio = dm.GetPrimaryDevice();
         if (radio == null)
         {
            MessageBox.Show("Bluetooh device not found");
            return;
         }

         // this device
         mm.Add("Status", radio.HardwareStatus.ToString());
         var mac = radio.LocalAddress;
         mm.Add("Mac", mac.ToString());

         #region Discovery
         // discover devices using non blocking call
         mm.Add("Discovering devices...");
         mm.StartProgress();

         List<BluetoothDeviceInfo> radios = await dm.DiscoverAll();

         // show result
         mm.EndProgress();
         mm.Add($"Found {radios.Count} device(s)");
         radios.ForEach(r => mm.Add(r.DeviceName, r.DeviceAddress.ToString()));
         #endregion

         #region Pairing
         // attempt pairing with first device in the list
         if (radios.Count == 0)
         {
            mm.Add($"Pairing was skipped.");
         }
         else
         {
            BluetoothDeviceInfo btInfo = radios[0];
            BluetoothAddress addr = new BluetoothAddress(btInfo.DeviceAddress.ToByteArray());
            mm.Add($"Initiating pairing with {btInfo.DeviceName}...");
            mm.StartProgress();

            var paired = await dm.PairDevice(addr);

            mm.EndProgress();
            var successPrefix = paired ? "" : "un";
            mm.Add($"Pairing was {successPrefix}successful.");

         }
         #endregion
      }


      class MessageManager
      {
         private List<string> Messages { get; set; } = new List<string>();
         private TextBox Box { get; set; }
         private ProgressBar Progress { get; set; }
         private System.Windows.Forms.Timer ProgressTimer = new System.Windows.Forms.Timer();

         internal MessageManager(TextBox box, ProgressBar progress)
         {
            Box = box;
            Progress = progress;
            Box.Text = string.Empty;
         }
         /// <summary>
         /// Add new message
         /// </summary>
         /// <param name="name"></param>
         /// <param name="value"></param>
         internal void Add(string name)
         {
            Add(name, string.Empty);
         }
         internal void Add(string name, string value = "")
         {
            string sep = string.IsNullOrWhiteSpace(value) ? string.Empty : "=";
            Messages.Add($"{name}{sep}{value}");
            Show();
         }
         internal void Add(List<KeyValuePair<string, string>> values)
         {
            values.ForEach(kvp =>
               Messages.Add($"{kvp.Key}={kvp.Value}"));
            Show();
         }

         internal void Empty()
         {
            Messages.Clear();
            Box.Text = string.Empty;
         }

         internal void Show()
         {
            StringBuilder sb = new StringBuilder();
            Messages.ForEach(m =>
            {
               sb.AppendLine(m);
            });
            Box.Text = sb.ToString();
         }

         internal void StartProgress()
         {
            Progress.Minimum = 0;
            Progress.Maximum = 100;
            Progress.Value = 0;
            ProgressTimer.Tick += ProgressTimer_Tick;
            ProgressTimer.Interval = 50;
            ProgressTimer.Start();
         }

         private void ProgressTimer_Tick(object sender, EventArgs e)
         {
            const int inc = 1;
            int val = Progress.Value + inc;
            val = val >= Progress.Maximum ? 0 : val;
            Progress.Value = val;
         }

         internal void EndProgress()
         {
            ProgressTimer.Stop();
            Progress.Value = 0;
         }
      }
   }
}
