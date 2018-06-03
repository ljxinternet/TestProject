using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using FragmentTest.Fragment;

namespace FragmentTest
{
    [Activity(Label = "PlayerActivity")]
    public class PlayListActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            int playId = Intent.GetIntExtra("PlayId", 0);

            Android.App.Fragment fragment = ContentFragment.GetInstance(playId);
            FragmentManager.BeginTransaction().Add(Android.Resource.Id.Content, fragment, "1").Commit();
        }




    }
}