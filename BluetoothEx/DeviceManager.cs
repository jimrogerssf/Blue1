using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using InTheHand.Net;
using InTheHand.Net.Bluetooth;
using InTheHand.Net.Sockets;
using InTheHand.Net.Bluetooth.Widcomm;
using InTheHand.Net.Bluetooth.Factory;

namespace BluetoothEx
{
   public class DeviceManager
   {
      private List<BluetoothDeviceInfo> DeviceList { get; } = new List<BluetoothDeviceInfo>();
      private TaskCompletionSource<bool> DeviceDiscoveryTask = null;

      /// <summary>
      /// Returns local Bluetooth device
      /// </summary>
      /// <returns></returns>
      public BluetoothRadio GetPrimaryDevice()
      {
         var radio = BluetoothRadio.PrimaryRadio;
         return radio ?? null;
      }

      /// <summary>
      /// Retuns all discoverable bluetooth devices
      /// </summary>
      /// <returns></returns>
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

      /// <summary>
      /// Pairs a bluetooth device
      /// </summary>
      /// <param name="device"></param>
      /// <returns></returns>
      public async Task<bool> PairDevice(BluetoothAddress device)
      {
         var paired = false;
         await Task.Run(() =>
         {
            paired = BluetoothSecurity.PairRequest(device, null);
         });

         return paired;
      }

   }
}
