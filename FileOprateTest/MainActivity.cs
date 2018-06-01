using System.IO;
using Android;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Net;
using Android.Widget;
using Android.OS;
using Android.Speech.Tts;
using Android.Support.V4.App;
using Android.Support.V7.App;
using Java.IO;
using Java.Lang;

namespace FileOprateTest
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        #region Field

        private EditText etText;

        #endregion

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            //
            Init();
        }

        void Init()
        {
            

            etText = FindViewById<EditText>(Resource.Id.editText1);
            etText.FocusChange += (sender, e) =>
            {
                NotifyTest();
                ActivityCompat.RequestPermissions(this, new string[] { Manifest.Permission.CallPhone }, 1);
            };
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            //
            //SaveFile(etText.Text);
        }

        void NotifyTest()
        {
            Intent intent=new Intent(Application.Context,typeof(SecondActivity));
            PendingIntent pIntent=PendingIntent.GetActivity(Application.Context,0,intent,PendingIntentFlags.UpdateCurrent);

            Notification.Builder builder=new Notification.Builder(Application.Context);
            builder.SetContentText("内容").SetContentTitle("标题").SetAutoCancel(true).SetLights(Color.Green,1000,1000);
            builder.SetDefaults(NotificationDefaults.All).SetPriority((int)IntentFilterPriority.HighPriority);
            builder.SetContentIntent(pIntent).SetSmallIcon(Resource.Drawable.notification_template_icon_bg);

            Notification notification = builder.Build();

            NotificationManager nm = (NotificationManager) GetSystemService(NotificationService);
            nm.Notify(1,notification);


        }

        void SaveFile(string text)
        {
            string path = Environment.ExternalStorageDirectory+"/"+ Environment.DirectoryDownloads + "/test.txt";

            if (!System.IO.File.Exists(path))
            {
                System.IO.File.Create(path);
            }

            Toast.MakeText(Application.Context,"写入文件",ToastLength.Short).Show();
            using (StreamWriter sw=new StreamWriter(path))
            {
                sw.Write(text);
            }
        }


        void ReadContent()
        {
            Uri uri=Uri.Parse("Content://FileOprateTest.FileOprateTest");
            


        }
    }
}

