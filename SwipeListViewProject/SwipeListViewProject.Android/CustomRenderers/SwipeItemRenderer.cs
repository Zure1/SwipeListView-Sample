using Android.Views;
using SwipeListViewProject.Components.SwipeListView;
using System;
using Xamarin.Forms.Platform.Android;

[assembly: Xamarin.Forms.ExportRenderer(typeof(SwipeItemView), typeof(SwipeListViewProject.Droid.CustomRenderers.SwipeItemRenderer))]
namespace SwipeListViewProject.Droid.CustomRenderers
{
    class SwipeItemRenderer : ViewRenderer
    {
        public SwipeItemRenderer() : base(Android.App.Application.Context) { }

        public override bool DispatchTouchEvent(MotionEvent touch)
        {
            if (TouchDispatcher.TouchingView == null && touch.ActionMasked == MotionEventActions.Down)
            {
                TouchDispatcher.TouchingView = Element as SwipeItemView;
                TouchDispatcher.StartingBiasX = touch.GetX();
                TouchDispatcher.StartingBiasY = touch.GetY();
                TouchDispatcher.InitialTouch = DateTime.Now;
            }

            return base.DispatchTouchEvent(touch);
        }
    }
}