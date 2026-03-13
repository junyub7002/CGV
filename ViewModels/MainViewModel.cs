using System.Collections.ObjectModel;
using System.Windows.Input;
using AveenoBudget.Commands;
using AveenoBudget.Models;

namespace AveenoBudget.ViewModels;

public class MainViewModel : BaseViewModel
{
    // ── 탭 / 선택 상태 ────────────────────────────────────
    private int _selectedTabIndex;
    public int SelectedTabIndex
    {
        get => _selectedTabIndex;
        set => SetProperty(ref _selectedTabIndex, value);
    }

    private int _selectedYear = 2021;
    public int SelectedYear
    {
        get => _selectedYear;
        set
        {
            if (SetProperty(ref _selectedYear, value))
                LoadExpenses();
        }
    }

    public ObservableCollection<int> AvailableYears { get; } =
        new(new[] { 2019, 2020, 2021, 2022, 2023, 2024, 2025 });

    // ── DataGrid 데이터 ───────────────────────────────────
    private ObservableCollection<ExpenseItem> _expenses = new();
    public ObservableCollection<ExpenseItem> Expenses
    {
        get => _expenses;
        set => SetProperty(ref _expenses, value);
    }

    private ExpenseItem? _summaryRow;
    public ExpenseItem? SummaryRow
    {
        get => _summaryRow;
        set => SetProperty(ref _summaryRow, value);
    }

    // ── 커맨드 ────────────────────────────────────────────
    public ICommand RefreshCommand          { get; }
    public ICommand SetStandardCommand      { get; }
    public ICommand CompareAnalysisCommand  { get; }
    public ICommand DetailViewCommand       { get; }
    public ICommand MultiAddCommand         { get; }
    public ICommand SingleAddCommand        { get; }

    // ──────────────────────────────────────────────────────
    public MainViewModel()
    {
        RefreshCommand         = new RelayCommand(_ => LoadExpenses());
        SetStandardCommand     = new RelayCommand(_ => OnSetStandard());
        CompareAnalysisCommand = new RelayCommand(_ => OnCompareAnalysis());
        DetailViewCommand      = new RelayCommand(_ => OnDetailView());
        MultiAddCommand        = new RelayCommand(_ => OnMultiAdd());
        SingleAddCommand       = new RelayCommand(_ => OnSingleAdd());

        LoadExpenses();
    }

