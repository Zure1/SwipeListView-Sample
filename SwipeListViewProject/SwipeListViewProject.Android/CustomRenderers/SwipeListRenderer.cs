using Android.Views;
using SwipeListViewProject.Components.SwipeListView;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(SwipeListView), typeof(SwipeListViewProject.Droid.CustomRenderers.SwipeListRenderer))]
namespace SwipeListViewProject.Droid.CustomRenderers
{
    class SwipeListRenderer : ListViewRenderer
    {
        public SwipeListRenderer() : base(Android.App.Application.Context) { }

        public override bool DispatchTouchEvent(MotionEvent touch)
        {
            if (TouchDispatcher.TouchingView != null)
            {
                double currentQuota = ((touch.GetX() - TouchDispatcher.StartingBiasX) / (double)Width);
                SwipeItemView touchedElement = (TouchDispatcher.TouchingView as SwipeItemView);
                switch (touch.ActionMasked)
                {
                    case MotionEventActions.Up:
                        Device.BeginInvokeOnMainThread( async () =>
                        {
                            await touchedElement.CompleteTranslationAsync(currentQuota);
                        });
                        (Element as SwipeListView).AppendTouchedElement(touchedElement);
                        TouchDispatcher.TouchingView = null;
                        TouchDispatcher.StartingBiasX = 0;
                        TouchDispatcher.StartingBiasY = 0;
                        break;
                    case MotionEventActions.Move:
                        TouchDispatcher.TouchingView.PerformTranslation(currentQuota);
                        break;
                }
            }
            return base.DispatchTouchEvent(touch);
        }
    }
}