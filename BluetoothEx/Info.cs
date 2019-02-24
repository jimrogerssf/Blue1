using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InTheHand.Net.Bluetooth;
using InTheHand.Net.Sockets;

namespace BluetoothEx
{
   public class DeviceManager
   {
      // device discovery
      public class DeviceDiscoveryCompletedEventArgs : EventArgs
      {
      }
      public event DeviceDiscoveryCompletedEvent DeviceDiscoveryCompleted;
      public delegate void DeviceDiscoveryCompletedEvent();

      private List<BluetoothDeviceInfo> DeviceList { get; } = new List<BluetoothDeviceInfo>();
      //private TaskCompletionSource<bool> DeviceDiscoveryTask { get; set; }

      /// <summary>
      /// Returns local Bluetooth device
      /// </summary>
      /// <returns></returns>
      public BluetoothRadio GetPrimaryDevice()
      {
         var radio = BluetoothRadio.PrimaryRadio;
         return radio ?? null;
      }

      TaskCompletionSource<bool> DeviceDiscoveryTask = new TaskCompletionSource<bool>();
      public async Task<List<BluetoothDeviceInfo>> DiscoverAll()
      {
         DeviceDiscoveryTask = new TaskCompletionSource<bool>();
         DeviceList.Clear();

         using (BluetoothComponent bc = new BluetoothComponent())
         {
            // bc.DiscoverDevicesProgress += Bc_DiscoverDevicesProgress;
            bc.DiscoverDevicesComplete += Bc_DiscoverDevicesComplete;
            bc.DiscoverDevicesAsync(255, true, true, true, false, "Blue1");
         }
         await DeviceDiscoveryTask.Task;
         return DeviceList;
      }

      private void Bc_DiscoverDevicesComplete(object sender, DiscoverDevicesEventArgs e)
      {
         Console.WriteLine("Devices discovered");

         e.Devices.ToList().ForEach(d => DeviceList.Add(d));
         DeviceDiscoveryTask.SetResult(true);
      }

      //private void Bc_DiscoverDevicesProgress(object sender, DiscoverDevicesEventArgs e)
      //{
      //   Console.WriteLine("Discovering devices");
      //}
   }
}
