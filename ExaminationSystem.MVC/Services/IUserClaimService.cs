namespace ExaminationSystem.MVC.Services
{
  public interface IUserClaimService
  {
	Task RefreshUserClaim(string claimType, string newValue);
  }
}
