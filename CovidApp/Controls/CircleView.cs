using SkiaSharp.Views.Forms;
using SkiaSharp;
using System.Runtime.CompilerServices;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CovidApp.Controls
{
    public class CircleView : SKCanvasView
    {
       public static readonly BindableProperty CircleColorProperty = BindableProperty.Create(nameof(CircleColor),
       typeof(Color), typeof(BoxGradient), Color.Blue,
       propertyChanging: (currentControl, oldValue, newValue) =>
       {
           var thisControl = currentControl as BoxGradient;
           thisControl.CircleColor = (Color)newValue;
       });

        public static readonly BindableProperty CircleBackgroundColorProperty = BindableProperty.Create(nameof(CircleBackgroundColor),
            typeof(Color), typeof(CircleView), Color.LightGray,
            propertyChanging: (currentControl, oldValue, newValue) =>
            {
                var thisControl = currentControl as BoxGradient;
                thisControl.CircleBackgroundColor = (Color)newValue;
            });

        public static readonly BindableProperty CircleStrokeWidthProperty = BindableProperty.Create(nameof(CircleStrokeWidth),
            typeof(float), typeof(CircleView), (float)4,
            propertyChanging: (currentControl, oldValue, newValue) =>
            {
                var thisControl = currentControl as BoxGradient;
                thisControl.CircleStrokeWidth = (float)newValue;
            });

        private SKColor myCircleColor;
        private SKColor myCircleBackgroundColor;
        private SKColor myBackgroundColor;
        private float myCircleStrokeWidth;

        public CircleView()
        {
            BackgroundColor = myBackgroundColor.ToFormsColor();
            CircleBackgroundColor = Color.LightGray;
            CircleColor = Color.Blue;
            CircleStrokeWidth = 4;
        }

        public Color CircleColor
        {
            get { return (Color)GetValue(CircleColorProperty); }
            set
            {
                SetValue(CircleColorProperty, value);
                myCircleColor = value.ToSKColor();
                InvalidateSurface();
            }
        }

        public float CircleStrokeWidth
        {
            get { return (float)GetValue(CircleStrokeWidthProperty); }
            set
            {
                SetValue(CircleStrokeWidthProperty, value);
                myCircleStrokeWidth = value;
                InvalidateSurface();
            }
        }

        public Color CircleBackgroundColor
        {
            get { return (Color)GetValue(CircleBackgroundColorProperty); }
            set
            {
                SetValue(CircleBackgroundColorProperty, value);
                myCircleBackgroundColor = value.ToSKColor();
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

            SKImageInfo info = e.Info;
            SKSurface surface = e.Surface;
            SKCanvas canvas = surface.Canvas;

            canvas.Clear(myBackgroundColor);

            SKPaint paint = new SKPaint
            {
                Style = SKPaintStyle.Fill,
                TextSize = 48,
                StrokeWidth = myCircleStrokeWidth,
                IsAntialias = true
            };

            var circleRadius = Math.Min(info.Width - myCircleStrokeWidth, info.Height - myCircleStrokeWidth) / 2;
            var circleMiddle = circleRadius + myCircleStrokeWidth / 2;
            paint.Color = myCircleBackgroundColor;
            canvas.DrawCircle(circleMiddle, circleMiddle, circleRadius, paint);
            paint.Style = SKPaintStyle.Stroke;
            paint.Color = myCircleColor;
            canvas.DrawCircle(circleMiddle, circleMiddle, circleRadius, paint);
        }
    }
}

