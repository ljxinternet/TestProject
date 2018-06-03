using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace FragmentTest.Fragment
{
    public class ListTitleFragment : ListFragment
    {
        public int playId;

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            this.ListAdapter=new ArrayAdapter<string>(Activity,Android.Resource.Layout.SimpleListItem1,ListClass.Titles);


            if (savedInstanceState!=null)
            {
                playId = savedInstanceState.GetInt("PlayId", 0);
            }
        }

        public override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);
            outState.PutInt("PlayId",playId);

          
        }


        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            return base.OnCreateView(inflater, container, savedInstanceState);
        }

        public override void OnListItemClick(ListView l, View v, int position, long id)
        {
            //base.OnListItemClick(l, v, position, id);
            ShowSelectContent(position);
        }

        private void ShowSelectContent(int playId)
        {
            var intent=new Intent(Activity,typeof(PlayListActivity));
            intent.GetIntExtra("PlayId", playId);
            StartActivity(intent);
            //throw new NotImplementedException();
        }
    }
}