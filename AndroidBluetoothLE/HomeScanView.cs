using Android.App;
using Android.Views;
using Android.OS;

namespace AndroidBluetoothLE
{
    [Activity(Label = "BluetoothLE", MainLauncher = true)]
    public class HomeScanView : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.ScanView);

            // Get our button from the layout resource,
            // and attach an event to it
//            Button button = FindViewById<Button>(Resource.Id.MyButton);

//            button.Click += delegate { button.Text = string.Format("{0} clicks!", count++); };
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Layout.ScanMenu, menu);

            return base.OnCreateOptionsMenu(menu);
        }
    }
}

