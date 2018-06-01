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
    using Android.Support.V7.Widget;
    using Java.Lang;

    public class FoodRecylerAdapter : RecyclerView.Adapter
    {
        private List<FoodClass> foodList;

        internal FoodRecylerAdapter(List<FoodClass> foodList)
        {
            this.foodList = foodList;
        }

        public override int ItemCount => foodList.Count;

        //public override int ItemCount => throw new NotImplementedException();

        

        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
        {
            FoodClass food = foodList[position];
            var holder = viewHolder as FoodViewHolder;

            holder.Name.Text = food.Name;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            //throw new NotImplementedException();
            View view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.Item_Person, parent, false);
            FoodViewHolder viewHolder=new FoodViewHolder(view);
            return viewHolder;
        }


        protected class FoodViewHolder : RecyclerView.ViewHolder
        {

            #region Field

             public TextView Name;


            #endregion

            public FoodViewHolder(View itemView)
                : base(itemView)
            {
                Name = itemView.FindViewById<TextView>(Resource.Id.txtView_Food);
            }
        }

    }


    
}