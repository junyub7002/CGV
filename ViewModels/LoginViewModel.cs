using System.Windows;
using System.Windows.Input;
using AveenoBudget.Commands;
using AveenoBudget.Views;

namespace AveenoBudget.ViewModels;

public class LoginViewModel : BaseViewModel
{
    // ── 프로퍼티 ──────────────────────────────────────────
    private string _userId = string.Empty;
    public string UserId
    {
        get => _userId;
        set => SetProperty(ref _userId, value);
    }

    private string _errorMessage = string.Empty;
    public string ErrorMessage
    {
        get => _errorMessage;
        set => SetProperty(ref _errorMessage, value);
    }

    private bool _hasError;
    public bool HasError
    {
        get => _hasError;
        set => SetProperty(ref _hasError, value);
    }

    // ── 커맨드 ────────────────────────────────────────────
    public ICommand LoginCommand  { get; }
    public ICommand CancelCommand { get; }
    public ICommand SignUpCommand { get; }

    // ──────────────────────────────────────────────────────
    public LoginViewModel()
    {
        LoginCommand  = new RelayCommand(ExecuteLogin,  CanLogin);
        CancelCommand = new RelayCommand(ExecuteCancel);
        SignUpCommand = new RelayCommand(ExecuteSignUp);
    }

    // ──────────────────────────────────────────────────────
    private bool CanLogin(object? _) =>
        !string.IsNullOrWhiteSpace(UserId);

    /// <summary>
    /// PasswordBox는 MVVM에서 code-behind로 비밀번호를 전달받는다
    /// (PasswordBox.Password는 바인딩 불가 → View에서 직접 호출)
    /// </summary>
    public void ExecuteLoginWithPassword(string password)
    {
        HasError = false;
        ErrorMessage = string.Empty;

        // TODO: 실제 인증 로직으로 교체
        if (UserId == "admin" && password == "1234")
        {
            OpenMainWindow();
        }
        else
        {
            HasError = true;
            ErrorMessage = "아이디 또는 비밀번호가 올바르지 않습니다.";
        }
    }

    private void ExecuteLogin(object? _)
    {
        // PasswordBox 값은 View code-behind에서 ExecuteLoginWithPassword 를 통해 전달
        // 이 경로는 엔터키 커맨드 등에서 사용
    }

    private void OpenMainWindow()
    {
        var mainView = new MainView();
        mainView.Show();

        Application.Current.Windows[0]?.Close();
    }

    private static void ExecuteCancel()
    {
        Application.Current.Shutdown();
    }

    private static void ExecuteSignUp()
    {
        MessageBox.Show("회원가입 기능은 준비 중입니다.", "안내",
            MessageBoxButton.OK, MessageBoxImage.Information);
    }
}
