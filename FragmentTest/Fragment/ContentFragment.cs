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
using Android.Webkit;
using Android.Widget;

namespace FragmentTest.Fragment
{
    public class ContentFragment : Android.App.Fragment
    {
        public int playId=> Arguments.GetInt("PlayID");

        private WebView web_View;

        public static ContentFragment GetInstance(int playId)
        {
            Bundle bundle=new Bundle();
            bundle.PutInt("PlayId",playId);

            return new ContentFragment()
            {
                Arguments = bundle
            };
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

            //return base.OnCreateView(inflater, container, savedInstanceState);

            return inflater.Inflate(Resource.Layout.left_Fragment, container, false);


            //if (container==null)
            //{
            //    return null;
            //}

            //var tvContent=new TextView(Activity);
            //tvContent.Text = ListClass.Content[playId];
            //tvContent.TextSize=80;
            //var scroll_View = new ScrollView(Activity);
            //scroll_View.AddView(web_View);
            //return scroll_View;
        }
    }
}