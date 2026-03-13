namespace AveenoBudget.Models;

/// <summary>
/// 지출 항목 – DataGrid 한 행에 해당
/// </summary>
public class ExpenseItem
{
    public string Category  { get; set; } = string.Empty;  // 분류명
    public string? Goal     { get; set; }                   // 목표

    public long? Jan  { get; set; }  // 1월
    public long? Feb  { get; set; }  // 2월
    public long? Mar  { get; set; }  // 3월
    public long? Apr  { get; set; }  // 4월
    public long? May  { get; set; }  // 5월
    public long? Jun  { get; set; }  // 6월
    public long? Jul  { get; set; }  // 7월
    public long? Aug  { get; set; }  // 8월
    public long? Sep  { get; set; }  // 9월
    public long? Oct  { get; set; }  // 10월
    public long? Nov  { get; set; }  // 11월
    public long? Dec  { get; set; }  // 12월

    public long  Annual  { get; set; }  // 년간
    public string Percent { get; set; } = string.Empty;  // (%)
}
