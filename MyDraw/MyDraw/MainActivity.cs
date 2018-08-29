using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Graphics.Drawables.Shapes;
using Android.Util;
using Android.Content.Res;
using System;
using Android.Animation;
using System.Collections.Generic;

namespace MyDraw
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private FingerDrawView fingerDrawView;
        private SeekBar colorBar;
        private SeekBar sizeBar;
        private Button clearButton;
        private Button undoButton;
        private Button redoButton;
        private Button imageButton;
        private ImageView img;

        List<Color> colors;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            FindResources();
            Initialize();
            AddEvents();

          

            GradientDrawable test = new GradientDrawable(GradientDrawable.Orientation.RightLeft, new int[] { new Color(255,0,0), new Color(255, 0, 255), new Color(0,0,255), new Color(0,255,255), new Color(0,255,0), new Color(255,255,0), new Color(255,0,0) })//(0, 0, GetWidth(), 0, new int[] { Color.Red, Color.Orange, Color.Yellow, Color.Green, Color.Blue, Color.Violet }, null, Shader.TileMode.Clamp);
            ;
             
            colorBar.SetProgressDrawableTiled(test);
            //colorBar.SetThumb(new ColorDrawable(new Color(255, 0, 0)));
      

        }

   

        private int GetWidth()
        {
            var metrix = Resources.DisplayMetrics;
            return metrix.WidthPixels;
        }
        private int GetHeight()
        {
            var metrix = Resources.DisplayMetrics;
            return metrix.HeightPixels;
        }

        private void FindResources()
        {
            fingerDrawView = FindViewById<FingerDrawView>(Resource.Id.myFingerDrawView);
            colorBar = FindViewById<SeekBar>(Resource.Id.seekbar_color);
            sizeBar = FindViewById<SeekBar>(Resource.Id.seekbar_size);
            clearButton = FindViewById<Button>(Resource.Id.clear_button);
            undoButton = FindViewById<Button>(Resource.Id.undo_button);
            redoButton = FindViewById<Button>(Resource.Id.redo_button);
            imageButton = FindViewById<Button>(Resource.Id.load_image);
        //    img = FindViewById<ImageView>(Resource.Id.imageViewEditImage);
        }
        private void AddEvents()
        {
            colorBar.ProgressChanged += ColorBar_ProgressChanged;
            sizeBar.ProgressChanged += SizeBar_ProgressChanged;
            clearButton.Click += ClearButton_Click;
            undoButton.Click += UndoButton_Click;
            imageButton.Click += ImageButton_Click;
            redoButton.Click += RedoButton_Click;
        }

        private void RedoButton_Click(object sender, EventArgs e)
        {
            fingerDrawView.Redo();
        }

        private void ImageButton_Click(object sender, System.EventArgs e)
        {
        
        }

        private void UndoButton_Click(object sender, System.EventArgs e)
        {
            fingerDrawView.Undo();
        }

        private void Initialize()
        {
            colors = new List<Color>();
            for (int i = 0; i <= 255; i += 17) 
            {
                colors.Add(new Color(255, i, 0));
            }
            for (int i = 255; i >= 0; i-=17)
            {
                colors.Add(new Color(i, 255, 0));
            }
            for (int i = 0; i <= 255; i += 17)
            {
                colors.Add(new Color(0, 255, i));
            }
            for (int i = 255; i >= 0; i -= 17) 
            {
                colors.Add(new Color(0, i, 255));
            }
            for (int i = 0; i <= 255; i += 17)
            {
                colors.Add(new Color(i, 0, 255));
            }
            for (int i = 255; i >= 0; i -= 17)
            {
                colors.Add(new Color(255, 0, i));
            }
        }

        private void ClearButton_Click(object sender, System.EventArgs e)
        {
            fingerDrawView.ClearAll();
        }
        private void SizeBar_ProgressChanged(object sender, SeekBar.ProgressChangedEventArgs e)
        {
            SeekBar seekbar = (SeekBar)sender;
            double size = (double)seekbar.Progress*2;
            fingerDrawView.LineSize = (float)size;

        }
        private void ColorBar_ProgressChanged(object sender, SeekBar.ProgressChangedEventArgs e)
        {
            Color clickedColor;
            SeekBar seekBar = (SeekBar)sender;   
            System.Diagnostics.Debug.WriteLine(string.Format("progress {0}, width {1}, lisrcount {2}", seekBar.Progress,  GetWidth(), colors.Count));
            try
            {
                clickedColor = colors[(int)((colors.Count / 100.0) * seekBar.Progress)];
            }
            catch(Exception)
            {
                clickedColor = colors[(int)(colors.Count - 1)];
            }
            System.Diagnostics.Debug.WriteLine(string.Format("colors.Count {0}, colors.Count/100 {1}, (colors.Count/100) * seekBar.Progress) {2}", colors.Count, colors.Count / 100.0, (colors.Count / 100.0) * seekBar.Progress));
            System.Diagnostics.Debug.WriteLine(string.Format("(int)((colors.Count/100) * seekBar.Progress) {0}", (int)((colors.Count / 100.0) * seekBar.Progress)));

            fingerDrawView.LineColor = clickedColor;
        }
    }
}