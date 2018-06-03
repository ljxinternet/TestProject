using System.ComponentModel;
using Android.App;
using Android.Content;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;

namespace FragmentTest
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    [IntentFilter(new string[]{ "android.intent.action.VIEW" })]
    [Category("android.intent.category.DEFAULT")]
    
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            //
            Init();
        }

        private void Init()
        {
            Button btnTest = FindViewById<Button>(Resource.Id.btnTest);
            btnTest.Click += BtnTest_Click;
        }

        private void BtnTest_Click(object sender, System.EventArgs e)
        {
            //var intent = new Intent(this, typeof(PlayListActivity));
            //StartActivity(intent);

            //throw new System.NotImplementedException();
        }
    }
}

