using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Graphics.Drawables.Shapes;

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

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            FindResources();
            Initialize();
            AddEvents();
            LinearGradient test = new LinearGradient(0, 0, 1000, 0, new int[] { Color.Red, Color.Orange, Color.Yellow, Color.Green, Color.Blue, Color.Violet }, null, Shader.TileMode.Clamp);
            ShapeDrawable shape = new ShapeDrawable(new RectShape());
            shape.Paint.SetShader(test);

            colorBar.SetProgressDrawableTiled(shape);
        
        }

    

        private void FindResources()
        {
            fingerDrawView = FindViewById<FingerDrawView>(Resource.Id.myFingerDrawView);
            colorBar = FindViewById<SeekBar>(Resource.Id.seekbar_color);
            sizeBar = FindViewById<SeekBar>(Resource.Id.seekbar_size);
            clearButton = FindViewById<Button>(Resource.Id.clear_button);
            undoButton = FindViewById<Button>(Resource.Id.undo_button);
            imageButton = FindViewById<Button>(Resource.Id.load_image);
        }
        private void AddEvents()
        {
            colorBar.ProgressChanged += ColorBar_ProgressChanged;
            sizeBar.ProgressChanged += SizeBar_ProgressChanged;
            clearButton.Click += ClearButton_Click;
            undoButton.Click += UndoButton_Click;
        }

        private void UndoButton_Click(object sender, System.EventArgs e)
        {
            fingerDrawView.Undo();
        }

        private void Initialize()
        {


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
            //////////////////////////////////////////////
            SeekBar seekbar = (SeekBar)sender;
            ColorDrawable color = (ColorDrawable)seekbar.ProgressDrawable;
            fingerDrawView.LineColor = color.Color;
        }
    }
}