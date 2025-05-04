using ExaminationSystem.MVC.IService;
using Microsoft.AspNetCore.Mvc;

public class TeachAtViewComponent : ViewComponent
{
    private readonly IPoolService _poolService;

    public TeachAtViewComponent(IPoolService poolService)
    {
        _poolService = poolService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var teachAtData = await _poolService.TeachAt(40404040404040); // Replace with actual claim
        return View("_TeachAtFilters", teachAtData);
    }
}
