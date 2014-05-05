package androidbluetoothle.client;


public class BluetoothDeviceScanner
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		android.bluetooth.BluetoothAdapter.LeScanCallback
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onLeScan:(Landroid/bluetooth/BluetoothDevice;I[B)V:GetOnLeScan_Landroid_bluetooth_BluetoothDevice_IarrayBHandler:Android.Bluetooth.BluetoothAdapter/ILeScanCallbackInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"";
		mono.android.Runtime.register ("AndroidBluetoothLE.Client.BluetoothDeviceScanner, AndroidBluetoothLE, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", BluetoothDeviceScanner.class, __md_methods);
	}


	public BluetoothDeviceScanner () throws java.lang.Throwable
	{
		super ();
		if (getClass () == BluetoothDeviceScanner.class)
			mono.android.TypeManager.Activate ("AndroidBluetoothLE.Client.BluetoothDeviceScanner, AndroidBluetoothLE, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	public BluetoothDeviceScanner (android.bluetooth.BluetoothAdapter p0) throws java.lang.Throwable
	{
		super ();
		if (getClass () == BluetoothDeviceScanner.class)
			mono.android.TypeManager.Activate ("AndroidBluetoothLE.Client.BluetoothDeviceScanner, AndroidBluetoothLE, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Bluetooth.BluetoothAdapter, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
	}


	public void onLeScan (android.bluetooth.BluetoothDevice p0, int p1, byte[] p2)
	{
		n_onLeScan (p0, p1, p2);
	}

	private native void n_onLeScan (android.bluetooth.BluetoothDevice p0, int p1, byte[] p2);

	java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
