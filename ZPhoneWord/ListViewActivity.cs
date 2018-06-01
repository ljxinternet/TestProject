
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

namespace Zcom.ndsc
{
    using System.Collections;

    using Android.Support.V7.Widget;

    using Zcom.ndsc.Food;

    [Activity(Label = "ListVIewActivity")]
    public class ListViewActivity : Activity
    {
        #region Field   


        

        #endregion


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Activity_Recycler);
            //
            InitUI();
        }

        private void InitUI()
        {
            //ListView listView = FindViewById<ListView>(Resource.Id.listView1);
            RecyclerView cView = FindViewById<RecyclerView>(Resource.Id.recyclerView1);
            


            List<FoodClass> foods=new List<FoodClass>();
            for (int i = 0; i < 50; i++)
            {
                foods.Add(new FoodClass("Apple",Resource.Drawable.notification_template_icon_bg));
            }

            //listView.ItemClick += (send, e) =>
            //    {
            //        FoodClass food = foods[e.Position];
            //        Toast.MakeText(this,food.Name,ToastLength.Short).Show();
            //    };

            FoodRecylerAdapter adapter=new FoodRecylerAdapter(foods);
            LinearLayoutManager manager=new LinearLayoutManager(this);
            cView.SetLayoutManager(manager);
            cView.SetAdapter(adapter);

            //FoodAdapter foodArrayAdapter=new FoodAdapter(this,Resource.Layout.Item_Person,foods);

            //listView.Adapter = foodArrayAdapter;
            //throw new NotImplementedException();
        }





        public static void ActionStart(Context context, string args)
        {
            Intent intent = new Intent(context, typeof(ListViewActivity));
            intent.PutExtra("arg1", args);
            context.StartActivity(intent);
        }
    }
}