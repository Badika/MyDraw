using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace MyDraw
{
    class FingerPaintPolyline
    {
        public FingerPaintPolyline()
        {
            Path = new Path();
        }

        public Color _Color { set; get; }
        public float Size { set; get; }
        public Path Path { private set; get; }
    }

    public class FingerDrawView : View
    {
        Dictionary<int, FingerPaintPolyline> inProgressPolylines = new Dictionary<int, FingerPaintPolyline>();
        List<FingerPaintPolyline> completedPolylines = new List<FingerPaintPolyline>();
        Paint paint = new Paint();

        // External interface accessed from MainActivity
        public Color LineColor { set; get; } = Color.Red;
        public float LineSize { set; get; } = 1;

        public FingerDrawView(Context context) : base(context)
        {
           
        }
        public FingerDrawView(Context context, IAttributeSet attrs) : base(context, attrs)
        {

        }

        public void ClearAll()
        {
            completedPolylines.Clear();
            Invalidate();
        }

        public void Undo()
        {
            if (completedPolylines.Count == 0) return;
            completedPolylines.RemoveAt(completedPolylines.Count-1);
            Invalidate();
        }
     

        // Overrides
        public override bool OnTouchEvent(MotionEvent args)
        {
            // Get the pointer index
            int pointerIndex = args.ActionIndex;

            // Get the id to identify a finger over the course of its progress
            int id = args.GetPointerId(pointerIndex);

            // Use ActionMasked here rather than Action to reduce the number of possibilities
            switch (args.ActionMasked)
            {
                case MotionEventActions.Down:
                case MotionEventActions.PointerDown:

                    // Create a Polyline, set the initial point, and store it
                    FingerPaintPolyline polyline = new FingerPaintPolyline
                    {
                        _Color = LineColor,
                        Size = LineSize
                    };

                    polyline.Path.MoveTo(args.GetX(pointerIndex),
                                         args.GetY(pointerIndex));

                    inProgressPolylines.Add(id, polyline);
                    break;

                case MotionEventActions.Move:

                    // Multiple Move events are bundled, so handle them differently
                    for (pointerIndex = 0; pointerIndex < args.PointerCount; pointerIndex++)
                    {
                        id = args.GetPointerId(pointerIndex);

                        inProgressPolylines[id].Path.LineTo(args.GetX(pointerIndex),
                                                            args.GetY(pointerIndex));
                    }
                    break;

                case MotionEventActions.Up:
                case MotionEventActions.Pointer1Up:

                    inProgressPolylines[id].Path.LineTo(args.GetX(pointerIndex),
                                                        args.GetY(pointerIndex));

                    // Transfer the in-progress polyline to a completed polyline
                    completedPolylines.Add(inProgressPolylines[id]);
                    inProgressPolylines.Remove(id);
                    break;

                case MotionEventActions.Cancel:
                    inProgressPolylines.Remove(id);
                    break;
            }

            // Invalidate to update the view
            Invalidate();

            // Request continued touch input
            return true;
        }

        protected override void OnDraw(Canvas canvas)
        {
            base.OnDraw(canvas);

            // Clear canvas to white
            paint.SetStyle(Paint.Style.Fill);
            paint.Color = Color.White;
            canvas.DrawPaint(paint);

            // Draw strokes
            paint.SetStyle(Paint.Style.Stroke);
            paint.StrokeCap = Paint.Cap.Round;
            paint.StrokeJoin = Paint.Join.Round;

            // Draw the completed polylines
            foreach (FingerPaintPolyline polyline in completedPolylines)
            {
                paint.Color = polyline._Color;
                paint.StrokeWidth = polyline.Size;
                canvas.DrawPath(polyline.Path, paint);
            }

            // Draw the in-progress polylines
            foreach (FingerPaintPolyline polyline in inProgressPolylines.Values)
            {
                paint.Color = polyline._Color;
                paint.StrokeWidth = polyline.Size;
                canvas.DrawPath(polyline.Path, paint);
            }
        }
      
    }
}