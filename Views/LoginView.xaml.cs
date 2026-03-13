using System.Windows;
using System.Windows.Controls;
using AveenoBudget.ViewModels;

namespace AveenoBudget.Views;

public partial class LoginView : Window
{
    public LoginView()
    {
        InitializeComponent();
    }

    // PasswordBox.Password는 DependencyProperty가 아니라 바인딩 불가
    // → PasswordChanged 이벤트에서 placeholder 가시성 관리
    private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
    {
        pwPlaceholder.Visibility = pwBox.Password.Length == 0
            ? Visibility.Visible
            : Visibility.Collapsed;
    }

    // 로그인 버튼 클릭 – ViewModel에 비밀번호 전달
    private void BtnLogin_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is LoginViewModel vm)
            vm.ExecuteLoginWithPassword(pwBox.Password);
    }
}
