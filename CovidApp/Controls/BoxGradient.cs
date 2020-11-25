using System;
using System.Collections.Generic;
using System.Text;
using SkiaSharp.Views.Forms;
using SkiaSharp;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace CovidApp.Controls
{
    public class BoxGradient : SKCanvasView
    {
        public static readonly BindableProperty CircleColorProperty = BindableProperty.Create(nameof(CircleColor),
   typeof(Color), typeof(BoxGradient), Color.Blue,
   propertyChanging: (currentControl, oldValue, newValue) =>
   {
       var thisControl = currentControl as BoxGradient;
       thisControl.CircleColor = (Color)newValue;
   });

        public static readonly BindableProperty CircleBackgroundColorProperty = BindableProperty.Create(nameof(CircleBackgroundColor),
            typeof(Color), typeof(BoxGradient), Color.LightGray,
            propertyChanging: (currentControl, oldValue, newValue) =>
            {
                var thisControl = currentControl as BoxGradient;
                thisControl.CircleBackgroundColor = (Color)newValue;
            });

        public static readonly BindableProperty CircleStrokeWidthProperty = BindableProperty.Create(nameof(CircleStrokeWidth),
            typeof(float), typeof(BoxGradient), (float)4,
            propertyChanging: (currentControl, oldValue, newValue) =>
            {
                var thisControl = currentControl as BoxGradient;
                thisControl.CircleStrokeWidth = (float)newValue;
            });

        private SKColor myCircleColor;
        private SKColor myCircleBackgroundColor;
        private SKColor myBackgroundColor;
        private float myCircleStrokeWidth;

        public BoxGradient()
        {
            BackgroundColor = myBackgroundColor.ToFormsColor();
            CircleBackgroundColor =  Color.LightGray;
            CircleColor = Color.Blue;
            CircleStrokeWidth = 40;
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

            SKRoundRect skRoundRect= new SKRoundRect(e.Info.Rect, 4);
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

            paint.Style = SKPaintStyle.Fill;
            paint.Color = myCircleColor;

            paint.Shader = SKShader.CreateLinearGradient(
                    new SKPoint(skRoundRect.Rect.Left, skRoundRect.Rect.Top),
                    new SKPoint(skRoundRect.Rect.Right, skRoundRect.Rect.Bottom),
                    new SKColor[] { SKColors.Red, SKColors.Orange },
                    new float[] { 0, 1f },
                    SKShaderTileMode.Repeat);

            
            canvas.DrawRoundRect(skRoundRect, paint);
        }
    }
}
