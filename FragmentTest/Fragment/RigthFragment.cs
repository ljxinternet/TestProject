using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace FragmentTest.Fragment
{
    using Android.Support.V4.App;
    public class RigthFragment : Fragment
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            //return base.OnCreateView(inflater, container, savedInstanceState);
            Android.Views.View view = inflater.Inflate(Resource.Layout.rigth_Fragment, container,false);
            return view;
        }
    }
}