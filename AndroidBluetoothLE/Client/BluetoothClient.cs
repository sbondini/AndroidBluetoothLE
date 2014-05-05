using System.Diagnostics;
using Android.App;
using Android.Bluetooth;
using Android.Content;
using Android.Content.PM;

namespace AndroidBluetoothLE.Client
{
    public class BluetoothClient
    {
        public BluetoothManager Manager { get; private set; }

        public BluetoothAdapter Adapter { get; private set; }

        public bool Initialize()
        {
            var context = Application.Context;

            if (!context.PackageManager.HasSystemFeature(PackageManager.FeatureBluetoothLe))
            {
                Debug.WriteLine("Bluetooth LE is not supported");
                return false;
            }

            var manager = (BluetoothManager)context.GetSystemService(Context.BluetoothService);
            
            Manager = manager;
            Adapter = manager.Adapter;
            return true;
        }
    }
}