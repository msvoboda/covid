using System;
using System.Collections.Generic;
using System.Text;
using SkiaSharp.Views.Forms;
using SkiaSharp;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using CovidApp.ChartData;
using System.Linq;

namespace CovidApp.Controls
{
    public class ChartControl : SKCanvasView
    {
     
        private SKColor myBackgroundColor;
        private SKColor myColorAxis;
        private SKColor myColorLine;
        private SKColor myColorText;
        private String myTitle = "";
        private String myTitleX = "Datum";
        private String myTitleY = "Denní přírustek";
        private float leftMargin = 4f, topMargin = 20f, bottomMargin = 40, rightMargin = 40f;
        private float LineWidth = 2f;
        private float ChartWidth = 4f;
        private float stepY = 1000f;


        public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title),
       typeof(String), typeof(ChartControl), "",
       propertyChanging: (currentControl, oldValue, newValue) =>
       {
           var thisControl = currentControl as ChartControl;
           thisControl.Title = (String)newValue;
       });

        public static readonly BindableProperty StepYProperty = BindableProperty.Create(nameof(StepY),
        typeof(float), typeof(ChartControl), 0f,
        propertyChanging: (currentControl, oldValue, newValue) =>
        {
           var thisControl = currentControl as ChartControl;
           thisControl.StepY = (float)newValue;
        });

        public ChartControl()
        {
            BackgroundColor = myBackgroundColor.ToFormsColor();
            myColorAxis = SKColors.DarkGray;
            myColorText = SKColors.Black;
            myColorLine = SKColors.Red;
        }
        
        private TimeSeries timeSeries = new TimeSeries();
        public void setTimeSeries(TimeSeries series)
        {
            timeSeries = series;
            InvalidateSurface();
        }

        public String Title
        {
            get { return (String)GetValue(TitleProperty); }
            set
            {
                SetValue(TitleProperty, value);
                myTitle = value;
                InvalidateSurface();
            }
        }

        public float StepY
        {
            get { return (float)GetValue(StepYProperty); }
            set
            {
                SetValue(StepYProperty, value);
                stepY = value;
                InvalidateSurface();
            }
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == nameof(BackgroundColor))
            {
                myBackgroundColor = BackgroundColor.ToSKColor();
            }
        }

        protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
        {
            base.OnPaintSurface(e);

            SKRect skRect = new SKRect(e.Info.Rect.Left, e.Info.Rect.Top, e.Info.Rect.Right, e.Info.Rect.Bottom);
            SKSurface surface = e.Surface;
            SKCanvas canvas = surface.Canvas;

            canvas.Clear(myBackgroundColor);

            SKPaint paint = new SKPaint
            {
                Style = SKPaintStyle.Fill,
                TextSize = 28,
                StrokeWidth = ChartWidth,
                IsAntialias = true
            };

            SKPaint paintAxis = new SKPaint
            {
                Style = SKPaintStyle.Fill,
                TextSize = 14,
                StrokeWidth = LineWidth,
                IsAntialias = true,
                Color = myColorText
              
            };

            SKPaint paintLinesAxis = new SKPaint
            {
                Style = SKPaintStyle.Fill,
                TextSize = 14,
                StrokeWidth = LineWidth,
                IsAntialias = true,
                Color = myColorAxis,
                StrokeCap = SKStrokeCap.Round,
                PathEffect = SKPathEffect.CreateDash(new[] { 2f, 6f }, 0)
            };

            //paint.Style = SKPaintStyle.Stroke;
            paint.Color = myColorAxis;
      
            // title
            //SKPoint ptTitle=new SKPoint(leftMargin,topMargin);
            //canvas.DrawText(myTitle, ptTitle, paint);
            using (var paintTitle = new SKPaint())
            {
                paintTitle.TextSize = 24.0f;
                paintTitle.IsAntialias = true;
                paintTitle.Color = new SKColor(0x9C, 0xAF, 0xB7);
                paintTitle.IsStroke = true;
                paintTitle.StrokeWidth = 1;
                paintTitle.TextAlign = SKTextAlign.Center;

                canvas.DrawText(myTitle, e.Info.Width / 2f, topMargin, paintTitle);
            }
            //Y Axis
            SKPoint ptAxis = new SKPoint(leftMargin+2, topMargin);
            canvas.DrawText(myTitleY, ptAxis, paintAxis);

            // Y Axis
            ptAxis = new SKPoint(skRect.Right - rightMargin, skRect.Bottom - bottomMargin);
            canvas.DrawText(myTitleX, ptAxis, paintAxis);
            // draw axis
            SKPoint pt1 = new SKPoint(skRect.Left + leftMargin, skRect.Bottom - bottomMargin);
            SKPoint pt2 = new SKPoint(skRect.Right - rightMargin, skRect.Bottom - bottomMargin);
            canvas.DrawLine(pt1, pt2,paintAxis);

            pt1 = new SKPoint(skRect.Left + leftMargin, skRect.Bottom - bottomMargin);
            pt2 = new SKPoint(skRect.Left + leftMargin, skRect.Top - topMargin);
            canvas.DrawLine(pt1, pt2, paintAxis);

            List<SKPoint> pointList = new List<SKPoint>();

            if (timeSeries.Count > 0)
            {
                float onex = (skRect.Width - leftMargin - rightMargin) / timeSeries.Count;
                float oney = (skRect.Height - topMargin - bottomMargin) / timeSeries.Max;
                
                for (float st = 0; st < this.timeSeries.Max; st += stepY)
                {
                    ptAxis = new SKPoint(skRect.Left + leftMargin+4, skRect.Bottom - bottomMargin - st * oney);
                    if (st > 0)
                    {
                        SKPoint ptLine = new SKPoint(skRect.Right - rightMargin, skRect.Bottom - bottomMargin - st * oney);
                        canvas.DrawLine(ptAxis, ptLine, paintLinesAxis);                       
                    }
                    canvas.DrawText(st.ToString("0"), ptAxis, paintAxis);
                }

                int index = 0;
                foreach (TimeValue val in timeSeries)
                {

                    float x = index * onex;
                    float y = oney * val.Value;
                    SKPoint pt = new SKPoint(leftMargin + x, skRect.Bottom - bottomMargin - y);
                    pointList.Add(pt);
                    index++;
                }

                SKPoint[] points = pointList.ToArray();
                paint.Style = SKPaintStyle.Stroke;
                paint.Color = myColorLine;
                if (pointList.Count > 0)
                {
                    SKPoint origin = new SKPoint(skRect.Left + leftMargin, skRect.Bottom - bottomMargin);
                    var path = new SKPath();

                    path.MoveTo(points.First().X, origin.Y);
                    path.LineTo(points.First());

                    var last = points.Length;
                    TimeValue lastMonth = null;
                    for (int i = 0; i < last; i++)
                    {
                        SKPoint pt = points[i];
                        TimeValue tval = timeSeries[i];
                        if (lastMonth == null || tval.DateTime.Month != lastMonth.DateTime.Month)
                        {
                            lastMonth = tval;
                            ptAxis = new SKPoint(pt.X, skRect.Bottom - (bottomMargin / 2));
                            canvas.DrawText(lastMonth.DateTime.ToString("MM/yy"), ptAxis, paintAxis);
                        }
                        if (i == 0) {
                            path.LineTo(pt);
                        }
                        else {
                            SKPoint pt0 = points[i-1];
                            path.ConicTo(pt0, pt, Math.Abs(pt.X-pt0.X));
                        }
                    }

                    canvas.DrawPath(path, paint);
                }
            }
        }
    }
}
