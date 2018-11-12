using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SwipeListViewProject.Components.SwipeListView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SwipeItemView : ContentView, INotifyPropertyChanged
    {
        public event EventHandler<object> SwipeLeftCompleted;
        public event EventHandler<object> SwipeRightCompleted;
        public event EventHandler<object> DismissSwipe;
        public double DismissSwipeBefore { get; set; } = 0.5;
        public uint SwipeDuration { get; set; } = 200;
        public bool ChangeOpacity { get; set; } = false;

        public static readonly BindableProperty SwipeLeftContentProperty;
        public static readonly BindableProperty SwipeRightContentProperty;
        public static readonly BindableProperty MainContentProperty;
        public static readonly BindableProperty BoundItemProperty;
        bool _isRightContentVisible = false;
        double mainContentPositionX;
        public bool IsRightContentVisible
        {
            get { return _isRightContentVisible; }
            set
            {
                if (value != _isRightContentVisible)
                {
                    OnPropertyChanged(nameof(IsRightContentVisible)); _isRightContentVisible = value;
                }
            }
        }
        public View MainContent
        {
            get { return (View)GetValue(MainContentProperty); }
            set
            {
                SetValue(MainContentProperty, value);
            }
        }
        public View SwipeLeftContent
        {
            get { return (View)GetValue(SwipeLeftContentProperty); }
            set
            {
                SetValue(SwipeLeftContentProperty, value);
            }
        }
        public View SwipeRightContent
        {
            get { return (View)GetValue(SwipeRightContentProperty); }
            set
            {
                SetValue(SwipeRightContentProperty, value);
            }
        }

        public object _boundItem;
        public object BoundItem
        {
            get
            {
                return GetValue(BoundItemProperty);
            }
            set
            {
                SetValue(BoundItemProperty, value);
            }
        }
        public SwipeItemView()
        {
            InitializeComponent();
            mainContent.BindingContext = this;
            leftContent.BindingContext = this;
            rightContent.BindingContext = this;
        }

        public void PerformTranslation(double quota)
        {
            if (quota > 0)
            {
                if (ChangeOpacity == true)
                {
                    mainContent.Opacity = 1 - Math.Abs(quota);
                }
            }
            else
            {
                if (ChangeOpacity == true)
                {
                    mainContent.Opacity = 1 - Math.Abs(quota);
                }
            }
            mainContent.TranslationX = quota * Width + mainContentPositionX; // TODO: Bug (TranslationX von anderen Cells wird beim Scrollen verändert, ohne dass man diese berührt...)

            if (mainContent.TranslationX > 0)
            {
                IsRightContentVisible = true;
            }
            else if (mainContent.TranslationX < 0)
            {
                IsRightContentVisible = false;
            }
        }

        public async Task CompleteTranslationAsync(double quota)
        {
            if (mainContentPositionX > 0) // Right Content sichtbar
            {
                if (quota > 0) // RightSwipe
                {
                    DismissSwipeBefore = rightContent.Width / mainContent.Width / 2;
                }
                else // LeftSwipe
                {
                    DismissSwipeBefore = leftContent.Width / mainContent.Width / 2 + rightContent.Width / mainContent.Width;
                }
            }
            else if (mainContentPositionX < 0) // Left Content sichtbar
            {
                if (quota > 0) // RightSwipe
                {
                    DismissSwipeBefore = rightContent.Width / mainContent.Width / 2 + leftContent.Width / mainContent.Width;
                }
                else // LeftSwipe
                {
                    DismissSwipeBefore = leftContent.Width / mainContent.Width / 2;
                }
            }
            else // Weder linker noch rechter Content sichtbar
            {
                if (quota > 0) // RightSwipe
                {
                    DismissSwipeBefore = rightContent.Width / mainContent.Width / 2;
                }
                else // LeftSwipe
                {
                    DismissSwipeBefore = leftContent.Width / mainContent.Width / 2;
                }
            }

            if (Math.Abs(quota) >= Math.Abs(DismissSwipeBefore))
            {
                if (quota > 0)
                {
                    IsRightContentVisible = true;
                    await mainContent.TranslateTo(rightContent.Width, 0, SwipeDuration);
                    if (ChangeOpacity == true)
                    {
                        await mainContent.FadeTo(0, SwipeDuration);
                    }
                    SwipeRightCompleted?.Invoke(this, BoundItem);
                }
                else
                {
                    IsRightContentVisible = false;
                    await mainContent.TranslateTo(-leftContent.Width, 0, SwipeDuration);
                    if (ChangeOpacity == true)
                    {
                        await mainContent.FadeTo(0, SwipeDuration);
                    }
                    SwipeLeftCompleted?.Invoke(this, BoundItem);
                }
            }
            else
            {
                DismissSwipe?.Invoke(this, BoundItem);
                await mainContent.TranslateTo(0, 0, SwipeDuration);
                if (ChangeOpacity == true)
                {
                    await mainContent.FadeTo(1, SwipeDuration);
                }
            }

            mainContentPositionX = mainContent.TranslationX;
        }

        public void PristineItem()
        {
            PerformTranslation(0);
            mainContent.TranslationX = 0;
            mainContent.TranslationX = 1;
            mainContent.TranslationX = 0;
            SetValue(TranslationXProperty, 0);
            IsRightContentVisible = true;
        }

        static SwipeItemView()
        {
            MainContentProperty = BindableProperty.Create(nameof(MainContent), typeof(View), typeof(SwipeItemView), null, propertyChanged: MainContentChanged);
            SwipeLeftContentProperty = BindableProperty.Create(nameof(SwipeLeftContent), typeof(View), typeof(SwipeItemView), null, propertyChanged: SwipeLeftContentChanged);
            SwipeRightContentProperty = BindableProperty.Create(nameof(SwipeRightContent), typeof(View), typeof(SwipeItemView), null, propertyChanged: SwipeRightContentChanged);
            BoundItemProperty = BindableProperty.Create(nameof(BoundItem), typeof(object), typeof(SwipeItemView), null, propertyChanged: BoundItemChanged);
        }

        private static void BoundItemChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (oldValue != newValue)
            {
                (bindable as SwipeItemView).innerContent.BindingContext = newValue;
                (bindable as SwipeItemView).innerLeftContent.BindingContext = newValue;
                (bindable as SwipeItemView).innerRightContent.BindingContext = newValue;
                bindable.SetValue(BoundItemProperty, newValue);

            }
        }

        private static void MainContentChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (oldValue != newValue)
            {
                (bindable as SwipeItemView).innerContent.Content = (View)newValue;
                bindable.SetValue(MainContentProperty, newValue);
            }
        }
        private static void SwipeLeftContentChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (oldValue != newValue)
            {
                (bindable as SwipeItemView).innerLeftContent.Content = (View)newValue;
                bindable.SetValue(SwipeLeftContentProperty, newValue);
            }
        }
        private static void SwipeRightContentChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (oldValue != newValue)
            {
                (bindable as SwipeItemView).innerRightContent.Content = (View)newValue;
                bindable.SetValue(SwipeRightContentProperty, newValue);
            }
        }
    }
}