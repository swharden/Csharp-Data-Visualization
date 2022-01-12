namespace MauiGraphicsApp
{
    public partial class MainPage : ContentPage
    {
        Timer MyTimer;

        public MainPage()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            MyGraphicsView.Invalidate();
        }

        private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            MyTimer?.Dispose();

            bool isChecked = e.Value;
            if (isChecked)
            {
                TimerCallback onTimeout = o =>
                {
                    MyGraphicsView.Invalidate();
                };

                MyTimer?.Dispose();
                MyTimer = new Timer(
                    callback: onTimeout,
                    state: null,
                    dueTime: TimeSpan.FromMilliseconds(5),
                    period: TimeSpan.FromMilliseconds(10));
            }
        }
    }
}