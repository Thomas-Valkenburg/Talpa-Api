namespace Talpa_Api.Context;

public abstract class DbInitializer
{
    public static void Initialize(UserContext userContext)
    {
        userContext.Database.EnsureCreated();


        if (userContext.Users.Any())
        {
            return;
        }

        userContext.SaveChanges();
    }
}