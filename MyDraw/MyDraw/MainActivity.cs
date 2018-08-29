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

          ;

            GradientDrawable test = new GradientDrawable(GradientDrawable.Orientation.RightLeft, new int[] { new Color(255,0,0), new Color(255, 0, 255), new Color(0,0,255), new Color(0,255,255), new Color(0,255,0), new Color(255,255,0), new Color(255,0,0) })//(0, 0, GetWidth(), 0, new int[] { Color.Red, Color.Orange, Color.Yellow, Color.Green, Color.Blue, Color.Violet }, null, Shader.TileMode.Clamp);
            ;
           
            // ShapeDrawable shape = new ShapeDrawable(new RectShape());

            //Bitmap bmp = Bitmap.CreateBitmap(new int[] { Color.Red, Color.Orange, Color.Yellow, Color.Green, Color.Blue, Color.Violet }, GetWidth(), 10, null);
            //Drawable temp = new BitmapDrawable(bmp);

       //     shape.Paint.SetShader(test);

            colorBar.SetProgressDrawableTiled(test);
        
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
            
        }

        private void ImageButton_Click(object sender, System.EventArgs e)
        {
            //img.set("@mipmap/bmp.jpg");
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
            //    Int64 c1 = 0xFFFF0000; // ARGB representation of RED
            //    Int64 c2 = 0xFFFFFF00; // ARGB representation of YELLOW
            //    Int64 c3 = 0xFF00FF00; // ARGB representation of GREEN
            //    ArgbEvaluator evaluator = new ArgbEvaluator();

            //    long thumb1 = (long)evaluator.Evaluate(0f, c1, c2); // 0f/9f = 0f
            //long thumb2 = (long)evaluator.Evaluate(2f / 9f, c1, c2);
            //int thumb3 = (int)evaluator.Evaluate(4f / 9f, c1, c2);
            //int thumb4 = (int)evaluator.Evaluate(6f / 9f, c1, c2);
            //       long thumb5 = (long)evaluator.Evaluate(8f / 9f, c1, c2);
            //int thumb6 = (int)evaluator.Evaluate(1f / 9f, c2, c3);
            //int thumb7 = (int)evaluator.Evaluate(3f / 9f, c2, c3);
            //int thumb8 = (int)evaluator.Evaluate(5f / 9f, c2, c3);
            //int thumb9 = (int)evaluator.Evaluate(7f / 9f, c2, c3);
            //int thumb10 = (int)evaluator.Evaluate(1f, c2, c3); // 9f/9f = 1f
            Color clickedColor;
            SeekBar seekBar = (SeekBar)sender;                                  //SeekBar seekbar = (SeekBar)sender;
            System.Diagnostics.Debug.WriteLine(string.Format("progress {0}, width {1}, lisrcount {2}", seekBar.Progress,  GetWidth(), colors.Count));


            // GradientDrawable test = new GradientDrawable(GradientDrawable.Orientation.RightLeft, new int[] { Color.Red, Color.Orange, Color.Yellow, Color.Green, Color.Blue, Color.Violet });
            //  ColorDrawable[] colors = test.ToArray<ColorDrawable>(); 
            //  clickedColor = new Color();

            // int temp = Convert.ToInt32(seekBar.Progress * colors.Count*2 / GetWidth());
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