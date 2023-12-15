namespace CustomerApp
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            if (IsUserLoggedIn())
            {
                // Nếu người dùng đã đăng nhập, mở màn hình chính
                MainPage = new AppShell();
            }
            else
            {
                // Nếu người dùng chưa đăng nhập, mở màn hình đăng nhập
                MainPage = new SignIn();
            }
        }
        private bool IsUserLoggedIn()
        {
            // Thực hiện logic kiểm tra đăng nhập ở đây
            // Ví dụ: kiểm tra xem có token đăng nhập hợp lệ trong bộ nhớ đệm hay không
            return false;
        }
    }
}