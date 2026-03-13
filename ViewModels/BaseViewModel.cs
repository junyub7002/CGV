using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AveenoBudget.ViewModels;

/// <summary>
/// 모든 ViewModel의 기반 클래스 – INotifyPropertyChanged 구현
/// </summary>
public abstract class BaseViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// 프로퍼티 변경 알림 발생
    /// </summary>
    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    /// <summary>
    /// 값을 설정하고 변경된 경우에만 알림을 발생시킨다.
    /// </summary>
    protected bool SetProperty<T>(ref T field, T value,
        [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value))
            return false;

        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}