    // ── 데이터 로딩 (샘플) ────────────────────────────────
    private void LoadExpenses()
    {
        Expenses = new ObservableCollection<ExpenseItem>
        {
            new() { Category = "식료품",  Jan=1_138_036, Feb=1_115_502, Mar=996_032,  Apr=1_064_710, May=1_183_864, Jun=1_208_882, Jul=1_096_991, Aug=1_264_884, Sep=894_246, Annual=9_963_147, Percent="28 %" },
            new() { Category = "주거비",  Jan=530_477,  Feb=585_902,  Mar=519_142,  Apr=524_335,  May=707_397,  Jun=473_915,  Jul=469_527,  Aug=535_955,  Sep=473_015,  Annual=4_819_665, Percent="13 %" },
            new() { Category = "교통비",  Jan=50_000,   Feb=73_250,   Mar=50_000,   Apr=50_000,   May=54_500,   Jun=59_000,   Jul=66_600,   Aug=52_800,   Sep=56_500,   Annual=512_650,  Percent="1 %"  },
            new() { Category = "문화비",  Jan=104_663,  Feb=46_566,   Mar=88_959,   Apr=112_671,  May=137_074,  Jun=96_657,   Jul=111_937,  Aug=45_688,   Sep=131_322,  Annual=875_537,  Percent="2 %"  },
            new() { Category = "통신비",  Jan=166_000,  Feb=166_000,  Mar=166_000,  Apr=166_000,  May=166_000,  Jun=166_000,  Jul=189_900,  Aug=176_900,  Sep=176_900,  Annual=1_539_700, Percent="4 %" },
            new() { Category = "의료비",  Jan=81_750,   Feb=60_650,   Mar=54_395,   Apr=35_616,   May=1_000,    Jun=68_525,   Jul=100,      Aug=182_298,  Sep=171_230,  Annual=655_564,  Percent="1 %"  },
            new() { Category = "차량유지", Jan=74_900,  Feb=122_400,  Mar=45_100,   Apr=9_710,    May=29_956,   Jun=77_190,   Jul=54_320,   Aug=98_810,   Sep=377_970,  Annual=890_356,  Percent="2 %"  },
            new() { Category = "보험료",  Jan=109_440,  Feb=109_440,  Mar=109_440,  Apr=109_440,  May=109_440,  Jun=109_440,  Jul=91_000,   Aug=91_000,   Sep=146_320,  Annual=984_960,  Percent="2 %"  },
            new() { Category = "잔돈금",  Jan=90_000,                                              May=40_000,                                                            Annual=130_000,  Percent="0 %"  },
            new() { Category = "모임비",  Jan=80_000,   Feb=80_000,   Mar=80_000,   Apr=80_000,   May=80_000,   Jun=80_000,   Jul=80_000,   Aug=80_000,   Sep=80_000,   Annual=720_000,  Percent="2 %"  },
            new() { Category = "용돈김동", Jan=150_000,  Feb=150_000,  Mar=150_000,  Apr=150_000,  May=150_000,  Jun=150_000,  Jul=150_000,  Aug=150_000,  Sep=150_000,  Annual=1_350_000, Percent="3 %" },
            new() { Category = "용돈활동", Jan=200_000,  Feb=200_000,  Mar=200_000,  Apr=200_000,  May=200_000,  Jun=200_000,  Jul=200_000,  Aug=200_000,  Sep=200_000,  Annual=1_800_000, Percent="5 %" },
            new() { Category = "용돈딸박", Jan=172_000,  Feb=273_692,  Mar=201_227,  Apr=84_650,   May=219_650,  Jun=212_665,  Jul=118_163,  Aug=133_162,  Sep=269_671,  Annual=1_684_880, Percent="4 %" },
            new() { Category = "용돈말박", Jan=167_550,  Feb=182_000,  Mar=249_530,  Apr=196_530,  May=306_530,  Jun=353_700,  Jul=203_700,  Aug=143_700,  Sep=143_700,  Annual=1_946_940, Percent="5 %" },
            new() { Category = "용돈부모", Jan=300_000,  Feb=300_000,  Mar=300_000,  Apr=300_000,  May=300_000,  Jun=300_000,  Jul=300_000,  Aug=300_000,  Sep=346_930,  Annual=2_746_930, Percent="7 %" },
            new() { Category = "청약저축", Jan=100_000,  Feb=100_000,  Mar=300_000,  Apr=100_000,  May=100_000,  Jun=100_000,  Jul=100_000,  Aug=100_000,  Sep=100_000,  Annual=1_100_000, Percent="3 %" },
            new() { Category = "의류비",  Jan=50_748,   Feb=78_648,   Mar=212_248,  Apr=80_193,   May=51_193,   Jun=34_503,   Jul=42_102,   Aug=19_000,   Sep=152_200,  Annual=720_835,  Percent="2 %"  },
            new() { Category = "여행비",                                             Apr=200_000,  May=134_200,  Jun=275_600,  Jul=394_533,  Aug=197_410,  Sep=113_640,  Annual=1_315_383, Percent="3 %" },
            new() { Category = "추석설날",              Feb=430_681,                                                                                        Sep=451_100,  Annual=881_781,  Percent="2 %"  },
        };

        SummaryRow = new ExpenseItem
        {
            Category = "합  계",
            Jan=3_565_564, Feb=4_074_731, Mar=3_722_073,
            Apr=3_463_855, May=3_970_804, Jun=3_966_077,
            Jul=3_668_873, Aug=3_771_607, Sep=4_434_744,
            Annual=34_638_328, Percent="100 %"
        };
    }

    // ── 커맨드 핸들러 (향후 확장) ─────────────────────────
    private static void OnSetStandard()      => System.Windows.MessageBox.Show("지출기준설정");
    private static void OnCompareAnalysis()  => System.Windows.MessageBox.Show("지출비교분석");
    private static void OnDetailView()       => System.Windows.MessageBox.Show("지출상세조회");
    private static void OnMultiAdd()         => System.Windows.MessageBox.Show("지출다중등록");
    private static void OnSingleAdd()        => System.Windows.MessageBox.Show("지출단일등록");
}
