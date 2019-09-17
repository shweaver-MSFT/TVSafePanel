using Windows.System.Profile;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace TVSafePanelSample.Controls
{
    /// <summary>
    /// A ContentControl that automatically pads the content based on the underlying platform.
    /// </summary>
    public class TVSafePanel : ContentControl
    {
        // https://docs.microsoft.com/en-us/windows/uwp/design/devices/designing-for-tv#tv-safe-area
        public static readonly Thickness DefaultBoundaryThickness = new Thickness(48, 27, 48, 27);
        public static readonly BoundaryVisibility DefaultBoundaryVisibility = BoundaryVisibility.Auto;

        // https://docs.microsoft.com/en-us/windows/uwp/design/devices/designing-for-tv#custom-visual-state-trigger-for-xbox
        private static readonly bool isTenFoot = AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Xbox";

        /// <summary>
        /// Identifies the <see cref="BoundaryThickness"/> property.
        /// </summary>
        public static readonly DependencyProperty BoundaryThicknessProperty = DependencyProperty.Register(
            nameof(BoundaryThickness),
            typeof(Thickness),
            typeof(TVSafePanel),
            new PropertyMetadata(DefaultBoundaryThickness, new PropertyChangedCallback(OnBoundaryThicknessChanged)));

        /// <summary>
        /// Identifies the <see cref="BoundaryVisibility"/> property.
        /// </summary>
        public static readonly DependencyProperty BoundaryVisibilityProperty = DependencyProperty.Register(
            nameof(BoundaryVisibility),
            typeof(BoundaryVisibility),
            typeof(TVSafePanel),
            new PropertyMetadata(DefaultBoundaryVisibility, new PropertyChangedCallback(OnBoundaryVisibilityChanged)));

        private static void OnBoundaryThicknessChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TVSafePanel panel)
            {
                panel.UpdateBoundary();
            }
        }

        private static void OnBoundaryVisibilityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TVSafePanel panel)
            {
                panel.UpdateBoundary();
            }
        }

        /// <summary>
        /// Gets or sets a Thickness value used to pad the Content
        /// </summary>
        public Thickness BoundaryThickness
        {
            get => (Thickness)GetValue(BoundaryThicknessProperty);
            set => SetValue(BoundaryThicknessProperty, value);
        }

        /// <summary>
        /// Gets or sets a value that determines when to apply the <see cref="BoundaryThickness"/>
        /// </summary>
        public BoundaryVisibility BoundaryVisibility
        {
            get => (BoundaryVisibility)GetValue(BoundaryVisibilityProperty);
            set => SetValue(BoundaryVisibilityProperty, value);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TVSafePanel"/> class.
        /// </summary>
        public TVSafePanel()
        {
            HorizontalContentAlignment = HorizontalAlignment.Stretch;
            VerticalContentAlignment = VerticalAlignment.Stretch;
        }

        private void UpdateBoundary()
        {
            bool showBoundary =
                BoundaryVisibility == BoundaryVisibility.Auto ? isTenFoot :
                BoundaryVisibility == BoundaryVisibility.Visible;

            if (showBoundary)
            {
                Padding = BoundaryThickness;
            }
            else
            {
                Padding = new Thickness();
            }
        }
    }
}
