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

namespace Zcom.ndsc.Food
{
    using Android.Content.Res;

    class FoodAdapter :ArrayAdapter<FoodClass>
    {

        Context context;

        private int resourceId;

        public FoodAdapter(Context context,int resourceId,List<FoodClass> foodList):base( context,  resourceId,foodList)
        {
            this.context = context;
            this.resourceId = resourceId;
        }

    
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view;

            if (convertView != null)
            {
                view = convertView;
            }
            else
            {
                view = LayoutInflater.From(Application.Context).Inflate(resourceId, parent, false);
            }

            FoodClass food = GetItem(position);
            //ImageView imgView = view.FindViewById<ImageView>(Resource.Id.imgView_Food);
            TextView txtView = view.FindViewById<TextView>(Resource.Id.txtView_Food);
            txtView.Text = food.Name;
            //imgView.SetImageResource(food.ImgId);

            return view;
        }

        

    }

    public class FoodClass
    {

        #region Field

        public string Name { get; }

        public int ImgId { get; }

        #endregion

        public FoodClass(string name, int imgId)
        {
            this.Name = name;
            this.ImgId = imgId;
        }
    }
}