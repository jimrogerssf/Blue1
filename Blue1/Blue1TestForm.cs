using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using InTheHand.Net.Bluetooth;
using InTheHand.Net.Sockets;
using BluetoothEx;

namespace Blue1
{
   public partial class Blue1TestForm : Form
   {
      public Blue1TestForm()
      {
         InitializeComponent();
      }

      private async void BtnConnect_Click(object sender, EventArgs e)
      {
         BluetoothEx.DeviceManager dm = new BluetoothEx.DeviceManager();
         MessageManager mm = new MessageManager(this.TBMessages);

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

         // discover devices
         mm.Add("Discovering devices...");
         List<BluetoothDeviceInfo> radios = await dm.DiscoverAll();
         mm.Add($"Found {radios.Count} device(s)");
         radios.ForEach(r => mm.Add(r.DeviceName, r.DeviceAddress.ToString()));

         // attempt pairing with first device
         ;

         // display all info
         mm.Show();
      }


      class MessageManager
      {
         private List<string> Messages { get; set; } = new List<string>();
         public TextBox Box { get; set; }

         public MessageManager(TextBox box)
         {
            Box = box;
            Box.Text = string.Empty;
         }
         /// <summary>
         /// Add new message
         /// </summary>
         /// <param name="name"></param>
         /// <param name="value"></param>
         public void Add(string name)
         {
            Add(name, string.Empty);
         }
         public void Add(string name, string value = "")
         {
            string sep = string.IsNullOrWhiteSpace(value) ? string.Empty : "=";
            Messages.Add($"{name}{sep}{value}");
            Show();
         }
         public void Add(List<KeyValuePair<string, string>> values)
         {
            values.ForEach(kvp =>
               Messages.Add($"{kvp.Key}={kvp.Value}"));
            Show();
         }

         public void Show()
         {
            StringBuilder sb = new StringBuilder();
            Messages.ForEach(m => sb.AppendLine(m));
            Box.Text = sb.ToString();
         }

         public void Empty()
         {
            Messages.Clear();
            Box.Text = string.Empty;
         }
      }
   }
}
