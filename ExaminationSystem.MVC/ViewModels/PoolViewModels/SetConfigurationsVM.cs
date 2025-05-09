using ExaminationSystem.MVC.ViewModels.StudentViewModels;

public class SetConfigurationsVM
{
    public ActivePoolResult Pool { get; set; }
    public List<StudentBasicInfoVM> Students { get; set; } = new List<StudentBasicInfoVM>();
    public Configuration Configuration { get; set; }
}
